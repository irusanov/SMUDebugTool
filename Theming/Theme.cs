using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Win32;

namespace ZenStatesDebugTool.Theming
{
    /// <summary>
    /// Colors of one theme. Tailwind slate scale, like XrpQuad, with the purple of the application icon as accent.
    /// </summary>
    public sealed class Palette
    {
        public bool IsDark;

        public Color Bg;          // window, title bar, tab list
        public Color Surface;     // pages, log, dialogs
        public Color Surface2;    // buttons, read-only fields, headers
        public Color Border;
        public Color Text;
        public Color Muted;
        public Color Accent;      // fills: primary buttons, selection
        public Color AccentText;  // text on accent fills
        public Color AccentFg;    // accent used as foreground (icons, focus border) on Surface/Bg
        public Color Field;       // editable inputs
        public Color DirtyField;  // edited, not yet applied value
        public Color Success;
        public Color Warning;
        public Color Danger;
        public Color Hover;       // hover fill of flat items

        /// <summary>Purple from the application icon (the tab color of the previous UI).</summary>
        public static readonly Color Purple = Color.FromArgb(0x68, 0x00, 0xAD);

        public static readonly Palette Light = new Palette
        {
            IsDark = false,
            Bg = Hex(0xF8FAFC),
            Surface = Hex(0xFFFFFF),
            Surface2 = Hex(0xF1F5F9),
            Border = Hex(0xE2E8F0),
            Text = Hex(0x0F172A),
            Muted = Hex(0x64748B),
            Accent = Purple,
            AccentText = Color.White,
            AccentFg = Purple,
            Field = Hex(0xFFFFFF),
            DirtyField = Hex(0xFEF3C7),
            Success = Hex(0x059669),
            Warning = Hex(0xB45309),
            Danger = Hex(0xE11D48),
            Hover = Hex(0xE2E8F0),
        };

        public static readonly Palette Dark = new Palette
        {
            IsDark = true,
            Bg = Hex(0x0F172A),
            Surface = Hex(0x1E293B),
            Surface2 = Hex(0x273449),
            Border = Hex(0x334155),
            Text = Hex(0xF1F5F9),
            Muted = Hex(0x94A3B8),
            Accent = Purple,
            AccentText = Color.White,
            // The icon purple is too dark to read on slate; a lighter tint of the same hue for text and outlines
            AccentFg = Hex(0xC084FC),
            Field = Hex(0x0F172A),
            DirtyField = Hex(0x4A3A10),
            Success = Hex(0x34D399),
            Warning = Hex(0xFBBF24),
            Danger = Hex(0xFB7185),
            Hover = Hex(0x334155),
        };

        private static Color Hex(int rgb) => Color.FromArgb((rgb >> 16) & 0xFF, (rgb >> 8) & 0xFF, rgb & 0xFF);

        /// <summary>Mixes <paramref name="color"/> over <paramref name="background"/> (amount 0..1).</summary>
        public static Color Blend(Color background, Color color, float amount)
        {
            return Color.FromArgb(
                (int)(background.R + (color.R - background.R) * amount),
                (int)(background.G + (color.G - background.G) * amount),
                (int)(background.B + (color.B - background.B) * amount));
        }
    }

    /// <summary>How a control is styled when it is not the default for its type.</summary>
    public enum ThemeRole
    {
        Default,
        Muted,          // secondary text
        Primary,        // accent button
        Danger,         // red action button
        Header,         // strip with a Surface2 background (CCD headers)
        IconButton,     // flat square button showing only its icon
        Card,           // panel with a 1px rounded border
        DangerZone,     // panel with a 1px rounded border in the danger color (settings that can make the system unstable)
        Callout,        // panel with a tinted rounded fill and border, for a notice (put an IconBox and a label in it)
        Plain,          // left alone by the theme
    }

    public enum ThemeMode
    {
        System = 0,
        Light = 1,
        Dark = 2,
    }

    /// <summary>Current theme, switching, and the styling of standard WinForms controls.</summary>
    public static class Theme
    {
        private static Palette current = Palette.Light;

        public static Palette Current => current;

        /// <summary>Raised after the palette changed; the argument is the previous palette.</summary>
        public static event Action<Palette> Changed;

        public static readonly Font UiFont = new Font("Segoe UI", 8.25f);
        private static readonly Dictionary<Font, Font> replacedFonts = new Dictionary<Font, Font>();

        // ---- mode ----

        public static void Initialize(ThemeMode mode)
        {
            current = Resolve(mode);
        }

        public static void Toggle()
        {
            SetMode(current.IsDark ? ThemeMode.Light : ThemeMode.Dark);
        }

        public static void SetMode(ThemeMode mode)
        {
            Palette previous = current;
            current = Resolve(mode);

            try
            {
                AppSettings.Instance.ThemeMode = (int)mode;
                AppSettings.Instance.Save();
            }
            catch
            {
            }

            if (previous != current)
                Changed?.Invoke(previous);
        }

        private static Palette Resolve(ThemeMode mode)
        {
            switch (mode)
            {
                case ThemeMode.Dark: return Palette.Dark;
                case ThemeMode.Light: return Palette.Light;
                default: return IsSystemDark() ? Palette.Dark : Palette.Light;
            }
        }

        // Windows "app mode" (Settings > Personalization > Colors)
        private static bool IsSystemDark()
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize"))
                    return key?.GetValue("AppsUseLightTheme") is int value && value == 0;
            }
            catch
            {
                return false;
            }
        }

        // ---- roles and icons ----

        private static readonly Dictionary<Control, ThemeRole> roles = new Dictionary<Control, ThemeRole>();
        private static readonly Dictionary<Control, LucideIcon> icons = new Dictionary<Control, LucideIcon>();
        private static readonly HashSet<Control> styled = new HashSet<Control>();

        public static void SetRole(Control control, ThemeRole role)
        {
            roles[control] = role;
            control.Disposed -= ForgetControl;
            control.Disposed += ForgetControl;

            if (styled.Contains(control))
                StyleOne(control);
        }

        public static ThemeRole GetRole(Control control)
        {
            return roles.TryGetValue(control, out ThemeRole role) ? role : ThemeRole.Default;
        }

        public static void SetIcon(Control control, LucideIcon icon)
        {
            if (icon == null)
                icons.Remove(control);
            else
                icons[control] = icon;

            control.Disposed -= ForgetControl;
            control.Disposed += ForgetControl;
            control.Invalidate();
        }

        public static LucideIcon GetIcon(Control control)
        {
            return icons.TryGetValue(control, out LucideIcon icon) ? icon : null;
        }

        private static void ForgetControl(object sender, EventArgs e)
        {
            Control control = (Control)sender;
            roles.Remove(control);
            icons.Remove(control);
            styled.Remove(control);
        }

        // Standard action buttons get an icon from their text
        private static LucideIcon IconForText(string text)
        {
            switch ((text ?? "").Trim().TrimEnd('.').ToLowerInvariant())
            {
                case "apply": return Icons.Check;
                case "refresh": return Icons.Refresh;
                case "save": return Icons.Save;
                case "load": return Icons.FolderOpen;
                case "discard": return Icons.Undo;
                case "clear": return Icons.Eraser;
                case "copy": return Icons.Copy;
                default: return null;
            }
        }

        // ---- applying ----

        /// <summary>Styles <paramref name="root"/> and everything inside it, now and when controls are added later.</summary>
        public static void Apply(Control root)
        {
            StyleOne(root);

            foreach (Control child in root.Controls)
                Apply(child);
        }

        private static void StyleOne(Control control)
        {
            bool first = styled.Add(control);
            if (first)
            {
                control.Disposed -= ForgetControl;
                control.Disposed += ForgetControl;
                control.ControlAdded += (s, e) => Apply(e.Control);
                RememberDesignerRole(control);
            }

            ThemeRole role = GetRole(control);
            if (role == ThemeRole.Plain)
                return;

            ReplaceFont(control);
            Palette p = current;

            if (control is Form)
            {
                // dialogs (Card role) use the surface color, windows the background
                control.BackColor = role == ThemeRole.Card ? p.Surface : p.Bg;
                control.ForeColor = p.Text;
            }
            else if (control is TabPage page)
            {
                page.UseVisualStyleBackColor = false;
                page.BackColor = p.Surface;
                page.ForeColor = p.Text;
            }
            else if (control is Button button)
            {
                ControlStyler.StyleButton(button, first);
            }
            else if (control is CheckBox || control is RadioButton)
            {
                ControlStyler.StyleCheck((ButtonBase)control, first);
            }
            else if (control is NumericUpDown nud)
            {
                ControlStyler.StyleUpDown(nud, first);
            }
            else if (control is ComboBox combo)
            {
                ControlStyler.StyleCombo(combo, first);
            }
            else if (control is TextBoxBase text)
            {
                ControlStyler.StyleTextBox(text, first);
            }
            else if (control is ListBox list)
            {
                // owner-drawn lists (the tab list) draw their own colors
                list.BackColor = list.DrawMode == DrawMode.Normal ? p.Field : p.Bg;
                list.ForeColor = p.Text;
                ControlStyler.SetDarkScrollBars(list);
                if (list.BorderStyle != BorderStyle.None)
                    ControlStyler.AttachBorder(list, first);
            }
            else if (control is DataGridView grid)
            {
                ControlStyler.StyleGrid(grid);
            }
            else if (control is StatusStrip || control is ToolStrip)
            {
                ControlStyler.StyleToolStrip((ToolStrip)control);
            }
            else if (control is SplitContainer split)
            {
                split.BackColor = p.Border;
                split.Panel1.BackColor = p.Bg;
                split.Panel2.BackColor = p.Surface;
            }
            else if (control is SplitterPanel)
            {
                // colored by its SplitContainer
            }
            else if (control is GroupBox group)
            {
                ControlStyler.StyleGroupBox(group, first);
            }
            else if (control is TabControl)
            {
                control.BackColor = p.Surface;
            }
            else if (control is Label || control is LinkLabel)
            {
                ControlStyler.StyleLabel((Label)control);
                control.ForeColor = role == ThemeRole.Muted ? p.Muted : role == ThemeRole.Danger ? p.Danger : p.Text;
                if (role == ThemeRole.Header)
                    control.BackColor = Color.Transparent;
                else if (control.BackColor != Color.Transparent)
                    control.ResetBackColor();
            }
            else if (control is Panel || control is TableLayoutPanel || control is FlowLayoutPanel || control is UserControl)
            {
                ControlStyler.StyleContainer(control, role, first);
            }
            else
            {
                control.ForeColor = p.Text;
            }
        }

        /// <summary>Tag value that keeps an "Apply" button in the default (secondary) style.</summary>
        public const string SecondaryTag = "secondary";

        // True when the button is marked secondary in the designer: Tag = "secondary"
        private static bool IsMarkedSecondary(Control control) =>
            control.Tag is string tag && string.Equals(tag.Trim(), SecondaryTag, StringComparison.OrdinalIgnoreCase);

        // Roles that follow from designer properties:
        // - a label colored GrayText/ControlDark is Muted (secondary text)
        // - an "Apply" button is Primary, unless its Tag is "secondary" (set it in the designer, or call
        //   Theme.SetRole(button, ThemeRole.Default) before or after the theme is applied)
        // A role set in code before the theme is applied is kept. Standard action buttons get their icon either way.
        private static void RememberDesignerRole(Control control)
        {
            bool hasRole = roles.TryGetValue(control, out ThemeRole role);

            if (control is Button button && IconForText(button.Text) != null && !icons.ContainsKey(control)
                && role != ThemeRole.IconButton && role != ThemeRole.Plain)
                icons[control] = IconForText(button.Text);

            if (hasRole)
                return;

            if (control is Label && (control.ForeColor == SystemColors.GrayText || control.ForeColor == SystemColors.ControlDark))
                roles[control] = ThemeRole.Muted;
            else if (control is Button applyButton && string.Equals(applyButton.Text, "Apply", StringComparison.OrdinalIgnoreCase)
                && !IsMarkedSecondary(applyButton))
                roles[control] = ThemeRole.Primary;
        }

        // The designer default and explicit "Microsoft Sans Serif" fonts become Segoe UI of the same size and style
        private static void ReplaceFont(Control control)
        {
            Font font = control.Font;
            if (font == null || !string.Equals(font.FontFamily.Name, "Microsoft Sans Serif", StringComparison.OrdinalIgnoreCase))
                return;

            if (!replacedFonts.TryGetValue(font, out Font replacement))
            {
                replacement = new Font("Segoe UI", font.Size, font.Style, font.Unit);
                replacedFonts[font] = replacement;
            }

            control.Font = replacement;
        }

        // ---- tooltips ----

        public static void StyleToolTip(ToolTip toolTip)
        {
            if (toolTip == null || toolTip.OwnerDraw)
                return;

            toolTip.OwnerDraw = true;
            toolTip.Popup += (s, e) =>
            {
                Size text = TextRenderer.MeasureText(toolTip.GetToolTip(e.AssociatedControl) ?? "", UiFont,
                    new Size(360, int.MaxValue), TextFormatFlags.WordBreak);
                e.ToolTipSize = new Size(Math.Min(text.Width, 360) + 18, text.Height + 12);
            };
            toolTip.Draw += (s, e) =>
            {
                Palette p = current;
                using (var back = new SolidBrush(p.IsDark ? p.Surface2 : p.Text))
                    e.Graphics.FillRectangle(back, e.Bounds);
                using (var pen = new Pen(p.IsDark ? p.Border : p.Text))
                    e.Graphics.DrawRectangle(pen, new Rectangle(0, 0, e.Bounds.Width - 1, e.Bounds.Height - 1));

                Rectangle textBounds = new Rectangle(9, 6, e.Bounds.Width - 18, e.Bounds.Height - 12);
                TextRenderer.DrawText(e.Graphics, e.ToolTipText, UiFont, textBounds, p.IsDark ? p.Text : p.Bg,
                    TextFormatFlags.WordBreak | TextFormatFlags.Left | TextFormatFlags.Top);
            };
        }

        // ---- window frame (Windows 10/11 DWM) ----

        private const int DWMWA_USE_IMMERSIVE_DARK_MODE = 20;
        private const int DWMWA_WINDOW_CORNER_PREFERENCE = 33;
        private const int DWMWA_BORDER_COLOR = 34;
        private const int DWMWCP_ROUND = 2;

        [DllImport("dwmapi.dll")]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attribute, ref int value, int size);

        /// <summary>Rounded corners, border color and dark mode of the window frame. Ignored where not supported.</summary>
        public static void ApplyWindowFrame(IntPtr handle)
        {
            try
            {
                int dark = current.IsDark ? 1 : 0;
                DwmSetWindowAttribute(handle, DWMWA_USE_IMMERSIVE_DARK_MODE, ref dark, sizeof(int));

                int corners = DWMWCP_ROUND;
                DwmSetWindowAttribute(handle, DWMWA_WINDOW_CORNER_PREFERENCE, ref corners, sizeof(int));

                int border = ColorTranslator.ToWin32(current.Border);
                DwmSetWindowAttribute(handle, DWMWA_BORDER_COLOR, ref border, sizeof(int));
            }
            catch (DllNotFoundException)
            {
            }
            catch (EntryPointNotFoundException)
            {
            }
        }
    }
}
