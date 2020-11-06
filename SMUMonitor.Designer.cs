namespace ZenStatesDebugTool
{
    partial class SMUMonitor
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SMUMonitor));
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.Cmd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Arg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Rsp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonStartStop = new System.Windows.Forms.Button();
            this.ButtonClear = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labelCmdAddr = new System.Windows.Forms.Label();
            this.labelRspAddr = new System.Windows.Forms.Label();
            this.labelArgAddr = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dataGridView2.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView2.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleVertical;
            this.dataGridView2.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Cmd,
            this.Arg,
            this.Rsp});
            this.tableLayoutPanel1.SetColumnSpan(this.dataGridView2, 4);
            this.dataGridView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView2.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView2.GridColor = System.Drawing.SystemColors.ControlLight;
            this.dataGridView2.Location = new System.Drawing.Point(3, 65);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dataGridView2.RowHeadersVisible = false;
            this.dataGridView2.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dataGridView2.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView2.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView2.ShowCellToolTips = false;
            this.dataGridView2.ShowEditingIcon = false;
            this.dataGridView2.ShowRowErrors = false;
            this.dataGridView2.Size = new System.Drawing.Size(336, 417);
            this.dataGridView2.TabIndex = 0;
            // 
            // Cmd
            // 
            this.Cmd.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Cmd.DataPropertyName = "Cmd";
            this.Cmd.HeaderText = "Command";
            this.Cmd.MinimumWidth = 50;
            this.Cmd.Name = "Cmd";
            this.Cmd.ReadOnly = true;
            this.Cmd.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Cmd.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Cmd.Width = 60;
            // 
            // Arg
            // 
            this.Arg.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Arg.DataPropertyName = "Arg";
            this.Arg.HeaderText = "Argument";
            this.Arg.MinimumWidth = 90;
            this.Arg.Name = "Arg";
            this.Arg.ReadOnly = true;
            this.Arg.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Arg.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Arg.Width = 90;
            // 
            // Rsp
            // 
            this.Rsp.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Rsp.DataPropertyName = "Rsp";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Rsp.DefaultCellStyle = dataGridViewCellStyle2;
            this.Rsp.HeaderText = "Response";
            this.Rsp.MinimumWidth = 90;
            this.Rsp.Name = "Rsp";
            this.Rsp.ReadOnly = true;
            this.Rsp.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Rsp.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.dataGridView2, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.buttonStartStop, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.ButtonClear, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.labelCmdAddr, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.labelRspAddr, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.labelArgAddr, 1, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(342, 514);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // buttonStartStop
            // 
            this.buttonStartStop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonStartStop.Location = new System.Drawing.Point(3, 488);
            this.buttonStartStop.Name = "buttonStartStop";
            this.buttonStartStop.Size = new System.Drawing.Size(84, 23);
            this.buttonStartStop.TabIndex = 1;
            this.buttonStartStop.Text = "Stop";
            this.buttonStartStop.UseVisualStyleBackColor = true;
            this.buttonStartStop.Click += new System.EventHandler(this.ButtonApply_Click);
            // 
            // ButtonClear
            // 
            this.ButtonClear.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ButtonClear.Location = new System.Drawing.Point(93, 488);
            this.ButtonClear.Name = "ButtonClear";
            this.ButtonClear.Size = new System.Drawing.Size(84, 23);
            this.ButtonClear.TabIndex = 2;
            this.ButtonClear.Text = "Clear";
            this.ButtonClear.UseVisualStyleBackColor = true;
            this.ButtonClear.Click += new System.EventHandler(this.ButtonClear_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 5);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.label1.Size = new System.Drawing.Size(84, 19);
            this.label1.TabIndex = 3;
            this.label1.Text = "CMD Address";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(3, 24);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.label2.Size = new System.Drawing.Size(84, 19);
            this.label2.TabIndex = 4;
            this.label2.Text = "RSP Address";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(3, 43);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.label3.Size = new System.Drawing.Size(84, 19);
            this.label3.TabIndex = 5;
            this.label3.Text = "ARG Address";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelCmdAddr
            // 
            this.labelCmdAddr.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.labelCmdAddr, 3);
            this.labelCmdAddr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelCmdAddr.Location = new System.Drawing.Point(93, 5);
            this.labelCmdAddr.Name = "labelCmdAddr";
            this.labelCmdAddr.Padding = new System.Windows.Forms.Padding(0, 3, 5, 3);
            this.labelCmdAddr.Size = new System.Drawing.Size(246, 19);
            this.labelCmdAddr.TabIndex = 6;
            this.labelCmdAddr.Text = "-";
            this.labelCmdAddr.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelRspAddr
            // 
            this.labelRspAddr.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.labelRspAddr, 3);
            this.labelRspAddr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelRspAddr.Location = new System.Drawing.Point(93, 24);
            this.labelRspAddr.Name = "labelRspAddr";
            this.labelRspAddr.Padding = new System.Windows.Forms.Padding(0, 3, 5, 3);
            this.labelRspAddr.Size = new System.Drawing.Size(246, 19);
            this.labelRspAddr.TabIndex = 7;
            this.labelRspAddr.Text = "-";
            this.labelRspAddr.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelArgAddr
            // 
            this.labelArgAddr.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.labelArgAddr, 3);
            this.labelArgAddr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelArgAddr.Location = new System.Drawing.Point(93, 43);
            this.labelArgAddr.Name = "labelArgAddr";
            this.labelArgAddr.Padding = new System.Windows.Forms.Padding(0, 3, 5, 3);
            this.labelArgAddr.Size = new System.Drawing.Size(246, 19);
            this.labelArgAddr.TabIndex = 8;
            this.labelArgAddr.Text = "-";
            this.labelArgAddr.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // SMUMonitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(342, 514);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SMUMonitor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SMU Monitor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SMUMonitor_FormClosing);
            this.Shown += new System.EventHandler(this.SMUMonitor_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button buttonStartStop;
        private System.Windows.Forms.Button ButtonClear;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cmd;
        private System.Windows.Forms.DataGridViewTextBoxColumn Arg;
        private System.Windows.Forms.DataGridViewTextBoxColumn Rsp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelCmdAddr;
        private System.Windows.Forms.Label labelRspAddr;
        private System.Windows.Forms.Label labelArgAddr;
    }
}