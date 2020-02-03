namespace ZenStatesDebugTool
{
    partial class ResultForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBoxFormResult = new System.Windows.Forms.TextBox();
            this.buttonSaveResult = new System.Windows.Forms.Button();
            this.buttonSaveAs = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxFormResult
            // 
            this.textBoxFormResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxFormResult.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxFormResult.Location = new System.Drawing.Point(13, 13);
            this.textBoxFormResult.Multiline = true;
            this.textBoxFormResult.Name = "textBoxFormResult";
            this.textBoxFormResult.ReadOnly = true;
            this.textBoxFormResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxFormResult.Size = new System.Drawing.Size(241, 246);
            this.textBoxFormResult.TabIndex = 0;
            this.textBoxFormResult.TabStop = false;
            // 
            // buttonSaveResult
            // 
            this.buttonSaveResult.Location = new System.Drawing.Point(13, 265);
            this.buttonSaveResult.Name = "buttonSaveResult";
            this.buttonSaveResult.Size = new System.Drawing.Size(75, 23);
            this.buttonSaveResult.TabIndex = 1;
            this.buttonSaveResult.Text = "Save";
            this.buttonSaveResult.UseVisualStyleBackColor = true;
            this.buttonSaveResult.Click += new System.EventHandler(this.buttonSaveResult_Click);
            // 
            // buttonSaveAs
            // 
            this.buttonSaveAs.Location = new System.Drawing.Point(95, 264);
            this.buttonSaveAs.Name = "buttonSaveAs";
            this.buttonSaveAs.Size = new System.Drawing.Size(75, 23);
            this.buttonSaveAs.TabIndex = 2;
            this.buttonSaveAs.Text = "Save As...";
            this.buttonSaveAs.UseVisualStyleBackColor = true;
            this.buttonSaveAs.Click += new System.EventHandler(this.buttonSaveAs_Click);
            // 
            // ResultForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(266, 295);
            this.Controls.Add(this.buttonSaveAs);
            this.Controls.Add(this.buttonSaveResult);
            this.Controls.Add(this.textBoxFormResult);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "ResultForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox textBoxFormResult;
        private System.Windows.Forms.Button buttonSaveResult;
        private System.Windows.Forms.Button buttonSaveAs;
    }
}