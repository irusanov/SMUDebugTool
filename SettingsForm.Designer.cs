using System.Windows.Forms;

namespace ZenStatesDebugTool
{
    partial class SettingsForm
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
                ols.Dispose();
                hMutexPci.Dispose();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.labelCMDAddress = new System.Windows.Forms.Label();
            this.labelRSP = new System.Windows.Forms.Label();
            this.textBoxCMDAddress = new System.Windows.Forms.TextBox();
            this.textBoxRSPAddress = new System.Windows.Forms.TextBox();
            this.labelARG0 = new System.Windows.Forms.Label();
            this.textBoxARG0 = new System.Windows.Forms.TextBox();
            this.labelCMD = new System.Windows.Forms.Label();
            this.textBoxCMD = new System.Windows.Forms.TextBox();
            this.labelARGAddress = new System.Windows.Forms.Label();
            this.textBoxARGAddress = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.labelStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonApply = new System.Windows.Forms.Button();
            this.buttonDefaults = new System.Windows.Forms.Button();
            this.buttonProbe = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageSmu = new System.Windows.Forms.TabPage();
            this.tabPagePci = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonPciWrite = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxPciAddress = new System.Windows.Forms.TextBox();
            this.buttonPciRead = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxPciValue = new System.Windows.Forms.TextBox();
            this.tabPagePstates = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.btnPstateWrite = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btnPstateRead = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.pstateDid = new System.Windows.Forms.TextBox();
            this.pstateFid = new System.Windows.Forms.TextBox();
            this.pstateIdBox = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.pstateFrequency = new System.Windows.Forms.TextBox();
            this.tabPageInfo = new System.Windows.Forms.TabPage();
            this.buttonExport = new System.Windows.Forms.Button();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.smuInfoLabel = new System.Windows.Forms.Label();
            this.biosInfoLabel = new System.Windows.Forms.Label();
            this.mbVendorInfoLabel = new System.Windows.Forms.Label();
            this.labelInfoCpu = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cpuInfoLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.mbModelInfoLabel = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cpuIdLabel = new System.Windows.Forms.Label();
            this.textBoxResult = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPageSmu.SuspendLayout();
            this.tabPagePci.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tabPagePstates.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tabPageInfo.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.labelCMDAddress, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelRSP, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.textBoxCMDAddress, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.textBoxRSPAddress, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.labelARG0, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.textBoxARG0, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.labelCMD, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.textBoxCMD, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.labelARGAddress, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.textBoxARGAddress, 1, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(0, 5, 5, 5);
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(186, 150);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // labelCMDAddress
            // 
            this.labelCMDAddress.AutoSize = true;
            this.labelCMDAddress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelCMDAddress.Location = new System.Drawing.Point(3, 5);
            this.labelCMDAddress.MinimumSize = new System.Drawing.Size(77, 27);
            this.labelCMDAddress.Name = "labelCMDAddress";
            this.labelCMDAddress.Padding = new System.Windows.Forms.Padding(0, 5, 5, 5);
            this.labelCMDAddress.Size = new System.Drawing.Size(77, 28);
            this.labelCMDAddress.TabIndex = 0;
            this.labelCMDAddress.Text = "CMD Address";
            this.labelCMDAddress.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelRSP
            // 
            this.labelRSP.AutoSize = true;
            this.labelRSP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelRSP.Location = new System.Drawing.Point(3, 33);
            this.labelRSP.MinimumSize = new System.Drawing.Size(77, 27);
            this.labelRSP.Name = "labelRSP";
            this.labelRSP.Padding = new System.Windows.Forms.Padding(0, 5, 5, 5);
            this.labelRSP.Size = new System.Drawing.Size(77, 28);
            this.labelRSP.TabIndex = 1;
            this.labelRSP.Text = "RSP Address";
            this.labelRSP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxCMDAddress
            // 
            this.textBoxCMDAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxCMDAddress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxCMDAddress.HideSelection = false;
            this.textBoxCMDAddress.Location = new System.Drawing.Point(87, 9);
            this.textBoxCMDAddress.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxCMDAddress.Name = "textBoxCMDAddress";
            this.textBoxCMDAddress.Size = new System.Drawing.Size(90, 20);
            this.textBoxCMDAddress.TabIndex = 2;
            // 
            // textBoxRSPAddress
            // 
            this.textBoxRSPAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxRSPAddress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxRSPAddress.HideSelection = false;
            this.textBoxRSPAddress.Location = new System.Drawing.Point(87, 37);
            this.textBoxRSPAddress.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxRSPAddress.Name = "textBoxRSPAddress";
            this.textBoxRSPAddress.Size = new System.Drawing.Size(90, 20);
            this.textBoxRSPAddress.TabIndex = 3;
            // 
            // labelARG0
            // 
            this.labelARG0.AutoSize = true;
            this.labelARG0.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelARG0.Location = new System.Drawing.Point(3, 117);
            this.labelARG0.MinimumSize = new System.Drawing.Size(77, 27);
            this.labelARG0.Name = "labelARG0";
            this.labelARG0.Padding = new System.Windows.Forms.Padding(0, 5, 5, 5);
            this.labelARG0.Size = new System.Drawing.Size(77, 28);
            this.labelARG0.TabIndex = 4;
            this.labelARG0.Text = "Argument";
            this.labelARG0.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxARG0
            // 
            this.textBoxARG0.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxARG0.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxARG0.HideSelection = false;
            this.textBoxARG0.Location = new System.Drawing.Point(87, 121);
            this.textBoxARG0.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxARG0.Name = "textBoxARG0";
            this.textBoxARG0.Size = new System.Drawing.Size(90, 20);
            this.textBoxARG0.TabIndex = 6;
            // 
            // labelCMD
            // 
            this.labelCMD.AutoSize = true;
            this.labelCMD.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelCMD.Location = new System.Drawing.Point(3, 89);
            this.labelCMD.MinimumSize = new System.Drawing.Size(77, 27);
            this.labelCMD.Name = "labelCMD";
            this.labelCMD.Padding = new System.Windows.Forms.Padding(0, 5, 5, 5);
            this.labelCMD.Size = new System.Drawing.Size(77, 28);
            this.labelCMD.TabIndex = 8;
            this.labelCMD.Text = "Command ID";
            this.labelCMD.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxCMD
            // 
            this.textBoxCMD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxCMD.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxCMD.HideSelection = false;
            this.textBoxCMD.Location = new System.Drawing.Point(87, 93);
            this.textBoxCMD.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxCMD.Name = "textBoxCMD";
            this.textBoxCMD.Size = new System.Drawing.Size(90, 20);
            this.textBoxCMD.TabIndex = 9;
            // 
            // labelARGAddress
            // 
            this.labelARGAddress.AutoSize = true;
            this.labelARGAddress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelARGAddress.Location = new System.Drawing.Point(3, 61);
            this.labelARGAddress.MinimumSize = new System.Drawing.Size(77, 27);
            this.labelARGAddress.Name = "labelARGAddress";
            this.labelARGAddress.Padding = new System.Windows.Forms.Padding(0, 5, 5, 5);
            this.labelARGAddress.Size = new System.Drawing.Size(77, 28);
            this.labelARGAddress.TabIndex = 10;
            this.labelARGAddress.Text = "ARG Address";
            this.labelARGAddress.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxARGAddress
            // 
            this.textBoxARGAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxARGAddress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxARGAddress.Location = new System.Drawing.Point(87, 65);
            this.textBoxARGAddress.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxARGAddress.Name = "textBoxARGAddress";
            this.textBoxARGAddress.Size = new System.Drawing.Size(90, 20);
            this.textBoxARGAddress.TabIndex = 11;
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.labelStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 224);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(469, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // labelStatus
            // 
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.labelStatus.Size = new System.Drawing.Size(22, 17);
            this.labelStatus.Text = "-";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.buttonApply, 3, 2);
            this.tableLayoutPanel2.Controls.Add(this.buttonDefaults, 3, 1);
            this.tableLayoutPanel2.Controls.Add(this.buttonProbe, 3, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(189, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.Padding = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(91, 182);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // buttonApply
            // 
            this.buttonApply.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonApply.Location = new System.Drawing.Point(8, 66);
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.Size = new System.Drawing.Size(75, 23);
            this.buttonApply.TabIndex = 0;
            this.buttonApply.Text = "Send";
            this.buttonApply.UseVisualStyleBackColor = true;
            this.buttonApply.Click += new System.EventHandler(this.ButtonApply_Click);
            // 
            // buttonDefaults
            // 
            this.buttonDefaults.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonDefaults.Location = new System.Drawing.Point(8, 37);
            this.buttonDefaults.Name = "buttonDefaults";
            this.buttonDefaults.Size = new System.Drawing.Size(75, 23);
            this.buttonDefaults.TabIndex = 1;
            this.buttonDefaults.Text = "Reset";
            this.buttonDefaults.UseVisualStyleBackColor = true;
            this.buttonDefaults.Click += new System.EventHandler(this.ButtonDefaults_Click);
            // 
            // buttonProbe
            // 
            this.buttonProbe.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonProbe.Location = new System.Drawing.Point(8, 8);
            this.buttonProbe.Name = "buttonProbe";
            this.buttonProbe.Size = new System.Drawing.Size(75, 23);
            this.buttonProbe.TabIndex = 2;
            this.buttonProbe.Text = "Scan";
            this.buttonProbe.UseVisualStyleBackColor = true;
            this.buttonProbe.Click += new System.EventHandler(this.ButtonScan_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageSmu);
            this.tabControl1.Controls.Add(this.tabPagePci);
            this.tabControl1.Controls.Add(this.tabPagePstates);
            this.tabControl1.Controls.Add(this.tabPageInfo);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(5, 5);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(291, 214);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
            this.tabControl1.TabIndex = 2;
            this.tabControl1.Selected += new System.Windows.Forms.TabControlEventHandler(this.TabControl1_Selected);
            // 
            // tabPageSmu
            // 
            this.tabPageSmu.Controls.Add(this.tableLayoutPanel1);
            this.tabPageSmu.Controls.Add(this.tableLayoutPanel2);
            this.tabPageSmu.Location = new System.Drawing.Point(4, 22);
            this.tabPageSmu.Name = "tabPageSmu";
            this.tabPageSmu.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tabPageSmu.Size = new System.Drawing.Size(283, 188);
            this.tabPageSmu.TabIndex = 0;
            this.tabPageSmu.Text = "SMU";
            this.tabPageSmu.UseVisualStyleBackColor = true;
            // 
            // tabPagePci
            // 
            this.tabPagePci.Controls.Add(this.tableLayoutPanel4);
            this.tabPagePci.Location = new System.Drawing.Point(4, 22);
            this.tabPagePci.Name = "tabPagePci";
            this.tabPagePci.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tabPagePci.Size = new System.Drawing.Size(282, 193);
            this.tabPagePci.TabIndex = 1;
            this.tabPagePci.Text = "PCI";
            this.tabPagePci.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.AutoSize = true;
            this.tableLayoutPanel4.ColumnCount = 3;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.Controls.Add(this.buttonPciWrite, 2, 1);
            this.tableLayoutPanel4.Controls.Add(this.label5, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.textBoxPciAddress, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.buttonPciRead, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.label7, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.textBoxPciValue, 1, 1);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.Padding = new System.Windows.Forms.Padding(0, 5, 5, 5);
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.Size = new System.Drawing.Size(276, 72);
            this.tableLayoutPanel4.TabIndex = 0;
            // 
            // buttonPciWrite
            // 
            this.buttonPciWrite.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonPciWrite.Location = new System.Drawing.Point(193, 39);
            this.buttonPciWrite.Name = "buttonPciWrite";
            this.buttonPciWrite.Size = new System.Drawing.Size(75, 25);
            this.buttonPciWrite.TabIndex = 5;
            this.buttonPciWrite.Text = "Write";
            this.buttonPciWrite.UseVisualStyleBackColor = true;
            this.buttonPciWrite.Click += new System.EventHandler(this.ButtonPciWrite_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(3, 5);
            this.label5.MinimumSize = new System.Drawing.Size(77, 27);
            this.label5.Name = "label5";
            this.label5.Padding = new System.Windows.Forms.Padding(0, 5, 5, 5);
            this.label5.Size = new System.Drawing.Size(77, 31);
            this.label5.TabIndex = 0;
            this.label5.Text = "PCI Register";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxPciAddress
            // 
            this.textBoxPciAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxPciAddress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxPciAddress.Location = new System.Drawing.Point(87, 9);
            this.textBoxPciAddress.Margin = new System.Windows.Forms.Padding(4, 4, 14, 4);
            this.textBoxPciAddress.Name = "textBoxPciAddress";
            this.textBoxPciAddress.Size = new System.Drawing.Size(89, 20);
            this.textBoxPciAddress.TabIndex = 1;
            this.textBoxPciAddress.Text = "0x0";
            this.textBoxPciAddress.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxPciAddress_KeyDown);
            // 
            // buttonPciRead
            // 
            this.buttonPciRead.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonPciRead.Location = new System.Drawing.Point(193, 8);
            this.buttonPciRead.Name = "buttonPciRead";
            this.buttonPciRead.Size = new System.Drawing.Size(75, 25);
            this.buttonPciRead.TabIndex = 2;
            this.buttonPciRead.Text = "Read";
            this.buttonPciRead.UseVisualStyleBackColor = true;
            this.buttonPciRead.Click += new System.EventHandler(this.ButtonPciRead_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Location = new System.Drawing.Point(3, 36);
            this.label7.MinimumSize = new System.Drawing.Size(77, 27);
            this.label7.Name = "label7";
            this.label7.Padding = new System.Windows.Forms.Padding(0, 5, 5, 5);
            this.label7.Size = new System.Drawing.Size(77, 31);
            this.label7.TabIndex = 3;
            this.label7.Text = "Value";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxPciValue
            // 
            this.textBoxPciValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxPciValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxPciValue.Location = new System.Drawing.Point(87, 40);
            this.textBoxPciValue.Margin = new System.Windows.Forms.Padding(4, 4, 14, 4);
            this.textBoxPciValue.Name = "textBoxPciValue";
            this.textBoxPciValue.Size = new System.Drawing.Size(89, 20);
            this.textBoxPciValue.TabIndex = 4;
            this.textBoxPciValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxPciValue_KeyDown);
            // 
            // tabPagePstates
            // 
            this.tabPagePstates.Controls.Add(this.tableLayoutPanel5);
            this.tabPagePstates.Location = new System.Drawing.Point(4, 22);
            this.tabPagePstates.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.tabPagePstates.Name = "tabPagePstates";
            this.tabPagePstates.Padding = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.tabPagePstates.Size = new System.Drawing.Size(283, 188);
            this.tabPagePstates.TabIndex = 3;
            this.tabPagePstates.Text = "PStates";
            this.tabPagePstates.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.AutoSize = true;
            this.tableLayoutPanel5.ColumnCount = 3;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Controls.Add(this.btnPstateWrite, 2, 1);
            this.tableLayoutPanel5.Controls.Add(this.label10, 0, 2);
            this.tableLayoutPanel5.Controls.Add(this.label8, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.btnPstateRead, 2, 0);
            this.tableLayoutPanel5.Controls.Add(this.label9, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.pstateDid, 1, 1);
            this.tableLayoutPanel5.Controls.Add(this.pstateFid, 1, 2);
            this.tableLayoutPanel5.Controls.Add(this.pstateIdBox, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.label11, 0, 3);
            this.tableLayoutPanel5.Controls.Add(this.pstateFrequency, 1, 3);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(2, 1);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.Padding = new System.Windows.Forms.Padding(0, 5, 5, 5);
            this.tableLayoutPanel5.RowCount = 4;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.Size = new System.Drawing.Size(279, 128);
            this.tableLayoutPanel5.TabIndex = 1;
            // 
            // btnPstateWrite
            // 
            this.btnPstateWrite.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPstateWrite.Enabled = false;
            this.btnPstateWrite.Location = new System.Drawing.Point(196, 39);
            this.btnPstateWrite.Name = "btnPstateWrite";
            this.btnPstateWrite.Size = new System.Drawing.Size(75, 25);
            this.btnPstateWrite.TabIndex = 5;
            this.btnPstateWrite.Text = "Write";
            this.btnPstateWrite.UseVisualStyleBackColor = true;
            this.btnPstateWrite.Click += new System.EventHandler(this.btnPstateWrite_Click);
            // 
            // label10
            // 
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Location = new System.Drawing.Point(2, 67);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(79, 28);
            this.label10.TabIndex = 6;
            this.label10.Text = "FID";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Location = new System.Drawing.Point(3, 5);
            this.label8.MinimumSize = new System.Drawing.Size(77, 27);
            this.label8.Name = "label8";
            this.label8.Padding = new System.Windows.Forms.Padding(0, 5, 5, 5);
            this.label8.Size = new System.Drawing.Size(77, 31);
            this.label8.TabIndex = 0;
            this.label8.Text = "PState ID";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnPstateRead
            // 
            this.btnPstateRead.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPstateRead.Location = new System.Drawing.Point(196, 8);
            this.btnPstateRead.Name = "btnPstateRead";
            this.btnPstateRead.Size = new System.Drawing.Size(75, 25);
            this.btnPstateRead.TabIndex = 2;
            this.btnPstateRead.Text = "Read";
            this.btnPstateRead.UseVisualStyleBackColor = true;
            this.btnPstateRead.Click += new System.EventHandler(this.btnPstateRead_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Location = new System.Drawing.Point(3, 36);
            this.label9.MinimumSize = new System.Drawing.Size(77, 27);
            this.label9.Name = "label9";
            this.label9.Padding = new System.Windows.Forms.Padding(0, 5, 5, 5);
            this.label9.Size = new System.Drawing.Size(77, 31);
            this.label9.TabIndex = 3;
            this.label9.Text = "DID";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pstateDid
            // 
            this.pstateDid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pstateDid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pstateDid.Location = new System.Drawing.Point(87, 40);
            this.pstateDid.Margin = new System.Windows.Forms.Padding(4, 4, 14, 4);
            this.pstateDid.Name = "pstateDid";
            this.pstateDid.ReadOnly = true;
            this.pstateDid.Size = new System.Drawing.Size(92, 20);
            this.pstateDid.TabIndex = 4;
            // 
            // pstateFid
            // 
            this.pstateFid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pstateFid.Location = new System.Drawing.Point(87, 71);
            this.pstateFid.Margin = new System.Windows.Forms.Padding(4, 4, 14, 4);
            this.pstateFid.Name = "pstateFid";
            this.pstateFid.ReadOnly = true;
            this.pstateFid.Size = new System.Drawing.Size(92, 20);
            this.pstateFid.TabIndex = 7;
            // 
            // pstateIdBox
            // 
            this.pstateIdBox.DisplayMember = "0";
            this.pstateIdBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pstateIdBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.pstateIdBox.IntegralHeight = false;
            this.pstateIdBox.Items.AddRange(new object[] {
            "0",
            "1",
            "2"});
            this.pstateIdBox.Location = new System.Drawing.Point(87, 9);
            this.pstateIdBox.Margin = new System.Windows.Forms.Padding(4, 4, 14, 4);
            this.pstateIdBox.Name = "pstateIdBox";
            this.pstateIdBox.Size = new System.Drawing.Size(92, 21);
            this.pstateIdBox.TabIndex = 8;
            // 
            // label11
            // 
            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.Location = new System.Drawing.Point(2, 95);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(79, 28);
            this.label11.TabIndex = 9;
            this.label11.Text = "Frequency";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pstateFrequency
            // 
            this.pstateFrequency.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pstateFrequency.Location = new System.Drawing.Point(87, 99);
            this.pstateFrequency.Margin = new System.Windows.Forms.Padding(4, 4, 14, 4);
            this.pstateFrequency.Name = "pstateFrequency";
            this.pstateFrequency.ReadOnly = true;
            this.pstateFrequency.Size = new System.Drawing.Size(92, 20);
            this.pstateFrequency.TabIndex = 10;
            // 
            // tabPageInfo
            // 
            this.tabPageInfo.Controls.Add(this.buttonExport);
            this.tabPageInfo.Controls.Add(this.tableLayoutPanel3);
            this.tabPageInfo.Location = new System.Drawing.Point(4, 22);
            this.tabPageInfo.Name = "tabPageInfo";
            this.tabPageInfo.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tabPageInfo.Size = new System.Drawing.Size(282, 193);
            this.tabPageInfo.TabIndex = 2;
            this.tabPageInfo.Text = "Info";
            this.tabPageInfo.UseVisualStyleBackColor = true;
            // 
            // buttonExport
            // 
            this.buttonExport.Location = new System.Drawing.Point(4, 157);
            this.buttonExport.Name = "buttonExport";
            this.buttonExport.Size = new System.Drawing.Size(75, 23);
            this.buttonExport.TabIndex = 1;
            this.buttonExport.Text = "Export";
            this.buttonExport.UseVisualStyleBackColor = true;
            this.buttonExport.Click += new System.EventHandler(this.buttonExport_Click);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.AutoSize = true;
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.smuInfoLabel, 1, 5);
            this.tableLayoutPanel3.Controls.Add(this.biosInfoLabel, 1, 4);
            this.tableLayoutPanel3.Controls.Add(this.mbVendorInfoLabel, 1, 2);
            this.tableLayoutPanel3.Controls.Add(this.labelInfoCpu, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.label1, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.label2, 0, 5);
            this.tableLayoutPanel3.Controls.Add(this.label3, 0, 4);
            this.tableLayoutPanel3.Controls.Add(this.cpuInfoLabel, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.label4, 0, 3);
            this.tableLayoutPanel3.Controls.Add(this.mbModelInfoLabel, 1, 3);
            this.tableLayoutPanel3.Controls.Add(this.label6, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.cpuIdLabel, 1, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.Padding = new System.Windows.Forms.Padding(0, 5, 5, 5);
            this.tableLayoutPanel3.RowCount = 6;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.Size = new System.Drawing.Size(276, 148);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // smuInfoLabel
            // 
            this.smuInfoLabel.AutoEllipsis = true;
            this.smuInfoLabel.AutoSize = true;
            this.smuInfoLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smuInfoLabel.Location = new System.Drawing.Point(74, 120);
            this.smuInfoLabel.Name = "smuInfoLabel";
            this.smuInfoLabel.Padding = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.smuInfoLabel.Size = new System.Drawing.Size(194, 23);
            this.smuInfoLabel.TabIndex = 7;
            this.smuInfoLabel.Text = "-";
            this.smuInfoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // biosInfoLabel
            // 
            this.biosInfoLabel.AutoEllipsis = true;
            this.biosInfoLabel.AutoSize = true;
            this.biosInfoLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.biosInfoLabel.Location = new System.Drawing.Point(74, 97);
            this.biosInfoLabel.Name = "biosInfoLabel";
            this.biosInfoLabel.Padding = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.biosInfoLabel.Size = new System.Drawing.Size(194, 23);
            this.biosInfoLabel.TabIndex = 6;
            this.biosInfoLabel.Text = "-";
            this.biosInfoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // mbVendorInfoLabel
            // 
            this.mbVendorInfoLabel.AutoEllipsis = true;
            this.mbVendorInfoLabel.AutoSize = true;
            this.mbVendorInfoLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mbVendorInfoLabel.Location = new System.Drawing.Point(74, 51);
            this.mbVendorInfoLabel.Name = "mbVendorInfoLabel";
            this.mbVendorInfoLabel.Padding = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.mbVendorInfoLabel.Size = new System.Drawing.Size(194, 23);
            this.mbVendorInfoLabel.TabIndex = 5;
            this.mbVendorInfoLabel.Text = "-";
            this.mbVendorInfoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelInfoCpu
            // 
            this.labelInfoCpu.AutoSize = true;
            this.labelInfoCpu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelInfoCpu.Location = new System.Drawing.Point(3, 5);
            this.labelInfoCpu.Name = "labelInfoCpu";
            this.labelInfoCpu.Padding = new System.Windows.Forms.Padding(0, 5, 5, 5);
            this.labelInfoCpu.Size = new System.Drawing.Size(65, 23);
            this.labelInfoCpu.TabIndex = 0;
            this.labelInfoCpu.Text = "CPU";
            this.labelInfoCpu.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 51);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(0, 5, 5, 5);
            this.label1.Size = new System.Drawing.Size(65, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "MB Vendor";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(3, 120);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(0, 5, 5, 5);
            this.label2.Size = new System.Drawing.Size(65, 23);
            this.label2.TabIndex = 2;
            this.label2.Text = "SMU";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(3, 97);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(0, 5, 5, 5);
            this.label3.Size = new System.Drawing.Size(65, 23);
            this.label3.TabIndex = 3;
            this.label3.Text = "BIOS";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cpuInfoLabel
            // 
            this.cpuInfoLabel.AutoEllipsis = true;
            this.cpuInfoLabel.AutoSize = true;
            this.cpuInfoLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cpuInfoLabel.Location = new System.Drawing.Point(74, 5);
            this.cpuInfoLabel.Name = "cpuInfoLabel";
            this.cpuInfoLabel.Padding = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.cpuInfoLabel.Size = new System.Drawing.Size(194, 23);
            this.cpuInfoLabel.TabIndex = 4;
            this.cpuInfoLabel.Text = "-";
            this.cpuInfoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(3, 74);
            this.label4.Name = "label4";
            this.label4.Padding = new System.Windows.Forms.Padding(0, 5, 5, 5);
            this.label4.Size = new System.Drawing.Size(65, 23);
            this.label4.TabIndex = 8;
            this.label4.Text = "MB Model";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // mbModelInfoLabel
            // 
            this.mbModelInfoLabel.AutoEllipsis = true;
            this.mbModelInfoLabel.AutoSize = true;
            this.mbModelInfoLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mbModelInfoLabel.Location = new System.Drawing.Point(74, 74);
            this.mbModelInfoLabel.Name = "mbModelInfoLabel";
            this.mbModelInfoLabel.Padding = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.mbModelInfoLabel.Size = new System.Drawing.Size(194, 23);
            this.mbModelInfoLabel.TabIndex = 9;
            this.mbModelInfoLabel.Text = "-";
            this.mbModelInfoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Location = new System.Drawing.Point(3, 28);
            this.label6.Name = "label6";
            this.label6.Padding = new System.Windows.Forms.Padding(0, 5, 5, 5);
            this.label6.Size = new System.Drawing.Size(65, 23);
            this.label6.TabIndex = 10;
            this.label6.Text = "CPUID";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cpuIdLabel
            // 
            this.cpuIdLabel.AutoSize = true;
            this.cpuIdLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cpuIdLabel.Location = new System.Drawing.Point(74, 28);
            this.cpuIdLabel.Name = "cpuIdLabel";
            this.cpuIdLabel.Padding = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.cpuIdLabel.Size = new System.Drawing.Size(194, 23);
            this.cpuIdLabel.TabIndex = 11;
            this.cpuIdLabel.Text = "-";
            this.cpuIdLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxResult
            // 
            this.textBoxResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxResult.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxResult.Location = new System.Drawing.Point(0, 5);
            this.textBoxResult.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.textBoxResult.Multiline = true;
            this.textBoxResult.Name = "textBoxResult";
            this.textBoxResult.ReadOnly = true;
            this.textBoxResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxResult.Size = new System.Drawing.Size(163, 214);
            this.textBoxResult.TabIndex = 4;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tabControl1);
            this.splitContainer1.Panel1.Padding = new System.Windows.Forms.Padding(5, 5, 4, 5);
            this.splitContainer1.Panel1MinSize = 300;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.textBoxResult);
            this.splitContainer1.Panel2.Padding = new System.Windows.Forms.Padding(0, 5, 5, 5);
            this.splitContainer1.Panel2MinSize = 0;
            this.splitContainer1.Size = new System.Drawing.Size(469, 224);
            this.splitContainer1.SplitterDistance = 300;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 5;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(469, 246);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SMU Debug Tool";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPageSmu.ResumeLayout(false);
            this.tabPageSmu.PerformLayout();
            this.tabPagePci.ResumeLayout(false);
            this.tabPagePci.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tabPagePstates.ResumeLayout(false);
            this.tabPagePstates.PerformLayout();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.tabPageInfo.ResumeLayout(false);
            this.tabPageInfo.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label labelCMDAddress;
        private System.Windows.Forms.Label labelRSP;
        private System.Windows.Forms.TextBox textBoxCMDAddress;
        private System.Windows.Forms.TextBox textBoxRSPAddress;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel labelStatus;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label labelARG0;
        private System.Windows.Forms.TextBox textBoxARG0;
        private System.Windows.Forms.Label labelCMD;
        private System.Windows.Forms.TextBox textBoxCMD;
        private System.Windows.Forms.Label labelARGAddress;
        private System.Windows.Forms.TextBox textBoxARGAddress;
        private System.Windows.Forms.Button buttonApply;
        private System.Windows.Forms.Button buttonDefaults;
        private System.Windows.Forms.Button buttonProbe;
        private System.Windows.Forms.TabPage tabPageSmu;
        private System.Windows.Forms.TabPage tabPagePci;
        private System.Windows.Forms.TabPage tabPageInfo;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label labelInfoCpu;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label smuInfoLabel;
        private System.Windows.Forms.Label biosInfoLabel;
        private System.Windows.Forms.Label mbVendorInfoLabel;
        private System.Windows.Forms.Label cpuInfoLabel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label mbModelInfoLabel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxPciAddress;
        private System.Windows.Forms.Button buttonPciRead;
        private System.Windows.Forms.TextBox textBoxResult;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label cpuIdLabel;
        private System.Windows.Forms.Button buttonPciWrite;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxPciValue;
        private System.Windows.Forms.Button buttonExport;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnPstateRead;
        private System.Windows.Forms.Button btnPstateWrite;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox pstateIdBox;
        private System.Windows.Forms.TextBox pstateFrequency;
        private System.Windows.Forms.TextBox pstateFid;
        private System.Windows.Forms.TextBox pstateDid;
        private System.Windows.Forms.TabPage tabPagePstates;
    }
}

