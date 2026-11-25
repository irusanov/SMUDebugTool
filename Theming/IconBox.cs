using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Windows.Forms;

namespace ZenStatesDebugTool.Theming
{
    /// <summary>Color of an <see cref="IconBox"/> icon, taken from the current theme.</summary>
    public enum IconTone
    {
        Accent,
        Text,
        Muted,
        Danger,
    }

    /// <summary>
    /// Shows one Lucide icon from <see cref="Icons"/>, in a theme color. The background is transparent, so it
    /// sits on cards and callouts that paint their own fill.
    /// </summary>
    [ToolboxItem(true)]
    public class IconBox : Control
    {
        private string iconName = "Info";
        private IconTone tone = IconTone.Accent;
        private float strokeWidth = 2f;

        public IconBox()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.Selectable, false);
            BackColor = Color.Transparent;
            TabStop = false;
            Size = new Size(16, 16);
        }

        /// <summary>Name of the icon field in <see cref="Icons"/>, for example "Info" or "TriangleAlert".</summary>
        [Category("Appearance"), DefaultValue("Info")]
        public string IconName
        {
            get => iconName;
            set { iconName = value; Invalidate(); }
        }

        [Category("Appearance"), DefaultValue(IconTone.Accent)]
        public IconTone Tone
        {
            get => tone;
            set { tone = value; Invalidate(); }
        }

        [Category("Appearance"), DefaultValue(2f)]
        public float StrokeWidth
        {
            get => strokeWidth;
            set { strokeWidth = value; Invalidate(); }
        }

        private LucideIcon Icon =>
            string.IsNullOrEmpty(iconName) ? null
            : typeof(Icons).GetField(iconName, BindingFlags.Public | BindingFlags.Static | BindingFlags.IgnoreCase)?.GetValue(null) as LucideIcon;

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            LucideIcon icon = Icon;
            if (icon == null)
                return;

            Palette p = Theme.Current;
            Color color = !Enabled ? p.Muted
                : tone == IconTone.Accent ? p.AccentFg
                : tone == IconTone.Danger ? p.Danger
                : tone == IconTone.Muted ? p.Muted
                : p.Text;

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.Half;
            icon.Draw(e.Graphics, ClientRectangle, color, Math.Min(ClientSize.Width, ClientSize.Height), strokeWidth);
        }
    }
}
