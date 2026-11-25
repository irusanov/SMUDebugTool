using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ZenStatesDebugTool.Theming
{
    /// <summary>
    /// Themed replacement for MessageBox (same overloads and results), drawn like the XrpQuad dialog: an icon badge,
    /// title, message and the buttons at the bottom right with the main action last.
    /// </summary>
    public static class ThemedMessageBox
    {
        public static DialogResult Show(string text) =>
            Show(text, "", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, null);

        public static DialogResult Show(string text, string caption) =>
            Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, null);

        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons) =>
            Show(text, caption, buttons, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, null);

        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon) =>
            Show(text, caption, buttons, icon, MessageBoxDefaultButton.Button1, null);

        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton) =>
            Show(text, caption, buttons, icon, defaultButton, null);

        public static DialogResult Show(IWin32Window owner, string text, string caption = "", MessageBoxButtons buttons = MessageBoxButtons.OK,
            MessageBoxIcon icon = MessageBoxIcon.None, MessageBoxDefaultButton defaultButton = MessageBoxDefaultButton.Button1) =>
            Show(text, caption, buttons, icon, defaultButton, owner);

        private static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon,
            MessageBoxDefaultButton defaultButton, IWin32Window owner)
        {
            Form ownerForm = owner as Form ?? Form.ActiveForm;
            if (ownerForm != null && ownerForm.InvokeRequired)
                return (DialogResult)ownerForm.Invoke(new Func<DialogResult>(() => Show(text, caption, buttons, icon, defaultButton, ownerForm)));

            using (var dialog = new DialogForm(text, caption, buttons, icon, defaultButton))
            {
                if (ownerForm != null && ownerForm.Visible && ownerForm.WindowState != FormWindowState.Minimized)
                    return dialog.ShowDialog(ownerForm);

                dialog.StartPosition = FormStartPosition.CenterScreen;
                dialog.TopMost = true;
                return dialog.ShowDialog();
            }
        }

        private enum Kind { Info, Question, Warning, Error }

        private sealed class DialogForm : ThemedForm
        {
            private readonly Kind kind;
            private readonly string caption;
            private readonly string message;
            private readonly Panel badge;
            private readonly Button defaultControl;

            public DialogForm(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton)
            {
                this.caption = string.IsNullOrEmpty(caption) ? Application.ProductName : caption;
                message = text ?? "";
                kind = KindOf(icon);

                ShowTitleBar = false;
                ShowThemeToggle = false;
                DragAnywhere = true;
                FormBorderStyle = FormBorderStyle.FixedDialog;
                MinimizeBox = false;
                MaximizeBox = false;
                ShowInTaskbar = false;
                ShowIcon = false;
                StartPosition = FormStartPosition.CenterParent;
                KeyPreview = true;
                Text = this.caption;
                AutoScaleMode = AutoScaleMode.None;
                Theme.SetRole(this, ThemeRole.Card);

                int px22 = ControlStyler.Px(22);
                int messageWidth = ControlStyler.Px(400);
                int badgeSize = ControlStyler.Px(36);
                int gap = ControlStyler.Px(14);

                badge = new Panel { Size = new Size(badgeSize, badgeSize), Margin = new Padding(0) };
                badge.Paint += PaintBadge;
                Theme.SetRole(badge, ThemeRole.Plain);

                var title = new Label
                {
                    AutoSize = true,
                    MaximumSize = new Size(messageWidth, 0),
                    Font = new Font("Segoe UI Semibold", 11.25f),
                    Text = this.caption,
                    Margin = new Padding(gap, ControlStyler.Px(7), 0, 0),
                    UseMnemonic = false,
                };

                var body = new Label
                {
                    AutoSize = true,
                    MaximumSize = new Size(messageWidth, 0),
                    Font = new Font("Segoe UI", 9.75f),
                    Text = message,
                    Margin = new Padding(gap, ControlStyler.Px(8), 0, 0),
                    UseMnemonic = false,
                };
                Theme.SetRole(body, ThemeRole.Muted);

                // Very long messages scroll instead of growing past the screen
                Control messageControl = body;
                int maxBody = (int)(Screen.PrimaryScreen.WorkingArea.Height * 0.55);
                if (body.PreferredSize.Height > maxBody)
                {
                    messageControl = new TextBox
                    {
                        Multiline = true,
                        ReadOnly = true,
                        BorderStyle = BorderStyle.None,
                        ScrollBars = ScrollBars.Vertical,
                        Text = message.Replace("\r\n", "\n").Replace("\n", "\r\n"),
                        Font = body.Font,
                        Size = new Size(messageWidth + ControlStyler.Px(20), maxBody),
                        Margin = body.Margin,
                        TabStop = false,
                    };
                }

                var buttonRow = new FlowLayoutPanel
                {
                    AutoSize = true,
                    AutoSizeMode = AutoSizeMode.GrowAndShrink,
                    FlowDirection = FlowDirection.RightToLeft,
                    WrapContents = false,
                    Anchor = AnchorStyles.Right,
                    Margin = new Padding(0, px22, 0, 0),
                };

                var layout = new TableLayoutPanel
                {
                    AutoSize = true,
                    AutoSizeMode = AutoSizeMode.GrowAndShrink,
                    ColumnCount = 2,
                    RowCount = 3,
                    Location = new Point(px22, px22 - ControlStyler.Px(2)),
                    Margin = new Padding(0),
                };
                layout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
                layout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
                layout.Controls.Add(badge, 0, 0);
                layout.Controls.Add(title, 1, 0);
                layout.Controls.Add(messageControl, 1, 1);
                layout.Controls.Add(buttonRow, 0, 2);
                layout.SetColumnSpan(buttonRow, 2);

                // Buttons in Windows order (Button1 first); shown right to left so the main action is last
                List<Tuple<string, DialogResult>> choices = Choices(buttons);
                Button[] created = new Button[choices.Count];
                for (int i = 0; i < choices.Count; i++)
                {
                    var button = new Button
                    {
                        Text = choices[i].Item1,
                        DialogResult = choices[i].Item2,
                        MinimumSize = new Size(ControlStyler.Px(90), ControlStyler.Px(30)),
                        AutoSize = true,
                        Padding = new Padding(ControlStyler.Px(10), 0, ControlStyler.Px(10), 0),
                        Margin = new Padding(ControlStyler.Px(6), 0, 0, 0),
                        Font = new Font("Segoe UI", 9f),
                    };
                    created[i] = button;
                    buttonRow.Controls.Add(button);
                }

                // A warning or error with a choice: the action is red and the safe choice is the default
                bool danger = (kind == Kind.Warning || kind == Kind.Error) && choices.Count > 1;
                Theme.SetRole(created[0], danger ? ThemeRole.Danger : ThemeRole.Primary);

                int defaultIndex = defaultButton == MessageBoxDefaultButton.Button3 ? 2 : defaultButton == MessageBoxDefaultButton.Button2 ? 1 : 0;
                if (danger && defaultButton == MessageBoxDefaultButton.Button1)
                    defaultIndex = choices.Count - 1;
                defaultIndex = Math.Min(defaultIndex, choices.Count - 1);
                AcceptButton = created[defaultIndex];
                defaultControl = created[defaultIndex];

                Button cancel = Array.Find(created, b => b.DialogResult == DialogResult.Cancel)
                    ?? Array.Find(created, b => b.DialogResult == DialogResult.No)
                    ?? (created.Length == 1 ? created[0] : null);
                CancelButton = cancel;

                Controls.Add(layout);

                Size preferred = layout.GetPreferredSize(Size.Empty);
                ClientSize = new Size(preferred.Width + 2 * px22, preferred.Height + 2 * px22 - ControlStyler.Px(4));
            }

            protected override void OnShown(EventArgs e)
            {
                base.OnShown(e);
                defaultControl.Focus();
            }

            private static Kind KindOf(MessageBoxIcon icon)
            {
                switch (icon)
                {
                    case MessageBoxIcon.Error: return Kind.Error;           // also Hand, Stop
                    case MessageBoxIcon.Warning: return Kind.Warning;       // also Exclamation
                    case MessageBoxIcon.Question: return Kind.Question;
                    default: return Kind.Info;                              // Information, Asterisk, None
                }
            }

            private static List<Tuple<string, DialogResult>> Choices(MessageBoxButtons buttons)
            {
                var list = new List<Tuple<string, DialogResult>>();
                Action<string, DialogResult> add = (text, result) => list.Add(Tuple.Create(text, result));

                switch (buttons)
                {
                    case MessageBoxButtons.OKCancel: add("OK", DialogResult.OK); add("Cancel", DialogResult.Cancel); break;
                    case MessageBoxButtons.YesNo: add("Yes", DialogResult.Yes); add("No", DialogResult.No); break;
                    case MessageBoxButtons.YesNoCancel: add("Yes", DialogResult.Yes); add("No", DialogResult.No); add("Cancel", DialogResult.Cancel); break;
                    case MessageBoxButtons.RetryCancel: add("Retry", DialogResult.Retry); add("Cancel", DialogResult.Cancel); break;
                    case MessageBoxButtons.AbortRetryIgnore: add("Abort", DialogResult.Abort); add("Retry", DialogResult.Retry); add("Ignore", DialogResult.Ignore); break;
                    default: add("OK", DialogResult.OK); break;
                }

                return list;
            }

            private void PaintBadge(object sender, PaintEventArgs e)
            {
                Palette p = Theme.Current;
                Color color = kind == Kind.Error ? p.Danger : kind == Kind.Warning ? p.Warning : p.AccentFg;
                Color back = ControlStyler.EffectiveBack(this);

                Graphics g = e.Graphics;
                using (var brush = new SolidBrush(back))
                    g.FillRectangle(brush, badge.ClientRectangle);

                g.SmoothingMode = SmoothingMode.AntiAlias;
                using (var brush = new SolidBrush(Palette.Blend(back, color, 0.18f)))
                    g.FillEllipse(brush, 0, 0, badge.Width - 1, badge.Height - 1);

                LucideIcon icon = kind == Kind.Error ? Icons.CircleAlert
                    : kind == Kind.Warning ? Icons.TriangleAlert
                    : kind == Kind.Question ? Icons.Question
                    : Icons.Info;
                icon.Draw(g, badge.ClientRectangle, color, ControlStyler.Px(18));
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);
                // Card look: the window itself is the dialog surface
                using (var pen = new Pen(Theme.Current.Border))
                    e.Graphics.DrawRectangle(pen, 0, 0, ClientSize.Width - 1, ClientSize.Height - 1);
            }

            protected override void OnThemeApplied(Palette previous)
            {
                badge.Invalidate();
            }

            // Ctrl+C copies the message, like the Windows message box
            protected override void OnKeyDown(KeyEventArgs e)
            {
                base.OnKeyDown(e);
                if (e.Control && e.KeyCode == Keys.C)
                {
                    try { Clipboard.SetText(caption + Environment.NewLine + Environment.NewLine + message); }
                    catch { }
                    e.Handled = true;
                }
            }
        }
    }
}
