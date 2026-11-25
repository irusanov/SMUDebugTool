using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ZenStatesDebugTool.Theming
{
    /// <summary>
    /// Form with the application theme and a custom title bar. The system caption is removed (WM_NCCALCSIZE) but
    /// the window keeps its normal styles, so the DWM shadow, Aero Snap, Alt+Space and the taskbar behave as
    /// usual. The title bar is drawn in the top of the client area, reserved through Padding.
    /// </summary>
    public class ThemedForm : Form
    {
        private const int WM_NCCALCSIZE = 0x0083;
        private const int WM_NCHITTEST = 0x0084;
        private const int WM_NCACTIVATE = 0x0086;
        private const int HTCLIENT = 1;
        private const int HTCAPTION = 2;
        private const int HTLEFT = 10, HTRIGHT = 11, HTTOP = 12, HTTOPLEFT = 13, HTTOPRIGHT = 14;
        private const int HTBOTTOM = 15, HTBOTTOMLEFT = 16, HTBOTTOMRIGHT = 17;
        private const int SM_CXSIZEFRAME = 32, SM_CYSIZEFRAME = 33, SM_CXPADDEDBORDER = 92;

        private enum CaptionButton { None, Theme, Minimize, Maximize, Close }

        private readonly bool designTime;
        private bool chrome;
        private CaptionButton hover = CaptionButton.None;
        private CaptionButton pressed = CaptionButton.None;
        private Icon smallIcon;
        private ToolTip titleToolTip;

        // Buttons a form adds to its title bar (left of the theme toggle) are CaptionButton values from this one up
        private const int CustomButtonBase = 100;
        private readonly System.Collections.Generic.List<TitleBarButton> titleBarButtons = new System.Collections.Generic.List<TitleBarButton>();
        private static readonly Font TitleFont = new Font("Segoe UI Semibold", 9f);

        public ThemedForm()
        {
            designTime = LicenseManager.UsageMode == LicenseUsageMode.Designtime;
            if (designTime)
                return;

            // Before InitializeComponent of the derived form, so its layout is scaled for this font like a
            // font chosen in the designer
            Font = Theme.UiFont;
            DoubleBuffered = true;
            Theme.Changed += OnThemeChanged;
        }

        /// <summary>Show the custom title bar. Off for dialogs that draw their own header.</summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool ShowTitleBar { get; set; } = true;

        /// <summary>Show the light/dark toggle in the title bar.</summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool ShowThemeToggle { get; set; } = true;

        /// <summary>A button in the title bar, drawn like the caption buttons.</summary>
        public sealed class TitleBarButton
        {
            /// <param name="icon">Icon to draw, asked on every paint so it can follow a state</param>
            /// <param name="click">Called when the button is clicked</param>
            /// <param name="toolTip">Tooltip text, asked when the pointer enters the button; null for none</param>
            public TitleBarButton(Func<LucideIcon> icon, Action click, Func<string> toolTip = null)
            {
                Icon = icon;
                Click = click;
                ToolTip = toolTip;
            }

            public Func<LucideIcon> Icon { get; }
            public Action Click { get; }
            public Func<string> ToolTip { get; }
        }

        /// <summary>Adds a button to the title bar, left of the theme toggle. Buttons added later go further left.</summary>
        protected void AddTitleBarButton(TitleBarButton button)
        {
            titleBarButtons.Insert(0, button);
            Invalidate(TitleBounds);
        }

        /// <summary>Redraws the title bar, for example after the state shown by a title bar button changed.</summary>
        protected void RefreshTitleBar() => Invalidate(TitleBounds);

        private TitleBarButton CustomButton(CaptionButton button)
        {
            int index = (int)button - CustomButtonBase;
            return index >= 0 && index < titleBarButtons.Count ? titleBarButtons[index] : null;
        }

        /// <summary>Drag the window by any empty part of the form (dialogs without a title bar).</summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool DragAnywhere { get; set; }

        protected int TitleBarHeight => ShowTitleBar ? ControlStyler.Px(34) : 0;

        private bool Resizable =>
            FormBorderStyle == FormBorderStyle.Sizable || FormBorderStyle == FormBorderStyle.SizableToolWindow;

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            if (designTime || chrome)
                return;

            // The designer's client size is the content; the title bar is added on top of it
            Size content = ClientSize;
            Padding padding = Padding;

            chrome = true;
            if (ControlStyler.IsWindows)
                SetWindowPos(Handle, IntPtr.Zero, 0, 0, 0, 0, SWP_FRAMECHANGED | SWP_NOMOVE | SWP_NOSIZE | SWP_NOZORDER | SWP_NOACTIVATE);

            Padding = new Padding(padding.Left, padding.Top + TitleBarHeight, padding.Right, padding.Bottom);
            ClientSize = new Size(content.Width, content.Height + TitleBarHeight);

            if (ControlStyler.IsWindows)
                Theme.ApplyWindowFrame(Handle);
        }

        // Without a system caption the window is all client area; WinForms would otherwise add the old frame
        protected override Size SizeFromClientSize(Size clientSize)
        {
            return chrome && ControlStyler.IsWindows ? clientSize : base.SizeFromClientSize(clientSize);
        }

        // Setting ClientSize at runtime (after the handle exists) does not go through SizeFromClientSize(Size):
        // WinForms adds the old caption and border itself, so the window came out wider and taller than asked, while
        // ClientSize still reported the requested size (the caption buttons were drawn short of the right edge).
        // The window is all client area, so the window size is the client size.
        protected override void SetClientSizeCore(int x, int y)
        {
            if (chrome && ControlStyler.IsWindows && WindowState == FormWindowState.Normal)
            {
                Size = new Size(x, y);
                return;
            }

            base.SetClientSizeCore(x, y);
        }

        protected override void OnLoad(EventArgs e)
        {
            if (!designTime)
            {
                Theme.Apply(this);
                StyleToolTips();
                OnThemeApplied(null);
            }

            base.OnLoad(e);
        }

        // Tool tips are components, not controls: find them among the form's fields
        private void StyleToolTips()
        {
            for (Type type = GetType(); type != null && type != typeof(ThemedForm); type = type.BaseType)
            {
                foreach (FieldInfo field in type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.DeclaredOnly))
                {
                    if (typeof(ToolTip).IsAssignableFrom(field.FieldType))
                        Theme.StyleToolTip(field.GetValue(this) as ToolTip);
                }
            }
        }

        private void OnThemeChanged(Palette previous)
        {
            if (IsDisposed)
                return;

            if (InvokeRequired)
            {
                BeginInvoke(new Action<Palette>(OnThemeChanged), previous);
                return;
            }

            SuspendLayout();
            Theme.Apply(this);
            OnThemeApplied(previous);
            ResumeLayout();

            if (IsHandleCreated && ControlStyler.IsWindows)
                Theme.ApplyWindowFrame(Handle);
            Invalidate(true);
        }

        /// <summary>
        /// Called after the theme has been applied to the form: on load (previous is null) and after a switch.
        /// Override to recolor what the form draws itself.
        /// </summary>
        protected virtual void OnThemeApplied(Palette previous)
        {
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Theme.Changed -= OnThemeChanged;
                smallIcon?.Dispose();
                titleToolTip?.Dispose();
            }

            base.Dispose(disposing);
        }

        // ---- frame ----

        protected override void WndProc(ref Message m)
        {
            if (chrome)
            {
                if (m.Msg == WM_NCCALCSIZE && m.WParam != IntPtr.Zero)
                {
                        if (WindowState == FormWindowState.Maximized)
                        {
                            // A maximized window extends past the screen by its frame; keep the content on screen
                            NCCALCSIZE_PARAMS calc = (NCCALCSIZE_PARAMS)Marshal.PtrToStructure(m.LParam, typeof(NCCALCSIZE_PARAMS));
                            int x = GetSystemMetrics(SM_CXSIZEFRAME) + GetSystemMetrics(SM_CXPADDEDBORDER);
                            int y = GetSystemMetrics(SM_CYSIZEFRAME) + GetSystemMetrics(SM_CXPADDEDBORDER);
                            calc.rgrc0.Left += x;
                            calc.rgrc0.Top += y;
                            calc.rgrc0.Right -= x;
                            calc.rgrc0.Bottom -= y;
                            Marshal.StructureToPtr(calc, m.LParam, false);
                        }
                        m.Result = IntPtr.Zero;
                        return;
                }

                switch (m.Msg)
                {
                    case WM_NCACTIVATE:
                        // Keep Windows from drawing an inactive caption over the client area
                        m.Result = DefWindowProc(Handle, m.Msg, m.WParam, new IntPtr(-1));
                        Invalidate(TitleBounds);
                        return;

                    case WM_NCHITTEST:
                        base.WndProc(ref m);
                        if (m.Result.ToInt32() == HTCLIENT)
                            m.Result = new IntPtr(HitTest(PointToClient(new Point(m.LParam.ToInt32()))));
                        return;
                }
            }

            base.WndProc(ref m);
        }

        private int HitTest(Point point)
        {
            if (Resizable && WindowState == FormWindowState.Normal)
            {
                int edge = ControlStyler.Px(6);
                bool left = point.X < edge, right = point.X >= ClientSize.Width - edge;
                bool top = point.Y < edge, bottom = point.Y >= ClientSize.Height - edge;

                if (top && left) return HTTOPLEFT;
                if (top && right) return HTTOPRIGHT;
                if (bottom && left) return HTBOTTOMLEFT;
                if (bottom && right) return HTBOTTOMRIGHT;
                if (left) return HTLEFT;
                if (right) return HTRIGHT;
                if (top) return HTTOP;
                if (bottom) return HTBOTTOM;
            }

            if (point.Y < TitleBarHeight)
                return ButtonAt(point) == CaptionButton.None ? HTCAPTION : HTCLIENT;

            // Only reached for the form's own background (child controls answer for themselves)
            return DragAnywhere ? HTCAPTION : HTCLIENT;
        }

        // ---- title bar ----

        private Rectangle TitleBounds => new Rectangle(0, 0, ClientSize.Width, TitleBarHeight);

        private int CaptionButtonWidth => ControlStyler.Px(44);

        private CaptionButton[] VisibleButtons()
        {
            var buttons = new System.Collections.Generic.List<CaptionButton>();
            for (int i = 0; i < titleBarButtons.Count; i++)
                buttons.Add((CaptionButton)(CustomButtonBase + i));
            if (ShowThemeToggle) buttons.Add(CaptionButton.Theme);
            if (MinimizeBox && FormBorderStyle != FormBorderStyle.SizableToolWindow && FormBorderStyle != FormBorderStyle.FixedToolWindow)
                buttons.Add(CaptionButton.Minimize);
            if (MaximizeBox && Resizable && FormBorderStyle != FormBorderStyle.SizableToolWindow)
                buttons.Add(CaptionButton.Maximize);
            if (ControlBox) buttons.Add(CaptionButton.Close);
            return buttons.ToArray();
        }

        private Rectangle ButtonBounds(CaptionButton button)
        {
            CaptionButton[] buttons = VisibleButtons();
            int index = Array.IndexOf(buttons, button);
            if (index < 0)
                return Rectangle.Empty;

            int width = CaptionButtonWidth;
            int x = ClientSize.Width - (buttons.Length - index) * width;
            return new Rectangle(x, 0, width, TitleBarHeight);
        }

        private CaptionButton ButtonAt(Point point)
        {
            foreach (CaptionButton button in VisibleButtons())
            {
                if (ButtonBounds(button).Contains(point))
                    return button;
            }
            return CaptionButton.None;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (!chrome || TitleBarHeight == 0)
                return;

            Palette p = Theme.Current;
            Graphics g = e.Graphics;
            Rectangle title = TitleBounds;
            bool active = ActiveForm == this;

            using (var brush = new SolidBrush(p.Bg))
                g.FillRectangle(brush, title);
            using (var pen = new Pen(p.Border))
                g.DrawLine(pen, 0, title.Bottom - 1, title.Right, title.Bottom - 1);

            int x = ControlStyler.Px(12);
            if (ShowIcon && Icon != null)
            {
                int size = ControlStyler.Px(16);
                if (smallIcon == null || smallIcon.Width != size)
                {
                    smallIcon?.Dispose();
                    smallIcon = new Icon(Icon, size, size);
                }
                g.DrawIcon(smallIcon, new Rectangle(x, (title.Height - size) / 2, size, size));
                x += size + ControlStyler.Px(8);
            }

            CaptionButton[] buttons = VisibleButtons();
            int textRight = title.Right - buttons.Length * CaptionButtonWidth - ControlStyler.Px(8);
            {
                TextRenderer.DrawText(g, Text, TitleFont, new Rectangle(x, 0, Math.Max(0, textRight - x), title.Height),
                    active ? p.Text : p.Muted,
                    TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.SingleLine | TextFormatFlags.EndEllipsis | TextFormatFlags.NoPrefix);
            }

            foreach (CaptionButton button in buttons)
                DrawCaptionButton(g, button, p, active);
        }

        private void DrawCaptionButton(Graphics g, CaptionButton button, Palette p, bool active)
        {
            Rectangle bounds = ButtonBounds(button);
            bool isHover = hover == button;
            bool isPressed = pressed == button && isHover;
            Color glyph = active ? p.Text : p.Muted;

            if (isHover)
            {
                Color fill = button == CaptionButton.Close
                    ? (isPressed ? Color.FromArgb(0xF1, 0x70, 0x7A) : Color.FromArgb(0xE8, 0x11, 0x23))
                    : (isPressed ? p.Border : p.Hover);
                using (var brush = new SolidBrush(fill))
                    g.FillRectangle(brush, bounds);
                if (button == CaptionButton.Close)
                    glyph = Color.White;
            }

            TitleBarButton custom = CustomButton(button);
            LucideIcon icon;
            if (custom != null)
            {
                icon = custom.Icon?.Invoke();
                if (icon != null)
                {
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    icon.Draw(g, bounds, glyph, ControlStyler.Px(15), 2f);
                }
                return;
            }

            switch (button)
            {
                case CaptionButton.Theme: icon = p.IsDark ? Icons.Sun : Icons.Moon; break;
                case CaptionButton.Minimize: icon = Icons.Minus; break;
                case CaptionButton.Maximize: icon = WindowState == FormWindowState.Maximized ? Icons.Copy : Icons.Square; break;
                default: icon = Icons.X; break;
            }

            float size = button == CaptionButton.Theme ? ControlStyler.Px(15) : ControlStyler.Px(14);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            icon.Draw(g, bounds, glyph, size, button == CaptionButton.Theme ? 2f : 1.5f);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            SetHover(chrome ? ButtonAt(e.Location) : CaptionButton.None);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            SetHover(CaptionButton.None);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (chrome && e.Button == MouseButtons.Left)
            {
                pressed = ButtonAt(e.Location);
                if (pressed != CaptionButton.None)
                    Invalidate(TitleBounds);
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            CaptionButton released = chrome ? ButtonAt(e.Location) : CaptionButton.None;
            CaptionButton wasPressed = pressed;
            pressed = CaptionButton.None;
            Invalidate(TitleBounds);

            if (e.Button != MouseButtons.Left || released == CaptionButton.None || released != wasPressed)
                return;

            switch (released)
            {
                case CaptionButton.Theme:
                    Theme.Toggle();
                    break;
                case CaptionButton.Minimize:
                    WindowState = FormWindowState.Minimized;
                    break;
                case CaptionButton.Maximize:
                    WindowState = WindowState == FormWindowState.Maximized ? FormWindowState.Normal : FormWindowState.Maximized;
                    break;
                case CaptionButton.Close:
                    Close();
                    break;
                default:
                    CustomButton(released)?.Click?.Invoke();
                    break;
            }
        }

        private void SetHover(CaptionButton button)
        {
            if (hover == button)
                return;
            hover = button;
            Invalidate(TitleBounds);
            UpdateTitleToolTip(button);
        }

        // Title bar buttons are painted, not controls: their tooltip is shown by hand
        private void UpdateTitleToolTip(CaptionButton button)
        {
            string text = CustomButton(button)?.ToolTip?.Invoke();
            if (string.IsNullOrEmpty(text))
            {
                titleToolTip?.Hide(this);
                return;
            }

            if (titleToolTip == null)
            {
                titleToolTip = new ToolTip();
                Theme.StyleToolTip(titleToolTip);
            }

            Rectangle bounds = ButtonBounds(button);
            titleToolTip.Show(text, this, bounds.Left, bounds.Bottom + ControlStyler.Px(4), 5000);
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            if (chrome)
                Invalidate(TitleBounds);
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            if (chrome)
                Invalidate(TitleBounds);
        }

        protected override void OnDeactivate(EventArgs e)
        {
            base.OnDeactivate(e);
            if (chrome)
                Invalidate(TitleBounds);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (chrome)
                Invalidate(TitleBounds);
        }

        // ---- native ----

        private const uint SWP_NOSIZE = 0x0001, SWP_NOMOVE = 0x0002, SWP_NOZORDER = 0x0004, SWP_NOACTIVATE = 0x0010, SWP_FRAMECHANGED = 0x0020;

        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int Left, Top, Right, Bottom;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct NCCALCSIZE_PARAMS
        {
            public RECT rgrc0, rgrc1, rgrc2;
            public IntPtr lppos;
        }

        [DllImport("user32.dll")]
        private static extern bool SetWindowPos(IntPtr hwnd, IntPtr insertAfter, int x, int y, int cx, int cy, uint flags);

        [DllImport("user32.dll")]
        private static extern int GetSystemMetrics(int index);

        [DllImport("user32.dll")]
        private static extern IntPtr DefWindowProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam);
    }
}
