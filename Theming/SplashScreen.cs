using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace ZenStatesDebugTool.Theming
{
    /// <summary>
    /// Startup splash: only the application icon on a transparent background, gently fading in and out. It runs on its own UI thread, so it keeps animating while the main window initializes
    /// (driver load, CPU detection) on the main thread.
    /// </summary>
    internal static class SplashScreen
    {
        private static SplashForm form;
        private static Thread thread;
        private static volatile bool closeRequested;

        /// <summary>Shows the splash. Returns once it is on screen.</summary>
        public static void Show()
        {
            if (form != null || !ControlStyler.IsWindows)
                return;

            Bitmap icon = LoadIcon();
            if (icon == null)
                return;

            using (var ready = new ManualResetEventSlim())
            {
                thread = new Thread(() =>
                {
                    SplashForm splash = new SplashForm(icon);
                    splash.HandleCreated += (_, __) =>
                    {
                        // Close() came before the splash was up (it took longer than the wait below)
                        if (closeRequested)
                            splash.FadeOut();
                        ready.Set();
                    };
                    form = splash;
                    Application.Run(splash);
                    icon.Dispose();
                })
                { IsBackground = true, Name = "Splash" };
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();

                // Never hold up the start for long if the splash thread has a problem
                ready.Wait(2000);
            }
        }

        /// <summary>Fades the splash out and closes it. Safe to call more than once, from any thread.</summary>
        public static void Close()
        {
            closeRequested = true;
            SplashForm splash = form;
            form = null;
            if (splash == null)
                return;

            try
            {
                if (splash.IsHandleCreated)
                    splash.BeginInvoke(new Action(splash.FadeOut));
            }
            catch (InvalidOperationException)
            {
                // Already closed
            }
        }

        // Size of the icon on screen at 100 % scaling
        private const int DisplaySize = 128;

        // The 256x256 frame of the application icon (stored as PNG in the .ico), drawn at DisplaySize: scaled down
        // it stays sharp, also at higher DPI. Falls back to the largest frame.
        private static Bitmap LoadIcon()
        {
            try
            {
                Icon appIcon = new ComponentResourceManager(typeof(SettingsForm)).GetObject("$this.Icon") as Icon;
                if (appIcon == null)
                    return null;

                byte[] data;
                using (var stream = new MemoryStream())
                {
                    appIcon.Save(stream);
                    data = stream.ToArray();
                }

                Bitmap png = ReadPngFrame(data, 256);
                if (png != null)
                    return png;

                using (var stream = new MemoryStream(data))
                using (var large = new Icon(stream, 256, 256))
                    return large.ToBitmap();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Splash icon could not be loaded: {ex.Message}");
                return null;
            }
        }

        private static Bitmap ReadPngFrame(byte[] ico, int size)
        {
            if (ico.Length < 6 || BitConverter.ToUInt16(ico, 2) != 1)
                return null;

            int count = BitConverter.ToUInt16(ico, 4);
            for (int i = 0; i < count; i++)
            {
                int entry = 6 + i * 16;
                if (entry + 16 > ico.Length)
                    break;

                int width = ico[entry] == 0 ? 256 : ico[entry];
                int length = BitConverter.ToInt32(ico, entry + 8);
                int offset = BitConverter.ToInt32(ico, entry + 12);
                bool isPng = offset + 8 <= ico.Length && ico[offset] == 0x89 && ico[offset + 1] == (byte)'P';
                if (width != size || !isPng || offset + length > ico.Length)
                    continue;

                using (var stream = new MemoryStream(ico, offset, length))
                using (var image = Image.FromStream(stream))
                    return new Bitmap(image);
            }
            return null;
        }

        private sealed class SplashForm : Form
        {
            private const int WS_EX_LAYERED = 0x00080000;
            private const int WS_EX_TOOLWINDOW = 0x00000080;
            private const int WS_EX_NOACTIVATE = 0x08000000;

            // One cycle: from full opacity down to MinOpacity and back, eased at both ends
            private static readonly TimeSpan PulseDuration = TimeSpan.FromMilliseconds(3000);
            private const float MinOpacity = 0.55f;
            private static readonly TimeSpan FadeDuration = TimeSpan.FromMilliseconds(300);

            private readonly Bitmap icon;
            private readonly System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer { Interval = 15 };
            private readonly System.Diagnostics.Stopwatch clock = System.Diagnostics.Stopwatch.StartNew();
            // The icon, drawn once; every frame only changes the window opacity
            private Bitmap frame;
            private IntPtr frameBitmap = IntPtr.Zero;
            private long fadeStart = -1;

            public SplashForm(Bitmap icon)
            {
                this.icon = icon;
                FormBorderStyle = FormBorderStyle.None;
                ShowInTaskbar = false;
                StartPosition = FormStartPosition.Manual;
                Text = Application.ProductName;
                timer.Tick += (_, __) => Render();
            }

            protected override CreateParams CreateParams
            {
                get
                {
                    CreateParams cp = base.CreateParams;
                    cp.ExStyle |= WS_EX_LAYERED | WS_EX_TOOLWINDOW | WS_EX_NOACTIVATE;
                    return cp;
                }
            }

            protected override bool ShowWithoutActivation => true;

            protected override void OnHandleCreated(EventArgs e)
            {
                base.OnHandleCreated(e);

                // DisplaySize at 100 %, scaled for the monitor
                int side = (int)Math.Round(DisplaySize * DeviceDpi / 96f);
                Rectangle area = Screen.PrimaryScreen.WorkingArea;
                Bounds = new Rectangle(area.Left + (area.Width - side) / 2, area.Top + (area.Height - side) / 2, side, side);

                // The 256 px icon scaled down once, in high quality (WrapMode keeps GDI+ from blending a halo
                // into the edges)
                frame = new Bitmap(side, side, PixelFormat.Format32bppArgb);
                using (Graphics g = Graphics.FromImage(frame))
                using (var edgeMode = new ImageAttributes())
                {
                    edgeMode.SetWrapMode(WrapMode.TileFlipXY);
                    g.Clear(Color.Transparent);
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    g.DrawImage(icon, new Rectangle(0, 0, side, side), 0, 0, icon.Width, icon.Height, GraphicsUnit.Pixel, edgeMode);
                }
                frameBitmap = frame.GetHbitmap(Color.FromArgb(0));

                Render();
                timer.Start();
            }

            public void FadeOut()
            {
                if (fadeStart < 0)
                    fadeStart = clock.ElapsedMilliseconds;
            }

            // 0 -> 1 -> 0 over one cycle, eased in and out at both ends (cosine)
            private static double Pulse(double t) => 0.5 - 0.5 * Math.Cos(2 * Math.PI * t);

            private void Render()
            {
                if (frame == null)
                    return;

                long now = clock.ElapsedMilliseconds;
                double phase = (now % (long)PulseDuration.TotalMilliseconds) / PulseDuration.TotalMilliseconds;
                // Starts at full opacity, dims to MinOpacity half way through the cycle
                double opacity = 1 - (1 - MinOpacity) * Pulse(phase);

                byte alpha = (byte)Math.Round(255 * opacity);
                if (fadeStart >= 0)
                {
                    double t = (now - fadeStart) / FadeDuration.TotalMilliseconds;
                    if (t >= 1)
                    {
                        timer.Stop();
                        Close();
                        return;
                    }
                    alpha = (byte)Math.Round(alpha * (1 - t));
                }

                UpdateLayered(alpha);
            }

            private void UpdateLayered(byte alpha)
            {
                IntPtr screenDc = GetDC(IntPtr.Zero);
                IntPtr memoryDc = CreateCompatibleDC(screenDc);
                IntPtr previous = SelectObject(memoryDc, frameBitmap);
                try
                {
                    var size = new SIZE { cx = frame.Width, cy = frame.Height };
                    var source = new POINT();
                    var position = new POINT { x = Left, y = Top };
                    var blend = new BLENDFUNCTION { BlendOp = 0, BlendFlags = 0, SourceConstantAlpha = alpha, AlphaFormat = 1 };
                    UpdateLayeredWindow(Handle, screenDc, ref position, ref size, memoryDc, ref source, 0, ref blend, 2);
                }
                finally
                {
                    SelectObject(memoryDc, previous);
                    DeleteDC(memoryDc);
                    ReleaseDC(IntPtr.Zero, screenDc);
                }
            }

            protected override void Dispose(bool disposing)
            {
                if (disposing)
                {
                    timer.Dispose();
                    frame?.Dispose();
                }
                if (frameBitmap != IntPtr.Zero)
                {
                    DeleteObject(frameBitmap);
                    frameBitmap = IntPtr.Zero;
                }
                base.Dispose(disposing);
            }

            [StructLayout(LayoutKind.Sequential)]
            private struct POINT { public int x, y; }

            [StructLayout(LayoutKind.Sequential)]
            private struct SIZE { public int cx, cy; }

            [StructLayout(LayoutKind.Sequential, Pack = 1)]
            private struct BLENDFUNCTION
            {
                public byte BlendOp, BlendFlags, SourceConstantAlpha, AlphaFormat;
            }

            [DllImport("user32.dll", SetLastError = true)]
            private static extern bool UpdateLayeredWindow(IntPtr hwnd, IntPtr hdcDst, ref POINT pptDst, ref SIZE psize,
                IntPtr hdcSrc, ref POINT pptSrc, int crKey, ref BLENDFUNCTION pblend, int dwFlags);

            [DllImport("user32.dll")]
            private static extern IntPtr GetDC(IntPtr hWnd);

            [DllImport("user32.dll")]
            private static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

            [DllImport("gdi32.dll")]
            private static extern IntPtr CreateCompatibleDC(IntPtr hDC);

            [DllImport("gdi32.dll")]
            private static extern bool DeleteDC(IntPtr hdc);

            [DllImport("gdi32.dll")]
            private static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);

            [DllImport("gdi32.dll")]
            private static extern bool DeleteObject(IntPtr hObject);
        }
    }
}
