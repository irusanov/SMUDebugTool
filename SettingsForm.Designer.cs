using System.Drawing;
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
            this.tabPageCPU = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonApplyPROCHOT = new System.Windows.Forms.Button();
            this.buttonApplySC = new System.Windows.Forms.Button();
            this.buttonApplyAC = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.comboBoxACF = new System.Windows.Forms.ComboBox();
            this.comboBoxSCF = new System.Windows.Forms.ComboBox();
            this.comboBoxCore = new System.Windows.Forms.ComboBox();
            this.checkBoxPROCHOT = new System.Windows.Forms.CheckBox();
            this.tabPageSmu = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.tabPagePci = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonPciWrite = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxPciAddress = new System.Windows.Forms.TextBox();
            this.buttonPciRead = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxPciValue = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.textBoxPciStartReg = new System.Windows.Forms.TextBox();
            this.textBoxPciEndReg = new System.Windows.Forms.TextBox();
            this.buttonPciScan = new System.Windows.Forms.Button();
            this.tabPageMsr = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel9 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonMsrWrite = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.textBoxMsrAddress = new System.Windows.Forms.TextBox();
            this.buttonMsrRead = new System.Windows.Forms.Button();
            this.label17 = new System.Windows.Forms.Label();
            this.textBoxMsrEdx = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.textBoxMsrStart = new System.Windows.Forms.TextBox();
            this.textBoxMsrEnd = new System.Windows.Forms.TextBox();
            this.buttonMsrScan = new System.Windows.Forms.Button();
            this.label20 = new System.Windows.Forms.Label();
            this.textBoxMsrEax = new System.Windows.Forms.TextBox();
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
            this.tabPageCPU.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            this.tabPageSmu.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.tabPagePci.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tabPageMsr.SuspendLayout();
            this.tableLayoutPanel9.SuspendLayout();
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
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(181, 150);
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
            this.textBoxCMDAddress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxCMDAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxCMDAddress.HideSelection = false;
            this.textBoxCMDAddress.Location = new System.Drawing.Point(87, 9);
            this.textBoxCMDAddress.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxCMDAddress.Name = "textBoxCMDAddress";
            this.textBoxCMDAddress.Size = new System.Drawing.Size(90, 20);
            this.textBoxCMDAddress.TabIndex = 2;
            // 
            // textBoxRSPAddress
            // 
            this.textBoxRSPAddress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxRSPAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxRSPAddress.HideSelection = false;
            this.textBoxRSPAddress.Location = new System.Drawing.Point(87, 37);
            this.textBoxRSPAddress.Margin = new System.Windows.Forms.Padding(4);
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
            this.textBoxARG0.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxARG0.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxARG0.HideSelection = false;
            this.textBoxARG0.Location = new System.Drawing.Point(87, 121);
            this.textBoxARG0.Margin = new System.Windows.Forms.Padding(4);
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
            this.textBoxCMD.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxCMD.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxCMD.HideSelection = false;
            this.textBoxCMD.Location = new System.Drawing.Point(87, 93);
            this.textBoxCMD.Margin = new System.Windows.Forms.Padding(4);
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
            this.textBoxARGAddress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxARGAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxARGAddress.Location = new System.Drawing.Point(87, 65);
            this.textBoxARGAddress.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxARGAddress.Name = "textBoxARGAddress";
            this.textBoxARGAddress.Size = new System.Drawing.Size(90, 20);
            this.textBoxARGAddress.TabIndex = 11;
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.labelStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 221);
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
            this.tableLayoutPanel2.Location = new System.Drawing.Point(190, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.Padding = new System.Windows.Forms.Padding(4, 5, 5, 5);
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(90, 97);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // buttonApply
            // 
            this.buttonApply.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonApply.Location = new System.Drawing.Point(7, 66);
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
            this.buttonDefaults.Location = new System.Drawing.Point(7, 37);
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
            this.buttonProbe.Location = new System.Drawing.Point(7, 8);
            this.buttonProbe.Name = "buttonProbe";
            this.buttonProbe.Size = new System.Drawing.Size(75, 23);
            this.buttonProbe.TabIndex = 2;
            this.buttonProbe.Text = "Scan";
            this.buttonProbe.UseVisualStyleBackColor = true;
            this.buttonProbe.Click += new System.EventHandler(this.ButtonScan_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageCPU);
            this.tabControl1.Controls.Add(this.tabPageSmu);
            this.tabControl1.Controls.Add(this.tabPagePci);
            this.tabControl1.Controls.Add(this.tabPageMsr);
            this.tabControl1.Controls.Add(this.tabPagePstates);
            this.tabControl1.Controls.Add(this.tabPageInfo);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(5, 5);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(291, 211);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
            this.tabControl1.TabIndex = 2;
            this.tabControl1.Selected += new System.Windows.Forms.TabControlEventHandler(this.TabControl1_Selected);
            // 
            // tabPageCPU
            // 
            this.tabPageCPU.Controls.Add(this.tableLayoutPanel8);
            this.tabPageCPU.Location = new System.Drawing.Point(4, 22);
            this.tabPageCPU.Name = "tabPageCPU";
            this.tabPageCPU.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCPU.Size = new System.Drawing.Size(283, 185);
            this.tabPageCPU.TabIndex = 4;
            this.tabPageCPU.Text = "CPU";
            this.tabPageCPU.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.AutoSize = true;
            this.tableLayoutPanel8.ColumnCount = 4;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel8.Controls.Add(this.buttonApplyPROCHOT, 0, 6);
            this.tableLayoutPanel8.Controls.Add(this.buttonApplySC, 3, 4);
            this.tableLayoutPanel8.Controls.Add(this.buttonApplyAC, 3, 1);
            this.tableLayoutPanel8.Controls.Add(this.label14, 0, 0);
            this.tableLayoutPanel8.Controls.Add(this.label16, 0, 3);
            this.tableLayoutPanel8.Controls.Add(this.comboBoxACF, 0, 1);
            this.tableLayoutPanel8.Controls.Add(this.comboBoxSCF, 0, 4);
            this.tableLayoutPanel8.Controls.Add(this.comboBoxCore, 1, 4);
            this.tableLayoutPanel8.Controls.Add(this.checkBoxPROCHOT, 0, 6);
            this.tableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel8.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.Padding = new System.Windows.Forms.Padding(0, 5, 5, 5);
            this.tableLayoutPanel8.RowCount = 7;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel8.Size = new System.Drawing.Size(277, 157);
            this.tableLayoutPanel8.TabIndex = 1;
            // 
            // buttonApplyPROCHOT
            // 
            this.buttonApplyPROCHOT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonApplyPROCHOT.Location = new System.Drawing.Point(193, 126);
            this.buttonApplyPROCHOT.Name = "buttonApplyPROCHOT";
            this.buttonApplyPROCHOT.Size = new System.Drawing.Size(76, 23);
            this.buttonApplyPROCHOT.TabIndex = 12;
            this.buttonApplyPROCHOT.Text = "Apply";
            this.buttonApplyPROCHOT.UseVisualStyleBackColor = true;
            this.buttonApplyPROCHOT.Click += new System.EventHandler(this.buttonApplyPROCHOT_Click);
            // 
            // buttonApplySC
            // 
            this.buttonApplySC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonApplySC.Location = new System.Drawing.Point(193, 87);
            this.buttonApplySC.Name = "buttonApplySC";
            this.buttonApplySC.Size = new System.Drawing.Size(76, 23);
            this.buttonApplySC.TabIndex = 5;
            this.buttonApplySC.Text = "Apply";
            this.buttonApplySC.UseVisualStyleBackColor = true;
            this.buttonApplySC.Click += new System.EventHandler(this.buttonApplySC_Click);
            // 
            // buttonApplyAC
            // 
            this.buttonApplyAC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonApplyAC.Location = new System.Drawing.Point(193, 28);
            this.buttonApplyAC.Name = "buttonApplyAC";
            this.buttonApplyAC.Size = new System.Drawing.Size(76, 23);
            this.buttonApplyAC.TabIndex = 2;
            this.buttonApplyAC.Text = "Apply";
            this.buttonApplyAC.UseVisualStyleBackColor = true;
            this.buttonApplyAC.Click += new System.EventHandler(this.buttonApplyAC_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.tableLayoutPanel8.SetColumnSpan(this.label14, 4);
            this.label14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label14.Location = new System.Drawing.Point(3, 5);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(266, 20);
            this.label14.TabIndex = 6;
            this.label14.Text = "All Core Frequency";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.tableLayoutPanel8.SetColumnSpan(this.label16, 4);
            this.label16.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label16.Location = new System.Drawing.Point(3, 64);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(266, 20);
            this.label16.TabIndex = 7;
            this.label16.Text = "Single Core Frequency";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // comboBoxACF
            // 
            this.comboBoxACF.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxACF.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxACF.FormattingEnabled = true;
            this.comboBoxACF.Location = new System.Drawing.Point(3, 28);
            this.comboBoxACF.Name = "comboBoxACF";
            this.comboBoxACF.Size = new System.Drawing.Size(84, 21);
            this.comboBoxACF.TabIndex = 9;
            // 
            // comboBoxSCF
            // 
            this.comboBoxSCF.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxSCF.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSCF.FormattingEnabled = true;
            this.comboBoxSCF.Location = new System.Drawing.Point(3, 87);
            this.comboBoxSCF.Name = "comboBoxSCF";
            this.comboBoxSCF.Size = new System.Drawing.Size(84, 21);
            this.comboBoxSCF.TabIndex = 10;
            // 
            // comboBoxCore
            // 
            this.comboBoxCore.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxCore.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCore.FormattingEnabled = true;
            this.comboBoxCore.Location = new System.Drawing.Point(93, 87);
            this.comboBoxCore.Name = "comboBoxCore";
            this.comboBoxCore.Size = new System.Drawing.Size(84, 21);
            this.comboBoxCore.TabIndex = 8;
            // 
            // checkBoxPROCHOT
            // 
            this.checkBoxPROCHOT.AutoSize = true;
            this.tableLayoutPanel8.SetColumnSpan(this.checkBoxPROCHOT, 3);
            this.checkBoxPROCHOT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkBoxPROCHOT.Location = new System.Drawing.Point(3, 126);
            this.checkBoxPROCHOT.Name = "checkBoxPROCHOT";
            this.checkBoxPROCHOT.Size = new System.Drawing.Size(184, 23);
            this.checkBoxPROCHOT.TabIndex = 11;
            this.checkBoxPROCHOT.Text = "PROCHOT";
            this.checkBoxPROCHOT.UseVisualStyleBackColor = true;
            // 
            // tabPageSmu
            // 
            this.tabPageSmu.Controls.Add(this.tableLayoutPanel7);
            this.tabPageSmu.Controls.Add(this.tableLayoutPanel6);
            this.tabPageSmu.Location = new System.Drawing.Point(4, 22);
            this.tabPageSmu.Margin = new System.Windows.Forms.Padding(0);
            this.tabPageSmu.Name = "tabPageSmu";
            this.tabPageSmu.Size = new System.Drawing.Size(283, 185);
            this.tabPageSmu.TabIndex = 0;
            this.tabPageSmu.Text = "SMU";
            this.tabPageSmu.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.AutoSize = true;
            this.tableLayoutPanel7.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel7.ColumnCount = 1;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(0, 156);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 1;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel7.Size = new System.Drawing.Size(283, 0);
            this.tableLayoutPanel7.TabIndex = 4;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.AutoSize = true;
            this.tableLayoutPanel6.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel6.ColumnCount = 2;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel6.Controls.Add(this.tableLayoutPanel2, 1, 0);
            this.tableLayoutPanel6.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 1;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel6.Size = new System.Drawing.Size(283, 156);
            this.tableLayoutPanel6.TabIndex = 3;
            // 
            // tabPagePci
            // 
            this.tabPagePci.Controls.Add(this.tableLayoutPanel4);
            this.tabPagePci.Location = new System.Drawing.Point(4, 22);
            this.tabPagePci.Name = "tabPagePci";
            this.tabPagePci.Padding = new System.Windows.Forms.Padding(3);
            this.tabPagePci.Size = new System.Drawing.Size(283, 185);
            this.tabPagePci.TabIndex = 1;
            this.tabPagePci.Text = "PCI";
            this.tabPagePci.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.AutoSize = true;
            this.tableLayoutPanel4.ColumnCount = 4;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.Controls.Add(this.buttonPciWrite, 3, 1);
            this.tableLayoutPanel4.Controls.Add(this.label5, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.textBoxPciAddress, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.buttonPciRead, 3, 0);
            this.tableLayoutPanel4.Controls.Add(this.label7, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.textBoxPciValue, 1, 1);
            this.tableLayoutPanel4.Controls.Add(this.label12, 0, 3);
            this.tableLayoutPanel4.Controls.Add(this.label13, 0, 4);
            this.tableLayoutPanel4.Controls.Add(this.textBoxPciStartReg, 1, 3);
            this.tableLayoutPanel4.Controls.Add(this.textBoxPciEndReg, 1, 4);
            this.tableLayoutPanel4.Controls.Add(this.buttonPciScan, 3, 3);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.Padding = new System.Windows.Forms.Padding(0, 5, 5, 5);
            this.tableLayoutPanel4.RowCount = 5;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.Size = new System.Drawing.Size(277, 145);
            this.tableLayoutPanel4.TabIndex = 0;
            // 
            // buttonPciWrite
            // 
            this.buttonPciWrite.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonPciWrite.Location = new System.Drawing.Point(194, 37);
            this.buttonPciWrite.Name = "buttonPciWrite";
            this.buttonPciWrite.Size = new System.Drawing.Size(75, 23);
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
            this.label5.Size = new System.Drawing.Size(77, 29);
            this.label5.TabIndex = 0;
            this.label5.Text = "PCI Register";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxPciAddress
            // 
            this.textBoxPciAddress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxPciAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxPciAddress.Location = new System.Drawing.Point(87, 9);
            this.textBoxPciAddress.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxPciAddress.Name = "textBoxPciAddress";
            this.textBoxPciAddress.Size = new System.Drawing.Size(90, 20);
            this.textBoxPciAddress.TabIndex = 1;
            this.textBoxPciAddress.Text = "0x0";
            this.textBoxPciAddress.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxPciAddress_KeyDown);
            // 
            // buttonPciRead
            // 
            this.buttonPciRead.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonPciRead.Location = new System.Drawing.Point(194, 8);
            this.buttonPciRead.Name = "buttonPciRead";
            this.buttonPciRead.Size = new System.Drawing.Size(75, 23);
            this.buttonPciRead.TabIndex = 2;
            this.buttonPciRead.Text = "Read";
            this.buttonPciRead.UseVisualStyleBackColor = true;
            this.buttonPciRead.Click += new System.EventHandler(this.ButtonPciRead_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Location = new System.Drawing.Point(3, 34);
            this.label7.MinimumSize = new System.Drawing.Size(77, 27);
            this.label7.Name = "label7";
            this.label7.Padding = new System.Windows.Forms.Padding(0, 5, 5, 5);
            this.label7.Size = new System.Drawing.Size(77, 29);
            this.label7.TabIndex = 3;
            this.label7.Text = "Value";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxPciValue
            // 
            this.textBoxPciValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxPciValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxPciValue.Location = new System.Drawing.Point(87, 38);
            this.textBoxPciValue.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxPciValue.Name = "textBoxPciValue";
            this.textBoxPciValue.Size = new System.Drawing.Size(90, 20);
            this.textBoxPciValue.TabIndex = 4;
            this.textBoxPciValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxPciValue_KeyDown);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label12.Location = new System.Drawing.Point(3, 83);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(77, 29);
            this.label12.TabIndex = 6;
            this.label12.Text = "Start Register";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label13.Location = new System.Drawing.Point(3, 112);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(77, 28);
            this.label13.TabIndex = 7;
            this.label13.Text = "End Register";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxPciStartReg
            // 
            this.textBoxPciStartReg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxPciStartReg.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxPciStartReg.Location = new System.Drawing.Point(87, 87);
            this.textBoxPciStartReg.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxPciStartReg.Name = "textBoxPciStartReg";
            this.textBoxPciStartReg.Size = new System.Drawing.Size(90, 20);
            this.textBoxPciStartReg.TabIndex = 8;
            this.textBoxPciStartReg.Text = "0x50000";
            // 
            // textBoxPciEndReg
            // 
            this.textBoxPciEndReg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxPciEndReg.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxPciEndReg.Location = new System.Drawing.Point(87, 116);
            this.textBoxPciEndReg.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxPciEndReg.Name = "textBoxPciEndReg";
            this.textBoxPciEndReg.Size = new System.Drawing.Size(90, 20);
            this.textBoxPciEndReg.TabIndex = 9;
            this.textBoxPciEndReg.Text = "0x50300";
            // 
            // buttonPciScan
            // 
            this.buttonPciScan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonPciScan.Location = new System.Drawing.Point(194, 86);
            this.buttonPciScan.Name = "buttonPciScan";
            this.buttonPciScan.Size = new System.Drawing.Size(75, 23);
            this.buttonPciScan.TabIndex = 10;
            this.buttonPciScan.Text = "Scan";
            this.buttonPciScan.UseVisualStyleBackColor = true;
            this.buttonPciScan.Click += new System.EventHandler(this.buttonPciScan_Click);
            // 
            // tabPageMsr
            // 
            this.tabPageMsr.Controls.Add(this.tableLayoutPanel9);
            this.tabPageMsr.Location = new System.Drawing.Point(4, 22);
            this.tabPageMsr.Name = "tabPageMsr";
            this.tabPageMsr.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMsr.Size = new System.Drawing.Size(283, 185);
            this.tabPageMsr.TabIndex = 5;
            this.tabPageMsr.Text = "MSR";
            this.tabPageMsr.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel9
            // 
            this.tableLayoutPanel9.AutoSize = true;
            this.tableLayoutPanel9.ColumnCount = 4;
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel9.Controls.Add(this.buttonMsrWrite, 3, 2);
            this.tableLayoutPanel9.Controls.Add(this.label15, 0, 0);
            this.tableLayoutPanel9.Controls.Add(this.textBoxMsrAddress, 1, 0);
            this.tableLayoutPanel9.Controls.Add(this.buttonMsrRead, 3, 0);
            this.tableLayoutPanel9.Controls.Add(this.label17, 0, 1);
            this.tableLayoutPanel9.Controls.Add(this.textBoxMsrEdx, 0, 2);
            this.tableLayoutPanel9.Controls.Add(this.label18, 0, 4);
            this.tableLayoutPanel9.Controls.Add(this.label19, 0, 5);
            this.tableLayoutPanel9.Controls.Add(this.textBoxMsrStart, 1, 4);
            this.tableLayoutPanel9.Controls.Add(this.textBoxMsrEnd, 1, 5);
            this.tableLayoutPanel9.Controls.Add(this.buttonMsrScan, 3, 4);
            this.tableLayoutPanel9.Controls.Add(this.label20, 1, 1);
            this.tableLayoutPanel9.Controls.Add(this.textBoxMsrEax, 1, 2);
            this.tableLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel9.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel9.Name = "tableLayoutPanel9";
            this.tableLayoutPanel9.Padding = new System.Windows.Forms.Padding(0, 5, 5, 5);
            this.tableLayoutPanel9.RowCount = 6;
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel9.Size = new System.Drawing.Size(277, 172);
            this.tableLayoutPanel9.TabIndex = 1;
            // 
            // buttonMsrWrite
            // 
            this.buttonMsrWrite.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonMsrWrite.Location = new System.Drawing.Point(193, 64);
            this.buttonMsrWrite.Name = "buttonMsrWrite";
            this.buttonMsrWrite.Size = new System.Drawing.Size(76, 23);
            this.buttonMsrWrite.TabIndex = 5;
            this.buttonMsrWrite.Text = "Write";
            this.buttonMsrWrite.UseVisualStyleBackColor = true;
            this.buttonMsrWrite.Click += new System.EventHandler(this.buttonMsrWrite_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label15.Location = new System.Drawing.Point(3, 5);
            this.label15.MinimumSize = new System.Drawing.Size(77, 27);
            this.label15.Name = "label15";
            this.label15.Padding = new System.Windows.Forms.Padding(0, 5, 5, 5);
            this.label15.Size = new System.Drawing.Size(84, 29);
            this.label15.TabIndex = 0;
            this.label15.Text = "MSR";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxMsrAddress
            // 
            this.textBoxMsrAddress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxMsrAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxMsrAddress.Location = new System.Drawing.Point(94, 9);
            this.textBoxMsrAddress.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxMsrAddress.Name = "textBoxMsrAddress";
            this.textBoxMsrAddress.Size = new System.Drawing.Size(82, 20);
            this.textBoxMsrAddress.TabIndex = 1;
            this.textBoxMsrAddress.Text = "0x00000000";
            // 
            // buttonMsrRead
            // 
            this.buttonMsrRead.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonMsrRead.Location = new System.Drawing.Point(193, 8);
            this.buttonMsrRead.Name = "buttonMsrRead";
            this.buttonMsrRead.Size = new System.Drawing.Size(76, 23);
            this.buttonMsrRead.TabIndex = 2;
            this.buttonMsrRead.Text = "Read";
            this.buttonMsrRead.UseVisualStyleBackColor = true;
            this.buttonMsrRead.Click += new System.EventHandler(this.buttonMsrRead_Click);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label17.Location = new System.Drawing.Point(3, 34);
            this.label17.MinimumSize = new System.Drawing.Size(77, 27);
            this.label17.Name = "label17";
            this.label17.Padding = new System.Windows.Forms.Padding(0, 5, 5, 5);
            this.label17.Size = new System.Drawing.Size(84, 27);
            this.label17.TabIndex = 3;
            this.label17.Text = "EDX (63 - 32)";
            this.label17.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // textBoxMsrEdx
            // 
            this.textBoxMsrEdx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxMsrEdx.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxMsrEdx.Location = new System.Drawing.Point(4, 65);
            this.textBoxMsrEdx.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxMsrEdx.Name = "textBoxMsrEdx";
            this.textBoxMsrEdx.Size = new System.Drawing.Size(82, 20);
            this.textBoxMsrEdx.TabIndex = 4;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label18.Location = new System.Drawing.Point(3, 110);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(84, 29);
            this.label18.TabIndex = 6;
            this.label18.Text = "Start Address";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label19.Location = new System.Drawing.Point(3, 139);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(84, 28);
            this.label19.TabIndex = 7;
            this.label19.Text = "End Address";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxMsrStart
            // 
            this.textBoxMsrStart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxMsrStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxMsrStart.Location = new System.Drawing.Point(94, 114);
            this.textBoxMsrStart.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxMsrStart.Name = "textBoxMsrStart";
            this.textBoxMsrStart.Size = new System.Drawing.Size(82, 20);
            this.textBoxMsrStart.TabIndex = 8;
            this.textBoxMsrStart.Text = "0x00000000";
            // 
            // textBoxMsrEnd
            // 
            this.textBoxMsrEnd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxMsrEnd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxMsrEnd.Location = new System.Drawing.Point(94, 143);
            this.textBoxMsrEnd.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxMsrEnd.Name = "textBoxMsrEnd";
            this.textBoxMsrEnd.Size = new System.Drawing.Size(82, 20);
            this.textBoxMsrEnd.TabIndex = 9;
            this.textBoxMsrEnd.Text = "0x0000FFFF";
            // 
            // buttonMsrScan
            // 
            this.buttonMsrScan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonMsrScan.Location = new System.Drawing.Point(193, 113);
            this.buttonMsrScan.Name = "buttonMsrScan";
            this.buttonMsrScan.Size = new System.Drawing.Size(76, 23);
            this.buttonMsrScan.TabIndex = 10;
            this.buttonMsrScan.Text = "Scan";
            this.buttonMsrScan.UseVisualStyleBackColor = true;
            this.buttonMsrScan.Click += new System.EventHandler(this.buttonMsrScan_Click);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label20.Location = new System.Drawing.Point(93, 34);
            this.label20.MinimumSize = new System.Drawing.Size(77, 27);
            this.label20.Name = "label20";
            this.label20.Padding = new System.Windows.Forms.Padding(0, 5, 5, 5);
            this.label20.Size = new System.Drawing.Size(84, 27);
            this.label20.TabIndex = 11;
            this.label20.Text = "EAX (31 - 0)";
            this.label20.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // textBoxMsrEax
            // 
            this.textBoxMsrEax.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxMsrEax.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxMsrEax.Location = new System.Drawing.Point(94, 65);
            this.textBoxMsrEax.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxMsrEax.Name = "textBoxMsrEax";
            this.textBoxMsrEax.Size = new System.Drawing.Size(82, 20);
            this.textBoxMsrEax.TabIndex = 12;
            // 
            // tabPagePstates
            // 
            this.tabPagePstates.Controls.Add(this.tableLayoutPanel5);
            this.tabPagePstates.Location = new System.Drawing.Point(4, 22);
            this.tabPagePstates.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.tabPagePstates.Name = "tabPagePstates";
            this.tabPagePstates.Padding = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.tabPagePstates.Size = new System.Drawing.Size(283, 185);
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
            this.tableLayoutPanel5.Size = new System.Drawing.Size(279, 124);
            this.tableLayoutPanel5.TabIndex = 1;
            // 
            // btnPstateWrite
            // 
            this.btnPstateWrite.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPstateWrite.Enabled = false;
            this.btnPstateWrite.Location = new System.Drawing.Point(196, 37);
            this.btnPstateWrite.Name = "btnPstateWrite";
            this.btnPstateWrite.Size = new System.Drawing.Size(75, 23);
            this.btnPstateWrite.TabIndex = 5;
            this.btnPstateWrite.Text = "Write";
            this.btnPstateWrite.UseVisualStyleBackColor = true;
            this.btnPstateWrite.Click += new System.EventHandler(this.btnPstateWrite_Click);
            // 
            // label10
            // 
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Location = new System.Drawing.Point(2, 63);
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
            this.label8.Size = new System.Drawing.Size(77, 29);
            this.label8.TabIndex = 0;
            this.label8.Text = "PState ID";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnPstateRead
            // 
            this.btnPstateRead.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPstateRead.Location = new System.Drawing.Point(196, 8);
            this.btnPstateRead.Name = "btnPstateRead";
            this.btnPstateRead.Size = new System.Drawing.Size(75, 23);
            this.btnPstateRead.TabIndex = 2;
            this.btnPstateRead.Text = "Read";
            this.btnPstateRead.UseVisualStyleBackColor = true;
            this.btnPstateRead.Click += new System.EventHandler(this.btnPstateRead_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Location = new System.Drawing.Point(3, 34);
            this.label9.MinimumSize = new System.Drawing.Size(77, 27);
            this.label9.Name = "label9";
            this.label9.Padding = new System.Windows.Forms.Padding(0, 5, 5, 5);
            this.label9.Size = new System.Drawing.Size(77, 29);
            this.label9.TabIndex = 3;
            this.label9.Text = "DID";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pstateDid
            // 
            this.pstateDid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pstateDid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pstateDid.Location = new System.Drawing.Point(87, 38);
            this.pstateDid.Margin = new System.Windows.Forms.Padding(4, 4, 14, 4);
            this.pstateDid.Name = "pstateDid";
            this.pstateDid.ReadOnly = true;
            this.pstateDid.Size = new System.Drawing.Size(92, 20);
            this.pstateDid.TabIndex = 4;
            // 
            // pstateFid
            // 
            this.pstateFid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pstateFid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pstateFid.Location = new System.Drawing.Point(87, 67);
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
            this.pstateIdBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.label11.Location = new System.Drawing.Point(2, 91);
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
            this.pstateFrequency.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pstateFrequency.Location = new System.Drawing.Point(87, 95);
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
            this.tabPageInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageInfo.Size = new System.Drawing.Size(283, 185);
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
            this.tableLayoutPanel3.Size = new System.Drawing.Size(277, 148);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // smuInfoLabel
            // 
            this.smuInfoLabel.AutoEllipsis = true;
            this.smuInfoLabel.AutoSize = true;
            this.smuInfoLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smuInfoLabel.Location = new System.Drawing.Point(74, 120);
            this.smuInfoLabel.Name = "smuInfoLabel";
            this.smuInfoLabel.Padding = new System.Windows.Forms.Padding(5);
            this.smuInfoLabel.Size = new System.Drawing.Size(195, 23);
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
            this.biosInfoLabel.Padding = new System.Windows.Forms.Padding(5);
            this.biosInfoLabel.Size = new System.Drawing.Size(195, 23);
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
            this.mbVendorInfoLabel.Padding = new System.Windows.Forms.Padding(5);
            this.mbVendorInfoLabel.Size = new System.Drawing.Size(195, 23);
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
            this.cpuInfoLabel.Padding = new System.Windows.Forms.Padding(5);
            this.cpuInfoLabel.Size = new System.Drawing.Size(195, 23);
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
            this.mbModelInfoLabel.Padding = new System.Windows.Forms.Padding(5);
            this.mbModelInfoLabel.Size = new System.Drawing.Size(195, 23);
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
            this.cpuIdLabel.Padding = new System.Windows.Forms.Padding(5);
            this.cpuIdLabel.Size = new System.Drawing.Size(195, 23);
            this.cpuIdLabel.TabIndex = 11;
            this.cpuIdLabel.Text = "-";
            this.cpuIdLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxResult
            // 
            this.textBoxResult.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxResult.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxResult.Location = new System.Drawing.Point(0, 5);
            this.textBoxResult.Margin = new System.Windows.Forms.Padding(5);
            this.textBoxResult.Multiline = true;
            this.textBoxResult.Name = "textBoxResult";
            this.textBoxResult.ReadOnly = true;
            this.textBoxResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxResult.Size = new System.Drawing.Size(163, 211);
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
            this.splitContainer1.Size = new System.Drawing.Size(469, 221);
            this.splitContainer1.SplitterDistance = 300;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 5;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(469, 243);
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
            this.tabPageCPU.ResumeLayout(false);
            this.tabPageCPU.PerformLayout();
            this.tableLayoutPanel8.ResumeLayout(false);
            this.tableLayoutPanel8.PerformLayout();
            this.tabPageSmu.ResumeLayout(false);
            this.tabPageSmu.PerformLayout();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.tabPagePci.ResumeLayout(false);
            this.tabPagePci.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tabPageMsr.ResumeLayout(false);
            this.tabPageMsr.PerformLayout();
            this.tableLayoutPanel9.ResumeLayout(false);
            this.tableLayoutPanel9.PerformLayout();
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
        private System.Windows.Forms.TextBox pstateDid;
        private System.Windows.Forms.TabPage tabPagePstates;
        private Label label12;
        private Label label13;
        private TextBox textBoxPciStartReg;
        private TextBox textBoxPciEndReg;
        private Button buttonPciScan;
        private TableLayoutPanel tableLayoutPanel6;
        private TableLayoutPanel tableLayoutPanel7;
        private TextBox pstateFid;
        private TabPage tabPageCPU;
        private TableLayoutPanel tableLayoutPanel8;
        private Button buttonApplySC;
        private Button buttonApplyAC;
        private Label label14;
        private Label label16;
        private ComboBox comboBoxCore;
        private ComboBox comboBoxACF;
        private ComboBox comboBoxSCF;
        private CheckBox checkBoxPROCHOT;
        private Button buttonApplyPROCHOT;
        private TabPage tabPageMsr;
        private TableLayoutPanel tableLayoutPanel9;
        private Button buttonMsrWrite;
        private Label label15;
        private TextBox textBoxMsrAddress;
        private Button buttonMsrRead;
        private Label label17;
        private TextBox textBoxMsrEdx;
        private Label label18;
        private Label label19;
        private TextBox textBoxMsrStart;
        private TextBox textBoxMsrEnd;
        private Button buttonMsrScan;
        private Label label20;
        private TextBox textBoxMsrEax;
    }
}

