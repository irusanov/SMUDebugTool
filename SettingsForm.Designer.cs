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
                cpu?.Dispose();
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.labelCMDAddress = new System.Windows.Forms.Label();
            this.labelRSP = new System.Windows.Forms.Label();
            this.textBoxCMDAddress = new System.Windows.Forms.TextBox();
            this.textBoxRSPAddress = new System.Windows.Forms.TextBox();
            this.labelARG0 = new System.Windows.Forms.Label();
            this.textBoxARG0 = new System.Windows.Forms.TextBox();
            this.labelCMD = new System.Windows.Forms.Label();
            this.labelARGAddress = new System.Windows.Forms.Label();
            this.textBoxARGAddress = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.comboBoxMailboxSelect = new System.Windows.Forms.ComboBox();
            this.textBoxCMD = new System.Windows.Forms.NumericUpDown();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.labelStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonSmuLog = new System.Windows.Forms.Button();
            this.buttonProbe = new System.Windows.Forms.Button();
            this.buttonPMTable = new System.Windows.Forms.Button();
            this.buttonDefaults = new System.Windows.Forms.Button();
            this.buttonApply = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageCPU = new System.Windows.Forms.TabPage();
            this.groupBoxCoreControl = new System.Windows.Forms.GroupBox();
            this.panelX3D = new System.Windows.Forms.Panel();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.radioButtonManualCoreControl = new System.Windows.Forms.RadioButton();
            this.panelManualCoreControl = new System.Windows.Forms.Panel();
            this.label67 = new System.Windows.Forms.Label();
            this.tableLayoutPanel15 = new System.Windows.Forms.TableLayoutPanel();
            this.labelCore15 = new System.Windows.Forms.Label();
            this.labelCore14 = new System.Windows.Forms.Label();
            this.labelCore13 = new System.Windows.Forms.Label();
            this.labelCore12 = new System.Windows.Forms.Label();
            this.labelCore11 = new System.Windows.Forms.Label();
            this.labelCore10 = new System.Windows.Forms.Label();
            this.labelCore9 = new System.Windows.Forms.Label();
            this.labelCore8 = new System.Windows.Forms.Label();
            this.checkBox15 = new System.Windows.Forms.CheckBox();
            this.checkBox14 = new System.Windows.Forms.CheckBox();
            this.checkBox13 = new System.Windows.Forms.CheckBox();
            this.checkBox12 = new System.Windows.Forms.CheckBox();
            this.checkBox11 = new System.Windows.Forms.CheckBox();
            this.checkBox10 = new System.Windows.Forms.CheckBox();
            this.checkBox9 = new System.Windows.Forms.CheckBox();
            this.checkBox8 = new System.Windows.Forms.CheckBox();
            this.labelCore7 = new System.Windows.Forms.Label();
            this.labelCore6 = new System.Windows.Forms.Label();
            this.labelCore5 = new System.Windows.Forms.Label();
            this.labelCore4 = new System.Windows.Forms.Label();
            this.labelCore3 = new System.Windows.Forms.Label();
            this.labelCore2 = new System.Windows.Forms.Label();
            this.labelCore1 = new System.Windows.Forms.Label();
            this.checkBox7 = new System.Windows.Forms.CheckBox();
            this.checkBox6 = new System.Windows.Forms.CheckBox();
            this.checkBox5 = new System.Windows.Forms.CheckBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox0 = new System.Windows.Forms.CheckBox();
            this.labelCore0 = new System.Windows.Forms.Label();
            this.buttonApplyCoreMap = new System.Windows.Forms.Button();
            this.checkBoxSMT = new System.Windows.Forms.CheckBox();
            this.radioButtonX3D = new System.Windows.Forms.RadioButton();
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
            this.tabPageCPUID = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel14 = new System.Windows.Forms.TableLayoutPanel();
            this.label50 = new System.Windows.Forms.Label();
            this.textBoxCpuid = new System.Windows.Forms.TextBox();
            this.buttonCpuidDecode = new System.Windows.Forms.Button();
            this.tableLayoutPanel10 = new System.Windows.Forms.TableLayoutPanel();
            this.label21 = new System.Windows.Forms.Label();
            this.textBoxCPUIDAddress = new System.Windows.Forms.TextBox();
            this.buttonCPUIDRead = new System.Windows.Forms.Button();
            this.label25 = new System.Windows.Forms.Label();
            this.textBoxCPUIDeax = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.textBoxCPUIDedx = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.textBoxCPUIDebx = new System.Windows.Forms.TextBox();
            this.textBoxCPUIDecx = new System.Windows.Forms.TextBox();
            this.buttonCPUIDScan = new System.Windows.Forms.Button();
            this.tabPagePbo = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel12 = new System.Windows.Forms.TableLayoutPanel();
            this.numericUpDownCO_8 = new System.Windows.Forms.NumericUpDown();
            this.buttonApplyCO = new System.Windows.Forms.Button();
            this.label31 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this.label46 = new System.Windows.Forms.Label();
            this.buttonGetCO = new System.Windows.Forms.Button();
            this.numericUpDownCO_0 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownCO_1 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownCO_2 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownCO_3 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownCO_4 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownCO_5 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownCO_6 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownCO_7 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownCO_9 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownCO_10 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownCO_11 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownCO_12 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownCO_13 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownCO_14 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownCO_15 = new System.Windows.Forms.NumericUpDown();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.btnSaveCOProfile = new System.Windows.Forms.Button();
            this.btnLoadCOProfile = new System.Windows.Forms.Button();
            this.checkBoxApplyCOStartup = new System.Windows.Forms.CheckBox();
            this.tabPageWmi = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel13 = new System.Windows.Forms.TableLayoutPanel();
            this.label49 = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.comboBoxAvailableCommands = new System.Windows.Forms.ComboBox();
            this.comboBoxAvailableValues = new System.Windows.Forms.ComboBox();
            this.label48 = new System.Windows.Forms.Label();
            this.textBoxWmiArgument = new System.Windows.Forms.TextBox();
            this.buttonWmiCmdSend = new System.Windows.Forms.Button();
            this.tabPagePstates = new System.Windows.Forms.TabPage();
            this.numericUpDownBclk = new System.Windows.Forms.NumericUpDown();
            this.labelBCLK = new System.Windows.Forms.Label();
            this.buttonBCLKApply = new System.Windows.Forms.Button();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.pstateDid = new System.Windows.Forms.TextBox();
            this.pstateFid = new System.Windows.Forms.TextBox();
            this.pstateIdBox = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.pstateFrequency = new System.Windows.Forms.TextBox();
            this.btnPstateWrite = new System.Windows.Forms.Button();
            this.btnPstateRead = new System.Windows.Forms.Button();
            this.tabPageInfo = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonExport = new System.Windows.Forms.Button();
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
            this.label23 = new System.Windows.Forms.Label();
            this.firmwareInfoLabel = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.modelInfoLabel = new System.Windows.Forms.Label();
            this.packageTypeInfoLabel = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.configInfoLabel = new System.Windows.Forms.Label();
            this.textBoxResult = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel11 = new System.Windows.Forms.TableLayoutPanel();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxCMD)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPageCPU.SuspendLayout();
            this.groupBoxCoreControl.SuspendLayout();
            this.panelX3D.SuspendLayout();
            this.panelManualCoreControl.SuspendLayout();
            this.tableLayoutPanel15.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            this.tabPageSmu.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.tabPagePci.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tabPageMsr.SuspendLayout();
            this.tableLayoutPanel9.SuspendLayout();
            this.tabPageCPUID.SuspendLayout();
            this.tableLayoutPanel14.SuspendLayout();
            this.tableLayoutPanel10.SuspendLayout();
            this.tabPagePbo.SuspendLayout();
            this.tableLayoutPanel12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCO_8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCO_0)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCO_1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCO_2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCO_3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCO_4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCO_5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCO_6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCO_7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCO_9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCO_10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCO_11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCO_12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCO_13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCO_14)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCO_15)).BeginInit();
            this.tabPageWmi.SuspendLayout();
            this.tableLayoutPanel13.SuspendLayout();
            this.tabPagePstates.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBclk)).BeginInit();
            this.tableLayoutPanel5.SuspendLayout();
            this.tabPageInfo.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.Controls.Add(this.labelCMDAddress, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.labelRSP, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.textBoxCMDAddress, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.textBoxRSPAddress, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.labelARG0, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.textBoxARG0, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.labelCMD, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.labelARGAddress, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.textBoxARGAddress, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.label30, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.comboBoxMailboxSelect, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.textBoxCMD, 1, 4);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(200, 179);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // labelCMDAddress
            // 
            this.labelCMDAddress.AutoSize = true;
            this.labelCMDAddress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelCMDAddress.Location = new System.Drawing.Point(3, 34);
            this.labelCMDAddress.MinimumSize = new System.Drawing.Size(77, 27);
            this.labelCMDAddress.Name = "labelCMDAddress";
            this.labelCMDAddress.Padding = new System.Windows.Forms.Padding(0, 5, 5, 5);
            this.labelCMDAddress.Size = new System.Drawing.Size(94, 28);
            this.labelCMDAddress.TabIndex = 0;
            this.labelCMDAddress.Text = "CMD Address";
            this.labelCMDAddress.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelRSP
            // 
            this.labelRSP.AutoSize = true;
            this.labelRSP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelRSP.Location = new System.Drawing.Point(3, 62);
            this.labelRSP.MinimumSize = new System.Drawing.Size(77, 27);
            this.labelRSP.Name = "labelRSP";
            this.labelRSP.Padding = new System.Windows.Forms.Padding(0, 5, 5, 5);
            this.labelRSP.Size = new System.Drawing.Size(94, 28);
            this.labelRSP.TabIndex = 1;
            this.labelRSP.Text = "RSP Address";
            this.labelRSP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxCMDAddress
            // 
            this.textBoxCMDAddress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxCMDAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxCMDAddress.HideSelection = false;
            this.textBoxCMDAddress.Location = new System.Drawing.Point(104, 38);
            this.textBoxCMDAddress.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxCMDAddress.Name = "textBoxCMDAddress";
            this.textBoxCMDAddress.Size = new System.Drawing.Size(92, 20);
            this.textBoxCMDAddress.TabIndex = 2;
            // 
            // textBoxRSPAddress
            // 
            this.textBoxRSPAddress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxRSPAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxRSPAddress.HideSelection = false;
            this.textBoxRSPAddress.Location = new System.Drawing.Point(104, 66);
            this.textBoxRSPAddress.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxRSPAddress.Name = "textBoxRSPAddress";
            this.textBoxRSPAddress.Size = new System.Drawing.Size(92, 20);
            this.textBoxRSPAddress.TabIndex = 3;
            // 
            // labelARG0
            // 
            this.labelARG0.AutoSize = true;
            this.labelARG0.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelARG0.Location = new System.Drawing.Point(3, 146);
            this.labelARG0.MinimumSize = new System.Drawing.Size(77, 27);
            this.labelARG0.Name = "labelARG0";
            this.labelARG0.Padding = new System.Windows.Forms.Padding(0, 5, 5, 5);
            this.labelARG0.Size = new System.Drawing.Size(94, 28);
            this.labelARG0.TabIndex = 4;
            this.labelARG0.Text = "Arguments";
            this.labelARG0.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxARG0
            // 
            this.textBoxARG0.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxARG0.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxARG0.HideSelection = false;
            this.textBoxARG0.Location = new System.Drawing.Point(104, 150);
            this.textBoxARG0.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxARG0.Name = "textBoxARG0";
            this.textBoxARG0.Size = new System.Drawing.Size(92, 20);
            this.textBoxARG0.TabIndex = 6;
            this.textBoxARG0.Text = "0";
            // 
            // labelCMD
            // 
            this.labelCMD.AutoSize = true;
            this.labelCMD.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelCMD.Location = new System.Drawing.Point(3, 118);
            this.labelCMD.MinimumSize = new System.Drawing.Size(77, 27);
            this.labelCMD.Name = "labelCMD";
            this.labelCMD.Padding = new System.Windows.Forms.Padding(0, 5, 5, 5);
            this.labelCMD.Size = new System.Drawing.Size(94, 28);
            this.labelCMD.TabIndex = 8;
            this.labelCMD.Text = "Command ID";
            this.labelCMD.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelARGAddress
            // 
            this.labelARGAddress.AutoSize = true;
            this.labelARGAddress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelARGAddress.Location = new System.Drawing.Point(3, 90);
            this.labelARGAddress.MinimumSize = new System.Drawing.Size(77, 27);
            this.labelARGAddress.Name = "labelARGAddress";
            this.labelARGAddress.Padding = new System.Windows.Forms.Padding(0, 5, 5, 5);
            this.labelARGAddress.Size = new System.Drawing.Size(94, 28);
            this.labelARGAddress.TabIndex = 10;
            this.labelARGAddress.Text = "ARG Address";
            this.labelARGAddress.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxARGAddress
            // 
            this.textBoxARGAddress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxARGAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxARGAddress.Location = new System.Drawing.Point(104, 94);
            this.textBoxARGAddress.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxARGAddress.Name = "textBoxARGAddress";
            this.textBoxARGAddress.Size = new System.Drawing.Size(92, 20);
            this.textBoxARGAddress.TabIndex = 11;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label30.Location = new System.Drawing.Point(3, 5);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(94, 29);
            this.label30.TabIndex = 12;
            this.label30.Text = "Mailbox";
            this.label30.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // comboBoxMailboxSelect
            // 
            this.comboBoxMailboxSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMailboxSelect.FormattingEnabled = true;
            this.comboBoxMailboxSelect.Items.AddRange(new object[] {
            "RSMU",
            "MP1",
            "HSMP"});
            this.comboBoxMailboxSelect.Location = new System.Drawing.Point(104, 9);
            this.comboBoxMailboxSelect.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxMailboxSelect.Name = "comboBoxMailboxSelect";
            this.comboBoxMailboxSelect.Size = new System.Drawing.Size(92, 21);
            this.comboBoxMailboxSelect.TabIndex = 13;
            this.comboBoxMailboxSelect.SelectedIndexChanged += new System.EventHandler(this.ComboBoxMailboxSelect_SelectedIndexChanged);
            // 
            // textBoxCMD
            // 
            this.textBoxCMD.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxCMD.Hexadecimal = true;
            this.textBoxCMD.Location = new System.Drawing.Point(104, 122);
            this.textBoxCMD.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxCMD.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.textBoxCMD.Name = "textBoxCMD";
            this.textBoxCMD.Size = new System.Drawing.Size(92, 20);
            this.textBoxCMD.TabIndex = 14;
            this.textBoxCMD.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.labelStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 410);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(652, 22);
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
            this.tableLayoutPanel2.Controls.Add(this.buttonSmuLog, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.buttonProbe, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.buttonPMTable, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.buttonDefaults, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.buttonApply, 0, 3);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(244, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.Padding = new System.Windows.Forms.Padding(4, 5, 5, 5);
            this.tableLayoutPanel2.RowCount = 5;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(90, 155);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // buttonSmuLog
            // 
            this.buttonSmuLog.Location = new System.Drawing.Point(7, 37);
            this.buttonSmuLog.Name = "buttonSmuLog";
            this.buttonSmuLog.Size = new System.Drawing.Size(75, 23);
            this.buttonSmuLog.TabIndex = 5;
            this.buttonSmuLog.Text = "Monitor";
            this.buttonSmuLog.UseVisualStyleBackColor = true;
            this.buttonSmuLog.Click += new System.EventHandler(this.ButtonSMUMonitor_Click);
            // 
            // buttonProbe
            // 
            this.buttonProbe.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonProbe.Location = new System.Drawing.Point(7, 66);
            this.buttonProbe.Name = "buttonProbe";
            this.buttonProbe.Size = new System.Drawing.Size(75, 23);
            this.buttonProbe.TabIndex = 2;
            this.buttonProbe.Text = "Scan";
            this.buttonProbe.UseVisualStyleBackColor = true;
            this.buttonProbe.Click += new System.EventHandler(this.ButtonScan_Click);
            // 
            // buttonPMTable
            // 
            this.buttonPMTable.Location = new System.Drawing.Point(7, 8);
            this.buttonPMTable.Name = "buttonPMTable";
            this.buttonPMTable.Size = new System.Drawing.Size(75, 23);
            this.buttonPMTable.TabIndex = 3;
            this.buttonPMTable.Text = "PMTable";
            this.buttonPMTable.UseVisualStyleBackColor = true;
            this.buttonPMTable.Click += new System.EventHandler(this.ButtonPMTable_Click);
            // 
            // buttonDefaults
            // 
            this.buttonDefaults.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonDefaults.Location = new System.Drawing.Point(7, 124);
            this.buttonDefaults.Name = "buttonDefaults";
            this.buttonDefaults.Size = new System.Drawing.Size(75, 23);
            this.buttonDefaults.TabIndex = 1;
            this.buttonDefaults.Text = "Reset";
            this.buttonDefaults.UseVisualStyleBackColor = true;
            this.buttonDefaults.Click += new System.EventHandler(this.ButtonDefaults_Click);
            // 
            // buttonApply
            // 
            this.buttonApply.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonApply.Location = new System.Drawing.Point(7, 95);
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.Size = new System.Drawing.Size(75, 23);
            this.buttonApply.TabIndex = 0;
            this.buttonApply.Text = "Send";
            this.buttonApply.UseVisualStyleBackColor = true;
            this.buttonApply.Click += new System.EventHandler(this.ButtonApply_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageCPU);
            this.tabControl1.Controls.Add(this.tabPageSmu);
            this.tabControl1.Controls.Add(this.tabPagePci);
            this.tabControl1.Controls.Add(this.tabPageMsr);
            this.tabControl1.Controls.Add(this.tabPageCPUID);
            this.tabControl1.Controls.Add(this.tabPagePbo);
            this.tabControl1.Controls.Add(this.tabPageWmi);
            this.tabControl1.Controls.Add(this.tabPagePstates);
            this.tabControl1.Controls.Add(this.tabPageInfo);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(5, 5);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(345, 400);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
            this.tabControl1.TabIndex = 2;
            this.tabControl1.Selected += new System.Windows.Forms.TabControlEventHandler(this.TabControl1_Selected);
            // 
            // tabPageCPU
            // 
            this.tabPageCPU.Controls.Add(this.groupBoxCoreControl);
            this.tabPageCPU.Controls.Add(this.tableLayoutPanel8);
            this.tabPageCPU.Location = new System.Drawing.Point(4, 40);
            this.tabPageCPU.Name = "tabPageCPU";
            this.tabPageCPU.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCPU.Size = new System.Drawing.Size(337, 356);
            this.tabPageCPU.TabIndex = 4;
            this.tabPageCPU.Text = "CPU";
            this.tabPageCPU.UseVisualStyleBackColor = true;
            // 
            // groupBoxCoreControl
            // 
            this.groupBoxCoreControl.Controls.Add(this.panelX3D);
            this.groupBoxCoreControl.Controls.Add(this.radioButtonManualCoreControl);
            this.groupBoxCoreControl.Controls.Add(this.panelManualCoreControl);
            this.groupBoxCoreControl.Controls.Add(this.radioButtonX3D);
            this.groupBoxCoreControl.Location = new System.Drawing.Point(3, 163);
            this.groupBoxCoreControl.Name = "groupBoxCoreControl";
            this.groupBoxCoreControl.Size = new System.Drawing.Size(331, 191);
            this.groupBoxCoreControl.TabIndex = 2;
            this.groupBoxCoreControl.TabStop = false;
            this.groupBoxCoreControl.Text = "Core Control";
            // 
            // panelX3D
            // 
            this.panelX3D.Controls.Add(this.button5);
            this.panelX3D.Controls.Add(this.button6);
            this.panelX3D.Location = new System.Drawing.Point(239, 12);
            this.panelX3D.Name = "panelX3D";
            this.panelX3D.Size = new System.Drawing.Size(89, 32);
            this.panelX3D.TabIndex = 9;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(8, 5);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(34, 23);
            this.button5.TabIndex = 5;
            this.button5.Text = "On";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.Button5_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(48, 5);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(35, 23);
            this.button6.TabIndex = 6;
            this.button6.Text = "Off";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.Button6_Click);
            // 
            // radioButtonManualCoreControl
            // 
            this.radioButtonManualCoreControl.AutoSize = true;
            this.radioButtonManualCoreControl.Location = new System.Drawing.Point(12, 41);
            this.radioButtonManualCoreControl.Name = "radioButtonManualCoreControl";
            this.radioButtonManualCoreControl.Size = new System.Drawing.Size(60, 17);
            this.radioButtonManualCoreControl.TabIndex = 4;
            this.radioButtonManualCoreControl.Text = "Manual";
            this.toolTip1.SetToolTip(this.radioButtonManualCoreControl, "Manual mode allows to pick specific cores to disable and/or SMT.");
            this.radioButtonManualCoreControl.UseVisualStyleBackColor = true;
            this.radioButtonManualCoreControl.CheckedChanged += new System.EventHandler(this.RadioButtonManualCoreControl_CheckedChanged);
            // 
            // panelManualCoreControl
            // 
            this.panelManualCoreControl.Controls.Add(this.label67);
            this.panelManualCoreControl.Controls.Add(this.tableLayoutPanel15);
            this.panelManualCoreControl.Controls.Add(this.buttonApplyCoreMap);
            this.panelManualCoreControl.Controls.Add(this.checkBoxSMT);
            this.panelManualCoreControl.Enabled = false;
            this.panelManualCoreControl.Location = new System.Drawing.Point(6, 64);
            this.panelManualCoreControl.Name = "panelManualCoreControl";
            this.panelManualCoreControl.Size = new System.Drawing.Size(322, 122);
            this.panelManualCoreControl.TabIndex = 8;
            // 
            // label67
            // 
            this.label67.AutoSize = true;
            this.label67.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label67.Location = new System.Drawing.Point(0, 102);
            this.label67.Name = "label67";
            this.label67.Size = new System.Drawing.Size(265, 13);
            this.label67.TabIndex = 2;
            this.label67.Text = "Deselect the cores you want to disable and click Apply";
            // 
            // tableLayoutPanel15
            // 
            this.tableLayoutPanel15.AutoSize = true;
            this.tableLayoutPanel15.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel15.ColumnCount = 8;
            this.tableLayoutPanel15.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel15.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel15.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel15.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel15.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel15.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel15.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel15.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel15.Controls.Add(this.labelCore15, 7, 4);
            this.tableLayoutPanel15.Controls.Add(this.labelCore14, 6, 4);
            this.tableLayoutPanel15.Controls.Add(this.labelCore13, 5, 4);
            this.tableLayoutPanel15.Controls.Add(this.labelCore12, 4, 4);
            this.tableLayoutPanel15.Controls.Add(this.labelCore11, 3, 4);
            this.tableLayoutPanel15.Controls.Add(this.labelCore10, 2, 4);
            this.tableLayoutPanel15.Controls.Add(this.labelCore9, 1, 4);
            this.tableLayoutPanel15.Controls.Add(this.labelCore8, 0, 4);
            this.tableLayoutPanel15.Controls.Add(this.checkBox15, 7, 3);
            this.tableLayoutPanel15.Controls.Add(this.checkBox14, 6, 3);
            this.tableLayoutPanel15.Controls.Add(this.checkBox13, 5, 3);
            this.tableLayoutPanel15.Controls.Add(this.checkBox12, 4, 3);
            this.tableLayoutPanel15.Controls.Add(this.checkBox11, 3, 3);
            this.tableLayoutPanel15.Controls.Add(this.checkBox10, 2, 3);
            this.tableLayoutPanel15.Controls.Add(this.checkBox9, 1, 3);
            this.tableLayoutPanel15.Controls.Add(this.checkBox8, 0, 3);
            this.tableLayoutPanel15.Controls.Add(this.labelCore7, 7, 1);
            this.tableLayoutPanel15.Controls.Add(this.labelCore6, 6, 1);
            this.tableLayoutPanel15.Controls.Add(this.labelCore5, 5, 1);
            this.tableLayoutPanel15.Controls.Add(this.labelCore4, 4, 1);
            this.tableLayoutPanel15.Controls.Add(this.labelCore3, 3, 1);
            this.tableLayoutPanel15.Controls.Add(this.labelCore2, 2, 1);
            this.tableLayoutPanel15.Controls.Add(this.labelCore1, 1, 1);
            this.tableLayoutPanel15.Controls.Add(this.checkBox7, 7, 0);
            this.tableLayoutPanel15.Controls.Add(this.checkBox6, 6, 0);
            this.tableLayoutPanel15.Controls.Add(this.checkBox5, 5, 0);
            this.tableLayoutPanel15.Controls.Add(this.checkBox4, 4, 0);
            this.tableLayoutPanel15.Controls.Add(this.checkBox3, 3, 0);
            this.tableLayoutPanel15.Controls.Add(this.checkBox2, 2, 0);
            this.tableLayoutPanel15.Controls.Add(this.checkBox1, 1, 0);
            this.tableLayoutPanel15.Controls.Add(this.checkBox0, 0, 0);
            this.tableLayoutPanel15.Controls.Add(this.labelCore0, 0, 1);
            this.tableLayoutPanel15.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel15.Name = "tableLayoutPanel15";
            this.tableLayoutPanel15.RowCount = 5;
            this.tableLayoutPanel15.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel15.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel15.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel15.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel15.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel15.Size = new System.Drawing.Size(224, 90);
            this.tableLayoutPanel15.TabIndex = 0;
            // 
            // labelCore15
            // 
            this.labelCore15.AutoSize = true;
            this.labelCore15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelCore15.Location = new System.Drawing.Point(199, 70);
            this.labelCore15.Name = "labelCore15";
            this.labelCore15.Size = new System.Drawing.Size(22, 20);
            this.labelCore15.TabIndex = 31;
            this.labelCore15.Text = "15";
            this.labelCore15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelCore14
            // 
            this.labelCore14.AutoSize = true;
            this.labelCore14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelCore14.Location = new System.Drawing.Point(171, 70);
            this.labelCore14.Name = "labelCore14";
            this.labelCore14.Size = new System.Drawing.Size(22, 20);
            this.labelCore14.TabIndex = 30;
            this.labelCore14.Text = "14";
            this.labelCore14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelCore13
            // 
            this.labelCore13.AutoSize = true;
            this.labelCore13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelCore13.Location = new System.Drawing.Point(143, 70);
            this.labelCore13.Name = "labelCore13";
            this.labelCore13.Size = new System.Drawing.Size(22, 20);
            this.labelCore13.TabIndex = 29;
            this.labelCore13.Text = "13";
            this.labelCore13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelCore12
            // 
            this.labelCore12.AutoSize = true;
            this.labelCore12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelCore12.Location = new System.Drawing.Point(115, 70);
            this.labelCore12.Name = "labelCore12";
            this.labelCore12.Size = new System.Drawing.Size(22, 20);
            this.labelCore12.TabIndex = 28;
            this.labelCore12.Text = "12";
            this.labelCore12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelCore11
            // 
            this.labelCore11.AutoSize = true;
            this.labelCore11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelCore11.Location = new System.Drawing.Point(87, 70);
            this.labelCore11.Name = "labelCore11";
            this.labelCore11.Size = new System.Drawing.Size(22, 20);
            this.labelCore11.TabIndex = 27;
            this.labelCore11.Text = "11";
            this.labelCore11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelCore10
            // 
            this.labelCore10.AutoSize = true;
            this.labelCore10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelCore10.Location = new System.Drawing.Point(59, 70);
            this.labelCore10.Name = "labelCore10";
            this.labelCore10.Size = new System.Drawing.Size(22, 20);
            this.labelCore10.TabIndex = 26;
            this.labelCore10.Text = "10";
            this.labelCore10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelCore9
            // 
            this.labelCore9.AutoSize = true;
            this.labelCore9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelCore9.Location = new System.Drawing.Point(31, 70);
            this.labelCore9.Name = "labelCore9";
            this.labelCore9.Size = new System.Drawing.Size(22, 20);
            this.labelCore9.TabIndex = 25;
            this.labelCore9.Text = "9";
            this.labelCore9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelCore8
            // 
            this.labelCore8.AutoSize = true;
            this.labelCore8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelCore8.Location = new System.Drawing.Point(3, 70);
            this.labelCore8.Name = "labelCore8";
            this.labelCore8.Size = new System.Drawing.Size(22, 20);
            this.labelCore8.TabIndex = 24;
            this.labelCore8.Text = "8";
            this.labelCore8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // checkBox15
            // 
            this.checkBox15.AutoSize = true;
            this.checkBox15.Enabled = false;
            this.checkBox15.Location = new System.Drawing.Point(203, 53);
            this.checkBox15.Margin = new System.Windows.Forms.Padding(7, 3, 0, 3);
            this.checkBox15.Name = "checkBox15";
            this.checkBox15.Size = new System.Drawing.Size(15, 14);
            this.checkBox15.TabIndex = 23;
            this.checkBox15.UseVisualStyleBackColor = true;
            // 
            // checkBox14
            // 
            this.checkBox14.AutoSize = true;
            this.checkBox14.Enabled = false;
            this.checkBox14.Location = new System.Drawing.Point(175, 53);
            this.checkBox14.Margin = new System.Windows.Forms.Padding(7, 3, 0, 3);
            this.checkBox14.Name = "checkBox14";
            this.checkBox14.Size = new System.Drawing.Size(15, 14);
            this.checkBox14.TabIndex = 22;
            this.checkBox14.UseVisualStyleBackColor = true;
            // 
            // checkBox13
            // 
            this.checkBox13.AutoSize = true;
            this.checkBox13.Enabled = false;
            this.checkBox13.Location = new System.Drawing.Point(147, 53);
            this.checkBox13.Margin = new System.Windows.Forms.Padding(7, 3, 0, 3);
            this.checkBox13.Name = "checkBox13";
            this.checkBox13.Size = new System.Drawing.Size(15, 14);
            this.checkBox13.TabIndex = 21;
            this.checkBox13.UseVisualStyleBackColor = true;
            // 
            // checkBox12
            // 
            this.checkBox12.AutoSize = true;
            this.checkBox12.Enabled = false;
            this.checkBox12.Location = new System.Drawing.Point(119, 53);
            this.checkBox12.Margin = new System.Windows.Forms.Padding(7, 3, 0, 3);
            this.checkBox12.Name = "checkBox12";
            this.checkBox12.Size = new System.Drawing.Size(15, 14);
            this.checkBox12.TabIndex = 20;
            this.checkBox12.UseVisualStyleBackColor = true;
            // 
            // checkBox11
            // 
            this.checkBox11.AutoSize = true;
            this.checkBox11.Enabled = false;
            this.checkBox11.Location = new System.Drawing.Point(91, 53);
            this.checkBox11.Margin = new System.Windows.Forms.Padding(7, 3, 0, 3);
            this.checkBox11.Name = "checkBox11";
            this.checkBox11.Size = new System.Drawing.Size(15, 14);
            this.checkBox11.TabIndex = 19;
            this.checkBox11.UseVisualStyleBackColor = true;
            // 
            // checkBox10
            // 
            this.checkBox10.AutoSize = true;
            this.checkBox10.Enabled = false;
            this.checkBox10.Location = new System.Drawing.Point(63, 53);
            this.checkBox10.Margin = new System.Windows.Forms.Padding(7, 3, 0, 3);
            this.checkBox10.Name = "checkBox10";
            this.checkBox10.Size = new System.Drawing.Size(15, 14);
            this.checkBox10.TabIndex = 18;
            this.checkBox10.UseVisualStyleBackColor = true;
            // 
            // checkBox9
            // 
            this.checkBox9.AutoSize = true;
            this.checkBox9.Enabled = false;
            this.checkBox9.Location = new System.Drawing.Point(35, 53);
            this.checkBox9.Margin = new System.Windows.Forms.Padding(7, 3, 0, 3);
            this.checkBox9.Name = "checkBox9";
            this.checkBox9.Size = new System.Drawing.Size(15, 14);
            this.checkBox9.TabIndex = 17;
            this.checkBox9.UseVisualStyleBackColor = true;
            // 
            // checkBox8
            // 
            this.checkBox8.AutoSize = true;
            this.checkBox8.Enabled = false;
            this.checkBox8.Location = new System.Drawing.Point(7, 53);
            this.checkBox8.Margin = new System.Windows.Forms.Padding(7, 3, 0, 3);
            this.checkBox8.Name = "checkBox8";
            this.checkBox8.Size = new System.Drawing.Size(15, 14);
            this.checkBox8.TabIndex = 16;
            this.checkBox8.UseVisualStyleBackColor = true;
            // 
            // labelCore7
            // 
            this.labelCore7.AutoSize = true;
            this.labelCore7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelCore7.Location = new System.Drawing.Point(199, 20);
            this.labelCore7.Name = "labelCore7";
            this.labelCore7.Size = new System.Drawing.Size(22, 20);
            this.labelCore7.TabIndex = 15;
            this.labelCore7.Text = "7";
            this.labelCore7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelCore6
            // 
            this.labelCore6.AutoSize = true;
            this.labelCore6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelCore6.Location = new System.Drawing.Point(171, 20);
            this.labelCore6.Name = "labelCore6";
            this.labelCore6.Size = new System.Drawing.Size(22, 20);
            this.labelCore6.TabIndex = 14;
            this.labelCore6.Text = "6";
            this.labelCore6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelCore5
            // 
            this.labelCore5.AutoSize = true;
            this.labelCore5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelCore5.Location = new System.Drawing.Point(143, 20);
            this.labelCore5.Name = "labelCore5";
            this.labelCore5.Size = new System.Drawing.Size(22, 20);
            this.labelCore5.TabIndex = 13;
            this.labelCore5.Text = "5";
            this.labelCore5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelCore4
            // 
            this.labelCore4.AutoSize = true;
            this.labelCore4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelCore4.Location = new System.Drawing.Point(115, 20);
            this.labelCore4.Name = "labelCore4";
            this.labelCore4.Size = new System.Drawing.Size(22, 20);
            this.labelCore4.TabIndex = 12;
            this.labelCore4.Text = "4";
            this.labelCore4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelCore3
            // 
            this.labelCore3.AutoSize = true;
            this.labelCore3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelCore3.Location = new System.Drawing.Point(87, 20);
            this.labelCore3.Name = "labelCore3";
            this.labelCore3.Size = new System.Drawing.Size(22, 20);
            this.labelCore3.TabIndex = 11;
            this.labelCore3.Text = "3";
            this.labelCore3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelCore2
            // 
            this.labelCore2.AutoSize = true;
            this.labelCore2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelCore2.Location = new System.Drawing.Point(59, 20);
            this.labelCore2.Name = "labelCore2";
            this.labelCore2.Size = new System.Drawing.Size(22, 20);
            this.labelCore2.TabIndex = 10;
            this.labelCore2.Text = "2";
            this.labelCore2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelCore1
            // 
            this.labelCore1.AutoSize = true;
            this.labelCore1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelCore1.Location = new System.Drawing.Point(31, 20);
            this.labelCore1.Name = "labelCore1";
            this.labelCore1.Size = new System.Drawing.Size(22, 20);
            this.labelCore1.TabIndex = 9;
            this.labelCore1.Text = "1";
            this.labelCore1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // checkBox7
            // 
            this.checkBox7.AutoSize = true;
            this.checkBox7.Enabled = false;
            this.checkBox7.Location = new System.Drawing.Point(203, 3);
            this.checkBox7.Margin = new System.Windows.Forms.Padding(7, 3, 0, 3);
            this.checkBox7.Name = "checkBox7";
            this.checkBox7.Size = new System.Drawing.Size(15, 14);
            this.checkBox7.TabIndex = 7;
            this.checkBox7.UseVisualStyleBackColor = true;
            // 
            // checkBox6
            // 
            this.checkBox6.AutoSize = true;
            this.checkBox6.Enabled = false;
            this.checkBox6.Location = new System.Drawing.Point(175, 3);
            this.checkBox6.Margin = new System.Windows.Forms.Padding(7, 3, 0, 3);
            this.checkBox6.Name = "checkBox6";
            this.checkBox6.Size = new System.Drawing.Size(15, 14);
            this.checkBox6.TabIndex = 6;
            this.checkBox6.UseVisualStyleBackColor = true;
            // 
            // checkBox5
            // 
            this.checkBox5.AutoSize = true;
            this.checkBox5.Enabled = false;
            this.checkBox5.Location = new System.Drawing.Point(147, 3);
            this.checkBox5.Margin = new System.Windows.Forms.Padding(7, 3, 0, 3);
            this.checkBox5.Name = "checkBox5";
            this.checkBox5.Size = new System.Drawing.Size(15, 14);
            this.checkBox5.TabIndex = 5;
            this.checkBox5.UseVisualStyleBackColor = true;
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.Enabled = false;
            this.checkBox4.Location = new System.Drawing.Point(119, 3);
            this.checkBox4.Margin = new System.Windows.Forms.Padding(7, 3, 0, 3);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(15, 14);
            this.checkBox4.TabIndex = 4;
            this.checkBox4.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Enabled = false;
            this.checkBox3.Location = new System.Drawing.Point(91, 3);
            this.checkBox3.Margin = new System.Windows.Forms.Padding(7, 3, 0, 3);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(15, 14);
            this.checkBox3.TabIndex = 3;
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Enabled = false;
            this.checkBox2.Location = new System.Drawing.Point(63, 3);
            this.checkBox2.Margin = new System.Windows.Forms.Padding(7, 3, 0, 3);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(15, 14);
            this.checkBox2.TabIndex = 2;
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Enabled = false;
            this.checkBox1.Location = new System.Drawing.Point(35, 3);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(7, 3, 0, 3);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(15, 14);
            this.checkBox1.TabIndex = 1;
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox0
            // 
            this.checkBox0.AutoSize = true;
            this.checkBox0.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox0.Enabled = false;
            this.checkBox0.Location = new System.Drawing.Point(7, 3);
            this.checkBox0.Margin = new System.Windows.Forms.Padding(7, 3, 0, 3);
            this.checkBox0.Name = "checkBox0";
            this.checkBox0.Size = new System.Drawing.Size(15, 14);
            this.checkBox0.TabIndex = 0;
            this.checkBox0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox0.UseVisualStyleBackColor = true;
            // 
            // labelCore0
            // 
            this.labelCore0.AutoSize = true;
            this.labelCore0.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelCore0.Location = new System.Drawing.Point(3, 20);
            this.labelCore0.Name = "labelCore0";
            this.labelCore0.Size = new System.Drawing.Size(22, 20);
            this.labelCore0.TabIndex = 8;
            this.labelCore0.Text = "0";
            this.labelCore0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonApplyCoreMap
            // 
            this.buttonApplyCoreMap.Location = new System.Drawing.Point(241, 67);
            this.buttonApplyCoreMap.Name = "buttonApplyCoreMap";
            this.buttonApplyCoreMap.Size = new System.Drawing.Size(75, 23);
            this.buttonApplyCoreMap.TabIndex = 1;
            this.buttonApplyCoreMap.Text = "Apply";
            this.buttonApplyCoreMap.UseVisualStyleBackColor = true;
            this.buttonApplyCoreMap.Click += new System.EventHandler(this.ButtonApplyCoreMap_Click);
            // 
            // checkBoxSMT
            // 
            this.checkBoxSMT.AutoSize = true;
            this.checkBoxSMT.Location = new System.Drawing.Point(241, 2);
            this.checkBoxSMT.Name = "checkBoxSMT";
            this.checkBoxSMT.Size = new System.Drawing.Size(49, 17);
            this.checkBoxSMT.TabIndex = 3;
            this.checkBoxSMT.Text = "SMT";
            this.checkBoxSMT.UseVisualStyleBackColor = true;
            // 
            // radioButtonX3D
            // 
            this.radioButtonX3D.AutoSize = true;
            this.radioButtonX3D.Checked = true;
            this.radioButtonX3D.Location = new System.Drawing.Point(12, 18);
            this.radioButtonX3D.Name = "radioButtonX3D";
            this.radioButtonX3D.Size = new System.Drawing.Size(107, 17);
            this.radioButtonX3D.TabIndex = 7;
            this.radioButtonX3D.TabStop = true;
            this.radioButtonX3D.Text = "X3D Turbo Mode";
            this.toolTip1.SetToolTip(this.radioButtonX3D, "X3D Mode disables SMT and second CCD module if available");
            this.radioButtonX3D.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.AutoSize = true;
            this.tableLayoutPanel8.ColumnCount = 4;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
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
            this.tableLayoutPanel8.Size = new System.Drawing.Size(331, 159);
            this.tableLayoutPanel8.TabIndex = 1;
            // 
            // buttonApplyPROCHOT
            // 
            this.buttonApplyPROCHOT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonApplyPROCHOT.Location = new System.Drawing.Point(247, 128);
            this.buttonApplyPROCHOT.Name = "buttonApplyPROCHOT";
            this.buttonApplyPROCHOT.Size = new System.Drawing.Size(76, 23);
            this.buttonApplyPROCHOT.TabIndex = 12;
            this.buttonApplyPROCHOT.Text = "Apply";
            this.buttonApplyPROCHOT.UseVisualStyleBackColor = true;
            this.buttonApplyPROCHOT.Click += new System.EventHandler(this.ButtonApplyPROCHOT_Click);
            // 
            // buttonApplySC
            // 
            this.buttonApplySC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonApplySC.Location = new System.Drawing.Point(247, 88);
            this.buttonApplySC.Name = "buttonApplySC";
            this.buttonApplySC.Size = new System.Drawing.Size(76, 24);
            this.buttonApplySC.TabIndex = 5;
            this.buttonApplySC.Text = "Apply";
            this.buttonApplySC.UseVisualStyleBackColor = true;
            this.buttonApplySC.Click += new System.EventHandler(this.ButtonApplySC_Click);
            // 
            // buttonApplyAC
            // 
            this.buttonApplyAC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonApplyAC.Location = new System.Drawing.Point(247, 28);
            this.buttonApplyAC.Name = "buttonApplyAC";
            this.buttonApplyAC.Size = new System.Drawing.Size(76, 24);
            this.buttonApplyAC.TabIndex = 2;
            this.buttonApplyAC.Text = "Apply";
            this.buttonApplyAC.UseVisualStyleBackColor = true;
            this.buttonApplyAC.Click += new System.EventHandler(this.ButtonApplyAC_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.tableLayoutPanel8.SetColumnSpan(this.label14, 4);
            this.label14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label14.Location = new System.Drawing.Point(3, 5);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(320, 20);
            this.label14.TabIndex = 6;
            this.label14.Text = "All Core Frequency";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.tableLayoutPanel8.SetColumnSpan(this.label16, 4);
            this.label16.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label16.Location = new System.Drawing.Point(3, 65);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(320, 20);
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
            this.comboBoxACF.Size = new System.Drawing.Size(94, 21);
            this.comboBoxACF.TabIndex = 9;
            // 
            // comboBoxSCF
            // 
            this.comboBoxSCF.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxSCF.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSCF.FormattingEnabled = true;
            this.comboBoxSCF.Location = new System.Drawing.Point(3, 88);
            this.comboBoxSCF.Name = "comboBoxSCF";
            this.comboBoxSCF.Size = new System.Drawing.Size(94, 21);
            this.comboBoxSCF.TabIndex = 10;
            // 
            // comboBoxCore
            // 
            this.comboBoxCore.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxCore.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCore.FormattingEnabled = true;
            this.comboBoxCore.Location = new System.Drawing.Point(103, 88);
            this.comboBoxCore.Name = "comboBoxCore";
            this.comboBoxCore.Size = new System.Drawing.Size(94, 21);
            this.comboBoxCore.TabIndex = 8;
            // 
            // checkBoxPROCHOT
            // 
            this.checkBoxPROCHOT.AutoSize = true;
            this.tableLayoutPanel8.SetColumnSpan(this.checkBoxPROCHOT, 3);
            this.checkBoxPROCHOT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkBoxPROCHOT.Location = new System.Drawing.Point(3, 128);
            this.checkBoxPROCHOT.Name = "checkBoxPROCHOT";
            this.checkBoxPROCHOT.Size = new System.Drawing.Size(238, 23);
            this.checkBoxPROCHOT.TabIndex = 11;
            this.checkBoxPROCHOT.Text = "PROCHOT";
            this.checkBoxPROCHOT.UseVisualStyleBackColor = true;
            // 
            // tabPageSmu
            // 
            this.tabPageSmu.Controls.Add(this.tableLayoutPanel7);
            this.tabPageSmu.Controls.Add(this.tableLayoutPanel6);
            this.tabPageSmu.Location = new System.Drawing.Point(4, 40);
            this.tabPageSmu.Margin = new System.Windows.Forms.Padding(0);
            this.tabPageSmu.Name = "tabPageSmu";
            this.tabPageSmu.Size = new System.Drawing.Size(337, 356);
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
            this.tableLayoutPanel7.Location = new System.Drawing.Point(0, 185);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 1;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel7.Size = new System.Drawing.Size(337, 0);
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
            this.tableLayoutPanel6.Size = new System.Drawing.Size(337, 185);
            this.tableLayoutPanel6.TabIndex = 3;
            // 
            // tabPagePci
            // 
            this.tabPagePci.Controls.Add(this.tableLayoutPanel4);
            this.tabPagePci.Location = new System.Drawing.Point(4, 40);
            this.tabPagePci.Name = "tabPagePci";
            this.tabPagePci.Padding = new System.Windows.Forms.Padding(3);
            this.tabPagePci.Size = new System.Drawing.Size(337, 356);
            this.tabPagePci.TabIndex = 1;
            this.tabPagePci.Text = "PCI";
            this.tabPagePci.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.AutoSize = true;
            this.tableLayoutPanel4.ColumnCount = 4;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
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
            this.tableLayoutPanel4.Size = new System.Drawing.Size(331, 151);
            this.tableLayoutPanel4.TabIndex = 0;
            // 
            // buttonPciWrite
            // 
            this.buttonPciWrite.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonPciWrite.Location = new System.Drawing.Point(248, 39);
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
            this.label5.Size = new System.Drawing.Size(94, 31);
            this.label5.TabIndex = 0;
            this.label5.Text = "PCI Register";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxPciAddress
            // 
            this.textBoxPciAddress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxPciAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxPciAddress.Location = new System.Drawing.Point(104, 9);
            this.textBoxPciAddress.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxPciAddress.Name = "textBoxPciAddress";
            this.textBoxPciAddress.Size = new System.Drawing.Size(92, 20);
            this.textBoxPciAddress.TabIndex = 1;
            this.textBoxPciAddress.Text = "0x0";
            this.textBoxPciAddress.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxPciAddress_KeyDown);
            // 
            // buttonPciRead
            // 
            this.buttonPciRead.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonPciRead.Location = new System.Drawing.Point(248, 8);
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
            this.label7.Size = new System.Drawing.Size(94, 31);
            this.label7.TabIndex = 3;
            this.label7.Text = "Value";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxPciValue
            // 
            this.textBoxPciValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxPciValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxPciValue.Location = new System.Drawing.Point(104, 40);
            this.textBoxPciValue.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxPciValue.Name = "textBoxPciValue";
            this.textBoxPciValue.Size = new System.Drawing.Size(92, 20);
            this.textBoxPciValue.TabIndex = 4;
            this.textBoxPciValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxPciValue_KeyDown);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label12.Location = new System.Drawing.Point(3, 87);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(94, 31);
            this.label12.TabIndex = 6;
            this.label12.Text = "Start Register";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label13.Location = new System.Drawing.Point(3, 118);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(94, 28);
            this.label13.TabIndex = 7;
            this.label13.Text = "End Register";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxPciStartReg
            // 
            this.textBoxPciStartReg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxPciStartReg.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxPciStartReg.Location = new System.Drawing.Point(104, 91);
            this.textBoxPciStartReg.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxPciStartReg.Name = "textBoxPciStartReg";
            this.textBoxPciStartReg.Size = new System.Drawing.Size(92, 20);
            this.textBoxPciStartReg.TabIndex = 8;
            this.textBoxPciStartReg.Text = "0x50000";
            // 
            // textBoxPciEndReg
            // 
            this.textBoxPciEndReg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxPciEndReg.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxPciEndReg.Location = new System.Drawing.Point(104, 122);
            this.textBoxPciEndReg.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxPciEndReg.Name = "textBoxPciEndReg";
            this.textBoxPciEndReg.Size = new System.Drawing.Size(92, 20);
            this.textBoxPciEndReg.TabIndex = 9;
            this.textBoxPciEndReg.Text = "0x50300";
            // 
            // buttonPciScan
            // 
            this.buttonPciScan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonPciScan.Location = new System.Drawing.Point(248, 90);
            this.buttonPciScan.Name = "buttonPciScan";
            this.buttonPciScan.Size = new System.Drawing.Size(75, 25);
            this.buttonPciScan.TabIndex = 10;
            this.buttonPciScan.Text = "Scan";
            this.buttonPciScan.UseVisualStyleBackColor = true;
            this.buttonPciScan.Click += new System.EventHandler(this.ButtonPciScan_Click);
            // 
            // tabPageMsr
            // 
            this.tabPageMsr.Controls.Add(this.tableLayoutPanel9);
            this.tabPageMsr.Location = new System.Drawing.Point(4, 40);
            this.tabPageMsr.Name = "tabPageMsr";
            this.tabPageMsr.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMsr.Size = new System.Drawing.Size(337, 356);
            this.tabPageMsr.TabIndex = 5;
            this.tabPageMsr.Text = "MSR";
            this.tabPageMsr.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel9
            // 
            this.tableLayoutPanel9.AutoSize = true;
            this.tableLayoutPanel9.ColumnCount = 4;
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
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
            this.tableLayoutPanel9.Size = new System.Drawing.Size(331, 178);
            this.tableLayoutPanel9.TabIndex = 1;
            // 
            // buttonMsrWrite
            // 
            this.buttonMsrWrite.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonMsrWrite.Location = new System.Drawing.Point(247, 66);
            this.buttonMsrWrite.Name = "buttonMsrWrite";
            this.buttonMsrWrite.Size = new System.Drawing.Size(76, 25);
            this.buttonMsrWrite.TabIndex = 5;
            this.buttonMsrWrite.Text = "Write";
            this.buttonMsrWrite.UseVisualStyleBackColor = true;
            this.buttonMsrWrite.Click += new System.EventHandler(this.ButtonMsrWrite_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label15.Location = new System.Drawing.Point(3, 5);
            this.label15.MinimumSize = new System.Drawing.Size(77, 27);
            this.label15.Name = "label15";
            this.label15.Padding = new System.Windows.Forms.Padding(0, 5, 5, 5);
            this.label15.Size = new System.Drawing.Size(94, 31);
            this.label15.TabIndex = 0;
            this.label15.Text = "MSR";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxMsrAddress
            // 
            this.textBoxMsrAddress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxMsrAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxMsrAddress.Location = new System.Drawing.Point(104, 9);
            this.textBoxMsrAddress.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxMsrAddress.Name = "textBoxMsrAddress";
            this.textBoxMsrAddress.Size = new System.Drawing.Size(92, 20);
            this.textBoxMsrAddress.TabIndex = 1;
            this.textBoxMsrAddress.Text = "0xC0011000";
            // 
            // buttonMsrRead
            // 
            this.buttonMsrRead.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonMsrRead.Location = new System.Drawing.Point(247, 8);
            this.buttonMsrRead.Name = "buttonMsrRead";
            this.buttonMsrRead.Size = new System.Drawing.Size(76, 25);
            this.buttonMsrRead.TabIndex = 2;
            this.buttonMsrRead.Text = "Read";
            this.buttonMsrRead.UseVisualStyleBackColor = true;
            this.buttonMsrRead.Click += new System.EventHandler(this.ButtonMsrRead_Click);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label17.Location = new System.Drawing.Point(3, 36);
            this.label17.MinimumSize = new System.Drawing.Size(77, 27);
            this.label17.Name = "label17";
            this.label17.Padding = new System.Windows.Forms.Padding(0, 5, 5, 5);
            this.label17.Size = new System.Drawing.Size(94, 27);
            this.label17.TabIndex = 3;
            this.label17.Text = "EDX (63 - 32)";
            this.label17.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // textBoxMsrEdx
            // 
            this.textBoxMsrEdx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxMsrEdx.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxMsrEdx.Location = new System.Drawing.Point(4, 67);
            this.textBoxMsrEdx.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxMsrEdx.Name = "textBoxMsrEdx";
            this.textBoxMsrEdx.Size = new System.Drawing.Size(92, 20);
            this.textBoxMsrEdx.TabIndex = 4;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label18.Location = new System.Drawing.Point(3, 114);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(94, 31);
            this.label18.TabIndex = 6;
            this.label18.Text = "Start Address";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label19.Location = new System.Drawing.Point(3, 145);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(94, 28);
            this.label19.TabIndex = 7;
            this.label19.Text = "End Address";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxMsrStart
            // 
            this.textBoxMsrStart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxMsrStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxMsrStart.Location = new System.Drawing.Point(104, 118);
            this.textBoxMsrStart.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxMsrStart.Name = "textBoxMsrStart";
            this.textBoxMsrStart.Size = new System.Drawing.Size(92, 20);
            this.textBoxMsrStart.TabIndex = 8;
            this.textBoxMsrStart.Text = "0xC0011000";
            // 
            // textBoxMsrEnd
            // 
            this.textBoxMsrEnd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxMsrEnd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxMsrEnd.Location = new System.Drawing.Point(104, 149);
            this.textBoxMsrEnd.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxMsrEnd.Name = "textBoxMsrEnd";
            this.textBoxMsrEnd.Size = new System.Drawing.Size(92, 20);
            this.textBoxMsrEnd.TabIndex = 9;
            this.textBoxMsrEnd.Text = "0xC0011FFF";
            // 
            // buttonMsrScan
            // 
            this.buttonMsrScan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonMsrScan.Location = new System.Drawing.Point(247, 117);
            this.buttonMsrScan.Name = "buttonMsrScan";
            this.buttonMsrScan.Size = new System.Drawing.Size(76, 25);
            this.buttonMsrScan.TabIndex = 10;
            this.buttonMsrScan.Text = "Scan";
            this.buttonMsrScan.UseVisualStyleBackColor = true;
            this.buttonMsrScan.Click += new System.EventHandler(this.ButtonMsrScan_Click);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label20.Location = new System.Drawing.Point(103, 36);
            this.label20.MinimumSize = new System.Drawing.Size(77, 27);
            this.label20.Name = "label20";
            this.label20.Padding = new System.Windows.Forms.Padding(0, 5, 5, 5);
            this.label20.Size = new System.Drawing.Size(94, 27);
            this.label20.TabIndex = 11;
            this.label20.Text = "EAX (31 - 0)";
            this.label20.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // textBoxMsrEax
            // 
            this.textBoxMsrEax.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxMsrEax.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxMsrEax.Location = new System.Drawing.Point(104, 67);
            this.textBoxMsrEax.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxMsrEax.Name = "textBoxMsrEax";
            this.textBoxMsrEax.Size = new System.Drawing.Size(92, 20);
            this.textBoxMsrEax.TabIndex = 12;
            // 
            // tabPageCPUID
            // 
            this.tabPageCPUID.Controls.Add(this.tableLayoutPanel14);
            this.tabPageCPUID.Controls.Add(this.tableLayoutPanel10);
            this.tabPageCPUID.Location = new System.Drawing.Point(4, 40);
            this.tabPageCPUID.Name = "tabPageCPUID";
            this.tabPageCPUID.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCPUID.Size = new System.Drawing.Size(337, 356);
            this.tabPageCPUID.TabIndex = 6;
            this.tabPageCPUID.Text = "CPUID";
            this.tabPageCPUID.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel14
            // 
            this.tableLayoutPanel14.ColumnCount = 4;
            this.tableLayoutPanel14.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel14.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel14.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel14.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel14.Controls.Add(this.label50, 0, 0);
            this.tableLayoutPanel14.Controls.Add(this.textBoxCpuid, 1, 0);
            this.tableLayoutPanel14.Controls.Add(this.buttonCpuidDecode, 3, 0);
            this.tableLayoutPanel14.Location = new System.Drawing.Point(3, 183);
            this.tableLayoutPanel14.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel14.Name = "tableLayoutPanel14";
            this.tableLayoutPanel14.Padding = new System.Windows.Forms.Padding(0, 4, 4, 4);
            this.tableLayoutPanel14.RowCount = 1;
            this.tableLayoutPanel14.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel14.Size = new System.Drawing.Size(334, 37);
            this.tableLayoutPanel14.TabIndex = 3;
            this.tableLayoutPanel14.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel14_Paint);
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label50.Location = new System.Drawing.Point(2, 4);
            this.label50.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label50.MinimumSize = new System.Drawing.Size(58, 22);
            this.label50.Name = "label50";
            this.label50.Padding = new System.Windows.Forms.Padding(0, 4, 4, 4);
            this.label50.Size = new System.Drawing.Size(96, 32);
            this.label50.TabIndex = 4;
            this.label50.Text = "CPUID";
            // 
            // textBoxCpuid
            // 
            this.textBoxCpuid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxCpuid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxCpuid.Location = new System.Drawing.Point(103, 7);
            this.textBoxCpuid.Name = "textBoxCpuid";
            this.textBoxCpuid.Size = new System.Drawing.Size(94, 20);
            this.textBoxCpuid.TabIndex = 2;
            // 
            // buttonCpuidDecode
            // 
            this.buttonCpuidDecode.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonCpuidDecode.Location = new System.Drawing.Point(251, 6);
            this.buttonCpuidDecode.Margin = new System.Windows.Forms.Padding(2);
            this.buttonCpuidDecode.Name = "buttonCpuidDecode";
            this.buttonCpuidDecode.Padding = new System.Windows.Forms.Padding(3);
            this.buttonCpuidDecode.Size = new System.Drawing.Size(77, 28);
            this.buttonCpuidDecode.TabIndex = 0;
            this.buttonCpuidDecode.Text = "Decode";
            this.buttonCpuidDecode.UseVisualStyleBackColor = true;
            this.buttonCpuidDecode.Click += new System.EventHandler(this.ButtonCpuidDecode_Click);
            // 
            // tableLayoutPanel10
            // 
            this.tableLayoutPanel10.AutoSize = true;
            this.tableLayoutPanel10.ColumnCount = 4;
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel10.Controls.Add(this.label21, 0, 0);
            this.tableLayoutPanel10.Controls.Add(this.textBoxCPUIDAddress, 1, 0);
            this.tableLayoutPanel10.Controls.Add(this.buttonCPUIDRead, 3, 0);
            this.tableLayoutPanel10.Controls.Add(this.label25, 0, 1);
            this.tableLayoutPanel10.Controls.Add(this.textBoxCPUIDeax, 1, 1);
            this.tableLayoutPanel10.Controls.Add(this.label22, 0, 4);
            this.tableLayoutPanel10.Controls.Add(this.label26, 0, 2);
            this.tableLayoutPanel10.Controls.Add(this.textBoxCPUIDedx, 1, 4);
            this.tableLayoutPanel10.Controls.Add(this.label27, 0, 3);
            this.tableLayoutPanel10.Controls.Add(this.textBoxCPUIDebx, 1, 2);
            this.tableLayoutPanel10.Controls.Add(this.textBoxCPUIDecx, 1, 3);
            this.tableLayoutPanel10.Controls.Add(this.buttonCPUIDScan, 3, 1);
            this.tableLayoutPanel10.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel10.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel10.Name = "tableLayoutPanel10";
            this.tableLayoutPanel10.Padding = new System.Windows.Forms.Padding(0, 5, 5, 5);
            this.tableLayoutPanel10.RowCount = 5;
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel10.Size = new System.Drawing.Size(331, 154);
            this.tableLayoutPanel10.TabIndex = 2;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(3, 5);
            this.label21.MinimumSize = new System.Drawing.Size(77, 27);
            this.label21.Name = "label21";
            this.label21.Padding = new System.Windows.Forms.Padding(0, 5, 5, 5);
            this.label21.Size = new System.Drawing.Size(77, 27);
            this.label21.TabIndex = 0;
            this.label21.Text = "CPUID";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxCPUIDAddress
            // 
            this.textBoxCPUIDAddress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxCPUIDAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxCPUIDAddress.Location = new System.Drawing.Point(104, 9);
            this.textBoxCPUIDAddress.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxCPUIDAddress.Name = "textBoxCPUIDAddress";
            this.textBoxCPUIDAddress.Size = new System.Drawing.Size(92, 20);
            this.textBoxCPUIDAddress.TabIndex = 1;
            this.textBoxCPUIDAddress.Text = "0x00000001";
            // 
            // buttonCPUIDRead
            // 
            this.buttonCPUIDRead.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonCPUIDRead.Location = new System.Drawing.Point(246, 8);
            this.buttonCPUIDRead.Name = "buttonCPUIDRead";
            this.buttonCPUIDRead.Size = new System.Drawing.Size(77, 25);
            this.buttonCPUIDRead.TabIndex = 2;
            this.buttonCPUIDRead.Text = "Read";
            this.buttonCPUIDRead.UseVisualStyleBackColor = true;
            this.buttonCPUIDRead.Click += new System.EventHandler(this.ButtonCPUIDRead_Click);
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label25.Location = new System.Drawing.Point(3, 36);
            this.label25.MinimumSize = new System.Drawing.Size(77, 27);
            this.label25.Name = "label25";
            this.label25.Padding = new System.Windows.Forms.Padding(0, 5, 5, 5);
            this.label25.Size = new System.Drawing.Size(94, 29);
            this.label25.TabIndex = 11;
            this.label25.Text = "EAX";
            this.label25.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // textBoxCPUIDeax
            // 
            this.textBoxCPUIDeax.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxCPUIDeax.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxCPUIDeax.Location = new System.Drawing.Point(104, 40);
            this.textBoxCPUIDeax.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxCPUIDeax.Name = "textBoxCPUIDeax";
            this.textBoxCPUIDeax.ReadOnly = true;
            this.textBoxCPUIDeax.Size = new System.Drawing.Size(92, 20);
            this.textBoxCPUIDeax.TabIndex = 12;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label22.Location = new System.Drawing.Point(3, 121);
            this.label22.MinimumSize = new System.Drawing.Size(77, 27);
            this.label22.Name = "label22";
            this.label22.Padding = new System.Windows.Forms.Padding(0, 5, 5, 5);
            this.label22.Size = new System.Drawing.Size(94, 28);
            this.label22.TabIndex = 3;
            this.label22.Text = "EDX";
            this.label22.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label26.Location = new System.Drawing.Point(3, 65);
            this.label26.MinimumSize = new System.Drawing.Size(77, 27);
            this.label26.Name = "label26";
            this.label26.Padding = new System.Windows.Forms.Padding(0, 5, 5, 5);
            this.label26.Size = new System.Drawing.Size(94, 28);
            this.label26.TabIndex = 13;
            this.label26.Text = "EBX";
            this.label26.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // textBoxCPUIDedx
            // 
            this.textBoxCPUIDedx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxCPUIDedx.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxCPUIDedx.Location = new System.Drawing.Point(104, 125);
            this.textBoxCPUIDedx.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxCPUIDedx.Name = "textBoxCPUIDedx";
            this.textBoxCPUIDedx.ReadOnly = true;
            this.textBoxCPUIDedx.Size = new System.Drawing.Size(92, 20);
            this.textBoxCPUIDedx.TabIndex = 4;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label27.Location = new System.Drawing.Point(3, 93);
            this.label27.MinimumSize = new System.Drawing.Size(77, 27);
            this.label27.Name = "label27";
            this.label27.Padding = new System.Windows.Forms.Padding(0, 5, 5, 5);
            this.label27.Size = new System.Drawing.Size(94, 28);
            this.label27.TabIndex = 14;
            this.label27.Text = "ECX";
            this.label27.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // textBoxCPUIDebx
            // 
            this.textBoxCPUIDebx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxCPUIDebx.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxCPUIDebx.Location = new System.Drawing.Point(104, 69);
            this.textBoxCPUIDebx.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxCPUIDebx.Name = "textBoxCPUIDebx";
            this.textBoxCPUIDebx.ReadOnly = true;
            this.textBoxCPUIDebx.Size = new System.Drawing.Size(92, 20);
            this.textBoxCPUIDebx.TabIndex = 15;
            // 
            // textBoxCPUIDecx
            // 
            this.textBoxCPUIDecx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxCPUIDecx.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxCPUIDecx.Location = new System.Drawing.Point(104, 97);
            this.textBoxCPUIDecx.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxCPUIDecx.Name = "textBoxCPUIDecx";
            this.textBoxCPUIDecx.ReadOnly = true;
            this.textBoxCPUIDecx.Size = new System.Drawing.Size(92, 20);
            this.textBoxCPUIDecx.TabIndex = 16;
            // 
            // buttonCPUIDScan
            // 
            this.buttonCPUIDScan.Location = new System.Drawing.Point(246, 39);
            this.buttonCPUIDScan.Name = "buttonCPUIDScan";
            this.buttonCPUIDScan.Size = new System.Drawing.Size(77, 23);
            this.buttonCPUIDScan.TabIndex = 10;
            this.buttonCPUIDScan.Text = "Scan";
            this.buttonCPUIDScan.UseVisualStyleBackColor = true;
            this.buttonCPUIDScan.Click += new System.EventHandler(this.ButtonCPUIDScan_Click);
            // 
            // tabPagePbo
            // 
            this.tabPagePbo.Controls.Add(this.tableLayoutPanel12);
            this.tabPagePbo.Location = new System.Drawing.Point(4, 40);
            this.tabPagePbo.Name = "tabPagePbo";
            this.tabPagePbo.Size = new System.Drawing.Size(337, 356);
            this.tabPagePbo.TabIndex = 7;
            this.tabPagePbo.Text = "PBO";
            this.tabPagePbo.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel12
            // 
            this.tableLayoutPanel12.ColumnCount = 6;
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel12.Controls.Add(this.numericUpDownCO_8, 3, 1);
            this.tableLayoutPanel12.Controls.Add(this.buttonApplyCO, 5, 1);
            this.tableLayoutPanel12.Controls.Add(this.label31, 0, 1);
            this.tableLayoutPanel12.Controls.Add(this.label32, 0, 2);
            this.tableLayoutPanel12.Controls.Add(this.label33, 0, 3);
            this.tableLayoutPanel12.Controls.Add(this.label34, 0, 4);
            this.tableLayoutPanel12.Controls.Add(this.label35, 0, 5);
            this.tableLayoutPanel12.Controls.Add(this.label36, 0, 6);
            this.tableLayoutPanel12.Controls.Add(this.label37, 0, 7);
            this.tableLayoutPanel12.Controls.Add(this.label38, 0, 8);
            this.tableLayoutPanel12.Controls.Add(this.label39, 2, 1);
            this.tableLayoutPanel12.Controls.Add(this.label40, 2, 2);
            this.tableLayoutPanel12.Controls.Add(this.label41, 2, 3);
            this.tableLayoutPanel12.Controls.Add(this.label42, 2, 4);
            this.tableLayoutPanel12.Controls.Add(this.label43, 2, 5);
            this.tableLayoutPanel12.Controls.Add(this.label44, 2, 6);
            this.tableLayoutPanel12.Controls.Add(this.label45, 2, 7);
            this.tableLayoutPanel12.Controls.Add(this.label46, 2, 8);
            this.tableLayoutPanel12.Controls.Add(this.buttonGetCO, 5, 2);
            this.tableLayoutPanel12.Controls.Add(this.numericUpDownCO_0, 1, 1);
            this.tableLayoutPanel12.Controls.Add(this.numericUpDownCO_1, 1, 2);
            this.tableLayoutPanel12.Controls.Add(this.numericUpDownCO_2, 1, 3);
            this.tableLayoutPanel12.Controls.Add(this.numericUpDownCO_3, 1, 4);
            this.tableLayoutPanel12.Controls.Add(this.numericUpDownCO_4, 1, 5);
            this.tableLayoutPanel12.Controls.Add(this.numericUpDownCO_5, 1, 6);
            this.tableLayoutPanel12.Controls.Add(this.numericUpDownCO_6, 1, 7);
            this.tableLayoutPanel12.Controls.Add(this.numericUpDownCO_7, 1, 8);
            this.tableLayoutPanel12.Controls.Add(this.numericUpDownCO_9, 3, 2);
            this.tableLayoutPanel12.Controls.Add(this.numericUpDownCO_10, 3, 3);
            this.tableLayoutPanel12.Controls.Add(this.numericUpDownCO_11, 3, 4);
            this.tableLayoutPanel12.Controls.Add(this.numericUpDownCO_12, 3, 5);
            this.tableLayoutPanel12.Controls.Add(this.numericUpDownCO_13, 3, 6);
            this.tableLayoutPanel12.Controls.Add(this.numericUpDownCO_14, 3, 7);
            this.tableLayoutPanel12.Controls.Add(this.numericUpDownCO_15, 3, 8);
            this.tableLayoutPanel12.Controls.Add(this.button1, 1, 0);
            this.tableLayoutPanel12.Controls.Add(this.button2, 1, 9);
            this.tableLayoutPanel12.Controls.Add(this.button3, 3, 0);
            this.tableLayoutPanel12.Controls.Add(this.button4, 3, 9);
            this.tableLayoutPanel12.Controls.Add(this.btnSaveCOProfile, 5, 3);
            this.tableLayoutPanel12.Controls.Add(this.btnLoadCOProfile, 5, 4);
            this.tableLayoutPanel12.Controls.Add(this.checkBoxApplyCOStartup, 0, 10);
            this.tableLayoutPanel12.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel12.Name = "tableLayoutPanel12";
            this.tableLayoutPanel12.RowCount = 11;
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel12.Size = new System.Drawing.Size(331, 315);
            this.tableLayoutPanel12.TabIndex = 0;
            // 
            // numericUpDownCO_8
            // 
            this.numericUpDownCO_8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericUpDownCO_8.Enabled = false;
            this.numericUpDownCO_8.Location = new System.Drawing.Point(173, 31);
            this.numericUpDownCO_8.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDownCO_8.Minimum = new decimal(new int[] {
            999,
            0,
            0,
            -2147483648});
            this.numericUpDownCO_8.Name = "numericUpDownCO_8";
            this.numericUpDownCO_8.Size = new System.Drawing.Size(44, 20);
            this.numericUpDownCO_8.TabIndex = 41;
            // 
            // buttonApplyCO
            // 
            this.buttonApplyCO.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonApplyCO.Location = new System.Drawing.Point(243, 31);
            this.buttonApplyCO.Name = "buttonApplyCO";
            this.buttonApplyCO.Size = new System.Drawing.Size(85, 22);
            this.buttonApplyCO.TabIndex = 5;
            this.buttonApplyCO.Text = "Apply";
            this.buttonApplyCO.UseVisualStyleBackColor = true;
            this.buttonApplyCO.Click += new System.EventHandler(this.ButtonApplyCO_Click);
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label31.Location = new System.Drawing.Point(3, 28);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(54, 28);
            this.label31.TabIndex = 0;
            this.label31.Text = "Core 0";
            this.label31.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label32.Location = new System.Drawing.Point(3, 56);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(54, 28);
            this.label32.TabIndex = 1;
            this.label32.Text = "Core 1";
            this.label32.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label33.Location = new System.Drawing.Point(3, 84);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(54, 28);
            this.label33.TabIndex = 2;
            this.label33.Text = "Core 2";
            this.label33.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label34.Location = new System.Drawing.Point(3, 112);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(54, 28);
            this.label34.TabIndex = 3;
            this.label34.Text = "Core 3";
            this.label34.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label35.Location = new System.Drawing.Point(3, 140);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(54, 28);
            this.label35.TabIndex = 4;
            this.label35.Text = "Core 4";
            this.label35.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label36.Location = new System.Drawing.Point(3, 168);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(54, 28);
            this.label36.TabIndex = 5;
            this.label36.Text = "Core 5";
            this.label36.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label37.Location = new System.Drawing.Point(3, 196);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(54, 28);
            this.label37.TabIndex = 6;
            this.label37.Text = "Core 6";
            this.label37.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label38.Location = new System.Drawing.Point(3, 224);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(54, 28);
            this.label38.TabIndex = 7;
            this.label38.Text = "Core 7";
            this.label38.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label39.Location = new System.Drawing.Point(113, 28);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(54, 28);
            this.label39.TabIndex = 8;
            this.label39.Text = "Core 8";
            this.label39.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label40.Location = new System.Drawing.Point(113, 56);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(54, 28);
            this.label40.TabIndex = 9;
            this.label40.Text = "Core 9";
            this.label40.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label41.Location = new System.Drawing.Point(113, 84);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(54, 28);
            this.label41.TabIndex = 10;
            this.label41.Text = "Core 10";
            this.label41.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label42.Location = new System.Drawing.Point(113, 112);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(54, 28);
            this.label42.TabIndex = 11;
            this.label42.Text = "Core 11";
            this.label42.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label43.Location = new System.Drawing.Point(113, 140);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(54, 28);
            this.label43.TabIndex = 12;
            this.label43.Text = "Core 12";
            this.label43.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label44.Location = new System.Drawing.Point(113, 168);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(54, 28);
            this.label44.TabIndex = 13;
            this.label44.Text = "Core 13";
            this.label44.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label45.Location = new System.Drawing.Point(113, 196);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(54, 28);
            this.label45.TabIndex = 14;
            this.label45.Text = "Core 14";
            this.label45.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label46.Location = new System.Drawing.Point(113, 224);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(54, 28);
            this.label46.TabIndex = 15;
            this.label46.Text = "Core 15";
            this.label46.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonGetCO
            // 
            this.buttonGetCO.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonGetCO.Location = new System.Drawing.Point(243, 59);
            this.buttonGetCO.Name = "buttonGetCO";
            this.buttonGetCO.Size = new System.Drawing.Size(85, 22);
            this.buttonGetCO.TabIndex = 32;
            this.buttonGetCO.Text = "Refresh";
            this.buttonGetCO.UseVisualStyleBackColor = true;
            this.buttonGetCO.Click += new System.EventHandler(this.buttonGetCO_Click);
            // 
            // numericUpDownCO_0
            // 
            this.numericUpDownCO_0.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericUpDownCO_0.Enabled = false;
            this.numericUpDownCO_0.Location = new System.Drawing.Point(63, 31);
            this.numericUpDownCO_0.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDownCO_0.Minimum = new decimal(new int[] {
            999,
            0,
            0,
            -2147483648});
            this.numericUpDownCO_0.Name = "numericUpDownCO_0";
            this.numericUpDownCO_0.Size = new System.Drawing.Size(44, 20);
            this.numericUpDownCO_0.TabIndex = 33;
            // 
            // numericUpDownCO_1
            // 
            this.numericUpDownCO_1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericUpDownCO_1.Enabled = false;
            this.numericUpDownCO_1.Location = new System.Drawing.Point(63, 59);
            this.numericUpDownCO_1.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDownCO_1.Minimum = new decimal(new int[] {
            999,
            0,
            0,
            -2147483648});
            this.numericUpDownCO_1.Name = "numericUpDownCO_1";
            this.numericUpDownCO_1.Size = new System.Drawing.Size(44, 20);
            this.numericUpDownCO_1.TabIndex = 34;
            // 
            // numericUpDownCO_2
            // 
            this.numericUpDownCO_2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericUpDownCO_2.Enabled = false;
            this.numericUpDownCO_2.Location = new System.Drawing.Point(63, 87);
            this.numericUpDownCO_2.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDownCO_2.Minimum = new decimal(new int[] {
            999,
            0,
            0,
            -2147483648});
            this.numericUpDownCO_2.Name = "numericUpDownCO_2";
            this.numericUpDownCO_2.Size = new System.Drawing.Size(44, 20);
            this.numericUpDownCO_2.TabIndex = 35;
            // 
            // numericUpDownCO_3
            // 
            this.numericUpDownCO_3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericUpDownCO_3.Enabled = false;
            this.numericUpDownCO_3.Location = new System.Drawing.Point(63, 115);
            this.numericUpDownCO_3.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDownCO_3.Minimum = new decimal(new int[] {
            999,
            0,
            0,
            -2147483648});
            this.numericUpDownCO_3.Name = "numericUpDownCO_3";
            this.numericUpDownCO_3.Size = new System.Drawing.Size(44, 20);
            this.numericUpDownCO_3.TabIndex = 36;
            // 
            // numericUpDownCO_4
            // 
            this.numericUpDownCO_4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericUpDownCO_4.Enabled = false;
            this.numericUpDownCO_4.Location = new System.Drawing.Point(63, 143);
            this.numericUpDownCO_4.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDownCO_4.Minimum = new decimal(new int[] {
            999,
            0,
            0,
            -2147483648});
            this.numericUpDownCO_4.Name = "numericUpDownCO_4";
            this.numericUpDownCO_4.Size = new System.Drawing.Size(44, 20);
            this.numericUpDownCO_4.TabIndex = 37;
            // 
            // numericUpDownCO_5
            // 
            this.numericUpDownCO_5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericUpDownCO_5.Enabled = false;
            this.numericUpDownCO_5.Location = new System.Drawing.Point(63, 171);
            this.numericUpDownCO_5.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDownCO_5.Minimum = new decimal(new int[] {
            999,
            0,
            0,
            -2147483648});
            this.numericUpDownCO_5.Name = "numericUpDownCO_5";
            this.numericUpDownCO_5.Size = new System.Drawing.Size(44, 20);
            this.numericUpDownCO_5.TabIndex = 38;
            // 
            // numericUpDownCO_6
            // 
            this.numericUpDownCO_6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericUpDownCO_6.Enabled = false;
            this.numericUpDownCO_6.Location = new System.Drawing.Point(63, 199);
            this.numericUpDownCO_6.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDownCO_6.Minimum = new decimal(new int[] {
            999,
            0,
            0,
            -2147483648});
            this.numericUpDownCO_6.Name = "numericUpDownCO_6";
            this.numericUpDownCO_6.Size = new System.Drawing.Size(44, 20);
            this.numericUpDownCO_6.TabIndex = 39;
            // 
            // numericUpDownCO_7
            // 
            this.numericUpDownCO_7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericUpDownCO_7.Enabled = false;
            this.numericUpDownCO_7.Location = new System.Drawing.Point(63, 227);
            this.numericUpDownCO_7.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDownCO_7.Minimum = new decimal(new int[] {
            999,
            0,
            0,
            -2147483648});
            this.numericUpDownCO_7.Name = "numericUpDownCO_7";
            this.numericUpDownCO_7.Size = new System.Drawing.Size(44, 20);
            this.numericUpDownCO_7.TabIndex = 40;
            // 
            // numericUpDownCO_9
            // 
            this.numericUpDownCO_9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericUpDownCO_9.Enabled = false;
            this.numericUpDownCO_9.Location = new System.Drawing.Point(173, 59);
            this.numericUpDownCO_9.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDownCO_9.Minimum = new decimal(new int[] {
            999,
            0,
            0,
            -2147483648});
            this.numericUpDownCO_9.Name = "numericUpDownCO_9";
            this.numericUpDownCO_9.Size = new System.Drawing.Size(44, 20);
            this.numericUpDownCO_9.TabIndex = 42;
            // 
            // numericUpDownCO_10
            // 
            this.numericUpDownCO_10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericUpDownCO_10.Enabled = false;
            this.numericUpDownCO_10.Location = new System.Drawing.Point(173, 87);
            this.numericUpDownCO_10.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDownCO_10.Minimum = new decimal(new int[] {
            999,
            0,
            0,
            -2147483648});
            this.numericUpDownCO_10.Name = "numericUpDownCO_10";
            this.numericUpDownCO_10.Size = new System.Drawing.Size(44, 20);
            this.numericUpDownCO_10.TabIndex = 43;
            // 
            // numericUpDownCO_11
            // 
            this.numericUpDownCO_11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericUpDownCO_11.Enabled = false;
            this.numericUpDownCO_11.Location = new System.Drawing.Point(173, 115);
            this.numericUpDownCO_11.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDownCO_11.Minimum = new decimal(new int[] {
            999,
            0,
            0,
            -2147483648});
            this.numericUpDownCO_11.Name = "numericUpDownCO_11";
            this.numericUpDownCO_11.Size = new System.Drawing.Size(44, 20);
            this.numericUpDownCO_11.TabIndex = 44;
            // 
            // numericUpDownCO_12
            // 
            this.numericUpDownCO_12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericUpDownCO_12.Enabled = false;
            this.numericUpDownCO_12.Location = new System.Drawing.Point(173, 143);
            this.numericUpDownCO_12.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDownCO_12.Minimum = new decimal(new int[] {
            999,
            0,
            0,
            -2147483648});
            this.numericUpDownCO_12.Name = "numericUpDownCO_12";
            this.numericUpDownCO_12.Size = new System.Drawing.Size(44, 20);
            this.numericUpDownCO_12.TabIndex = 45;
            // 
            // numericUpDownCO_13
            // 
            this.numericUpDownCO_13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericUpDownCO_13.Enabled = false;
            this.numericUpDownCO_13.Location = new System.Drawing.Point(173, 171);
            this.numericUpDownCO_13.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDownCO_13.Minimum = new decimal(new int[] {
            999,
            0,
            0,
            -2147483648});
            this.numericUpDownCO_13.Name = "numericUpDownCO_13";
            this.numericUpDownCO_13.Size = new System.Drawing.Size(44, 20);
            this.numericUpDownCO_13.TabIndex = 46;
            // 
            // numericUpDownCO_14
            // 
            this.numericUpDownCO_14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericUpDownCO_14.Enabled = false;
            this.numericUpDownCO_14.Location = new System.Drawing.Point(173, 199);
            this.numericUpDownCO_14.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDownCO_14.Minimum = new decimal(new int[] {
            999,
            0,
            0,
            -2147483648});
            this.numericUpDownCO_14.Name = "numericUpDownCO_14";
            this.numericUpDownCO_14.Size = new System.Drawing.Size(44, 20);
            this.numericUpDownCO_14.TabIndex = 47;
            // 
            // numericUpDownCO_15
            // 
            this.numericUpDownCO_15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericUpDownCO_15.Enabled = false;
            this.numericUpDownCO_15.Location = new System.Drawing.Point(173, 227);
            this.numericUpDownCO_15.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDownCO_15.Minimum = new decimal(new int[] {
            999,
            0,
            0,
            -2147483648});
            this.numericUpDownCO_15.Name = "numericUpDownCO_15";
            this.numericUpDownCO_15.Size = new System.Drawing.Size(44, 20);
            this.numericUpDownCO_15.TabIndex = 48;
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(63, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(44, 22);
            this.button1.TabIndex = 49;
            this.button1.Text = "+";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.Button_ccd0_inc_Click);
            // 
            // button2
            // 
            this.button2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(63, 255);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(44, 22);
            this.button2.TabIndex = 50;
            this.button2.Text = "-";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.Button_ccd0_dec_Click);
            // 
            // button3
            // 
            this.button3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(173, 3);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(44, 22);
            this.button3.TabIndex = 51;
            this.button3.Text = "+";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.Button_ccd1_inc_Click);
            // 
            // button4
            // 
            this.button4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.Location = new System.Drawing.Point(173, 255);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(44, 22);
            this.button4.TabIndex = 52;
            this.button4.Text = "-";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.Button_ccd1_dec_Click);
            // 
            // btnSaveCOProfile
            // 
            this.btnSaveCOProfile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSaveCOProfile.Location = new System.Drawing.Point(243, 87);
            this.btnSaveCOProfile.Name = "btnSaveCOProfile";
            this.btnSaveCOProfile.Size = new System.Drawing.Size(85, 22);
            this.btnSaveCOProfile.TabIndex = 53;
            this.btnSaveCOProfile.Text = "Save";
            this.btnSaveCOProfile.UseVisualStyleBackColor = true;
            this.btnSaveCOProfile.Click += new System.EventHandler(this.BtnSaveCOProfile_Click);
            // 
            // btnLoadCOProfile
            // 
            this.btnLoadCOProfile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnLoadCOProfile.Location = new System.Drawing.Point(243, 115);
            this.btnLoadCOProfile.Name = "btnLoadCOProfile";
            this.btnLoadCOProfile.Size = new System.Drawing.Size(85, 22);
            this.btnLoadCOProfile.TabIndex = 54;
            this.btnLoadCOProfile.Text = "Load";
            this.btnLoadCOProfile.UseVisualStyleBackColor = true;
            this.btnLoadCOProfile.Click += new System.EventHandler(this.BtnLoadCOProfile_Click);
            // 
            // checkBoxApplyCOStartup
            // 
            this.checkBoxApplyCOStartup.AutoSize = true;
            this.tableLayoutPanel12.SetColumnSpan(this.checkBoxApplyCOStartup, 4);
            this.checkBoxApplyCOStartup.Dock = System.Windows.Forms.DockStyle.Left;
            this.checkBoxApplyCOStartup.Location = new System.Drawing.Point(3, 283);
            this.checkBoxApplyCOStartup.Name = "checkBoxApplyCOStartup";
            this.checkBoxApplyCOStartup.Size = new System.Drawing.Size(165, 29);
            this.checkBoxApplyCOStartup.TabIndex = 55;
            this.checkBoxApplyCOStartup.Text = "Apply saved profile on startup";
            this.checkBoxApplyCOStartup.UseVisualStyleBackColor = true;
            this.checkBoxApplyCOStartup.Click += new System.EventHandler(this.CheckBoxApplyCOStartup_CheckedChanged);
            // 
            // tabPageWmi
            // 
            this.tabPageWmi.Controls.Add(this.tableLayoutPanel13);
            this.tabPageWmi.Location = new System.Drawing.Point(4, 40);
            this.tabPageWmi.Name = "tabPageWmi";
            this.tabPageWmi.Size = new System.Drawing.Size(337, 356);
            this.tabPageWmi.TabIndex = 8;
            this.tabPageWmi.Text = "AMD ACPI";
            this.tabPageWmi.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel13
            // 
            this.tableLayoutPanel13.ColumnCount = 2;
            this.tableLayoutPanel13.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutPanel13.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel13.Controls.Add(this.label49, 0, 2);
            this.tableLayoutPanel13.Controls.Add(this.label47, 0, 0);
            this.tableLayoutPanel13.Controls.Add(this.comboBoxAvailableCommands, 1, 0);
            this.tableLayoutPanel13.Controls.Add(this.comboBoxAvailableValues, 1, 1);
            this.tableLayoutPanel13.Controls.Add(this.label48, 0, 1);
            this.tableLayoutPanel13.Controls.Add(this.textBoxWmiArgument, 1, 2);
            this.tableLayoutPanel13.Controls.Add(this.buttonWmiCmdSend, 1, 3);
            this.tableLayoutPanel13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel13.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel13.Name = "tableLayoutPanel13";
            this.tableLayoutPanel13.RowCount = 5;
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel13.Size = new System.Drawing.Size(337, 356);
            this.tableLayoutPanel13.TabIndex = 0;
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label49.Location = new System.Drawing.Point(3, 54);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(84, 26);
            this.label49.TabIndex = 4;
            this.label49.Text = "Argument";
            this.label49.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label47.Location = new System.Drawing.Point(3, 0);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(84, 27);
            this.label47.TabIndex = 0;
            this.label47.Text = "Command";
            this.label47.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // comboBoxAvailableCommands
            // 
            this.comboBoxAvailableCommands.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxAvailableCommands.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAvailableCommands.FormattingEnabled = true;
            this.comboBoxAvailableCommands.Location = new System.Drawing.Point(93, 3);
            this.comboBoxAvailableCommands.Name = "comboBoxAvailableCommands";
            this.comboBoxAvailableCommands.Size = new System.Drawing.Size(241, 21);
            this.comboBoxAvailableCommands.TabIndex = 1;
            this.comboBoxAvailableCommands.SelectedIndexChanged += new System.EventHandler(this.ComboBoxAvailableCommands_SelectedIndexChanged);
            // 
            // comboBoxAvailableValues
            // 
            this.comboBoxAvailableValues.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxAvailableValues.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAvailableValues.FormattingEnabled = true;
            this.comboBoxAvailableValues.Location = new System.Drawing.Point(93, 30);
            this.comboBoxAvailableValues.Name = "comboBoxAvailableValues";
            this.comboBoxAvailableValues.Size = new System.Drawing.Size(241, 21);
            this.comboBoxAvailableValues.TabIndex = 2;
            this.comboBoxAvailableValues.SelectedIndexChanged += new System.EventHandler(this.ComboBoxAvailableValues_SelectedIndexChanged);
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label48.Location = new System.Drawing.Point(3, 27);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(84, 27);
            this.label48.TabIndex = 3;
            this.label48.Text = "Values";
            this.label48.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxWmiArgument
            // 
            this.textBoxWmiArgument.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxWmiArgument.Location = new System.Drawing.Point(93, 57);
            this.textBoxWmiArgument.Name = "textBoxWmiArgument";
            this.textBoxWmiArgument.Size = new System.Drawing.Size(241, 20);
            this.textBoxWmiArgument.TabIndex = 5;
            // 
            // buttonWmiCmdSend
            // 
            this.buttonWmiCmdSend.Location = new System.Drawing.Point(93, 83);
            this.buttonWmiCmdSend.Name = "buttonWmiCmdSend";
            this.buttonWmiCmdSend.Size = new System.Drawing.Size(75, 23);
            this.buttonWmiCmdSend.TabIndex = 6;
            this.buttonWmiCmdSend.Text = "Send";
            this.buttonWmiCmdSend.UseVisualStyleBackColor = true;
            this.buttonWmiCmdSend.Click += new System.EventHandler(this.ButtonWmiCmdSend_Click);
            // 
            // tabPagePstates
            // 
            this.tabPagePstates.Controls.Add(this.numericUpDownBclk);
            this.tabPagePstates.Controls.Add(this.labelBCLK);
            this.tabPagePstates.Controls.Add(this.buttonBCLKApply);
            this.tabPagePstates.Controls.Add(this.tableLayoutPanel5);
            this.tabPagePstates.Location = new System.Drawing.Point(4, 40);
            this.tabPagePstates.Name = "tabPagePstates";
            this.tabPagePstates.Padding = new System.Windows.Forms.Padding(3);
            this.tabPagePstates.Size = new System.Drawing.Size(337, 356);
            this.tabPagePstates.TabIndex = 3;
            this.tabPagePstates.Text = "PStates";
            this.tabPagePstates.UseVisualStyleBackColor = true;
            // 
            // numericUpDownBclk
            // 
            this.numericUpDownBclk.DecimalPlaces = 4;
            this.numericUpDownBclk.Increment = new decimal(new int[] {
            625,
            0,
            0,
            262144});
            this.numericUpDownBclk.Location = new System.Drawing.Point(106, 147);
            this.numericUpDownBclk.Maximum = new decimal(new int[] {
            150,
            0,
            0,
            0});
            this.numericUpDownBclk.Minimum = new decimal(new int[] {
            95,
            0,
            0,
            0});
            this.numericUpDownBclk.Name = "numericUpDownBclk";
            this.numericUpDownBclk.Size = new System.Drawing.Size(94, 20);
            this.numericUpDownBclk.TabIndex = 5;
            this.numericUpDownBclk.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // labelBCLK
            // 
            this.labelBCLK.AutoSize = true;
            this.labelBCLK.Location = new System.Drawing.Point(6, 150);
            this.labelBCLK.Name = "labelBCLK";
            this.labelBCLK.Size = new System.Drawing.Size(41, 13);
            this.labelBCLK.TabIndex = 4;
            this.labelBCLK.Text = "label50";
            // 
            // buttonBCLKApply
            // 
            this.buttonBCLKApply.Location = new System.Drawing.Point(252, 147);
            this.buttonBCLKApply.Name = "buttonBCLKApply";
            this.buttonBCLKApply.Size = new System.Drawing.Size(75, 23);
            this.buttonBCLKApply.TabIndex = 3;
            this.buttonBCLKApply.Text = "Apply";
            this.buttonBCLKApply.UseVisualStyleBackColor = true;
            this.buttonBCLKApply.Click += new System.EventHandler(this.ButtonBCLKApply_Click);
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.AutoSize = true;
            this.tableLayoutPanel5.ColumnCount = 4;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel5.Controls.Add(this.label10, 0, 2);
            this.tableLayoutPanel5.Controls.Add(this.label8, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.label9, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.pstateDid, 1, 1);
            this.tableLayoutPanel5.Controls.Add(this.pstateFid, 1, 2);
            this.tableLayoutPanel5.Controls.Add(this.pstateIdBox, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.label11, 0, 3);
            this.tableLayoutPanel5.Controls.Add(this.pstateFrequency, 1, 3);
            this.tableLayoutPanel5.Controls.Add(this.btnPstateWrite, 3, 1);
            this.tableLayoutPanel5.Controls.Add(this.btnPstateRead, 3, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.Padding = new System.Windows.Forms.Padding(0, 5, 5, 5);
            this.tableLayoutPanel5.RowCount = 4;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.Size = new System.Drawing.Size(331, 128);
            this.tableLayoutPanel5.TabIndex = 1;
            // 
            // label10
            // 
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Location = new System.Drawing.Point(2, 65);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(96, 29);
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
            this.label8.Size = new System.Drawing.Size(94, 31);
            this.label8.TabIndex = 0;
            this.label8.Text = "PState ID";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Location = new System.Drawing.Point(3, 36);
            this.label9.MinimumSize = new System.Drawing.Size(77, 27);
            this.label9.Name = "label9";
            this.label9.Padding = new System.Windows.Forms.Padding(0, 5, 5, 5);
            this.label9.Size = new System.Drawing.Size(94, 29);
            this.label9.TabIndex = 3;
            this.label9.Text = "DID";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pstateDid
            // 
            this.pstateDid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pstateDid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pstateDid.Location = new System.Drawing.Point(103, 39);
            this.pstateDid.Name = "pstateDid";
            this.pstateDid.ReadOnly = true;
            this.pstateDid.Size = new System.Drawing.Size(94, 20);
            this.pstateDid.TabIndex = 4;
            // 
            // pstateFid
            // 
            this.pstateFid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pstateFid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pstateFid.Location = new System.Drawing.Point(103, 68);
            this.pstateFid.Name = "pstateFid";
            this.pstateFid.ReadOnly = true;
            this.pstateFid.Size = new System.Drawing.Size(94, 20);
            this.pstateFid.TabIndex = 7;
            // 
            // pstateIdBox
            // 
            this.pstateIdBox.DisplayMember = "0";
            this.pstateIdBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.pstateIdBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.pstateIdBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pstateIdBox.IntegralHeight = false;
            this.pstateIdBox.Items.AddRange(new object[] {
            "0",
            "1",
            "2"});
            this.pstateIdBox.Location = new System.Drawing.Point(103, 8);
            this.pstateIdBox.Name = "pstateIdBox";
            this.pstateIdBox.Size = new System.Drawing.Size(94, 21);
            this.pstateIdBox.TabIndex = 8;
            // 
            // label11
            // 
            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.Location = new System.Drawing.Point(2, 94);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(96, 29);
            this.label11.TabIndex = 9;
            this.label11.Text = "Frequency";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pstateFrequency
            // 
            this.pstateFrequency.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pstateFrequency.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pstateFrequency.Location = new System.Drawing.Point(103, 97);
            this.pstateFrequency.Name = "pstateFrequency";
            this.pstateFrequency.ReadOnly = true;
            this.pstateFrequency.Size = new System.Drawing.Size(94, 20);
            this.pstateFrequency.TabIndex = 10;
            // 
            // btnPstateWrite
            // 
            this.btnPstateWrite.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPstateWrite.Enabled = false;
            this.btnPstateWrite.Location = new System.Drawing.Point(246, 39);
            this.btnPstateWrite.Name = "btnPstateWrite";
            this.btnPstateWrite.Size = new System.Drawing.Size(77, 23);
            this.btnPstateWrite.TabIndex = 11;
            this.btnPstateWrite.Text = "Write";
            this.btnPstateWrite.UseVisualStyleBackColor = true;
            this.btnPstateWrite.Click += new System.EventHandler(this.BtnPstateWrite_Click);
            // 
            // btnPstateRead
            // 
            this.btnPstateRead.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPstateRead.Location = new System.Drawing.Point(246, 8);
            this.btnPstateRead.Name = "btnPstateRead";
            this.btnPstateRead.Size = new System.Drawing.Size(77, 25);
            this.btnPstateRead.TabIndex = 12;
            this.btnPstateRead.Text = "Read";
            this.btnPstateRead.UseVisualStyleBackColor = true;
            this.btnPstateRead.Click += new System.EventHandler(this.BtnPstateRead_Click);
            // 
            // tabPageInfo
            // 
            this.tabPageInfo.Controls.Add(this.tableLayoutPanel3);
            this.tabPageInfo.Location = new System.Drawing.Point(4, 40);
            this.tabPageInfo.Name = "tabPageInfo";
            this.tabPageInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageInfo.Size = new System.Drawing.Size(337, 356);
            this.tabPageInfo.TabIndex = 2;
            this.tabPageInfo.Text = "Info";
            this.tabPageInfo.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.AutoSize = true;
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.buttonExport, 0, 11);
            this.tableLayoutPanel3.Controls.Add(this.smuInfoLabel, 1, 9);
            this.tableLayoutPanel3.Controls.Add(this.biosInfoLabel, 1, 7);
            this.tableLayoutPanel3.Controls.Add(this.mbVendorInfoLabel, 1, 5);
            this.tableLayoutPanel3.Controls.Add(this.labelInfoCpu, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.label1, 0, 5);
            this.tableLayoutPanel3.Controls.Add(this.label2, 0, 9);
            this.tableLayoutPanel3.Controls.Add(this.label3, 0, 7);
            this.tableLayoutPanel3.Controls.Add(this.cpuInfoLabel, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.label4, 0, 6);
            this.tableLayoutPanel3.Controls.Add(this.mbModelInfoLabel, 1, 6);
            this.tableLayoutPanel3.Controls.Add(this.label6, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.cpuIdLabel, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.label23, 0, 8);
            this.tableLayoutPanel3.Controls.Add(this.firmwareInfoLabel, 1, 8);
            this.tableLayoutPanel3.Controls.Add(this.label24, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.label28, 0, 3);
            this.tableLayoutPanel3.Controls.Add(this.modelInfoLabel, 1, 2);
            this.tableLayoutPanel3.Controls.Add(this.packageTypeInfoLabel, 1, 3);
            this.tableLayoutPanel3.Controls.Add(this.label29, 0, 4);
            this.tableLayoutPanel3.Controls.Add(this.configInfoLabel, 1, 4);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.Padding = new System.Windows.Forms.Padding(0, 5, 5, 5);
            this.tableLayoutPanel3.RowCount = 12;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.Size = new System.Drawing.Size(331, 350);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // buttonExport
            // 
            this.buttonExport.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.buttonExport.AutoSize = true;
            this.tableLayoutPanel3.SetColumnSpan(this.buttonExport, 2);
            this.buttonExport.Location = new System.Drawing.Point(3, 316);
            this.buttonExport.Name = "buttonExport";
            this.buttonExport.Size = new System.Drawing.Size(102, 26);
            this.buttonExport.TabIndex = 1;
            this.buttonExport.Text = "Debug Report";
            this.buttonExport.UseVisualStyleBackColor = true;
            this.buttonExport.Click += new System.EventHandler(this.ButtonExport_Click);
            // 
            // smuInfoLabel
            // 
            this.smuInfoLabel.AutoEllipsis = true;
            this.smuInfoLabel.AutoSize = true;
            this.smuInfoLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smuInfoLabel.Location = new System.Drawing.Point(74, 212);
            this.smuInfoLabel.Name = "smuInfoLabel";
            this.smuInfoLabel.Padding = new System.Windows.Forms.Padding(5);
            this.smuInfoLabel.Size = new System.Drawing.Size(249, 23);
            this.smuInfoLabel.TabIndex = 7;
            this.smuInfoLabel.Text = "-";
            this.smuInfoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // biosInfoLabel
            // 
            this.biosInfoLabel.AutoEllipsis = true;
            this.biosInfoLabel.AutoSize = true;
            this.biosInfoLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.biosInfoLabel.Location = new System.Drawing.Point(74, 166);
            this.biosInfoLabel.Name = "biosInfoLabel";
            this.biosInfoLabel.Padding = new System.Windows.Forms.Padding(5);
            this.biosInfoLabel.Size = new System.Drawing.Size(249, 23);
            this.biosInfoLabel.TabIndex = 6;
            this.biosInfoLabel.Text = "-";
            this.biosInfoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // mbVendorInfoLabel
            // 
            this.mbVendorInfoLabel.AutoEllipsis = true;
            this.mbVendorInfoLabel.AutoSize = true;
            this.mbVendorInfoLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mbVendorInfoLabel.Location = new System.Drawing.Point(74, 120);
            this.mbVendorInfoLabel.Name = "mbVendorInfoLabel";
            this.mbVendorInfoLabel.Padding = new System.Windows.Forms.Padding(5);
            this.mbVendorInfoLabel.Size = new System.Drawing.Size(249, 23);
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
            this.label1.Location = new System.Drawing.Point(3, 120);
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
            this.label2.Location = new System.Drawing.Point(3, 212);
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
            this.label3.Location = new System.Drawing.Point(3, 166);
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
            this.cpuInfoLabel.Size = new System.Drawing.Size(249, 23);
            this.cpuInfoLabel.TabIndex = 4;
            this.cpuInfoLabel.Text = "-";
            this.cpuInfoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(3, 143);
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
            this.mbModelInfoLabel.Location = new System.Drawing.Point(74, 143);
            this.mbModelInfoLabel.Name = "mbModelInfoLabel";
            this.mbModelInfoLabel.Padding = new System.Windows.Forms.Padding(5);
            this.mbModelInfoLabel.Size = new System.Drawing.Size(249, 23);
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
            this.cpuIdLabel.Size = new System.Drawing.Size(249, 23);
            this.cpuIdLabel.TabIndex = 11;
            this.cpuIdLabel.Text = "-";
            this.cpuIdLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label23.Location = new System.Drawing.Point(3, 189);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(65, 23);
            this.label23.TabIndex = 12;
            this.label23.Text = "Firmware";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // firmwareInfoLabel
            // 
            this.firmwareInfoLabel.AutoSize = true;
            this.firmwareInfoLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.firmwareInfoLabel.Location = new System.Drawing.Point(74, 189);
            this.firmwareInfoLabel.Name = "firmwareInfoLabel";
            this.firmwareInfoLabel.Padding = new System.Windows.Forms.Padding(5);
            this.firmwareInfoLabel.Size = new System.Drawing.Size(249, 23);
            this.firmwareInfoLabel.TabIndex = 13;
            this.firmwareInfoLabel.Text = "-";
            this.firmwareInfoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label24.Location = new System.Drawing.Point(3, 51);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(65, 23);
            this.label24.TabIndex = 14;
            this.label24.Text = "Model";
            this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label28.Location = new System.Drawing.Point(3, 74);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(65, 23);
            this.label28.TabIndex = 15;
            this.label28.Text = "Package";
            this.label28.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // modelInfoLabel
            // 
            this.modelInfoLabel.AutoSize = true;
            this.modelInfoLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.modelInfoLabel.Location = new System.Drawing.Point(74, 51);
            this.modelInfoLabel.Name = "modelInfoLabel";
            this.modelInfoLabel.Padding = new System.Windows.Forms.Padding(5);
            this.modelInfoLabel.Size = new System.Drawing.Size(249, 23);
            this.modelInfoLabel.TabIndex = 16;
            this.modelInfoLabel.Text = "-";
            this.modelInfoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // packageTypeInfoLabel
            // 
            this.packageTypeInfoLabel.AutoSize = true;
            this.packageTypeInfoLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.packageTypeInfoLabel.Location = new System.Drawing.Point(74, 74);
            this.packageTypeInfoLabel.Name = "packageTypeInfoLabel";
            this.packageTypeInfoLabel.Padding = new System.Windows.Forms.Padding(5);
            this.packageTypeInfoLabel.Size = new System.Drawing.Size(249, 23);
            this.packageTypeInfoLabel.TabIndex = 17;
            this.packageTypeInfoLabel.Text = "-";
            this.packageTypeInfoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label29.Location = new System.Drawing.Point(3, 97);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(65, 23);
            this.label29.TabIndex = 18;
            this.label29.Text = "Config";
            this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // configInfoLabel
            // 
            this.configInfoLabel.AutoSize = true;
            this.configInfoLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.configInfoLabel.Location = new System.Drawing.Point(74, 97);
            this.configInfoLabel.Name = "configInfoLabel";
            this.configInfoLabel.Padding = new System.Windows.Forms.Padding(5);
            this.configInfoLabel.Size = new System.Drawing.Size(249, 23);
            this.configInfoLabel.TabIndex = 19;
            this.configInfoLabel.Text = "-";
            this.configInfoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxResult
            // 
            this.textBoxResult.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxResult.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxResult.Location = new System.Drawing.Point(3, 0);
            this.textBoxResult.Margin = new System.Windows.Forms.Padding(0);
            this.textBoxResult.Multiline = true;
            this.textBoxResult.Name = "textBoxResult";
            this.textBoxResult.ReadOnly = true;
            this.textBoxResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxResult.Size = new System.Drawing.Size(289, 380);
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
            this.splitContainer1.Panel1MinSize = 220;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel11);
            this.splitContainer1.Panel2.Padding = new System.Windows.Forms.Padding(0, 5, 5, 5);
            this.splitContainer1.Panel2MinSize = 0;
            this.splitContainer1.Size = new System.Drawing.Size(652, 410);
            this.splitContainer1.SplitterDistance = 354;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 5;
            // 
            // tableLayoutPanel11
            // 
            this.tableLayoutPanel11.ColumnCount = 1;
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel11.Controls.Add(this.textBoxResult, 0, 0);
            this.tableLayoutPanel11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel11.Location = new System.Drawing.Point(0, 5);
            this.tableLayoutPanel11.Name = "tableLayoutPanel11";
            this.tableLayoutPanel11.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.tableLayoutPanel11.RowCount = 2;
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel11.Size = new System.Drawing.Size(292, 400);
            this.tableLayoutPanel11.TabIndex = 5;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(652, 432);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SMU Debug Tool";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SettingsForm_FormClosed);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxCMD)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPageCPU.ResumeLayout(false);
            this.tabPageCPU.PerformLayout();
            this.groupBoxCoreControl.ResumeLayout(false);
            this.groupBoxCoreControl.PerformLayout();
            this.panelX3D.ResumeLayout(false);
            this.panelManualCoreControl.ResumeLayout(false);
            this.panelManualCoreControl.PerformLayout();
            this.tableLayoutPanel15.ResumeLayout(false);
            this.tableLayoutPanel15.PerformLayout();
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
            this.tabPageCPUID.ResumeLayout(false);
            this.tabPageCPUID.PerformLayout();
            this.tableLayoutPanel14.ResumeLayout(false);
            this.tableLayoutPanel14.PerformLayout();
            this.tableLayoutPanel10.ResumeLayout(false);
            this.tableLayoutPanel10.PerformLayout();
            this.tabPagePbo.ResumeLayout(false);
            this.tableLayoutPanel12.ResumeLayout(false);
            this.tableLayoutPanel12.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCO_8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCO_0)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCO_1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCO_2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCO_3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCO_4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCO_5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCO_6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCO_7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCO_9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCO_10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCO_11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCO_12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCO_13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCO_14)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCO_15)).EndInit();
            this.tabPageWmi.ResumeLayout(false);
            this.tableLayoutPanel13.ResumeLayout(false);
            this.tableLayoutPanel13.PerformLayout();
            this.tabPagePstates.ResumeLayout(false);
            this.tabPagePstates.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBclk)).EndInit();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.tabPageInfo.ResumeLayout(false);
            this.tabPageInfo.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel11.ResumeLayout(false);
            this.tableLayoutPanel11.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
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
        private TabPage tabPageCPUID;
        private TableLayoutPanel tableLayoutPanel10;
        private Label label21;
        private TextBox textBoxCPUIDAddress;
        private Button buttonCPUIDRead;
        private Label label25;
        private TextBox textBoxCPUIDeax;
        private Label label22;
        private Label label26;
        private TextBox textBoxCPUIDedx;
        private Label label27;
        private TextBox textBoxCPUIDebx;
        private TextBox textBoxCPUIDecx;
        private Button buttonCPUIDScan;
        private TableLayoutPanel tableLayoutPanel11;
        private Button btnPstateWrite;
        private Button btnPstateRead;
        private Label label23;
        private Label firmwareInfoLabel;
        private Label label24;
        private Label label28;
        private Label modelInfoLabel;
        private Label packageTypeInfoLabel;
        private Label label29;
        private Label configInfoLabel;
        private Button buttonPMTable;
        private Button buttonSmuLog;
        private Label label30;
        private ComboBox comboBoxMailboxSelect;
        private NumericUpDown textBoxCMD;
        private TabPage tabPagePbo;
        private TableLayoutPanel tableLayoutPanel12;
        private NumericUpDown numericUpDownCO_8;
        private Button buttonApplyCO;
        private Label label31;
        private Label label32;
        private Label label33;
        private Label label34;
        private Label label35;
        private Label label36;
        private Label label37;
        private Label label38;
        private Label label39;
        private Label label40;
        private Label label41;
        private Label label42;
        private Label label43;
        private Label label44;
        private Label label45;
        private Label label46;
        private Button buttonGetCO;
        private NumericUpDown numericUpDownCO_0;
        private NumericUpDown numericUpDownCO_1;
        private NumericUpDown numericUpDownCO_2;
        private NumericUpDown numericUpDownCO_3;
        private NumericUpDown numericUpDownCO_4;
        private NumericUpDown numericUpDownCO_5;
        private NumericUpDown numericUpDownCO_6;
        private NumericUpDown numericUpDownCO_7;
        private NumericUpDown numericUpDownCO_9;
        private NumericUpDown numericUpDownCO_10;
        private NumericUpDown numericUpDownCO_11;
        private NumericUpDown numericUpDownCO_12;
        private NumericUpDown numericUpDownCO_13;
        private NumericUpDown numericUpDownCO_14;
        private NumericUpDown numericUpDownCO_15;
        private BindingSource bindingSource1;
        private TabPage tabPageWmi;
        private TableLayoutPanel tableLayoutPanel13;
        private Label label47;
        private ComboBox comboBoxAvailableCommands;
        private ComboBox comboBoxAvailableValues;
        private Label label48;
        private Label label49;
        private TextBox textBoxWmiArgument;
        private Button buttonWmiCmdSend;
        private Label labelBCLK;
        private Button buttonBCLKApply;
        private NumericUpDown numericUpDownBclk;
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private TableLayoutPanel tableLayoutPanel14;
        private Label label50;
        private TextBox textBoxCpuid;
        private Button buttonCpuidDecode;
        private Button btnSaveCOProfile;
        private Button btnLoadCOProfile;
        private CheckBox checkBoxApplyCOStartup;
        private GroupBox groupBoxCoreControl;
        private TableLayoutPanel tableLayoutPanel15;
        private CheckBox checkBox7;
        private CheckBox checkBox6;
        private CheckBox checkBox5;
        private CheckBox checkBox4;
        private CheckBox checkBox3;
        private CheckBox checkBox2;
        private CheckBox checkBox1;
        private CheckBox checkBox0;
        private Label labelCore0;
        private Label labelCore15;
        private Label labelCore14;
        private Label labelCore13;
        private Label labelCore12;
        private Label labelCore11;
        private Label labelCore10;
        private Label labelCore9;
        private Label labelCore8;
        private CheckBox checkBox15;
        private CheckBox checkBox14;
        private CheckBox checkBox13;
        private CheckBox checkBox12;
        private CheckBox checkBox11;
        private CheckBox checkBox10;
        private CheckBox checkBox9;
        private CheckBox checkBox8;
        private Label labelCore7;
        private Label labelCore6;
        private Label labelCore5;
        private Label labelCore4;
        private Label labelCore3;
        private Label labelCore2;
        private Label labelCore1;
        private Button buttonApplyCoreMap;
        private Label label67;
        private CheckBox checkBoxSMT;
        private Button button6;
        private Button button5;
        private RadioButton radioButtonX3D;
        private RadioButton radioButtonManualCoreControl;
        private Panel panelManualCoreControl;
        private Panel panelX3D;
        private ToolTip toolTip1;
    }
}

