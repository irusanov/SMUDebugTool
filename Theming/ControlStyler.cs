using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ZenStatesDebugTool.Theming
{
    /// <summary>
    /// Paints the standard WinForms controls in the current palette. Painting is done after the control's own
    /// drawing (Paint event or a subclassed WM_PAINT), so the controls, their designer settings and their events
    /// stay as they are.
    /// </summary>
    internal static class ControlStyler
    {
        // ---- helpers ----

        // The native parts (subclassed painting, scroll bar themes) only exist on Windows
        public static readonly bool IsWindows = Environment.OSVersion.Platform == PlatformID.Win32NT;

        private static float dpiScale = -1;
        private static float systemDpi = -1;

        /// <summary>
        /// Scale of the window being laid out or drawn, against 96 DPI. Starts at the system DPI; a ThemedForm sets
        /// it to the DPI of its monitor when it is created and when it moves to a monitor with another DPI. It is
        /// shared by all windows, so two windows on monitors with different DPIs use the scale of the last one.
        /// </summary>
        public static float DpiScale
        {
            get
            {
                if (dpiScale < 0)
                    dpiScale = SystemDpi / 96f;
                return dpiScale;
            }
        }

        /// <summary>DPI that fonts are converted with (GDI uses the system DPI, not the one of the monitor).</summary>
        public static float SystemDpi
        {
            get
            {
                if (systemDpi < 0)
                {
                    using (Graphics g = Graphics.FromHwnd(IntPtr.Zero))
                        systemDpi = g.DpiX;
                }
                return systemDpi;
            }
        }

        public static void SetDpi(int dpi)
        {
            if (dpi > 0)
                dpiScale = dpi / 96f;
        }

        public static int Px(float value) => (int)Math.Round(value * DpiScale);

        /// <summary>The color behind a control: the first non-transparent background up the parent chain.</summary>
        public static Color EffectiveBack(Control control)
        {
            for (Control c = control; c != null; c = c.Parent)
            {
                if (c.BackColor.A == 255)
                    return c.BackColor;
            }
            return Theme.Current.Surface;
        }

        public static GraphicsPath RoundedRect(RectangleF r, float radius)
        {
            var path = new GraphicsPath();
            float d = Math.Min(radius * 2, Math.Min(r.Width, r.Height));
            if (d <= 0.5f)
            {
                path.AddRectangle(r);
                return path;
            }

            path.AddArc(r.Left, r.Top, d, d, 180, 90);
            path.AddArc(r.Right - d, r.Top, d, d, 270, 90);
            path.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
            path.AddArc(r.Left, r.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }

        private static readonly PropertyInfo doubleBufferedProperty =
            typeof(Control).GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);

        public static void SetDoubleBuffered(Control control)
        {
            try { doubleBufferedProperty?.SetValue(control, true, null); }
            catch { }
        }

        [DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
        private static extern int SetWindowTheme(IntPtr hwnd, string subAppName, string subIdList);

        private static readonly HashSet<Control> scrollThemed = new HashSet<Control>();

        /// <summary>Dark scroll bars (Windows 10 1809 and later) for controls with native scroll bars.</summary>
        public static void SetDarkScrollBars(Control control, string darkTheme = "DarkMode_Explorer")
        {
            if (!IsWindows)
                return;

            if (scrollThemed.Add(control))
            {
                control.HandleCreated += (s, e) => ApplyScrollTheme(control, darkTheme);
                control.Disposed += (s, e) => scrollThemed.Remove(control);
            }

            if (control.IsHandleCreated)
                ApplyScrollTheme(control, darkTheme);
        }

        private static void ApplyScrollTheme(Control control, string darkTheme)
        {
            try
            {
                SetWindowTheme(control.Handle, Theme.Current.IsDark ? darkTheme : "Explorer", null);
            }
            catch (DllNotFoundException)
            {
            }
        }

        private static readonly Font semiBold = new Font("Segoe UI Semibold", 8.25f);

        // ---- buttons ----

        private sealed class PointerState
        {
            public bool Hover;
            public bool Pressed;
        }

        private static readonly Dictionary<Control, PointerState> pointer = new Dictionary<Control, PointerState>();

        private static PointerState TrackPointer(Control control)
        {
            if (pointer.TryGetValue(control, out PointerState state))
                return state;

            state = new PointerState();
            pointer[control] = state;
            control.MouseEnter += (s, e) => { state.Hover = true; control.Invalidate(); };
            control.MouseLeave += (s, e) => { state.Hover = false; state.Pressed = false; control.Invalidate(); };
            control.MouseDown += (s, e) => { if (e.Button == MouseButtons.Left) { state.Pressed = true; control.Invalidate(); } };
            control.MouseUp += (s, e) => { state.Pressed = false; control.Invalidate(); };
            control.EnabledChanged += (s, e) => { state.Hover = false; state.Pressed = false; control.Invalidate(); };
            control.GotFocus += (s, e) => control.Invalidate();
            control.LostFocus += (s, e) => control.Invalidate();
            control.Disposed += (s, e) => pointer.Remove(control);
            return state;
        }

        public static void StyleButton(Button button, bool first)
        {
            Palette p = Theme.Current;
            button.UseVisualStyleBackColor = false;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.BackColor = EffectiveBack(button.Parent ?? button);
            button.FlatAppearance.MouseOverBackColor = button.BackColor;
            button.FlatAppearance.MouseDownBackColor = button.BackColor;
            button.ForeColor = p.Text;

            if (first)
            {
                TrackPointer(button);
                OwnerPaint(button, g => DrawButton(button, g), () => true);
                button.TextChanged += (s, e) => button.Invalidate();
            }

            button.Invalidate();
        }

        /// <summary>
        /// Takes over WM_PAINT of a control, so its own painting (which leaves clipping on the device context and
        /// drew under ours) never runs. Drawing goes through a BufferedGraphics at (0,0), the same double buffer
        /// WinForms uses, so GDI text renders normally and nothing flickers.
        /// </summary>
        private sealed class OwnerPaintWindow : NativeWindow
        {
            private const int WM_PAINT = 0x000F;
            private const int WM_ERASEBKGND = 0x0014;
            private const int WM_PRINTCLIENT = 0x0318;

            private readonly Control control;
            private readonly Action<Graphics> draw;
            private readonly Func<bool> active;

            public OwnerPaintWindow(Control control, Action<Graphics> draw, Func<bool> active)
            {
                this.control = control;
                this.draw = draw;
                this.active = active;
                control.HandleCreated += (s, e) => AssignHandle(control.Handle);
                control.HandleDestroyed += (s, e) => ReleaseHandle();
                if (control.IsHandleCreated)
                    AssignHandle(control.Handle);
            }

            protected override void WndProc(ref Message m)
            {
                if (!active())
                {
                    base.WndProc(ref m);
                    return;
                }

                switch (m.Msg)
                {
                    case WM_ERASEBKGND:
                        m.Result = (IntPtr)1;
                        return;

                    case WM_PAINT:
                    {
                        IntPtr hdc = BeginPaint(Handle, out PAINTSTRUCT paint);
                        try
                        {
                            Render(hdc);
                        }
                        finally
                        {
                            EndPaint(Handle, ref paint);
                        }
                        m.Result = IntPtr.Zero;
                        return;
                    }

                    case WM_PRINTCLIENT:
                        Render(m.WParam);
                        m.Result = IntPtr.Zero;
                        return;
                }

                base.WndProc(ref m);
            }

            private void Render(IntPtr hdc)
            {
                Size size = control.ClientSize;
                if (size.Width <= 0 || size.Height <= 0)
                    return;

                using (BufferedGraphics buffer = BufferedGraphicsManager.Current.Allocate(hdc, new Rectangle(Point.Empty, size)))
                {
                    draw(buffer.Graphics);
                    buffer.Render(hdc);
                }
            }
        }

        private static readonly HashSet<Control> ownerPainted = new HashSet<Control>();

        // On Windows the control's painting is replaced; elsewhere (Mono) a Paint handler draws over it
        private static void OwnerPaint(Control control, Action<Graphics> draw, Func<bool> active)
        {
            if (!ownerPainted.Add(control))
                return;

            control.Disposed += (s, e) => ownerPainted.Remove(control);
            if (IsWindows)
                new OwnerPaintWindow(control, draw, active);
            else
                control.Paint += (s, e) => { if (active()) draw(e.Graphics); };
        }

        private static void DrawButton(Button button, Graphics g)
        {
            Palette p = Theme.Current;
            PointerState state = TrackPointer(button);
            ThemeRole role = Theme.GetRole(button);
            Rectangle client = button.ClientRectangle;
            Color back = EffectiveBack(button.Parent ?? button);

            using (var brush = new SolidBrush(back))
                g.FillRectangle(brush, client);

            g.SmoothingMode = SmoothingMode.AntiAlias;
            // Pixel centres at .5: a 1px outline at x.5 covers exactly one pixel row (the right and bottom edges were half cut)
            g.PixelOffsetMode = PixelOffsetMode.Half;
            LucideIcon icon = Theme.GetIcon(button);

            if (role == ThemeRole.IconButton)
            {
                if (button.Enabled && (state.Hover || state.Pressed))
                {
                    using (var path = RoundedRect(new RectangleF(0.5f, 0.5f, client.Width - 1, client.Height - 1), Px(3)))
                    using (var brush = new SolidBrush(state.Pressed ? p.Accent : p.Hover))
                        g.FillPath(brush, path);
                }

                Color iconColor = !button.Enabled ? Palette.Blend(back, p.Muted, 0.5f) : state.Pressed ? p.AccentText : p.Text;
                icon?.Draw(g, client, iconColor, Math.Min(client.Width, client.Height) - Px(4), 2.25f);
                return;
            }

            Color fill, border, text;
            if (role == ThemeRole.Primary || role == ThemeRole.Danger)
            {
                Color baseColor = role == ThemeRole.Danger ? p.Danger : p.Accent;
                fill = !button.Enabled ? Palette.Blend(back, baseColor, 0.35f)
                    : state.Pressed ? Palette.Blend(baseColor, Color.Black, 0.18f)
                    : state.Hover ? Palette.Blend(baseColor, Color.White, 0.12f)
                    : baseColor;
                border = fill;
                text = button.Enabled ? Color.White : Palette.Blend(fill, Color.White, 0.6f);
            }
            else
            {
                fill = !button.Enabled ? p.Surface2 : state.Pressed ? p.Border : state.Hover ? p.Hover : p.Surface2;
                border = p.Border;
                text = button.Enabled ? p.Text : DisabledText(fill);
            }

            if (button.Focused && button.Enabled)
                border = role == ThemeRole.Default ? p.AccentFg : Palette.Blend(border, Color.White, 0.5f);

            RectangleF shape = new RectangleF(0.5f, 0.5f, client.Width - 1, client.Height - 1);
            using (var path = RoundedRect(shape, Px(3)))
            {
                using (var brush = new SolidBrush(fill))
                    g.FillPath(brush, path);
                using (var pen = new Pen(border))
                    g.DrawPath(pen, path);
            }

            DrawContent(g, client, button.Text, button.Font, icon, text, true);
        }

        // The icon sits at a fixed place on the left; the text is centred in the button, or in the space right of
        // the icon when centring would overlap it
        private static void DrawContent(Graphics g, Rectangle bounds, string text, Font font, LucideIcon icon, Color color, bool showPrefix)
        {
            TextFormatFlags flags = TextFormatFlags.SingleLine | TextFormatFlags.VerticalCenter | TextFormatFlags.NoPadding | TextFormatFlags.Left;
            if (!showPrefix)
                flags |= TextFormatFlags.HidePrefix;

            int iconSize = Px(13);
            int iconLeft = Px(7);
            int gap = Px(4);
            int margin = Px(2);
            Size textSize = string.IsNullOrEmpty(text) ? Size.Empty : TextRenderer.MeasureText(g, text, font, Size.Empty, flags);

            int textLeft = bounds.Left + margin;
            if (icon != null)
            {
                icon.Draw(g, new RectangleF(bounds.Left + iconLeft, bounds.Top, iconSize, bounds.Height), color, iconSize, 2.25f);
                textLeft = bounds.Left + iconLeft + iconSize + gap;
            }

            if (textSize.Width == 0)
                return;

            int textRight = bounds.Right - margin;
            int x = Math.Max(textLeft, bounds.Left + (bounds.Width - textSize.Width) / 2);
            if (x + textSize.Width > textRight)
                x = Math.Max(textLeft, textRight - textSize.Width);

            int room = textRight - x;
            if (textSize.Width > room)
                flags |= TextFormatFlags.EndEllipsis;
            else
                flags |= TextFormatFlags.NoClipping;

            Rectangle textBounds = new Rectangle(x, bounds.Top, Math.Max(0, Math.Min(room, textSize.Width + Px(2))), bounds.Height);
            TextRenderer.DrawText(g, text, font, textBounds, color, flags);
        }

        // Text of a disabled control: muted, kept readable on the background (WinForms would use its own gray)
        public static Color DisabledText(Color back)
        {
            return Palette.Blend(back, Theme.Current.Muted, 0.75f);
        }

        // ---- labels ----

        private static readonly HashSet<Label> labelsHooked = new HashSet<Label>();

        public static void StyleLabel(Label label)
        {
            if (!labelsHooked.Add(label))
                return;

            OwnerPaint(label, g => DrawDisabledLabel(label, g, EffectiveBack(label)), () => !label.Enabled);
            label.EnabledChanged += (s, e) => label.Invalidate();
            label.Disposed += (s, e) => labelsHooked.Remove(label);
        }

        // WinForms draws disabled labels embossed in system gray, unreadable on the dark theme: drawn here instead
        private static void DrawDisabledLabel(Label label, Graphics g, Color back)
        {
            using (var brush = new SolidBrush(back))
                g.FillRectangle(brush, label.ClientRectangle);

            TextFormatFlags flags = AlignmentFlags(label.TextAlign);
            if (!label.UseMnemonic)
                flags |= TextFormatFlags.NoPrefix;
            flags |= label.AutoSize ? TextFormatFlags.SingleLine : TextFormatFlags.WordBreak;
            if (label.AutoEllipsis)
                flags |= TextFormatFlags.EndEllipsis;

            Rectangle bounds = new Rectangle(label.Padding.Left, label.Padding.Top,
                label.ClientSize.Width - label.Padding.Horizontal, label.ClientSize.Height - label.Padding.Vertical);
            TextRenderer.DrawText(g, label.Text, label.Font, bounds, DisabledText(back), flags);
        }

        private static TextFormatFlags AlignmentFlags(ContentAlignment align)
        {
            TextFormatFlags flags;
            switch (align)
            {
                case ContentAlignment.TopCenter: flags = TextFormatFlags.Top | TextFormatFlags.HorizontalCenter; break;
                case ContentAlignment.TopRight: flags = TextFormatFlags.Top | TextFormatFlags.Right; break;
                case ContentAlignment.MiddleLeft: flags = TextFormatFlags.VerticalCenter | TextFormatFlags.Left; break;
                case ContentAlignment.MiddleCenter: flags = TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter; break;
                case ContentAlignment.MiddleRight: flags = TextFormatFlags.VerticalCenter | TextFormatFlags.Right; break;
                case ContentAlignment.BottomLeft: flags = TextFormatFlags.Bottom | TextFormatFlags.Left; break;
                case ContentAlignment.BottomCenter: flags = TextFormatFlags.Bottom | TextFormatFlags.HorizontalCenter; break;
                case ContentAlignment.BottomRight: flags = TextFormatFlags.Bottom | TextFormatFlags.Right; break;
                default: flags = TextFormatFlags.Top | TextFormatFlags.Left; break;
            }
            return flags;
        }

        // ---- check boxes and radio buttons ----

        public static void StyleCheck(ButtonBase control, bool first)
        {
            control.BackColor = Color.Transparent;
            control.ForeColor = Theme.Current.Text;

            if (first)
            {
                TrackPointer(control);
                OwnerPaint(control, g => DrawCheck(control, g), () => true);
            }

            control.Invalidate();
        }

        private static void DrawCheck(ButtonBase control, Graphics g)
        {
            Palette p = Theme.Current;
            PointerState state = TrackPointer(control);
            Rectangle client = control.ClientRectangle;
            Color back = EffectiveBack(control.Parent ?? control);

            using (var brush = new SolidBrush(back))
                g.FillRectangle(brush, client);

            CheckBox checkBox = control as CheckBox;
            RadioButton radio = control as RadioButton;
            bool isChecked = checkBox != null ? checkBox.CheckState != CheckState.Unchecked : radio.Checked;
            bool indeterminate = checkBox != null && checkBox.CheckState == CheckState.Indeterminate;
            ContentAlignment align = checkBox != null ? checkBox.CheckAlign : radio.CheckAlign;
            bool boxRight = align == ContentAlignment.MiddleRight || align == ContentAlignment.TopRight || align == ContentAlignment.BottomRight;

            int box = Px(13);
            int boxX = boxRight ? client.Right - box - 1 : client.Left + 1;
            int boxY = client.Top + (client.Height - box) / 2;
            if (align == ContentAlignment.TopLeft || align == ContentAlignment.TopRight)
                boxY = client.Top + Px(2);
            RectangleF boxRect = new RectangleF(boxX + 0.5f, boxY + 0.5f, box - 1, box - 1);

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.PixelOffsetMode = PixelOffsetMode.Half;
            Color accent = control.Enabled ? p.Accent : Palette.Blend(back, p.Accent, 0.4f);
            Color outline = !control.Enabled ? p.Border : state.Hover || control.Focused ? p.AccentFg : Palette.Blend(back, p.Muted, 0.7f);

            using (GraphicsPath path = radio != null ? Ellipse(boxRect) : RoundedRect(boxRect, Px(3)))
            {
                if (isChecked && checkBox != null)
                {
                    using (var brush = new SolidBrush(accent))
                        g.FillPath(brush, path);
                }
                else
                {
                    using (var brush = new SolidBrush(control.Enabled ? p.Field : p.Surface2))
                        g.FillPath(brush, path);
                    using (var pen = new Pen(isChecked ? (control.Enabled ? p.AccentFg : accent) : outline, isChecked ? 1.5f : 1f))
                        g.DrawPath(pen, path);
                }
            }

            if (isChecked && checkBox != null)
                (indeterminate ? Icons.Minus : Icons.Check).Draw(g, boxRect, Color.White, box - Px(2), 3.5f);
            else if (isChecked)
            {
                float dot = box * 0.46f;
                using (var brush = new SolidBrush(control.Enabled ? p.AccentFg : accent))
                    g.FillEllipse(brush, boxRect.Left + (boxRect.Width - dot) / 2, boxRect.Top + (boxRect.Height - dot) / 2, dot, dot);
            }

            int gap = Px(5);
            Rectangle textBounds = boxRight
                ? new Rectangle(client.Left, client.Top, boxX - gap - client.Left, client.Height)
                : new Rectangle(boxX + box + gap, client.Top, client.Right - (boxX + box + gap), client.Height);

            TextFormatFlags flags = TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.NoPadding | TextFormatFlags.NoClipping;
            flags |= control.AutoSize ? TextFormatFlags.SingleLine : TextFormatFlags.WordBreak;
            TextRenderer.DrawText(g, control.Text, control.Font, textBounds, control.Enabled ? p.Text : DisabledText(back), flags);
        }

        private static GraphicsPath Ellipse(RectangleF r)
        {
            var path = new GraphicsPath();
            path.AddEllipse(r);
            return path;
        }

        // ---- borders of native edit controls ----

        /// <summary>
        /// Subclasses a native edit-like control: draws its rounded outline (outline) and/or centres a single-line
        /// edit vertically (center). Windows always draws single-line edit text at the top, so the client area is
        /// shrunk to one line in WM_NCCALCSIZE and the space above and below is painted in the field color.
        /// </summary>
        private sealed class BorderWindow : NativeWindow
        {
            private const int WM_PAINT = 0x000F;
            private const int WM_NCCALCSIZE = 0x0083;
            private const int WM_NCHITTEST = 0x0084;
            private const int WM_NCPAINT = 0x0085;
            private const int HTCLIENT = 1;
            private const uint SWP_NOSIZE = 0x0001, SWP_NOMOVE = 0x0002, SWP_NOZORDER = 0x0004, SWP_NOACTIVATE = 0x0010, SWP_FRAMECHANGED = 0x0020;

            private readonly Control control;
            private readonly bool outline;
            private readonly bool center;
            private readonly int inset;

            // inset: frame width reserved on every side (for a control without a system border)
            public BorderWindow(Control control, bool outline, bool center, int inset = 0)
            {
                this.control = control;
                this.outline = outline;
                this.center = center;
                this.inset = inset;
                control.HandleCreated += (s, e) => Attach();
                control.HandleDestroyed += (s, e) => ReleaseHandle();
                if (outline)
                {
                    control.Enter += (s, e) => Redraw();
                    control.Leave += (s, e) => Redraw();
                    control.MouseEnter += (s, e) => Redraw();
                    control.MouseLeave += (s, e) => Redraw();
                }
                if (center)
                {
                    control.FontChanged += (s, e) => Recalculate();
                    control.BackColorChanged += (s, e) => Redraw();
                }
                if (control.IsHandleCreated)
                    Attach();
            }

            private void Attach()
            {
                AssignHandle(control.Handle);
                if (center)
                    Recalculate();
            }

            // Makes Windows ask for the frame size again (WM_NCCALCSIZE)
            private void Recalculate()
            {
                if (control.IsHandleCreated)
                    SetWindowPos(control.Handle, IntPtr.Zero, 0, 0, 0, 0, SWP_FRAMECHANGED | SWP_NOMOVE | SWP_NOSIZE | SWP_NOZORDER | SWP_NOACTIVATE);
            }

            private void Redraw()
            {
                if (control.IsHandleCreated)
                    DrawFrame();
            }

            protected override void WndProc(ref Message m)
            {
                if (center && m.Msg == WM_NCCALCSIZE && m.WParam != IntPtr.Zero)
                {
                    base.WndProc(ref m);
                    var calc = (NCCALCSIZE_PARAMS)Marshal.PtrToStructure(m.LParam, typeof(NCCALCSIZE_PARAMS));
                    calc.rgrc0.Left += inset;
                    calc.rgrc0.Top += inset;
                    calc.rgrc0.Right -= inset;
                    calc.rgrc0.Bottom -= inset;
                    int lineHeight = TextRenderer.MeasureText("Wg", control.Font).Height;
                    int extra = (calc.rgrc0.Bottom - calc.rgrc0.Top) - lineHeight;
                    if (extra > 1)
                    {
                        calc.rgrc0.Top += extra / 2;
                        calc.rgrc0.Bottom -= extra - extra / 2;
                    }
                    Marshal.StructureToPtr(calc, m.LParam, false);
                    return;
                }

                if (center && m.Msg == WM_NCHITTEST)
                {
                    // Clicks above and below the text line still go to the text
                    base.WndProc(ref m);
                    m.Result = (IntPtr)HTCLIENT;
                    return;
                }

                base.WndProc(ref m);
                if (m.Msg == WM_PAINT || m.Msg == WM_NCPAINT)
                    DrawFrame();
            }

            private void DrawFrame()
            {
                IntPtr hdc = GetWindowDC(Handle);
                if (hdc == IntPtr.Zero)
                    return;

                try
                {
                    GetWindowRect(Handle, out RECT rect);
                    int width = rect.Right - rect.Left;
                    int height = rect.Bottom - rect.Top;

                    using (Graphics g = Graphics.FromHdc(hdc))
                    {
                        if (center)
                        {
                            // The frame area (everything outside the client) in the field color
                            // Real client area from Windows: WinForms caches the client size and does not see the
                            // change made in WM_NCCALCSIZE, which left part of the frame unpainted
                            GetClientRect(Handle, out RECT clientRect);
                            var origin = new POINT();
                            ClientToScreen(Handle, ref origin);
                            var client = new Rectangle(origin.X - rect.Left, origin.Y - rect.Top,
                                clientRect.Right - clientRect.Left, clientRect.Bottom - clientRect.Top);
                            using (var frame = new Region(new Rectangle(0, 0, width, height)))
                            using (var brush = new SolidBrush(control.BackColor))
                            {
                                frame.Exclude(client);
                                g.FillRegion(brush, frame);
                            }
                        }

                        if (!outline)
                            return;

                        DrawOutline(g, width, height, control);
                    }
                }
                finally
                {
                    ReleaseDC(Handle, hdc);
                }
            }
        }

        // Rounded outline of an input, like the buttons; the corners outside it get the parent's color
        private static void DrawOutline(Graphics g, int width, int height, Control control)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.PixelOffsetMode = PixelOffsetMode.Half;

            using (GraphicsPath path = RoundedRect(new RectangleF(0.5f, 0.5f, width - 1, height - 1), Px(3)))
            {
                using (var outside = new Region(new Rectangle(0, 0, width, height)))
                using (var brush = new SolidBrush(EffectiveBack(control.Parent ?? control)))
                {
                    outside.Exclude(path);
                    g.FillRegion(brush, outside);
                }

                using (var pen = new Pen(BorderColor(control)))
                    g.DrawPath(pen, path);
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct NCCALCSIZE_PARAMS
        {
            public RECT rgrc0, rgrc1, rgrc2;
            public IntPtr lppos;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct POINT
        {
            public int X, Y;
        }

        [DllImport("user32.dll")]
        private static extern bool ClientToScreen(IntPtr hwnd, ref POINT point);

        [DllImport("user32.dll")]
        private static extern bool SetWindowPos(IntPtr hwnd, IntPtr insertAfter, int x, int y, int cx, int cy, uint flags);

        private static Color BorderColor(Control control)
        {
            Palette p = Theme.Current;
            if (!control.Enabled)
                return p.Border;
            if (control.ContainsFocus)
                return p.AccentFg;
            if (control.ClientRectangle.Contains(control.PointToClient(Control.MousePosition)))
                return Palette.Blend(p.Border, p.Muted, 0.5f);
            return p.Border;
        }

        private static readonly HashSet<Control> bordered = new HashSet<Control>();

        public static void AttachBorder(Control control, bool first)
        {
            if (!IsWindows || !bordered.Add(control))
                return;

            control.Disposed += (s, e) => bordered.Remove(control);

            // Inner padding: left and right margin of the text, and the text line centred vertically
            if (control is TextBoxBase textBox)
            {
                // a framed single-line box has no system border: the frame is reserved here
                bool framed = textBox is TextBoxBase tb && framedText.Contains(tb);
                new BorderWindow(control, true, !textBox.Multiline, framed ? 1 : 0);
                KeepTextMargins(control);
            }
            else if (control is NumericUpDown)
            {
                new BorderWindow(control, true, false);
                foreach (Control child in control.Controls)
                {
                    if (child is TextBoxBase)
                    {
                        new BorderWindow(child, false, true);
                        KeepTextMargins(child);
                    }
                }
            }
            else
            {
                new BorderWindow(control, true, false);
            }
        }

        private const int EM_SETMARGINS = 0x00D3;
        private const int EC_LEFTMARGIN = 0x0001;
        private const int EC_RIGHTMARGIN = 0x0002;

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam);

        // The margins are reset when the handle is recreated or the font changes, so they are set again then
        private static void KeepTextMargins(Control edit)
        {
            EventHandler apply = (s, e) =>
            {
                if (!edit.IsHandleCreated)
                    return;
                int margin = Px(4);
                SendMessage(edit.Handle, EM_SETMARGINS, (IntPtr)(EC_LEFTMARGIN | EC_RIGHTMARGIN), (IntPtr)((margin & 0xFFFF) | (margin << 16)));
                edit.Invalidate();
            };

            edit.HandleCreated += apply;
            edit.FontChanged += apply;
            apply(edit, EventArgs.Empty);
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int Left, Top, Right, Bottom;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct PAINTSTRUCT
        {
            public IntPtr hdc;
            public bool fErase;
            public RECT rcPaint;
            public bool fRestore;
            public bool fIncUpdate;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public byte[] rgbReserved;
        }

        [DllImport("user32.dll")]
        private static extern IntPtr GetWindowDC(IntPtr hwnd);

        [DllImport("user32.dll")]
        private static extern IntPtr GetDC(IntPtr hwnd);

        [DllImport("user32.dll")]
        private static extern int ReleaseDC(IntPtr hwnd, IntPtr hdc);

        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr hwnd, out RECT rect);

        [DllImport("user32.dll")]
        private static extern bool GetClientRect(IntPtr hwnd, out RECT rect);

        [DllImport("user32.dll")]
        private static extern IntPtr BeginPaint(IntPtr hwnd, out PAINTSTRUCT paint);

        [DllImport("user32.dll")]
        private static extern bool EndPaint(IntPtr hwnd, ref PAINTSTRUCT paint);

        // ---- text boxes ----

        // Single-line text boxes whose system border was replaced by the theme's own frame
        private static readonly HashSet<TextBoxBase> framedText = new HashSet<TextBoxBase>();

        public static void StyleTextBox(TextBoxBase text, bool first)
        {
            Palette p = Theme.Current;

            // A single-line box with a border loses the system border (Windows drew it over the theme's frame);
            // the theme reserves a 1px frame itself, centres the text line and draws the rounded outline. The
            // box keeps its height.
            if (IsWindows && !text.Multiline && text.BorderStyle != BorderStyle.None && framedText.Add(text))
            {
                int height = text.Height;
                text.AutoSize = false;
                text.BorderStyle = BorderStyle.None;
                text.Height = height;
                text.Disposed += (s, e) => framedText.Remove(text);
                AttachBorder(text, first);
            }
            else if (text.BorderStyle == BorderStyle.Fixed3D)
            {
                text.BorderStyle = BorderStyle.FixedSingle;
            }

            bool framed = framedText.Contains(text);
            if (text.BorderStyle == BorderStyle.None && !framed)
                text.BackColor = EffectiveBack(text.Parent ?? text);
            else
                text.BackColor = text.ReadOnly ? p.Surface2 : p.Field;
            text.ForeColor = p.Text;

            if (first)
            {
                text.ReadOnlyChanged += (s, e) => StyleTextBox(text, false);
            }

            if (text.Multiline)
                SetDarkScrollBars(text);
            if (text.BorderStyle != BorderStyle.None)
                AttachBorder(text, first);
        }

        // ---- numeric up/down ----

        private sealed class SpinButtonsWindow : NativeWindow
        {
            private const int WM_PAINT = 0x000F;
            private readonly Control buttons;
            private readonly NumericUpDown owner;

            public SpinButtonsWindow(Control buttons, NumericUpDown owner)
            {
                this.buttons = buttons;
                this.owner = owner;
                buttons.HandleCreated += (s, e) => AssignHandle(buttons.Handle);
                buttons.HandleDestroyed += (s, e) => ReleaseHandle();
                if (buttons.IsHandleCreated)
                    AssignHandle(buttons.Handle);
            }

            protected override void WndProc(ref Message m)
            {
                base.WndProc(ref m);
                if (m.Msg == WM_PAINT)
                    Draw();
            }

            private void Draw()
            {
                IntPtr hdc = GetDC(Handle);
                if (hdc == IntPtr.Zero)
                    return;

                try
                {
                    GetClientRect(Handle, out RECT rect);
                    int width = rect.Right - rect.Left;
                    int height = rect.Bottom - rect.Top;
                    Palette p = Theme.Current;
                    Point mouse = buttons.PointToClient(Control.MousePosition);
                    bool hover = owner.Enabled && mouse.X >= 0 && mouse.X < width && mouse.Y >= 0 && mouse.Y < height;
                    bool pressed = hover && (Control.MouseButtons & MouseButtons.Left) != 0;
                    Color back = owner.BackColor;

                    using (Graphics g = Graphics.FromHdc(hdc))
                    {
                        using (var brush = new SolidBrush(back))
                            g.FillRectangle(brush, 0, 0, width, height);

                        int half = height / 2;
                        if (hover)
                        {
                            Rectangle part = mouse.Y < half ? new Rectangle(0, 0, width, half) : new Rectangle(0, half, width, height - half);
                            using (var brush = new SolidBrush(pressed ? p.Border : p.Hover))
                                g.FillRectangle(brush, part);
                        }

                        Color glyph = owner.Enabled ? p.Muted : p.Border;
                        float size = Math.Min(width, half * 2f) * 0.9f;
                        Icons.ChevronUp.Draw(g, new RectangleF(0, 0, width, half + 1), glyph, size, 2.5f);
                        Icons.ChevronDown.Draw(g, new RectangleF(0, half - 1, width, height - half + 1), glyph, size, 2.5f);

                        // The buttons sit on the spinner's right border, which the spinner cannot draw over
                        // (its children are clipped out): draw that part of the outline here
                        Point offset = owner.PointToClient(buttons.PointToScreen(Point.Empty));
                        g.TranslateTransform(-offset.X, -offset.Y);
                        DrawOutline(g, owner.Width, owner.Height, owner);
                    }
                }
                finally
                {
                    ReleaseDC(Handle, hdc);
                }
            }
        }

        private static readonly HashSet<NumericUpDown> spinStyled = new HashSet<NumericUpDown>();

        public static void StyleUpDown(NumericUpDown nud, bool first)
        {
            Palette p = Theme.Current;
            // the dirty-value highlight of the application sets its own color; keep it if set
            if (nud.BackColor != p.DirtyField && nud.BackColor != Palette.Light.DirtyField && nud.BackColor != Palette.Dark.DirtyField)
                nud.BackColor = p.Field;
            nud.ForeColor = p.Text;
            if (nud.BorderStyle == BorderStyle.FixedSingle)
                nud.BorderStyle = BorderStyle.Fixed3D;

            if (spinStyled.Add(nud))
            {
                nud.Disposed += (s, e) => spinStyled.Remove(nud);
                foreach (Control child in nud.Controls)
                {
                    if (IsWindows && !(child is TextBox))
                    {
                        new SpinButtonsWindow(child, nud);
                        child.MouseMove += (s, e) => child.Invalidate();
                        child.MouseLeave += (s, e) => child.Invalidate();
                        child.MouseDown += (s, e) => child.Invalidate();
                        child.MouseUp += (s, e) => child.Invalidate();
                    }
                }

                nud.Enter += (s, e) => nud.Invalidate(true);
                nud.Leave += (s, e) => nud.Invalidate(true);
                nud.MouseEnter += (s, e) => nud.Invalidate(true);
                nud.MouseLeave += (s, e) => nud.Invalidate(true);
            }

            if (nud.BorderStyle != BorderStyle.None)
                AttachBorder(nud, first);

            nud.Invalidate(true);
        }

        // ---- combo boxes ----

        private sealed class ComboWindow : NativeWindow
        {
            private const int WM_PAINT = 0x000F;
            private readonly ComboBox combo;

            public ComboWindow(ComboBox combo)
            {
                this.combo = combo;
                combo.HandleCreated += (s, e) => AssignHandle(combo.Handle);
                combo.HandleDestroyed += (s, e) => ReleaseHandle();
                if (combo.IsHandleCreated)
                    AssignHandle(combo.Handle);
            }

            protected override void WndProc(ref Message m)
            {
                if (m.Msg != WM_PAINT || combo.DropDownStyle != ComboBoxStyle.DropDownList)
                {
                    base.WndProc(ref m);
                    return;
                }

                // A drop-down list is drawn completely here: field, selected text and chevron
                IntPtr hdc = BeginPaint(Handle, out PAINTSTRUCT paint);
                try
                {
                    GetClientRect(Handle, out RECT rect);
                    using (Graphics g = Graphics.FromHdc(hdc))
                        Draw(g, new Rectangle(0, 0, rect.Right - rect.Left, rect.Bottom - rect.Top));
                }
                finally
                {
                    EndPaint(Handle, ref paint);
                }

                m.Result = IntPtr.Zero;
            }

            private void Draw(Graphics g, Rectangle bounds)
            {
                Palette p = Theme.Current;
                Color back = EffectiveBack(combo.Parent ?? combo);
                using (var brush = new SolidBrush(back))
                    g.FillRectangle(brush, bounds);

                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.PixelOffsetMode = PixelOffsetMode.Half;
                RectangleF shape = new RectangleF(0.5f, 0.5f, bounds.Width - 1, bounds.Height - 1);
                using (var path = RoundedRect(shape, Px(4)))
                {
                    using (var brush = new SolidBrush(combo.Enabled ? p.Field : p.Surface2))
                        g.FillPath(brush, path);
                    using (var pen = new Pen(BorderColor(combo)))
                        g.DrawPath(pen, path);
                }

                int chevron = Px(14);
                Rectangle textBounds = new Rectangle(Px(6), 0, bounds.Width - chevron - Px(12), bounds.Height);
                TextRenderer.DrawText(g, combo.Text, combo.Font, textBounds, combo.Enabled ? p.Text : p.Muted,
                    TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.SingleLine | TextFormatFlags.EndEllipsis | TextFormatFlags.NoPrefix);

                Icons.ChevronDown.Draw(g, new RectangleF(bounds.Width - chevron - Px(5), 0, chevron, bounds.Height),
                    combo.Enabled ? p.Muted : p.Border, chevron, 2f);
            }
        }

        private static readonly HashSet<ComboBox> combosStyled = new HashSet<ComboBox>();

        public static void StyleCombo(ComboBox combo, bool first)
        {
            Palette p = Theme.Current;
            combo.BackColor = p.Field;
            combo.ForeColor = p.Text;
            combo.FlatStyle = FlatStyle.Flat;

            if (combosStyled.Add(combo))
            {
                combo.Disposed += (s, e) => combosStyled.Remove(combo);

                if (combo.DrawMode == DrawMode.Normal)
                {
                    combo.DrawMode = DrawMode.OwnerDrawFixed;
                    combo.ItemHeight = Math.Max(combo.ItemHeight, combo.Font.Height + Px(4));
                    combo.DrawItem += DrawComboItem;
                }

                if (IsWindows)
                    new ComboWindow(combo);
                EventHandler redraw = (s, e) => combo.Invalidate();
                combo.MouseEnter += redraw;
                combo.MouseLeave += redraw;
                combo.Enter += redraw;
                combo.Leave += redraw;
                combo.SelectedIndexChanged += redraw;
                combo.EnabledChanged += redraw;
                combo.DropDownClosed += redraw;
            }

            SetDarkScrollBars(combo, "DarkMode_CFD");
            combo.Invalidate();
        }

        private static void DrawComboItem(object sender, DrawItemEventArgs e)
        {
            ComboBox combo = (ComboBox)sender;
            Palette p = Theme.Current;
            if (e.Index < 0)
                return;

            bool selected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;
            using (var brush = new SolidBrush(selected ? p.Accent : p.Surface))
                e.Graphics.FillRectangle(brush, e.Bounds);

            Rectangle textBounds = new Rectangle(e.Bounds.Left + Px(6), e.Bounds.Top, e.Bounds.Width - Px(8), e.Bounds.Height);
            TextRenderer.DrawText(e.Graphics, combo.GetItemText(combo.Items[e.Index]), combo.Font, textBounds,
                selected ? p.AccentText : p.Text,
                TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.SingleLine | TextFormatFlags.NoPrefix);
        }

        // ---- grids ----

        public static void StyleGrid(DataGridView grid)
        {
            Palette p = Theme.Current;
            grid.EnableHeadersVisualStyles = false;
            grid.BackgroundColor = p.Surface;
            grid.BorderStyle = BorderStyle.None;
            grid.GridColor = p.Border;
            grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            grid.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            grid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;

            Color selection = Palette.Blend(p.Surface, p.Accent, p.IsDark ? 0.55f : 0.18f);

            grid.DefaultCellStyle.BackColor = p.Surface;
            grid.DefaultCellStyle.ForeColor = p.Text;
            grid.DefaultCellStyle.SelectionBackColor = selection;
            grid.DefaultCellStyle.SelectionForeColor = p.Text;
            grid.AlternatingRowsDefaultCellStyle.BackColor = Palette.Blend(p.Surface, p.Surface2, 0.6f);
            grid.AlternatingRowsDefaultCellStyle.ForeColor = p.Text;
            grid.AlternatingRowsDefaultCellStyle.SelectionBackColor = selection;
            grid.AlternatingRowsDefaultCellStyle.SelectionForeColor = p.Text;

            foreach (DataGridViewCellStyle header in new[] { grid.ColumnHeadersDefaultCellStyle, grid.RowHeadersDefaultCellStyle })
            {
                header.BackColor = p.Surface2;
                header.ForeColor = p.Muted;
                header.SelectionBackColor = p.Surface2;
                header.SelectionForeColor = p.Text;
            }

            foreach (DataGridViewColumn column in grid.Columns)
            {
                if (column.DefaultCellStyle.BackColor != Color.Empty)
                    column.DefaultCellStyle.BackColor = Color.Empty;
                if (column.DefaultCellStyle.ForeColor != Color.Empty)
                    column.DefaultCellStyle.ForeColor = Color.Empty;
            }

            SetDarkScrollBars(grid);
            foreach (Control child in grid.Controls)
                SetDarkScrollBars(child);

            grid.Invalidate();
        }

        // ---- tool strips (status bar) ----

        private sealed class ThemedToolStripRenderer : ToolStripProfessionalRenderer
        {
            protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
            {
                using (var brush = new SolidBrush(Theme.Current.Bg))
                    e.Graphics.FillRectangle(brush, e.AffectedBounds);
            }

            protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
            {
                using (var pen = new Pen(Theme.Current.Border))
                    e.Graphics.DrawLine(pen, 0, 0, e.ToolStrip.Width, 0);
            }

            protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
            {
                e.TextColor = Theme.Current.Muted;
                base.OnRenderItemText(e);
            }

            protected override void OnRenderStatusStripSizingGrip(ToolStripRenderEventArgs e)
            {
            }
        }

        public static void StyleToolStrip(ToolStrip strip)
        {
            if (!(strip.Renderer is ThemedToolStripRenderer))
                strip.Renderer = new ThemedToolStripRenderer();

            strip.BackColor = Theme.Current.Bg;
            strip.ForeColor = Theme.Current.Muted;
            foreach (ToolStripItem item in strip.Items)
                item.ForeColor = Theme.Current.Muted;
            strip.Invalidate();
        }

        // ---- group boxes ----

        public static void StyleGroupBox(GroupBox group, bool first)
        {
            group.ResetBackColor();
            group.ForeColor = Theme.Current.Text;
            if (first)
                group.Paint += PaintGroupBox;
            group.Invalidate();
        }

        private static void PaintGroupBox(object sender, PaintEventArgs e)
        {
            GroupBox group = (GroupBox)sender;
            Palette p = Theme.Current;
            Graphics g = e.Graphics;
            Color back = EffectiveBack(group);

            Size text = TextRenderer.MeasureText(g, group.Text, semiBold);
            int top = text.Height / 2;

            using (var brush = new SolidBrush(back))
                g.FillRectangle(brush, group.ClientRectangle);

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.PixelOffsetMode = PixelOffsetMode.Half;
            using (var path = RoundedRect(new RectangleF(0.5f, top + 0.5f, group.Width - 1, group.Height - top - 1), Px(6)))
            using (var pen = new Pen(p.Border))
                g.DrawPath(pen, path);

            if (!string.IsNullOrEmpty(group.Text))
            {
                Rectangle label = new Rectangle(Px(8), 0, text.Width + Px(4), text.Height);
                using (var brush = new SolidBrush(back))
                    g.FillRectangle(brush, label);
                TextRenderer.DrawText(g, group.Text, semiBold, new Point(label.Left + Px(2), 0), group.Enabled ? p.Text : p.Muted);
            }
        }

        // ---- containers ----

        private static readonly HashSet<Control> cards = new HashSet<Control>();

        public static void StyleContainer(Control control, ThemeRole role, bool first)
        {
            Palette p = Theme.Current;
            switch (role)
            {
                case ThemeRole.Header:
                    control.BackColor = p.Surface2;
                    control.ForeColor = p.Text;
                    break;

                case ThemeRole.Card:
                case ThemeRole.DangerZone:
                case ThemeRole.Callout:
                    control.ResetBackColor();
                    if (cards.Add(control))
                    {
                        control.Paint += PaintCard;
                        control.Disposed += (s, e) => cards.Remove(control);
                        control.Resize += (s, e) => control.Invalidate();
                    }
                    control.Invalidate();
                    break;

                default:
                    if (control.BackColor != Color.Transparent)
                        control.ResetBackColor();
                    control.ForeColor = p.Text;
                    break;
            }

            if (control is ScrollableControl scrollable && scrollable.AutoScroll)
                SetDarkScrollBars(control);
        }

        private static void PaintCard(object sender, PaintEventArgs e)
        {
            Control control = (Control)sender;
            Palette p = Theme.Current;
            ThemeRole role = Theme.GetRole(control);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.Half;
            using (var path = RoundedRect(new RectangleF(0.5f, 0.5f, control.Width - 1, control.Height - 1), Px(6)))
            {
                Color border = role == ThemeRole.DangerZone ? p.Danger : p.Border;
                if (role == ThemeRole.Callout)
                {
                    // Accent tint; labels inside need a transparent background to show it
                    using (var brush = new SolidBrush(Palette.Blend(control.BackColor, p.Accent, p.IsDark ? 0.16f : 0.07f)))
                        e.Graphics.FillPath(brush, path);
                    border = Palette.Blend(control.BackColor, p.Accent, p.IsDark ? 0.45f : 0.3f);
                }

                using (var pen = new Pen(border))
                    e.Graphics.DrawPath(pen, path);
            }
        }
    }
}
