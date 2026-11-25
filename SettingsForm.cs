using Microsoft.VisualBasic.Devices;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using ZenStates.Core;
using ZenStatesDebugTool.Properties;
using Application = System.Windows.Forms.Application;
using static ZenStates.Core.Cpu;
using Microsoft.Win32.TaskScheduler;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Diagnostics;
using ZenStates.Core.Drivers;
using ZenStates.Core.Hardware.Smu;
using ZenStates.Core.Hardware;
using ZenStates.Core.Hardware.Apob;

namespace ZenStatesDebugTool
{
    public partial class SettingsForm : Form
    {
        //private static readonly int Threads = Convert.ToInt32(Environment.GetEnvironmentVariable("NUMBER_OF_PROCESSORS"));
        private BackgroundWorker backgroundWorker1;
        private readonly NUMAUtil _numaUtil;
        private readonly Cpu cpu;
        List<SmuAddressSet> matches = new List<SmuAddressSet>();
        private readonly Mailbox testMailbox = new Mailbox();
        private readonly string wmiAMDACPI = "AMD_ACPI";
        private readonly string wmiScope = "root\\wmi";
        private readonly string profilesPath;
        private readonly string defaultsPath;
        private ManagementObject classInstance;
        private string instanceName;
        private ManagementBaseObject pack;
        private const string profilesFolderName = "profiles";
        private const string filename = "co_profile.txt";
        private readonly string[] args;
        private readonly bool isApplyProfile;
        private readonly Dictionary<int, NumericUpDown> coControls = new Dictionary<int, NumericUpDown>();
        private static readonly ToolTip formToolTip = new ToolTip { AutoPopDelay = 10000, InitialDelay = 400, ReshowDelay = 100 };

        public SettingsForm()
        {
            InitializeComponent();
            InitTabStrip();
            InitLogToolbar();
            _numaUtil = new NUMAUtil();
            Log($"Detected NUMA nodes. ({_numaUtil.HighestNumaNode + 1})");

            try
            {
                profilesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, profilesFolderName);
                defaultsPath =  Path.Combine(profilesPath, filename);
                
                args = Environment.GetCommandLineArgs();
                foreach (string arg in args)
                {
                    isApplyProfile |= (arg.ToLower() == "--applyprofile");
                }

                cpu = new Cpu();

                InitForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resources.Error);
                Dispose();
                ExitApplication();
            }
        }

        private void ExitApplication()
        {
            cpu?.Dispose();

            if (AppSettings.Instance.AutoUninstallDriver)
            {
                try
                {
                    DriverCleanup.CleanupDriverIfLastInstance(
                        (DriverCleaner.NotificationLevel)AppSettings.Instance.AutoUninstallDriverNotificationLevel);
                }
                catch { }
            }

            if (Application.MessageLoop)
                Application.Exit();
            else
                Environment.Exit(1);
        }

        private void InitTestMailbox(uint msgAddr, uint rspAddr, uint argAddr)
        {
            testMailbox.SMU_ADDR_MSG = msgAddr;
            testMailbox.SMU_ADDR_RSP = rspAddr;
            testMailbox.SMU_ADDR_ARG = argAddr;
            ResetSmuAddresses();
        }

        private void InitTestMailbox(Mailbox mailbox)
        {
            testMailbox.SMU_ADDR_MSG = mailbox.SMU_ADDR_MSG;
            testMailbox.SMU_ADDR_RSP = mailbox.SMU_ADDR_RSP;
            testMailbox.SMU_ADDR_ARG = mailbox.SMU_ADDR_ARG;
            ResetSmuAddresses();
        }

        private void ResetSmuAddresses()
        {
            textBoxCMDAddress.Text = $"0x{Convert.ToString(testMailbox.SMU_ADDR_MSG, 16).ToUpper()}";
            textBoxRSPAddress.Text = $"0x{Convert.ToString(testMailbox.SMU_ADDR_RSP, 16).ToUpper()}";
            textBoxARGAddress.Text = $"0x{Convert.ToString(testMailbox.SMU_ADDR_ARG, 16).ToUpper()}";
        }

        private void DisplaySystemInfo()
        {
            try
            {
                cpuInfoLabel.Text = cpu.systemInfo.CpuName;
                modelInfoLabel.Text = $"{cpu.systemInfo.Model:X2}";
                packageTypeInfoLabel.Text = cpu.info.packageType.ToString();
                mbVendorInfoLabel.Text = cpu.systemInfo.MbVendor;
                mbModelInfoLabel.Text = cpu.systemInfo.MbName;
                biosInfoLabel.Text = cpu.systemInfo.BiosVersion;
                smuInfoLabel.Text = cpu.systemInfo.SmuVersion.ToString();
                firmwareInfoLabel.Text = $"{cpu.systemInfo.PatchLevel:X8}";
                cpuIdLabel.Text = $"{cpu.systemInfo.CpuId} ({cpu.info.codeName})";
                configInfoLabel.Text = $"{cpu.info.topology.ccds} CCD / {cpu.info.topology.ccxs} CCX / {cpu.systemInfo.PhysicalCoreCount} physical cores";
            }
            catch { }
        }

        private void InitForm()
        {
            /*if (cpu.Status == Utils.LibStatus.PARTIALLY_OK)
            {
                if (cpu.LastError != null)
                    MessageBox.Show(cpu.LastError.Message, Resources.Error);
            }*/

            if (cpu.smu.Version == 0)
            {
                MessageBox.Show("Error getting SMU version!\n" +
                    "Default SMU addresses are not responding to commands.",
                    "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (!Directory.Exists(profilesPath))
            {
                MessageBox.Show("Profiles directory does not exist, created one for you.");
                Directory.CreateDirectory(profilesPath);
            }

            InitTestMailbox(cpu.smu.Rsmu);
            DisplaySystemInfo();

            InitPstates();

            PopulateFrequencyList(comboBoxACF.Items);
            PopulateFrequencyList(comboBoxSCF.Items);
            PopulateCCDList(comboBoxCore.Items);
            PopulateMailboxesList(comboBoxMailboxSelect.Items);

            comboBoxCore.SelectedIndex = 0;
            double multi = GetCurrentMulti();
            if (multi >= 5.50)
            {
                int index = (int)((multi - 5.50) / 0.25);
                if (index > -1 && index < comboBoxACF.Items.Count && index < comboBoxSCF.Items.Count)
                {
                    comboBoxACF.SelectedIndex = index;
                    comboBoxSCF.SelectedIndex = index;
                }
            }

            InitCoreControl();
            InitPboLayout();
            InitCsToolTips();
            InitPBO();
            InitCS();
            PopulateWmiFunctions();

            double? currentBclk = cpu.bclk?.GetBclk();
            labelBCLK.Text = currentBclk + " MHz";
            numericUpDownBclk.Text = $"{currentBclk}";

            var prochotEnabled = cpu.IsProchotEnabled();
            checkBoxPROCHOT.Checked = prochotEnabled ?? false;
            //checkBoxPROCHOT.Enabled = prochotEnabled;
            //buttonApplyPROCHOT.Enabled = prochotEnabled;

            comboBoxMailboxSelect.SelectedIndex = 0;

            checkBoxAutoUninstallDriver.Checked = AppSettings.Instance.AutoUninstallDriver;
            InitDriverNotificationCombo();

            formToolTip.SetToolTip(checkBoxPROCHOT, "Disables temperature throttling. Can be useful on extreme cooling.");
            formToolTip.SetToolTip(checkBoxAutoUninstallDriver, "Stops and removes the inpoutx64 driver service when the last instance of SMUDebugTool or ZenTimings exits.");

            if (isApplyProfile)
            {
                int coApplied, coFailed, limitsApplied, limitsFailed, csApplied, csFailed;
                PboProfile profile = LoadPboProfile();
                ApplyCOProfile(profile, out coApplied, out coFailed);
                ApplyPboLimitsProfile(profile, out limitsApplied, out limitsFailed);
                ApplyCSProfile(out csApplied, out csFailed);
                InitPBO();
                InitCS();
                tabControl1.SelectedTab = tabPagePbo;

                // Launched non-interactively (scheduled task as SYSTEM, session 0): no window to show, so notify the
                // logged-on user and exit once the profile is applied
                if (!Environment.UserInteractive)
                {
                    NotifyProfileApplied(coApplied, coFailed, limitsApplied, limitsFailed, csApplied, csFailed);
                    Load += (_, __) => BeginInvoke(new System.Action(Application.Exit));
                }
            }

            SetStatusText($"{cpu.info.codeName}. Ready.");
        }

        private void ApplyCOProfile(PboProfile profile, out int applied, out int failed)
        {
            applied = 0;
            failed = 0;

            List<Tuple<int, int>> margins = profile.Margins;
            if (margins.Count > 0 && IsCurveOptimizerSupported)
            {
                foreach (var margin in margins)
                {
                    int index = margin.Item1;
                    int value = margin.Item2;
                    if (IsCoreEnabled(index))
                    {
                        if (cpu.SetPsmMarginSingleCore(EncodeCoreMarginBitmask(index), Convert.ToInt32(value)))
                            applied++;
                        else
                            failed++;
                    }
                }
            }
        }

        // FMax and the PPT/TDC/EDC/scalar values of the saved profile
        private void ApplyPboLimitsProfile(PboProfile profile, out int applied, out int failed)
        {
            int ok = 0;
            int bad = 0;

            Action<bool> count = success =>
            {
                if (success)
                    ok++;
                else
                    bad++;
            };

            if (profile.FMax.HasValue)
                count(cpu.SetFMax((uint)ClampValue(numericUpDownFmax, profile.FMax.Value)));
            if (profile.Ppt.HasValue && IsPptSupported)
                count(cpu.SetPPTLimit((uint)ClampValue(numericUpDownPpt, profile.Ppt.Value)) == SMU.Status.OK);
            if (profile.Tdc.HasValue && IsTdcSupported)
                count(cpu.SetTDCVDDLimit((uint)ClampValue(numericUpDownTdc, profile.Tdc.Value)) == SMU.Status.OK);
            if (profile.Edc.HasValue && IsEdcSupported)
                count(cpu.SetEDCVDDLimit((uint)ClampValue(numericUpDownEdc, profile.Edc.Value)) == SMU.Status.OK);
            if (profile.Scalar.HasValue && IsPboScalarSupported)
                count(cpu.SetPBOScalar((uint)ClampValue(numericUpDownScalar, profile.Scalar.Value)) == SMU.Status.OK);

            applied = ok;
            failed = bad;
        }

        private static decimal ClampValue(NumericUpDown control, decimal value)
        {
            return Math.Max(control.Minimum, Math.Min(control.Maximum, value));
        }

        // Tells the logged-on user what the startup/resume run applied. Nothing is shown if there was no saved profile.
        private static void NotifyProfileApplied(int coApplied, int coFailed, int limitsApplied, int limitsFailed, int csApplied, int csFailed)
        {
            List<string> parts = new List<string>();

            if (coApplied > 0)
                parts.Add($"Curve Optimizer ({coApplied} {(coApplied == 1 ? "core" : "cores")})");
            if (limitsApplied > 0)
                parts.Add($"PBO limits ({limitsApplied} {(limitsApplied == 1 ? "value" : "values")})");
            if (csApplied > 0)
                parts.Add($"Curve Shaper ({csApplied} {(csApplied == 1 ? "tier" : "tiers")})");

            int failed = coFailed + limitsFailed + csFailed;
            if (parts.Count == 0 && failed == 0)
                return;

            string message = parts.Count > 0 ? "Saved profile applied: " + string.Join(", ", parts) + "." : "The saved profile could not be applied.";
            if (failed > 0 && parts.Count > 0)
                message += $" {failed} failed.";

            ProfileNotification.ShowInUserSession(message, failed > 0);
        }

        // TODO: Detect OC Mode and return PState freq if on auto
        private double GetCurrentMulti()
        {
            double multi = cpu.GetCoreMulti();
            if (multi == 0)
                SetStatusText($@"Error getting current frequency!");

            return multi;
        }

        private void PopulateFrequencyList(ComboBox.ObjectCollection l)
        {
            for (double multi = 5.5; multi <= 70; multi += 0.25)
            {
                l.Add((object)new FrequencyListItem(multi, string.Format("x{0:0.00}", multi)));
            }
        }

        private void PopulateCCDList(ComboBox.ObjectCollection l)
        {
            ApobCoreMap coreMap = cpu.info.apob?.CoreMap;
            if (coreMap != null && coreMap.Cores.Count > 0)
            {
                foreach (ApobCoreMapCore core in coreMap.Cores)
                {
                    uint? mask = cpu.MakeCoreMaskForLogicalCore(core.LogicalIndex);
                    if (mask != null)
                        l.Add(new CoreListItem(core.PhysicalCcd, core.PhysicalCcx, core.PhysicalCore, core.LogicalIndex, mask.Value));
                }
                return;
            }

            int ccxInCcd = cpu.info.family == Cpu.Family.FAMILY_19H ? 1 : 2;
            int coresInCcx = 8 / ccxInCcd;
            for (int core = 0; core < cpu.info.topology.cores; ++core)
            {
                int ccd = core / 8;
                int ccx = core / coresInCcx;
                l.Add(new CoreListItem(ccd, ccx, core, core, cpu.MakeCoreMask((uint)core, (uint)ccd, (uint)ccx)));
            }
        }

        private void PopulateMailboxesList(ComboBox.ObjectCollection l)
        {
            l.Clear();
            l.Add(new MailboxListItem("RSMU", cpu.smu.Rsmu));
            l.Add(new MailboxListItem("MP1", cpu.smu.Mp1Smu));
            l.Add(new MailboxListItem("HSMP", cpu.smu.Hsmp));
        }

        private void AddMailboxToList(string label, SmuAddressSet addressSet)
        {
            comboBoxMailboxSelect.Items.Add(new MailboxListItem(label, addressSet));
        }

        private void InitCoreControl()
        {
            uint cores = (uint)GetPhysicalCoreCount();
            uint coresPerGroup = 8;
            uint logicalIndexGroup1 = 0;
            uint logicalIndexGroup2 = 0;

            for (uint i = 0; i < cores; i++)
            {
                uint mapIndex = i / coresPerGroup;

                if (IsCoreEnabled((int)i))
                {
                    try
                    {
                        CheckBox control = (CheckBox)Controls.Find($"checkBox{i}", true)[0];
                        if (control != null)
                        {
                            control.Enabled = true;
                            control.Checked = true;

                            int? logicalInCcd = GetLogicalIndexInCcd((int)i);

                            if (mapIndex == 0) // Group 1
                            {
                                control.Tag = $"{logicalInCcd ?? (int)logicalIndexGroup1}";
                                logicalIndexGroup1++;
                            }
                            else // Group 2
                            {
                                control.Tag = $"{logicalInCcd ?? (int)logicalIndexGroup2}";
                                logicalIndexGroup2++;
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Error initializing core {i}: {e}");
                    }
                }
            }

            checkBoxSMT.Checked = cpu.systemInfo.SMT;
        }

        private static int ConvertMarginToInt(uint value)
        {
            return (sbyte)(unchecked(value));
        }

        private static readonly string[] CsTierNames = { "min", "low", "medium", "high", "max" };
        private static readonly string[] CsBandNames = { "low", "medium", "high" };

        // [tier, band] -> control; tiers: 0=min..4=max, bands: 0=low T, 1=med T, 2=high T
        private NumericUpDown[,] CsControls => new[,]
        {
            { cs_min_low, cs_min_med, cs_min_high },
            { cs_low_low, cs_low_med, cs_low_high },
            { cs_med_low, cs_med_med, cs_med_high },
            { cs_high_low, cs_high_med, cs_high_high },
            { cs_max_low, cs_max_med, cs_max_high },
        };

        private bool IsCurveShaperSupported =>
            cpu.smu.Rsmu.SMU_MSG_GetCurveShaperMargin != 0 && cpu.smu.Rsmu.SMU_MSG_SetCurveShaperMargin != 0;

        private bool IsCurveOptimizerSupported =>
            cpu.smu.Rsmu.SMU_MSG_SetDldoPsmMargin != 0 || cpu.smu.Mp1Smu.SMU_MSG_SetDldoPsmMargin != 0;

        // The Curve Shaper layout is in the designer; only the tooltips are set here
        private void InitCsToolTips()
        {
            NumericUpDown[,] controls = CsControls;
            for (int tier = 0; tier < controls.GetLength(0); tier++)
            {
                for (int band = 0; band < controls.GetLength(1); band++)
                    formToolTip.SetToolTip(controls[tier, band], $"Frequency tier: {CsTierNames[tier]}, temperature band: {CsBandNames[band]}");
            }

            formToolTip.SetToolTip(buttonApplyCS, "Write the changed (highlighted) Curve Shaper margins to the SMU.");
            formToolTip.SetToolTip(buttonRefreshCS, "Re-read the current margins from the SMU.");
            formToolTip.SetToolTip(buttonResetCS, "Discard pending edits and restore the last read values.");
            formToolTip.SetToolTip(buttonZeroCS, "Set all margins to 0 (not applied until you press Apply).");
            formToolTip.SetToolTip(buttonSaveCSProfile, "Save the Curve Shaper margins shown here to the profile file. Curve Optimizer settings in the file are kept.");
            formToolTip.SetToolTip(buttonLoadCSProfile, "Load the saved Curve Shaper margins from the profile file (not applied until you press Apply).");
        }

        // Equal columns, like the PBO tab's button rows; null leaves a column empty
        private static TableLayoutPanel CreateButtonRow(int columns, Padding margin, params Button[] buttons)
        {
            TableLayoutPanel row = new TableLayoutPanel
            {
                AutoSize = false,
                ColumnCount = columns,
                RowCount = 1,
                Dock = DockStyle.Fill,
                Margin = margin,
                Height = 25,
            };

            for (int i = 0; i < columns; i++)
                row.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F / columns));
            row.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            for (int i = 0; i < buttons.Length; i++)
            {
                Button button = buttons[i];
                if (button == null)
                    continue;

                button.AutoSize = false;
                button.Dock = DockStyle.Fill;
                button.Margin = new Padding(1, 0, 1, 0);
                row.Controls.Add(button, i, 0);
            }

            return row;
        }

        private void SetCurveShaperEnabled(bool enabled)
        {
            foreach (NumericUpDown nud in CsControls)
                nud.Enabled = enabled;

            buttonRefreshCS.Enabled = enabled;
            buttonZeroCS.Enabled = enabled;
            buttonSaveCSProfile.Enabled = enabled;
            buttonLoadCSProfile.Enabled = enabled;
            UpdateDiscardButtons();
        }

        private void InitCS(bool showStatus = false)
        {
            if (!IsCurveShaperSupported)
            {
                SetCurveShaperEnabled(false);
                labelCsInfo.Text = $"Curve Shaper is not supported on {cpu.info.codeName}.";
                return;
            }

            uint[] csValues = cpu.GetAllCurveShaperMargins();
            NumericUpDown[,] controls = CsControls;

            if (csValues == null || csValues.Length < controls.GetLength(0))
            {
                SetCurveShaperEnabled(false);
                labelCsInfo.Text = "Failed to read Curve Shaper margins from the SMU.";
                if (showStatus)
                    SetStatusText("Failed to read Curve Shaper margins.");
                return;
            }

            SetCurveShaperEnabled(true);

            for (int tier = 0; tier < controls.GetLength(0); tier++)
            {
                for (int band = 0; band < controls.GetLength(1); band++)
                {
                    int shift = 8 * (band + 1);
                    SetBaselineValue(controls[tier, band], ConvertMarginToInt(csValues[tier] >> shift & 0xFF));
                }
            }

            labelCsInfo.Text = "Rows are frequency tiers, columns are temperature bands. Range -50 to +30. Edited cells are highlighted until applied."
                + Environment.NewLine + Environment.NewLine
                + "Save and Load use the same profile as the PBO tab. Each tab saves only its own settings and keeps the other's, and the startup/resume task applies both.";

            UpdateDiscardButtons();

            if (showStatus)
                SetStatusText("Curve Shaper margins refreshed.");
        }

        // Controls whose value could not be read from the hardware have no baseline. They count as edited once
        // their value has been changed (by the user or by loading a profile).
        private static readonly HashSet<NumericUpDown> editedWithoutBaseline = new HashSet<NumericUpDown>();

        // Baseline (last value read from hardware) is kept in Tag so edits can be highlighted and reverted.
        private static void SetBaselineValue(NumericUpDown control, decimal value)
        {
            value = Math.Max(control.Minimum, Math.Min(control.Maximum, value));
            editedWithoutBaseline.Remove(control);
            control.Tag = value;
            control.Value = value;
            UpdateDirtyState(control);
        }

        private static bool IsEdited(NumericUpDown control)
        {
            return control.Tag is decimal baseline ? control.Value != baseline : editedWithoutBaseline.Contains(control);
        }

        private static bool HasValue(NumericUpDown control)
        {
            return control.Tag is decimal || editedWithoutBaseline.Contains(control);
        }

        private static void UpdateDirtyState(NumericUpDown control)
        {
            bool dirty = IsEdited(control);
            control.BackColor = dirty ? Color.FromArgb(255, 249, 196) : SystemColors.Window;
            control.Font = dirty ? new Font(control.Font, FontStyle.Bold) : new Font(control.Font, FontStyle.Regular);
        }

        private void MarginControl_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown control = sender as NumericUpDown;
            if (control != null)
            {
                if (!(control.Tag is decimal))
                    editedWithoutBaseline.Add(control);

                UpdateDirtyState(control);
                UpdateDiscardButtons();
            }
        }

        private static bool HasPendingEdits(IEnumerable<NumericUpDown> controls)
        {
            return controls.Any(c => c.Enabled && IsEdited(c));
        }

        // Every editable value on the PBO tab: Curve Optimizer margins, PPT/TDC/EDC, scalar and FMax
        private IEnumerable<NumericUpDown> PboControls =>
            coControls.Values.Concat(new[] { numericUpDownPpt, numericUpDownTdc, numericUpDownEdc, numericUpDownScalar, numericUpDownFmax });

        // "Discard" is only useful while there are edits that have not been applied
        private void UpdateDiscardButtons()
        {
            if (cpu == null)
                return;

            bool pboEdits = HasPendingEdits(PboControls);

            if (btnResetCO != null)
                btnResetCO.Enabled = pboEdits;

            if (buttonApplyPbo != null)
                buttonApplyPbo.Enabled = pboEdits;

            // Apply and Discard follow the pending edits, like on the PBO tab
            bool csEdits = HasPendingEdits(CsControls.Cast<NumericUpDown>());

            if (buttonResetCS != null)
                buttonResetCS.Enabled = csEdits;

            if (buttonApplyCS != null)
                buttonApplyCS.Enabled = csEdits;
        }

        private void ResetToBaseline(IEnumerable<NumericUpDown> controls)
        {
            foreach (NumericUpDown control in controls)
            {
                if (control.Tag is decimal baseline)
                {
                    control.Value = baseline;
                }
                else if (editedWithoutBaseline.Remove(control))
                {
                    // Nothing to go back to: the value stays, but it is no longer pending
                    UpdateDirtyState(control);
                    UpdateDiscardButtons();
                }
            }
        }

        private void InitPBO()
        {
            bool supported = IsCurveOptimizerSupported;

            if (supported)
            {
                uint cores = (uint)GetPhysicalCoreCount();
                for (var i = 0; i < cores; i++)
                {
                    NumericUpDown control = GetCOControl(i);
                    if (control == null)
                        continue;

                    if (IsCoreEnabled(i))
                    {
                        control.Enabled = true;
                        uint? margin = cpu.GetPsmMarginSingleCore(EncodeCoreMarginBitmask(i));
                        if (margin != null)
                            SetBaselineValue(control, Convert.ToDecimal((int)margin));
                    }
                    else
                    {
                        control.Enabled = false;
                        formToolTip.SetToolTip(control, $"Core {i} is disabled (fused off or not present).");
                    }
                }
            }

            foreach (Control control in flowLayoutPanelCcdActions.Controls)
                control.Enabled = supported;
            UpdateDiscardButtons();
            // The profile also holds FMax and the PPT/TDC/EDC/scalar values
            btnSaveCOProfile.Enabled = supported || HasLimitControls || numericUpDownFmax.Enabled;
            btnLoadCOProfile.Enabled = btnSaveCOProfile.Enabled;
            buttonGetCO.Enabled = supported || HasLimitControls;

            if (!supported)
                SetStatusText($"Curve Optimizer is not supported on {cpu.info.codeName}.");

            /*using (RegistryKey key = Registry.CurrentUser.OpenSubKey
                ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                if (key != null)
                {
                    checkBoxApplyCOStartup.Checked = key.GetValue("RyzenSDT") != null;
                }
            }*/

            checkBoxApplyCOStartup.Checked = TaskExists("RyzenSDT");
            SetBaselineValue(numericUpDownFmax, cpu.GetFMax());
            UpdateDiscardButtons();
        }

        private void InitPboLayout()
        {
            BuildCoActionBar();
            BuildCcdBlocks();
            InitPowerLimitRow();
        }

        private void BuildCoActionBar()
        {
            // Repurpose flowLayoutPanelCcdActions as the CO-section action bar
            flowLayoutPanelCcdActions.Controls.Clear();
            flowLayoutPanelCcdActions.WrapContents = false;
            flowLayoutPanelCcdActions.Visible = true;
            flowLayoutPanelCcdActions.FlowDirection = FlowDirection.LeftToRight;
            flowLayoutPanelCcdActions.AutoSize = false;
            flowLayoutPanelCcdActions.Dock = DockStyle.None;
            flowLayoutPanelCcdActions.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            flowLayoutPanelCcdActions.Size = new Size(3 * (79 + 2), 27);

            Button allDecBtn = CreateActionButton("All \u2212", "Decrease the margin of every enabled core by 1.", AllCcdDecrement_Click);
            Button allIncBtn = CreateActionButton("All +", "Increase the margin of every enabled core by 1.", AllCcdIncrement_Click);
            Button allZeroBtn = CreateActionButton("Zero all", "Set the margin of every enabled core to 0 (not applied until you press Apply).", AllCcdZero_Click);

            flowLayoutPanelCcdActions.Controls.Add(allDecBtn);
            flowLayoutPanelCcdActions.Controls.Add(allIncBtn);
            flowLayoutPanelCcdActions.Controls.Add(allZeroBtn);

            btnResetCO.Text = "Discard";
            formToolTip.SetToolTip(buttonGetCO, "Re-read the current margins, PPT/TDC/EDC limits, PBO scalar and FMax.");
            formToolTip.SetToolTip(btnResetCO, "Discard pending edits and restore the last read values.");
            formToolTip.SetToolTip(btnSaveCOProfile, "Save the values shown here (margins, PPT/TDC/EDC, scalar and FMax) to the profile file. Curve Shaper settings in the file are kept (save those from the Curve Shaper tab).");
            formToolTip.SetToolTip(checkBoxApplyCOStartup, "Apply the saved profile (Curve Optimizer, PPT/TDC/EDC, scalar, FMax and Curve Shaper) at system startup, user logon and resume from sleep.");
            formToolTip.SetToolTip(btnLoadCOProfile, "Load the values from the profile file (not applied until you press Apply).");
            formToolTip.SetToolTip(buttonApplyPbo, "Write the highlighted (changed) values to the SMU: Curve Optimizer margins, PPT, TDC, EDC, scalar and FMax.");
            formToolTip.SetToolTip(numericUpDownFmax, "Maximum boost frequency limit in MHz.");
        }

        // ---- PPT / TDC / EDC / PBO scalar / FMax row (PBO tab) ----

        // The controls are in the designer (tableLayoutPanel12). They use the same edited-value logic as the
        // Curve Optimizer margins: a changed value is highlighted, and only changed values are written on Apply.

        private bool IsPptSupported => cpu.smu.Rsmu.SMU_MSG_SetFastLimit != 0 || cpu.smu.Mp1Smu.SMU_MSG_SetFastLimit != 0;
        private bool IsTdcSupported => cpu.smu.Rsmu.SMU_MSG_SetTDCVDDLimit != 0 || cpu.smu.Mp1Smu.SMU_MSG_SetTDCVDDLimit != 0;
        private bool IsEdcSupported => cpu.smu.Rsmu.SMU_MSG_SetEDCVDDLimit != 0 || cpu.smu.Mp1Smu.SMU_MSG_SetEDCVDDLimit != 0;
        private bool IsPboScalarSupported => cpu.smu.Rsmu.SMU_MSG_SetPBOScalar != 0 || cpu.smu.Mp1Smu.SMU_MSG_SetPBOScalar != 0;

        private bool HasLimitControls =>
            (numericUpDownPpt.Enabled || numericUpDownTdc.Enabled || numericUpDownEdc.Enabled || numericUpDownScalar.Enabled);

        // After Apply the PM table is re-read up to this many times, this far apart, until it shows the new value
        private const int LimitReadbackAttempts = 40;
        // A limit that still shows its previous value after this many reads is taken as kept by the firmware (clamped to
        // the value it already had). The PM table normally reflects a new limit by the 2nd read.
        private const int LimitUnchangedReads = 4;
        private const int LimitReadbackDelayMs = 25;

        // Highest value that still looks like a real limit when it is read from the PM table
        private const float MaxPlausibleLimit = 2000f;

        private void InitPowerLimitRow()
        {
            formToolTip.SetToolTip(numericUpDownPpt, "PPT (package power tracking) limit in watts.");
            formToolTip.SetToolTip(numericUpDownTdc, "TDC (thermal design current) limit in amps, VDD rail.");
            formToolTip.SetToolTip(numericUpDownEdc, "EDC (electrical design current) limit in amps, VDD rail.");
            formToolTip.SetToolTip(numericUpDownScalar, "PBO scalar, 1x to 10x.");

            numericUpDownPpt.Enabled = IsPptSupported;
            numericUpDownTdc.Enabled = IsTdcSupported;
            numericUpDownEdc.Enabled = IsEdcSupported;
            numericUpDownScalar.Enabled = IsPboScalarSupported;

            if (!HasLimitControls)
            {
                foreach (Control c in new Control[] { labelPpt, labelTdc, labelEdc, labelScalar,
                    numericUpDownPpt, numericUpDownTdc, numericUpDownEdc, numericUpDownScalar })
                    c.Visible = false;
                return;
            }

            RefreshPowerLimits(true);
        }

        private bool IsLimitEdited(NumericUpDown spinner)
        {
            return spinner.Enabled && IsEdited(spinner);
        }

        private static bool IsPlausibleLimit(float value)
        {
            return !float.IsNaN(value) && !float.IsInfinity(value) && value > 0 && value <= MaxPlausibleLimit;
        }

        private static float ReadCell(float[] table, int index)
        {
            return table != null && index >= 0 && index < table.Length ? table[index] : float.NaN;
        }

        // Reads the configured PPT/TDC/EDC limits from the PM table and the PBO scalar from the SMU and puts them in
        // the spinners. The PM table layout depends on the table version, see PowerLimitMap.
        private void RefreshPowerLimits(bool logResult, bool keepEdits = false)
        {
            try
            {
                uint version = cpu.GetTableVersion().TableVersion;
                string readout = "";

                if (numericUpDownPpt.Enabled || numericUpDownTdc.Enabled || numericUpDownEdc.Enabled)
                {
                    if (cpu.powerTable == null)
                    {
                        if (logResult)
                            Log("Power table is not available, current PPT/TDC/EDC limits can't be read.", LogLevel.Warning);
                    }
                    else if (!PowerLimitMap.TryGet(version, out PowerLimitCells cells))
                    {
                        if (logResult)
                            Log($"PM table 0x{version:X6} has no known PPT/TDC/EDC layout, current limits can't be shown. Values you enter are still written as given.", LogLevel.Warning);
                    }
                    else if (cpu.RefreshPowerTable() != SMU.Status.OK)
                    {
                        if (logResult)
                            Log("PM table refresh failed, current PPT/TDC/EDC limits can't be read.", LogLevel.Warning);
                    }
                    else
                    {
                        float[] table = cpu.powerTable.Table;
                        readout += ReadLimitInto(numericUpDownPpt, table, cells.PptLimit, "PPT", "W", keepEdits);
                        readout += ReadLimitInto(numericUpDownTdc, table, cells.TdcLimit, "TDC", "A", keepEdits);
                        readout += ReadLimitInto(numericUpDownEdc, table, cells.EdcLimit, "EDC", "A", keepEdits);
                    }
                }

                if (numericUpDownScalar.Enabled && !(keepEdits && IsEdited(numericUpDownScalar))
                    && cpu.smu.Rsmu.SMU_MSG_GetPBOScalar != 0 && cpu.TryGetPBOScalar(out float scalar))
                {
                    decimal rounded = Math.Round((decimal)scalar);
                    SetBaselineValue(numericUpDownScalar, rounded);
                    readout += $"Scalar {rounded}x\n";
                }

                if (logResult && readout.Length > 0)
                    LogCommand($"Read power limits (PM table 0x{version:X6})", readout.TrimEnd('\n'));
            }
            catch (Exception ex)
            {
                Log($"Reading the power limits failed: {ex.Message}", LogLevel.Warning);
            }
            finally
            {
                UpdateDiscardButtons();
            }
        }

        private string ReadLimitInto(NumericUpDown spinner, float[] table, int cell, string name, string unit, bool keepEdits)
        {
            if (!spinner.Enabled || (keepEdits && IsEdited(spinner)))
                return "";

            float value = ReadCell(table, cell);

            if (!IsPlausibleLimit(value))
            {
                // Say which cell was rejected, so a wrong index for this PM table shows up in the log
                return cell < 0
                    ? $"{name}: no limit cell known for this table\n"
                    : $"{name}: cell {cell} reads {value.ToString("0.##", CultureInfo.InvariantCulture)}, not used\n";
            }

            decimal rounded = Math.Round((decimal)value);
            SetBaselineValue(spinner, rounded);
            return $"{name} {rounded} {unit} (cell {cell})\n";
        }

        // Writes the PPT/TDC/EDC/scalar values that have been changed
        // A limit written to the SMU: the value sent and the value the PM table showed before (null if unknown)
        private sealed class LimitWrite
        {
            public decimal Target;
            public decimal? Previous;

            // The PM table shows the new limit once its value has moved: to the target, or away from the previous value
            // (the firmware may clamp the limit, so the target itself may never show up)
            public bool Settled(NumericUpDown spinner) =>
                spinner.Value == Target || (Previous.HasValue && spinner.Value != Previous.Value);

            // Stop waiting: the value has moved, or it still shows the previous value after a few reads (clamped to it).
            // Without a previous value only the target itself ends the wait.
            public bool Done(NumericUpDown spinner, int reads) =>
                Settled(spinner) || (Previous.HasValue && reads >= LimitUnchangedReads);
        }

        private void ApplyLimitsEdited(List<string> applied, List<string> failed)
        {
            var sent = new Dictionary<NumericUpDown, LimitWrite>();

            ApplyLimit(numericUpDownPpt, "PPT", "W", v => cpu.SetPPTLimit(v), applied, failed, sent);
            ApplyLimit(numericUpDownTdc, "TDC", "A", v => cpu.SetTDCVDDLimit(v), applied, failed, sent);
            ApplyLimit(numericUpDownEdc, "EDC", "A", v => cpu.SetEDCVDDLimit(v), applied, failed, sent);
            ApplyLimit(numericUpDownScalar, "PBO scalar", "x", v => cpu.SetPBOScalar(v), applied, failed, sent);

            if (sent.Count == 0)
                return;

            // Show what the hardware reports now. The SMU takes a new limit at once, but the PM table only reflects it
            // after its next update (usually by the 2nd read). Read again a few times (bounded, only after Apply) until
            // every written limit has moved from its previous value.
            Cursor previousCursor = Cursor;
            Cursor = Cursors.WaitCursor;
            Stopwatch watch = Stopwatch.StartNew();
            int reads = 0;

            try
            {
                do
                {
                    if (reads > 0)
                        Thread.Sleep(LimitReadbackDelayMs);

                    RefreshPowerLimits(false, true);
                    reads++;
                }
                while (reads < LimitReadbackAttempts && sent.Any(pair => !pair.Value.Done(pair.Key, reads)));
            }
            finally
            {
                Cursor = previousCursor;
            }

            if (sent.All(pair => pair.Value.Settled(pair.Key)))
                Log($"PM table reflected the new limits after {watch.ElapsedMilliseconds} ms ({reads} read{(reads == 1 ? "" : "s")}).");

            foreach (KeyValuePair<NumericUpDown, LimitWrite> pair in sent)
            {
                NumericUpDown spinner = pair.Key;
                LimitWrite write = pair.Value;

                if (spinner.Value == write.Target)
                    continue;

                if (write.Settled(spinner))
                    Log($"{LimitName(spinner)} reads {spinner.Value} after setting {write.Target}: the firmware clamped the value.", LogLevel.Warning);
                else if (write.Previous.HasValue)
                    Log($"{LimitName(spinner)} still reads {spinner.Value} after setting {write.Target} ({reads} reads over {watch.ElapsedMilliseconds} ms): the firmware probably clamped it to the value it already had. Refresh re-reads the PM table.", LogLevel.Warning);
                else
                    Log($"{LimitName(spinner)} still reads {spinner.Value} after setting {write.Target}, {reads} reads over {watch.ElapsedMilliseconds} ms. The firmware may have ignored the value, or the PM table has not updated yet (Refresh re-reads it).", LogLevel.Warning);
            }
        }

        private string LimitName(NumericUpDown spinner)
        {
            if (spinner == numericUpDownPpt) return "PPT";
            if (spinner == numericUpDownTdc) return "TDC";
            if (spinner == numericUpDownEdc) return "EDC";
            return "PBO scalar";
        }

        private void ApplyLimit(NumericUpDown spinner, string name, string unit, Func<uint, SMU.Status> set, List<string> applied, List<string> failed, Dictionary<NumericUpDown, LimitWrite> sent)
        {
            if (!IsLimitEdited(spinner))
                return;

            uint value = (uint)spinner.Value;
            SMU.Status status = set(value);
            string command = $"Set {name} {value}{(unit == "x" ? "x" : " " + unit)}";

            if (status == SMU.Status.OK)
            {
                LogCommand(command, "SMU status: OK", LogLevel.Success);
                applied.Add(name);
                sent[spinner] = new LimitWrite { Target = spinner.Value, Previous = spinner.Tag as decimal? };
                SetBaselineValue(spinner, spinner.Value);
            }
            else
            {
                LogCommand(command, $"SMU status: {status}", LogLevel.Error);
                failed.Add(name);
            }
        }

        private static Button CreateActionButton(string text, string tooltipText, EventHandler onClick)
        {
            Button button = new Button
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowOnly,
                Margin = new Padding(1, 0, 1, 0),
                Padding = new Padding(0),
                Size = new Size(79, 25),
                Text = text,
                UseVisualStyleBackColor = true,
                Dock = DockStyle.Fill,
            };
            button.Click += onClick;
            formToolTip.SetToolTip(button, tooltipText);
            return button;
        }

        private void AllCcdZero_Click(object sender, EventArgs e)
        {
            foreach (NumericUpDown control in coControls.Values)
            {
                if (control.Enabled)
                    control.Value = 0;
            }
        }

        private void AllCcdDecrement_Click(object sender, EventArgs e)
        {
            int ccdCount = GetCcdCount();
            for (int ccd = 0; ccd < ccdCount; ccd++)
                BulkMarginChangeHandler(ccd, -1);
        }

        private void AllCcdIncrement_Click(object sender, EventArgs e)
        {
            int ccdCount = GetCcdCount();
            for (int ccd = 0; ccd < ccdCount; ccd++)
                BulkMarginChangeHandler(ccd, 1);
        }

        private void BuildCcdBlocks()
        {
            coControls.Clear();

            flowLayoutPanelCOList.SuspendLayout();
            flowLayoutPanelCOList.Controls.Clear();
            flowLayoutPanelCOList.WrapContents = true;
            flowLayoutPanelCOList.Padding = new Padding(0);

            int ccdCount = GetCcdCount();
            const int coresPerCcd = 8;
            const int panelWidth = 160;
            const int headerHeight = 20;
            const int headerButtonWidth = 16;
            const int headerButtonSpacing = 2;
            const int rowHeight = 21;
            const int coresPerRow = 2;
            // label width wide enough for "C127"
            const int labelWidth = 30;
            const int nudWidth = 44;
            const int colSpacing = 6;
            // left column x-offsets
            const int col0LabelX = 2;
            const int col0NudX = col0LabelX + labelWidth;
            // right column x-offsets
            const int col1LabelX = col0NudX + nudWidth + colSpacing;
            const int col1NudX = col1LabelX + labelWidth;

            for (int ccd = 0; ccd < ccdCount; ccd++)
            {
                int startCore = ccd * coresPerCcd;
                int endCore = Math.Min(startCore + coresPerCcd, GetPhysicalCoreCount());
                int coresInCcd = endCore - startCore;
                if (coresInCcd <= 0) break;

                int coreRows = (int)Math.Ceiling(coresInCcd / (double)coresPerRow);
                int panelHeight = headerHeight + coreRows * rowHeight + 4;

                Panel ccdPanel = new Panel
                {
                    BorderStyle = BorderStyle.FixedSingle,
                    Margin = new Padding(1),
                    Name = $"panelCCD_{ccd}",
                    Size = new Size(panelWidth, panelHeight)
                };

                // Header: dark strip containing the label and the icon buttons
                Panel header = new Panel
                {
                    BackColor = SystemColors.ControlDark,
                    Location = new Point(0, 0),
                    Margin = new Padding(0),
                    Size = new Size(panelWidth, headerHeight)
                };

                Label ccdLabel = new Label
                {
                    AutoSize = false,
                    BackColor = Color.Transparent,
                    ForeColor = SystemColors.ControlLightLight,
                    Font = new Font("Microsoft Sans Serif", 7.5f, FontStyle.Bold),
                    Location = new Point(0, 0),
                    Padding = new Padding(3, 0, 0, 0),
                    // leave room for the three icon buttons on the right
                    Size = new Size(panelWidth - 3 * (headerButtonWidth + headerButtonSpacing) - 2, headerHeight),
                    Text = $"CCD {ccd}",
                    TextAlign = ContentAlignment.MiddleLeft
                };

                int buttonY = (headerHeight - headerButtonWidth) / 2;
                Button resetBtn = CreateCcdHeaderButton(CcdHeaderIcon.Reset, panelWidth - 3 * (headerButtonWidth + headerButtonSpacing) - 2, buttonY, headerButtonWidth, Tuple.Create(ccd, 0), $"Reset CCD {ccd} to the last read values");
                Button decBtn = CreateCcdHeaderButton(CcdHeaderIcon.Minus, panelWidth - 2 * (headerButtonWidth + headerButtonSpacing) - 2, buttonY, headerButtonWidth, Tuple.Create(ccd, -1), $"Decrease all cores of CCD {ccd} by 1");
                Button incBtn = CreateCcdHeaderButton(CcdHeaderIcon.Plus, panelWidth - (headerButtonWidth + headerButtonSpacing) - 2, buttonY, headerButtonWidth, Tuple.Create(ccd, 1), $"Increase all cores of CCD {ccd} by 1");
                resetBtn.Click += CcdBulkButton_Click;
                decBtn.Click += CcdBulkButton_Click;
                incBtn.Click += CcdBulkButton_Click;

                header.Controls.Add(ccdLabel);
                header.Controls.Add(resetBtn);
                header.Controls.Add(decBtn);
                header.Controls.Add(incBtn);
                ccdPanel.Controls.Add(header);

                // Core grid: column-major (top-to-bottom, then next column)
                int yOffset = headerHeight + 1;
                for (int row = 0; row < coreRows; row++)
                {
                    for (int col = 0; col < coresPerRow; col++)
                    {
                        // column-major index: col 0 = cores 0..coreRows-1, col 1 = cores coreRows..end
                        int localIndex = col * coreRows + row;
                        if (localIndex >= coresInCcd) continue;

                        int coreIndex = startCore + localIndex;
                        int labelX = col == 0 ? col0LabelX : col1LabelX;
                        int nudX = col == 0 ? col0NudX : col1NudX;

                        Label lbl = new Label
                        {
                            Location = new Point(labelX, yOffset + 3),
                            Name = $"labelCO_{coreIndex}",
                            Size = new Size(labelWidth, 14),
                            Text = $"C{coreIndex}",
                            TextAlign = ContentAlignment.MiddleLeft
                        };

                        NumericUpDown nud = new NumericUpDown
                        {
                            Enabled = false,
                            Location = new Point(nudX, yOffset),
                            Margin = new Padding(0),
                            Maximum = 999,
                            Minimum = -999,
                            Name = $"numericUpDownCO_{coreIndex}",
                            Size = new Size(nudWidth, 20),
                            TextAlign = HorizontalAlignment.Right,
                            Tag = coreIndex
                        };
                        nud.ValueChanged += MarginControl_ValueChanged;
                        formToolTip.SetToolTip(nud, $"Curve Optimizer margin for core {coreIndex} (CCD {ccd}, core {localIndex}). Negative = undervolt.");

                        ccdPanel.Controls.Add(lbl);
                        ccdPanel.Controls.Add(nud);
                        coControls[coreIndex] = nud;
                    }

                    yOffset += rowHeight;
                }

                flowLayoutPanelCOList.Controls.Add(ccdPanel);
            }

            flowLayoutPanelCOList.ResumeLayout();
        }

        private enum CcdHeaderIcon { Reset, Minus, Plus }

        private static Button CreateCcdHeaderButton(CcdHeaderIcon icon, int x, int y, int size, Tuple<int, int> action, string tooltipText)
        {
            Button button = new Button
            {
                BackColor = SystemColors.ControlDark,
                FlatStyle = FlatStyle.Flat,
                ForeColor = SystemColors.ControlLightLight,
                Location = new Point(x, y),
                Margin = new Padding(0),
                Size = new Size(size, size),
                Tag = action,
                TabStop = false,
                UseVisualStyleBackColor = false
            };
            button.FlatAppearance.BorderSize = 0;
            button.FlatAppearance.MouseOverBackColor = SystemColors.ControlDarkDark;
            button.FlatAppearance.MouseDownBackColor = SystemColors.Highlight;
            button.FlatAppearance.BorderColor = SystemColors.ControlDark;
            button.Paint += (s, e) => DrawCcdHeaderIcon(e.Graphics, button.ClientRectangle, icon, button.Enabled ? button.ForeColor : SystemColors.ControlLight);
            formToolTip.SetToolTip(button, tooltipText);
            return button;
        }

        // Icons are drawn as vector paths so they stay crisp at any DPI (no SVG support in WinForms on .NET Framework).
        private static void DrawCcdHeaderIcon(Graphics g, Rectangle bounds, CcdHeaderIcon icon, Color color)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            float cx = bounds.Left + bounds.Width / 2f;
            float cy = bounds.Top + bounds.Height / 2f;
            // 10px glyph inside a 16px button
            float half = Math.Min(bounds.Width, bounds.Height) * 0.3f;

            using (Pen pen = new Pen(color, 1.6f) { StartCap = System.Drawing.Drawing2D.LineCap.Round, EndCap = System.Drawing.Drawing2D.LineCap.Round })
            {
                switch (icon)
                {
                    case CcdHeaderIcon.Plus:
                        g.DrawLine(pen, cx, cy - half, cx, cy + half);
                        goto case CcdHeaderIcon.Minus;

                    case CcdHeaderIcon.Minus:
                        g.DrawLine(pen, cx - half, cy, cx + half, cy);
                        break;

                    case CcdHeaderIcon.Reset:
                        // "Undo" arrow: a curved stroke sweeping left into an arrowhead.
                        float x0 = cx - half, x1 = cx + half;
                        float yTop = cy - half * 0.45f, yBottom = cy + half * 0.75f;
                        using (var path = new System.Drawing.Drawing2D.GraphicsPath())
                        {
                            path.AddBezier(x0, yTop, cx - half * 0.2f, yTop - half * 0.9f, x1 + half * 0.1f, yTop - half * 0.3f, x1 - half * 0.15f, yBottom);
                            g.DrawPath(pen, path);
                        }
                        float headLen = half * 0.8f;
                        g.DrawLine(pen, x0, yTop, x0 + headLen, yTop - headLen * 0.9f);
                        g.DrawLine(pen, x0, yTop, x0 + headLen, yTop + headLen * 0.9f);
                        break;
                }
            }
        }

        private void CcdBulkButton_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            Tuple<int, int> action = button?.Tag as Tuple<int, int>;
            if (action == null)
                return;

            if (action.Item2 == 0)
                ResetToBaseline(GetCcdControls(action.Item1));
            else
                BulkMarginChangeHandler(action.Item1, action.Item2);
        }

        private IEnumerable<NumericUpDown> GetCcdControls(int ccd)
        {
            int startCore = ccd * 8;
            int endCore = Math.Min(startCore + 8, GetPhysicalCoreCount());
            for (int i = startCore; i < endCore; i++)
            {
                NumericUpDown control = GetCOControl(i);
                if (control != null)
                    yield return control;
            }
        }

        private void BtnResetCO_Click(object sender, EventArgs e)
        {
            ResetToBaseline(PboControls);
            SetStatusText("Pending edits reverted.");
        }

        private NumericUpDown GetCOControl(int coreIndex)
        {
            NumericUpDown control;
            return coControls.TryGetValue(coreIndex, out control) ? control : null;
        }

        private int GetCcdCount()
        {
            if (cpu.info.topology.ccds > 0)
            {
                return (int)cpu.info.topology.ccds;
            }

            return Math.Max(1, (int)Math.Ceiling(GetPhysicalCoreCount() / 8.0));
        }

        private int GetPhysicalCoreCount()
        {
            return (int)cpu.info.topology.physicalCores;
        }

        private ApobCoreMap GetApobCoreMap()
        {
            ApobCoreMap coreMap = cpu.info.apob?.CoreMap;
            return coreMap != null && coreMap.Cores.Count > 0 ? coreMap : null;
        }

        // The UI indexes cores by physical slot: CCD = index / 8, core within the CCD = index % 8.
        private ApobCoreMapCore GetApobCore(int coreIndex)
        {
            ApobCoreMap coreMap = GetApobCoreMap();
            if (coreMap == null)
                return null;

            int ccd = coreIndex / 8;
            int coreInCcd = coreIndex % 8;
            int coreSlotsPerComplex = Math.Max(1, coreMap.CoreSlotsPerComplex);

            foreach (ApobCoreMapCore core in coreMap.Cores)
            {
                if (core.PhysicalCcd == ccd && core.PhysicalCcx * coreSlotsPerComplex + core.PhysicalCore == coreInCcd)
                    return core;
            }

            return null;
        }

        // Position of the core among the enabled cores of its CCD, in logical (OS) order.
        private int? GetLogicalIndexInCcd(int coreIndex)
        {
            ApobCoreMapCore target = GetApobCore(coreIndex);
            if (target == null)
                return null;

            int index = 0;
            foreach (ApobCoreMapCore core in GetApobCoreMap().Cores)
            {
                if (core.PhysicalCcd != target.PhysicalCcd)
                    continue;
                if (core == target)
                    return index;
                index++;
            }

            return null;
        }

        private bool IsCoreEnabled(int coreIndex)
        {
            if (GetApobCoreMap() != null)
                return GetApobCore(coreIndex) != null;

            int mapIndex = coreIndex / 8;
            int coreInGroup = coreIndex % 8;
            return mapIndex >= 0
                && cpu.info.topology.coreDisableMap != null
                && mapIndex < cpu.info.topology.coreDisableMap.Length
                && ((~cpu.info.topology.coreDisableMap[mapIndex] >> coreInGroup) & 1) == 1;
        }

        private void ApplyFrequencyAllCoreSetting(int frequency)
        {
            if (cpu.SetFrequencyAllCore(Convert.ToUInt32(frequency)))
                SetStatusText(string.Format("Set frequency to {0} MHz!", frequency));
            else
                HandleError("Error setting frequency!");
        }

        private void ApplyFrequencySingleCoreSetting(CoreListItem i, int frequency)
        {
            if (cpu.SetFrequencySingleCore(i.Mask, Convert.ToUInt32(frequency)))
                SetStatusText(string.Format("Set core {0} frequency to {1} MHz!", i, frequency));
            else
                HandleError("Error setting frequency!");
        }

        private void EnableOCMode(bool prochotEnabled = true)
        {
            if (cpu.smu.SendSmuCommand(cpu.smu.Rsmu, cpu.smu.Rsmu.SMU_MSG_EnableOcMode, prochotEnabled ? 0U : 0x1000000))
                SetStatusText(prochotEnabled ? "PROCHOT enabled." : "PROCHOT disabled.");
            else
                HandleError("Error setting OC Mode!");
        }

        private void DisableOCMode()
        {
            if (cpu.DisableOcMode() == SMU.Status.OK)
                SetStatusText(string.Format("Set OK!"));
            else
                HandleError("Error disabling OC Mode!");
        }

        private void SetStatusText(string status)
        {
            labelStatus.Text = status;
            Console.WriteLine($"CMD Status: {status}");
        }

        private void SetButtonsState(bool enabled = true)
        {
            buttonApply.Enabled = enabled;
            buttonDefaults.Enabled = enabled;
            buttonProbe.Enabled = enabled;
            buttonPciRead.Enabled = enabled;
            buttonPciScan.Enabled = enabled;
            buttonExport.Enabled = enabled;
            buttonMsrRead.Enabled = enabled;
            buttonMsrScan.Enabled = enabled;
            buttonMsrWrite.Enabled = enabled;
            buttonPMTable.Enabled = enabled;
            buttonSmuLog.Enabled = enabled;

            textBoxCMDAddress.Enabled = enabled;
            textBoxRSPAddress.Enabled = enabled;
            textBoxARGAddress.Enabled = enabled;
            textBoxCMD.Enabled = enabled;
            textBoxARG0.Enabled = enabled;
            textBoxPciAddress.Enabled = enabled;
            textBoxPciValue.Enabled = enabled;
            textBoxPciStartReg.Enabled = enabled;
            textBoxPciEndReg.Enabled = enabled;
            textBoxMsrAddress.Enabled = enabled;
            textBoxMsrEdx.Enabled = enabled;
            textBoxMsrEax.Enabled = enabled;
            textBoxMsrStart.Enabled = enabled;
            textBoxMsrEnd.Enabled = enabled;
            comboBoxMailboxSelect.Enabled = enabled;
            // textBoxResult.Enabled = enabled;
        }

        private void TryConvertToUint(string text, out uint address)
        {
            try
            {
                address = Convert.ToUInt32(text.Trim().ToLower(), 16);
            }
            catch
            {
                throw new ApplicationException("Invalid hexadecimal value.");
            }
        }

        private void HandleError(string message, string title = "Error")
        {
            SetStatusText(Resources.Error);
            Log(message, LogLevel.Error);
            MessageBox.Show(message, title);
        }

        private void ShowResultMessageBox(uint[] data, string title = "SMU response", string context = null)
        {
            string responseString = context ?? "";
            string[] hexArray = new string[data.Length];
            string[] decArray = new string[data.Length];
            string[] binArray = new string[data.Length];

            for (var i = 0; i < data.Length; i++)
            {
                hexArray[i] = $"0x{Convert.ToString(data[i], 16).ToUpper()}";
                decArray[i] = $"{Convert.ToString(data[i], 10).ToUpper()}";
                binArray[i] = $"{Convert.ToString(data[i], 2).ToUpper()}";
            }

            responseString += "HEX: " + string.Join(", ", hexArray);
            responseString += Environment.NewLine;
            responseString += "DEC: " + string.Join(", ", decArray);
            responseString += Environment.NewLine;
            responseString += "BIN: " + string.Join(", ", binArray);

            Console.WriteLine($"Response: {responseString}");
            LogCommand(title, responseString);
        }

        private void ShowResult(uint data)
        {
            string responseString =
                $"HEX: 0x{Convert.ToString(data, 16).ToUpper()}" +
                Environment.NewLine +
                $"INT: {Convert.ToString(data, 10).ToUpper()}" +
                Environment.NewLine +
                $"BIN: {Convert.ToString(data, 2).PadLeft(32, '0')}";
            Console.WriteLine($"Response: {responseString}");
            LogCommand($"PCI read {textBoxPciAddress.Text.Trim()}", responseString);
        }

        private void ShowResultForm(string title="Result", string result="No result")
        {
            Invoke(new MethodInvoker(delegate
            {
                var resultForm = new ResultForm();
                resultForm.textBoxFormResult.Text = result;
                resultForm.Text = title;
                resultForm.Show();
            }));
        }

        // TODO: Show all args
        private void ApplySettings()
        {
            try
            {
                uint[] args = ZenStates.Core.Utils.MakeCmdArgs();
                string[] userArgs = textBoxARG0.Text.Trim().Split(',');

                TryConvertToUint(textBoxCMDAddress.Text, out uint addrMsg);
                TryConvertToUint(textBoxRSPAddress.Text, out uint addrRsp);
                TryConvertToUint(textBoxARGAddress.Text, out uint addrArg);
                TryConvertToUint(textBoxCMD.Text, out uint command);

                testMailbox.SMU_ADDR_MSG = addrMsg;
                testMailbox.SMU_ADDR_RSP = addrRsp;
                testMailbox.SMU_ADDR_ARG = addrArg;

                for (var i = 0; i < userArgs.Length; i++)
                {
                    if (i == args.Length)
                        break;

                    TryConvertToUint(userArgs[i], out uint temp);
                    args[i] = temp;
                }
                

                Console.WriteLine("MSG Address:  0x" + Convert.ToString(testMailbox.SMU_ADDR_MSG, 16).ToUpper());
                Console.WriteLine("RSP Address:  0x" + Convert.ToString(testMailbox.SMU_ADDR_RSP, 16).ToUpper());
                Console.WriteLine("ARG0 Address: 0x" + Convert.ToString(testMailbox.SMU_ADDR_ARG, 16).ToUpper());
                Console.WriteLine("ARG0        : 0x" + Convert.ToString(args[0], 16).ToUpper());

                uint[] argsIn = (uint[])args.Clone();
                SMU.Status status = cpu.smu.SendSmuCommand(testMailbox, command, ref args);
                string statusText = GetSMUStatus.GetByType(status);

                string context =
                    $"Mailbox: {comboBoxMailboxSelect.Text}   MSG 0x{testMailbox.SMU_ADDR_MSG:X}   RSP 0x{testMailbox.SMU_ADDR_RSP:X}   ARG 0x{testMailbox.SMU_ADDR_ARG:X}" +
                    Environment.NewLine +
                    "ARG in: " + string.Join(", ", argsIn.Select(a => $"0x{a:X}")) +
                    Environment.NewLine +
                    $"Status: {statusText}" +
                    Environment.NewLine;

                if (status == SMU.Status.OK)
                    ShowResultMessageBox(args, $"SMU command 0x{command:X}", context);
                else
                    LogCommand($"SMU command 0x{command:X} failed", context, LogLevel.Error);

                SetStatusText(statusText);
            }
            catch (ApplicationException ex)
            {
                HandleError(ex.Message);
            }
        }

        private void ButtonDefaults_Click(object sender, EventArgs e)
        {
            InitTestMailbox(cpu.smu.Rsmu);
            comboBoxMailboxSelect.SelectedIndex = 0;
            textBoxCMD.Value = 1;
            textBoxARG0.Text = "0";
        }

        private void ButtonApply_Click(object sender, EventArgs e)
        {
            try
            {
                ApplySettings();
            }
            catch (ApplicationException ex)
            {
                HandleError(ex.Message, "Error reading response");
            }
        }

        private void HandlePciReadBtnClick()
        {
            try
            {
                SetStatusText("Reading, please wait...");
                SetButtonsState(false);

                TryConvertToUint(textBoxPciAddress.Text, out uint address);
                uint data = cpu.ReadDword(address);

                textBoxPciValue.Text = $"0x{data:X8}";

                SetButtonsState();
                SetStatusText(GetSMUStatus.GetByType(SMU.Status.OK));
                ShowResult(data);
            }
            catch (ApplicationException ex)
            {
                SetButtonsState();
                HandleError(ex.Message);
            }
        }

        private void HandlePciWriteBtnClick()
        {
            try
            {
                SetStatusText("Writing, please wait...");
                SetButtonsState(false);

                TryConvertToUint(textBoxPciAddress.Text, out uint address);
                TryConvertToUint(textBoxPciValue.Text, out uint data);

                bool res = false;
                if (cpu.WriteDwordEx(cpu.smu.SMU_OFFSET_ADDR, address))
                    res = cpu.WriteDwordEx(cpu.smu.SMU_OFFSET_DATA, data);

                if (res)
                    SetStatusText("Write OK.");
                else
                    SetStatusText(Resources.Error);

                SetButtonsState();
            }
            catch (ApplicationException ex)
            {
                SetButtonsState();
                HandleError(ex.Message);
            }
        }

        private void ButtonPciRead_Click(object sender, EventArgs e)
        {
            HandlePciReadBtnClick();
        }

        private void TextBoxPciAddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                HandlePciReadBtnClick();
        }

        private void ButtonPciWrite_Click(object sender, EventArgs e)
        {
            HandlePciWriteBtnClick();
        }

        private void TextBoxPciValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                HandlePciWriteBtnClick();
        }

        private SMU.Status TrySettings(uint msgAddr, uint rspAddr, uint argAddr, uint cmd, uint value)
        {
            uint[] args = new uint[6];
            args[0] = value;

            testMailbox.SMU_ADDR_MSG = msgAddr;
            testMailbox.SMU_ADDR_RSP = rspAddr;
            testMailbox.SMU_ADDR_ARG = argAddr;

            return cpu.smu.SendSmuCommand(testMailbox, cmd, ref args);
        }

        private void ScanSmuRange(uint start, uint end, uint step, uint offset)
        {
            matches = new List<SmuAddressSet>();

            List<KeyValuePair<uint, uint>> temp = new List<KeyValuePair<uint, uint>>();

            while (start <= end)
            {
                uint smuRspAddress = start + offset;
 
                if (cpu.ReadDword(start) != 0xFFFFFFFF)
                {
                    // Send unknown command 0xFF to each pair of this start and possible response addresses
                    if (cpu.WriteDwordEx(start, 0xFF))
                    {
                        Thread.Sleep(10);

                        while (smuRspAddress <= end)
                        {
                            // Expect UNKNOWN_CMD status to be returned if the mailbox works
                            if (cpu.ReadDword(smuRspAddress) == 0xFE)
                            {
                                // Send Get_SMU_Version command
                                if (cpu.WriteDwordEx(start, 0x2))
                                {
                                    Thread.Sleep(10);
                                    if (cpu.ReadDword(smuRspAddress) == 0x1)
                                        temp.Add(new KeyValuePair<uint, uint>(start, smuRspAddress));
                                }
                            }
                            smuRspAddress += step;
                        }
                    }
                }

                start += step;
            }

            if (temp.Count > 0)
            {
                for (var i = 0; i < temp.Count; i++)
                {
                    Console.WriteLine($"{temp[i].Key:X8}: {temp[i].Value:X8}");
                }

                Console.WriteLine();
            }

            List<uint> possibleArgAddresses = new List<uint>();

            foreach (var pair in temp)
            {
                Console.WriteLine($"Testing {pair.Key:X8}: {pair.Value:X8}");

                if (TrySettings(pair.Key, pair.Value, 0xFFFFFFFF, 0x2, 0xFF) == SMU.Status.OK)
                {
                    var smuArgAddress = pair.Value + 4;
                    while (smuArgAddress <= end)
                    {
                        if (cpu.ReadDword(smuArgAddress) == cpu.smu.Version)
                        {
                            possibleArgAddresses.Add(smuArgAddress);
                        }
                        smuArgAddress += step;
                    }
                }

                // Verify the arg address returns correct value (should be test argument + 1)
                foreach (var address in possibleArgAddresses)
                {
                    uint testArg = 0xFAFAFAFA;
                    var retries = 3;

                    while (retries > 0)
                    {
                        testArg++;
                        retries--;

                        // Send test command
                        if (TrySettings(pair.Key, pair.Value, address, 0x1, testArg) == SMU.Status.OK)
                            if (cpu.ReadDword(address) != testArg + 1)
                                retries = -1;
                    }

                    if (retries == 0)
                    {
                        matches.Add(new SmuAddressSet(pair.Key, pair.Value, address));

                        string responseString =
                                $"CMD:  0x{pair.Key:X8}" +
                                Environment.NewLine +
                                $"RSP:  0x{pair.Value:X8}" +
                                Environment.NewLine +
                                $"ARG:  0x{address:X8}";

                        LogScanOutput(responseString);

                        break;
                    }
                }
            }
        }

        /*private void ScanSmuRange_old(uint start, uint end, int step, byte offset)
        {
            matches = new List<SmuAddressSet>();

            while (start <= end)
            {
                uint smuRspAddress = start + offset;
                uint smuArgAddress = 0xFFFFFFFF;

                if (cpu.ReadDword(start) != 0xFFFFFFFF)
                {
                    // Check if CMD-RSP pair returns correct status, while using a placeholder ARG address
                    if (TrySettings(start, smuRspAddress, smuArgAddress, testMailbox.SMU_MSG_TestMessage, 0x0) == SMU.Status.OK)
                    {
                        // Send smu version command, so the corresponding ARG0 address changes its value
                        TrySettings(start, smuRspAddress, smuArgAddress, testMailbox.SMU_MSG_GetSmuVersion, 0x0);
                        bool match = false;

                        smuArgAddress = smuRspAddress + 4;

                        // Scan for ARG address
                        while ((smuArgAddress <= end) && !match)
                        {
                            // Check if smu version major is in range
                            var currentRegValue = (cpu.ReadDword(smuArgAddress) & 0x00FF0000) >> 16;
                            Console.WriteLine($"REG: 0x{smuArgAddress:X8} Value: 0x{currentRegValue:X8}");
                            if (currentRegValue > 1 && currentRegValue <= 99)
                            {
                                // Send test message with an argument, using the potential ARG0 address
                                var argValue = (uint)matches.Count * 2 + 99;
                                TrySettings(start, smuRspAddress, smuArgAddress, testMailbox.SMU_MSG_TestMessage, argValue);
                                currentRegValue = cpu.ReadDword(smuArgAddress);
                                Console.WriteLine($"REG: 0x{smuArgAddress:X8} Value: 0x{currentRegValue:X8}");

                                // Check the address for expected value (argument + 1)
                                if (currentRegValue == argValue + 1)
                                {
                                    match = true;
                                    matches.Add(new SmuAddressSet(start, smuRspAddress, smuArgAddress));

                                    string responseString =
                                        $"CMD:  0x{start:X8}" +
                                        Environment.NewLine +
                                        $"RSP:  0x{smuRspAddress:X8}" +
                                        Environment.NewLine +
                                        $"ARG:  0x{smuArgAddress:X8}";

                                    smuArgAddress += 20;

                                    LogScanOutput(responseString);
                                }
                            }

                            smuArgAddress += 0x4;
                        }
                    }
                }

                start += (uint)step;
            }
        }*/

        private void RunBackgroundTask(DoWorkEventHandler task, RunWorkerCompletedEventHandler completedHandler, string title = "Task")
        {
            try
            {
                SetButtonsState(false);
                BeginLogSection($"{title} started");

                backgroundWorker1 = new BackgroundWorker();
                backgroundWorker1.DoWork += task;
                backgroundWorker1.RunWorkerCompleted += (sender, e) =>
                {
                    if (e.Error != null)
                        Log($"{title} failed: {e.Error.Message}", LogLevel.Error);
                    else if (e.Cancelled)
                        Log($"{title} cancelled", LogLevel.Warning);
                    else
                        Log($"{title} finished", LogLevel.Success);
                };
                backgroundWorker1.RunWorkerCompleted += completedHandler;
                backgroundWorker1.RunWorkerAsync();
            }
            catch (ApplicationException ex)
            {
                SetStatusText(Resources.Error);
                SetButtonsState();
                HandleError(ex.Message);
            }
        }

        private void BackgroundWorkerTrySettings_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Invoke(new MethodInvoker(delegate
                {
                    SetStatusText("Scanning SMU addresses, please wait...");
                }));

                switch (cpu.info.codeName)
                {
                    case Cpu.CodeName.BristolRidge:
                        //ScanSmuRange(0x13000000, 0x13000F00, 4, 0x10);
                        break;
                    case Cpu.CodeName.RavenRidge:
                    case Cpu.CodeName.Picasso:
                    case Cpu.CodeName.FireFlight:
                    case Cpu.CodeName.Dali:
                    case Cpu.CodeName.Renoir:
                        ScanSmuRange(0x03B10500, 0x03B10998, 8, 0x3C);
                        ScanSmuRange(0x03B10A00, 0x03B10AFF, 4, 0x60);
                        break;
                    case Cpu.CodeName.PinnacleRidge:
                    case Cpu.CodeName.SummitRidge:
                    case Cpu.CodeName.Matisse:
                    case Cpu.CodeName.Whitehaven:
                    case Cpu.CodeName.Naples:
                    case Cpu.CodeName.Colfax:
                    case Cpu.CodeName.Vermeer:
                    //case Cpu.CodeName.Raphael:
                        ScanSmuRange(0x03B10500, 0x03B10998, 8, 0x3C);
                        ScanSmuRange(0x03B10500, 0x03B10AFF, 4, 0x4C);
                        break;
                    case Cpu.CodeName.Raphael:
                    case Cpu.CodeName.GraniteRidge:
                        ScanSmuRange(0x03B10500, 0x03B10998, 8, 0x3C);
                        // ScanSmuRange(0x03B10500, 0x03B10AFF, 4, 0x4C);
                        break;
                    case Cpu.CodeName.Rome:
                        ScanSmuRange(0x03B10500, 0x03B10AFF, 4, 0x4C);
                        break;
                    default:
                        break;
                }
            }
            catch (ApplicationException)
            {
                Invoke(new MethodInvoker(delegate
                {
                    SetButtonsState();
                    SetStatusText(Resources.Error);
                }));
            }
        }

        private void ButtonScan_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show(
                "The scan process might crash your system or have other unexpected results. " +
                Environment.NewLine +
                "It could take up to 1 minute, depending on the system and current workload." +
                Environment.NewLine +
                "Do you want to continue?",
                "Confirm Scan",
                MessageBoxButtons.OKCancel
            );

            if (confirmResult == DialogResult.OK)
                RunBackgroundTask(BackgroundWorkerTrySettings_DoWork, SmuScan_WorkerCompleted, "SMU address scan");
        }

        private enum LogLevel { Info, Success, Warning, Error }

        private static readonly Color LogTimeColor = SystemColors.GrayText;
        private static readonly Color LogSuccessColor = Color.FromArgb(0, 128, 0);
        private static readonly Color LogWarningColor = Color.FromArgb(176, 112, 0);
        private static readonly Color LogErrorColor = Color.FromArgb(192, 0, 0);

        // Messages logged before the log control has a window handle (during form construction).
        // Writing formatted text would force the handle, and the form's, to be created too early.
        private readonly List<System.Action> pendingLog = new List<System.Action>();

        // Where scan output is inserted (end of the section header written by BeginLogSection); -1 = end of log
        private int logAppendAnchor = -1;
        private Font logBoldFont;

        private void RunOnLog(System.Action write)
        {
            if (!textBoxResult.IsHandleCreated)
            {
                pendingLog.Add(write);
                return;
            }

            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(() => write()));
                return;
            }

            write();
        }

        private static Color GetLogColor(LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Success: return LogSuccessColor;
                case LogLevel.Warning: return LogWarningColor;
                case LogLevel.Error: return LogErrorColor;
                default: return SystemColors.WindowText;
            }
        }

        // Status message: timestamped and colored by level, newest entry on top
        private void Log(string message, LogLevel level = LogLevel.Info)
        {
            DateTime time = DateTime.Now;
            RunOnLog(() => WriteLogEntry(time, message, null, level));
        }

        // Command and its output: timestamped bold header naming what was run, output indented below it,
        // newest entry on top
        private void LogCommand(string command, string output, LogLevel level = LogLevel.Info)
        {
            DateTime time = DateTime.Now;
            RunOnLog(() => WriteLogEntry(time, command, output, level));
        }

        private static string Indent(string text)
        {
            string[] lines = text.TrimEnd('\r', '\n').Replace("\r\n", "\n").Split('\n');
            return string.Join("\n", lines.Select(l => l.Length > 0 ? "  " + l : l));
        }

        private void WriteLogEntry(DateTime time, string title, string output, LogLevel level)
        {
            int lengthBefore = textBoxResult.TextLength;
            bool hasOutput = !string.IsNullOrWhiteSpace(output);

            if (logBoldFont == null)
                logBoldFont = new Font(textBoxResult.Font, FontStyle.Bold);

            textBoxResult.Select(0, 0);
            textBoxResult.SelectionFont = textBoxResult.Font;
            textBoxResult.SelectionColor = LogTimeColor;
            textBoxResult.SelectedText = time.ToString("HH:mm:ss", CultureInfo.InvariantCulture) + "  ";

            textBoxResult.SelectionFont = hasOutput ? logBoldFont : textBoxResult.Font;
            textBoxResult.SelectionColor = GetLogColor(level);
            textBoxResult.SelectedText = hasOutput ? title + "\n" : title + "\n\n";

            if (hasOutput)
            {
                textBoxResult.SelectionFont = textBoxResult.Font;
                textBoxResult.SelectionColor = SystemColors.WindowText;
                textBoxResult.SelectedText = Indent(output) + "\n\n";
            }

            // Keep the scan insertion point attached to its section while newer entries are added above it
            if (logAppendAnchor >= 0)
                logAppendAnchor += textBoxResult.TextLength - lengthBefore;

            textBoxResult.Select(0, 0);
            textBoxResult.ScrollToCaret();
        }

        // Starts a section (for example a scan): header line on top, scan output follows right below it
        private void BeginLogSection(string title)
        {
            Log(title);

            if (textBoxResult.IsHandleCreated)
            {
                int newline = textBoxResult.Text.IndexOf('\n');
                logAppendAnchor = newline >= 0 ? newline + 1 : textBoxResult.TextLength;
            }
        }

        // Scan output: inserted under the current section header, in the order it is found
        private void LogScanOutput(string text)
        {
            RunOnLog(() =>
            {
                int position = logAppendAnchor >= 0 ? Math.Min(logAppendAnchor, textBoxResult.TextLength) : textBoxResult.TextLength;

                textBoxResult.Select(position, 0);
                textBoxResult.SelectionFont = textBoxResult.Font;
                textBoxResult.SelectionColor = SystemColors.WindowText;
                textBoxResult.SelectedText = "\n" + Indent(text) + "\n";

                if (logAppendAnchor >= 0)
                    logAppendAnchor = textBoxResult.SelectionStart;
            });
        }

        private void InitLogToolbar()
        {
            textBoxResult.HandleCreated += (s, e) =>
            {
                foreach (System.Action write in pendingLog)
                    write();
                pendingLog.Clear();
            };

            Button clear = CreateActionButton("Clear", "Clear the log.", (s, e) => textBoxResult.Clear());
            Button copy = CreateActionButton("Copy", "Copy the selected log text, or the whole log if nothing is selected.", (s, e) =>
            {
                string text = textBoxResult.SelectionLength > 0 ? textBoxResult.SelectedText : textBoxResult.Text;
                if (!string.IsNullOrEmpty(text))
                    Clipboard.SetText(text);
            });
            Button save = CreateActionButton("Save...", "Save the log to a text file.", SaveLog_Click);

            Button[] buttons = { clear, copy, save };
            textBoxResult.TextChanged += (s, e) =>
            {
                foreach (Button button in buttons)
                    button.Enabled = textBoxResult.TextLength > 0;
            };
            foreach (Button button in buttons)
                button.Enabled = textBoxResult.TextLength > 0;

            TableLayoutPanel row = CreateButtonRow(4, new Padding(0, 3, 0, 0), clear, copy, save);
            tableLayoutPanel11.RowStyles[1].SizeType = SizeType.Absolute;
            tableLayoutPanel11.RowStyles[1].Height = 28;
            tableLayoutPanel11.Controls.Add(row, 0, 1);
        }

        private void SaveLog_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Filter = "Text file (*.txt)|*.txt|All files (*.*)|*.*";
                dialog.FileName = "SMUDebugTool_log_" + DateTime.Now.ToString("yyyyMMdd_HHmmss", CultureInfo.InvariantCulture) + ".txt";

                if (dialog.ShowDialog(this) != DialogResult.OK)
                    return;

                try
                {
                    File.WriteAllText(dialog.FileName, textBoxResult.Text);
                    Log($"Log saved to {dialog.FileName}", LogLevel.Success);
                }
                catch (Exception ex)
                {
                    HandleError($"Could not save the log: {ex.Message}");
                }
            }
        }

        // Tab list shown as a column on the left. The TabControl itself has no visible headers
        // (HiddenHeaderTabControl); this list selects its pages and draws horizontal text.
        private ListBox tabStrip;
        private bool syncingTabStrip;

        // Purple from the application icon
        private static readonly Color TabAccentColor = Color.FromArgb(104, 0, 173);

        private void InitTabStrip()
        {
            tabStrip = new ListBox
            {
                Name = "listBoxTabs",
                BorderStyle = BorderStyle.None,
                BackColor = SystemColors.Control,
                DrawMode = DrawMode.OwnerDrawFixed,
                IntegralHeight = false,
                ItemHeight = 28,
                Width = 90,
                Dock = DockStyle.Left,
                TabStop = false,
            };

            foreach (TabPage page in tabControl1.TabPages)
                tabStrip.Items.Add(page.Text);

            tabStrip.SelectedIndex = tabControl1.SelectedIndex;
            tabStrip.DrawItem += TabStrip_DrawItem;
            tabStrip.SelectedIndexChanged += TabStrip_SelectedIndexChanged;
            tabControl1.SelectedIndexChanged += TabControl1_SelectedIndexChanged;

            splitContainer1.Panel1.Controls.Add(tabStrip);
            tabControl1.BringToFront(); // keeps the TabControl as the Fill control next to the docked list
        }

        private void TabStrip_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (syncingTabStrip || tabStrip.SelectedIndex < 0)
                return;

            syncingTabStrip = true;
            try { tabControl1.SelectedIndex = tabStrip.SelectedIndex; }
            finally { syncingTabStrip = false; }
        }

        // Keeps the list in step when the page is changed from code (for example SelectedTab = tabPagePbo)
        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (syncingTabStrip || tabControl1.SelectedIndex < 0)
                return;

            syncingTabStrip = true;
            try { tabStrip.SelectedIndex = tabControl1.SelectedIndex; }
            finally { syncingTabStrip = false; }
        }

        private void TabStrip_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0 || e.Index >= tabStrip.Items.Count)
                return;

            bool selected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;
            Rectangle bounds = e.Bounds;

            using (Brush background = new SolidBrush(selected ? SystemColors.Window : SystemColors.Control))
                e.Graphics.FillRectangle(background, bounds);

            if (selected)
            {
                using (Brush accent = new SolidBrush(TabAccentColor))
                    e.Graphics.FillRectangle(accent, new Rectangle(bounds.Left, bounds.Top, 3, bounds.Height));
            }

            Rectangle textBounds = new Rectangle(bounds.Left + 10, bounds.Top, bounds.Width - 12, bounds.Height);
            TextRenderer.DrawText(e.Graphics, tabStrip.Items[e.Index].ToString(), tabStrip.Font, textBounds,
                SystemColors.ControlText,
                TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.SingleLine | TextFormatFlags.EndEllipsis);
        }

        private void TabControl1_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPage == tabPageInfo)
                splitContainer1.Panel2Collapsed = true;
            else if (splitContainer1.Panel2Collapsed)
                splitContainer1.Panel2Collapsed = false;
        }

        public string GenerateReportJson()
        {
            StringWriter sw = new StringWriter();
            JsonTextWriter writer = new JsonTextWriter(sw)
            {
                Formatting = Formatting.Indented
            };

            // {
            writer.WriteStartObject();

            writer.WritePropertyName("AppVersion");
            writer.WriteValue(Application.ProductVersion);

            writer.WritePropertyName("OSVersion");
            writer.WriteValue(new ComputerInfo().OSFullName);

            Type type = cpu.systemInfo.GetType();
            PropertyInfo[] properties = type.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                writer.WritePropertyName(property.Name);
                if (property.Name == "CpuId" || property.Name == "PatchLevel")
                    writer.WriteValue($"{property.GetValue(cpu.systemInfo, null):X8}");
                else if (property.Name == "SmuVersion")
                    writer.WriteValue(cpu.systemInfo.SmuVersion.ToString());
                else
                    writer.WriteValue(property.GetValue(cpu.systemInfo, null));
            }

            // "SmuAddresses:"
            writer.WritePropertyName("Mailboxes");
            writer.WriteStartArray();
            foreach (SmuAddressSet set in matches)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("MsgAddress");
                writer.WriteValue($"0x{set.MsgAddress:X8}");
                writer.WritePropertyName("RspAddress");
                writer.WriteValue($"0x{set.RspAddress:X8}");
                writer.WritePropertyName("ArgAddress");
                writer.WriteValue($"0x{set.ArgAddress:X8}");
                writer.WriteEndObject();
            }
            writer.WriteEndArray();

            // }
            writer.WriteEndObject();

            sw.Close();

            return sw.ToString();
        }

        private void BackgroundWorkerReport_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            string unixTimestamp = Convert.ToString((DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalMinutes);
            string fileName = $@"SMUDebug_{unixTimestamp}.json";

            if (File.Exists(fileName))
                File.Delete(fileName);

            using (var sw = new StreamWriter(fileName, true))
            {
                sw.WriteLine(GenerateReportJson());
            }

            //ResetSmuAddresses();
            SetButtonsState();
            SetStatusText("Report Complete.");
            MessageBox.Show($"Report saved as {fileName}");
        }

        // MSRC001_006[4..B] P-state definition (P0..P7).
        // Family 17h/19h: CpuFid[7:0], CpuDfsId[13:8], CpuVid[21:14], IddValue[29:22], IddDiv[31:30]; CoreCOF = 200 * Fid / Did MHz.
        // Family 1Ah+:    CpuFid[11:0] (no DID), CpuVid[21:14], IddValue[29:22], IddDiv[31:30]; CoreCOF = Fid * 5 MHz.
        // SVI3 parts (AM5 Raphael/Phoenix/Granite Ridge, ...): 9-bit VID with CpuVid[8] in bit 32 (EDX[0]).
        // Bit 63 (EDX[31]) is PstateEn.
        private const uint MSR_PSTATE_DEF_BASE = 0xC0010064;
        private const int PSTATE_COUNT = 8;

        private enum PstateVidEncoding
        {
            Unknown,
            SVI2,
            SVI3,
        }

        private static uint PstateMsr(int pstateId) => MSR_PSTATE_DEF_BASE + (uint)pstateId;

        private bool PstateHasDid => cpu.info.family < Cpu.Family.FAMILY_1AH;

        private uint PstateMaxFid => PstateHasDid ? 0xFFu : 0xFFFu;

        private PstateVidEncoding VidEncoding
        {
            get
            {
                if (cpu.info.family >= Cpu.Family.FAMILY_1AH)
                    return PstateVidEncoding.SVI3;

                if (cpu.info.family == Cpu.Family.FAMILY_17H)
                    return PstateVidEncoding.SVI2;

                switch (cpu.info.codeName)
                {
                    // Zen 4: AM5 (Raphael, Phoenix/Phoenix2/HawkPoint desktop APUs), mobile and server
                    case Cpu.CodeName.Raphael:
                    case Cpu.CodeName.DragonRange:
                    case Cpu.CodeName.Phoenix:
                    case Cpu.CodeName.Phoenix2:
                    case Cpu.CodeName.HawkPoint:
                    case Cpu.CodeName.Genoa:
                    case Cpu.CodeName.Bergamo:
                    case Cpu.CodeName.StormPeak:
                        return PstateVidEncoding.SVI3;
                    // Zen 3
                    case Cpu.CodeName.Vermeer:
                    case Cpu.CodeName.Cezanne:
                    case Cpu.CodeName.Milan:
                    case Cpu.CodeName.Chagall:
                        return PstateVidEncoding.SVI2;
                    default:
                        return PstateVidEncoding.Unknown;
                }
            }
        }

        private static bool IsPstateEnabled(uint edx) => (edx >> 31 & 1) == 1;

        private void DecodePstate(uint eax, out uint fid, out uint did)
        {
            if (PstateHasDid)
            {
                fid = eax & 0xFF;
                did = eax >> 8 & 0x3F;
            }
            else
            {
                fid = eax & 0xFFF;
                did = 0;
            }
        }

        private uint EncodePstate(uint eax, uint fid, uint did)
        {
            if (PstateHasDid)
                return eax & ~0x3FFFu | (did & 0x3F) << 8 | fid & 0xFF;

            return eax & ~0xFFFu | fid & 0xFFF;
        }

        private uint DecodePstateVid(uint eax, uint edx)
        {
            uint vid = eax >> 14 & 0xFF;
            if (VidEncoding == PstateVidEncoding.SVI3)
                vid |= (edx & 1) << 8;
            return vid;
        }

        private uint PstateMaxVid => VidEncoding == PstateVidEncoding.SVI3 ? 0x1FFu : 0xFFu;

        // Accepts "0x7B", decimal "123", or a voltage such as "1.2" / "1.2V"; anything after "(" is ignored.
        private bool TryParsePstateVid(string text, out uint vid)
        {
            vid = 0;
            if (VidEncoding == PstateVidEncoding.Unknown || string.IsNullOrEmpty(text))
                return false;

            int paren = text.IndexOf('(');
            string s = (paren >= 0 ? text.Substring(0, paren) : text).Trim();
            if (s.Length == 0)
                return false;

            if (s.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
                return uint.TryParse(s.Substring(2), System.Globalization.NumberStyles.HexNumber,
                    System.Globalization.CultureInfo.InvariantCulture, out vid);

            if (s.EndsWith("V", StringComparison.OrdinalIgnoreCase) || s.Contains("."))
            {
                if (!double.TryParse(s.TrimEnd('V', 'v').Trim(), System.Globalization.NumberStyles.Float,
                    System.Globalization.CultureInfo.InvariantCulture, out double voltage))
                    return false;

                vid = VidEncoding == PstateVidEncoding.SVI3
                    ? ZenStates.Core.Utils.VoltageToVidSVI3(voltage)
                    : ZenStates.Core.Utils.VoltageToVid(voltage);
                return true;
            }

            return uint.TryParse(s, out vid);
        }

        private void EncodePstateVid(uint vid, ref uint eax, ref uint edx)
        {
            eax = eax & ~(0xFFu << 14) | (vid & 0xFF) << 14;
            if (VidEncoding == PstateVidEncoding.SVI3)
                edx = edx & ~1u | vid >> 8 & 1;
        }

        private string FormatPstateVid(uint eax, uint edx)
        {
            uint vid = DecodePstateVid(eax, edx);
            switch (VidEncoding)
            {
                case PstateVidEncoding.SVI2:
                    return $"0x{vid:X2} ({ZenStates.Core.Utils.VidToVoltage(vid):0.000}V)";
                case PstateVidEncoding.SVI3:
                    return $"0x{vid:X2} ({ZenStates.Core.Utils.VidToVoltageSVI3(vid):0.000}V)";
                default:
                    return $"0x{vid:X2}";
            }
        }

        private double PstateFrequencyMhz(uint fid, uint did)
        {
            if (PstateHasDid)
                return did == 0 ? 0 : 200.0 * fid / did;

            // FID values 0x00..0x0F are not valid on Family 1Ah
            return fid > 0x0F ? fid * 5.0 : 0;
        }

        private string FormatPstateFrequency(uint fid, uint did)
        {
            double mhz = PstateFrequencyMhz(fid, did);
            return mhz > 0 ? $"{mhz:0.##}MHz" : "N/A";
        }

        // Returns an error message, or null when FID/DID are within the field ranges.
        private string ValidatePstate(uint fid, uint did)
        {
            if (fid < 0x10 || fid > PstateMaxFid)
                return $"FID must be between 16 and {PstateMaxFid}.";

            if (PstateHasDid && (did < 1 || did > 0x3F))
                return "DID must be between 1 and 63.";

            return null;
        }

        private void InitPstates()
        {
            pstateIdBox.Items.Clear();
            for (int i = 0; i < PSTATE_COUNT; i++)
            {
                uint eax = 0, edx = 0;
                if (cpu.ReadMsr(PstateMsr(i), ref eax, ref edx) && IsPstateEnabled(edx))
                    pstateIdBox.Items.Add(i.ToString());
            }

            // Fall back to the full list if no enabled P-state could be read
            if (pstateIdBox.Items.Count == 0)
            {
                for (int i = 0; i < PSTATE_COUNT; i++)
                    pstateIdBox.Items.Add(i.ToString());
            }

            pstateIdBox.SelectedIndex = 0;
            pstateIdBox.SelectedIndexChanged += PstateIdBox_SelectedIndexChanged;

            pstateFid.Minimum = 0;
            pstateFid.Maximum = PstateMaxFid;
            pstateDid.Minimum = 0;
            pstateDid.Maximum = 0x3F;
            pstateVid.Hexadecimal = true;
            pstateVid.Minimum = 0;
            pstateVid.Maximum = VidEncoding == PstateVidEncoding.Unknown ? 0xFF : PstateMaxVid;

            pstateFid.ValueChanged += PstateFidDid_ValueChanged;
            pstateDid.ValueChanged += PstateFidDid_ValueChanged;
            pstateVid.ValueChanged += PstateVid_ValueChanged;

            ToolTip toolTip = new ToolTip();
            if (PstateHasDid)
            {
                toolTip.SetToolTip(pstateFid, "CpuFid[7:0]. Frequency = 200 x FID / DID MHz");
                toolTip.SetToolTip(pstateDid, "CpuDfsId[13:8]");
            }
            else
            {
                // Family 1Ah (Zen 5, e.g. AM5 Granite Ridge): CpuFid[11:0], no divider
                label9.Text = "DID (n/a)";
                pstateDid.Enabled = false;
                toolTip.SetToolTip(pstateFid, "CpuFid[11:0]. Frequency = FID x 5 MHz");
            }

            ResetPstateFields();
        }

        private void ResetPstateFields()
        {
            pstateDid.Value = 0;
            pstateFid.Value = 0;
            pstateVid.Value = 0;
            pstateFrequency.Text = "";
            labelPstateVid.Text = "VID";
            pstateDid.Enabled = false;
            pstateFid.Enabled = false;
            pstateVid.Enabled = false;
            btnPstateWrite.Enabled = false;
        }

        private void PstateIdBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Don't allow writing values that were read from a different P-state
            ResetPstateFields();
        }

        private void ButtonExport_Click(object sender, EventArgs e)
        {
            RunBackgroundTask(BackgroundWorkerTrySettings_DoWork, BackgroundWorkerReport_RunWorkerCompleted, "SMU report");
        }

        private void PstateFidDid_ValueChanged(object sender, EventArgs e)
        {
            pstateFrequency.Text = FormatPstateFrequency((uint)pstateFid.Value, (uint)pstateDid.Value);
        }

        private void PstateVid_ValueChanged(object sender, EventArgs e)
        {
            uint vid = (uint)pstateVid.Value;
            switch (VidEncoding)
            {
                case PstateVidEncoding.SVI2:
                    labelPstateVid.Text = $"VID ({ZenStates.Core.Utils.VidToVoltage(vid):0.000}V)";
                    break;
                case PstateVidEncoding.SVI3:
                    labelPstateVid.Text = $"VID ({ZenStates.Core.Utils.VidToVoltageSVI3(vid):0.000}V)";
                    break;
                default:
                    labelPstateVid.Text = "VID";
                    break;
            }
        }

        private void BtnPstateRead_Click(object sender, EventArgs e)
        {
            uint eax = default, edx = default;
            var pstateId = int.Parse((string)pstateIdBox.SelectedItem);
            if (!cpu.ReadMsr(PstateMsr(pstateId), ref eax, ref edx))
            {
                SetStatusText($@"Error reading PState {pstateId}!");
                return;
            }

            uint CpuFid, CpuDfsId;
            DecodePstate(eax, out CpuFid, out CpuDfsId);

            pstateDid.Value = Math.Min(CpuDfsId, (uint)pstateDid.Maximum);
            pstateFid.Value = Math.Min(CpuFid, (uint)pstateFid.Maximum);
            pstateVid.Value = Math.Min(DecodePstateVid(eax, edx), (uint)pstateVid.Maximum);
            pstateFrequency.Text = FormatPstateFrequency(CpuFid, CpuDfsId);
            PstateVid_ValueChanged(pstateVid, EventArgs.Empty);

            SetStatusText(IsPstateEnabled(edx)
                ? $@"PState {pstateId} successfully read."
                : $@"PState {pstateId} read, but it is disabled (PstateEn=0).");

            pstateDid.Enabled = PstateHasDid;
            pstateFid.Enabled = true;
            pstateVid.Enabled = VidEncoding != PstateVidEncoding.Unknown;
            btnPstateWrite.Enabled = true;
        }

        private void BtnPstateWrite_Click(object sender, EventArgs e)
        {
            uint fid = (uint)pstateFid.Value;
            uint did = PstateHasDid ? (uint)pstateDid.Value : 0;

            string validationError = ValidatePstate(fid, did);
            if (validationError != null)
            {
                MessageBox.Show(validationError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var pstateId = int.Parse((string)pstateIdBox.SelectedItem);

            var confirmResult = MessageBox.Show(
                $@"This will change PState {pstateId} to {FormatPstateFrequency(fid, did)} and your CPU frequency." +
                Environment.NewLine +
                @"Setting a high frequency could crash/damage your system." +
                Environment.NewLine +
                @"Do you want to continue?",
                @"Confirm PState change",
                MessageBoxButtons.OKCancel
            );

            if (confirmResult != DialogResult.OK) return;

            uint eax = default, edx = default;

            // Re-read so only FID (and DID, where present) change; VID, IDD, PstateEn and
            // reserved bits keep their currently programmed values.
            if (!cpu.ReadMsr(PstateMsr(pstateId), ref eax, ref edx))
            {
                SetStatusText($@"Error reading PState {pstateId}!");
                return;
            }

            uint currentVid = DecodePstateVid(eax, edx);
            eax = EncodePstate(eax, fid, did);

            if (pstateVid.Enabled)
            {
                uint newVid = (uint)pstateVid.Value;

                if (newVid != currentVid)
                    EncodePstateVid(newVid, ref eax, ref edx);
            }

            if (_numaUtil.HighestNumaNode > 0)
            {
                for (var i = 0; i < (int)_numaUtil.HighestNumaNode; i++)
                {
                    if (!WritePstateClick(pstateId, eax, edx, i)) return;
                }
            }
            else
            {
                if (!WritePstateClick(pstateId, eax, edx)) return;
            }

            pstateFrequency.Text = FormatPstateFrequency(fid, did);
            SetStatusText($@"Successfully written PState {pstateId}.");
        }

        // P0 fix C001_0015 HWCR[21]=1
        // Fixes timer issues when not using HPET
        public bool ApplyTscWorkaround()
        {
            uint eax = 0, edx = 0;

            if (cpu.ReadMsr(0xC0010015, ref eax, ref edx))
            {
                eax |= 0x200000;
                return cpu.WriteMsr(0xC0010015, eax, edx);
            }

            SetStatusText($@"Error applying TSC fix!");
            return false;
        }

        private bool WritePstateClick(int pstateId, uint eax, uint edx, int numanode = 0)
        {
            if (_numaUtil.HighestNumaNode > 0) _numaUtil.SetThreadProcessorAffinity((ushort)(numanode + 1), Enumerable.Range(0, Environment.ProcessorCount).ToArray());

            if (!ApplyTscWorkaround()) return false;

            if (!cpu.WriteMsr(PstateMsr(pstateId), eax, edx))
            {
                SetStatusText($@"Error writing PState {pstateId}!");
                return false;
            }

            return true;
        }

        private void PciScan_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                TryConvertToUint(textBoxPciStartReg.Text, out uint startReg);
                TryConvertToUint(textBoxPciEndReg.Text, out uint endReg);

                if (endReg <= startReg)
                {
                    HandleError("End register is not greater than start register");
                    return;
                }

                Invoke(new MethodInvoker(delegate
                {
                    SetStatusText("Scanning PCI addresses, please wait...");
                }));

                string result = "REG         Value(HEX) Value(BIN)" + Environment.NewLine;

                while (startReg <= endReg)
                {
                    var data = cpu.ReadDword(startReg);
                    result += $"0x{startReg:X8}: 0x{data:X8} {Convert.ToString(data, 2).PadLeft(32, '0')}" + Environment.NewLine;
                    startReg += 4;
                }
                    
                ShowResultForm("PCI Scan result", result);
            }
            catch (ApplicationException ex)
            {
                Invoke(new MethodInvoker(delegate
                {
                    SetButtonsState();
                    HandleError(ex.Message);
                }));
            }
        }

        private void Scan_WorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            SetButtonsState();
            SetStatusText("Scan Complete.");
        }

        private void SmuScan_WorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            int index = comboBoxMailboxSelect.SelectedIndex;
            PopulateMailboxesList(comboBoxMailboxSelect.Items);

            for (var i = 0; i < matches.Count; i++)
            {
                AddMailboxToList($"Mailbox {i + 1}", matches[i]);
            }

            if (index > comboBoxMailboxSelect.Items.Count)
                index = 0;

            comboBoxMailboxSelect.SelectedIndex = index;
            SetButtonsState();
            //ResetSmuAddresses();
            SetStatusText("Scan Complete.");
        }

        private void ButtonPciScan_Click(object sender, EventArgs e)
        {
            RunBackgroundTask(PciScan_DoWork, Scan_WorkerCompleted, "PCI scan");
        }

        private void ButtonApplyAC_Click(object sender, EventArgs e)
        {
            int frequency = (int)(((FrequencyListItem)comboBoxACF.SelectedItem).multi * 100.00);
            ApplyFrequencyAllCoreSetting(frequency);
        }

        private void ButtonApplySC_Click(object sender, EventArgs e)
        {
            ApplyFrequencyAllCoreSetting(550);
            int frequency = (int)(((FrequencyListItem)comboBoxSCF.SelectedItem).multi * 100.00);
            ApplyFrequencySingleCoreSetting((CoreListItem)comboBoxCore.SelectedItem, frequency);
        }

        private void ButtonApplyPROCHOT_Click(object sender, EventArgs e)
        {
            if (checkBoxPROCHOT.Checked)
            {
                DisableOCMode();
            }
            EnableOCMode(checkBoxPROCHOT.Checked);
            if (!checkBoxPROCHOT.Checked && cpu.IsProchotEnabled() == true)
            {
                checkBoxPROCHOT.Checked = true;
                HandleError($@"Error, PROCHOT could not be disabled!");
            }
            /*else
            {
                checkBoxPROCHOT.Enabled = false;
                buttonApplyPROCHOT.Enabled = false;
            }*/
        }

        private void ReadMsr_Task(object sender, DoWorkEventArgs e)
        {
            try
            {
                Invoke(new MethodInvoker(delegate
                {
                    SetStatusText("Scanning MSR range, please wait...");
                }));

                string result = "MSR         EDX(63-32) EAX(31-0)" + Environment.NewLine;

                TryConvertToUint(textBoxMsrStart.Text, out uint startReg);
                TryConvertToUint(textBoxMsrEnd.Text, out uint endReg);

                while (startReg <= endReg)
                {
                    uint eax = default, edx = default;
                    if (cpu.ReadMsr(startReg, ref eax, ref edx))
                    {
                        result += $"0x{startReg:X8}: 0x{edx:X8} 0x{eax:X8}" + Environment.NewLine;
                    }

                    startReg += 1;
                }

                ShowResultForm("MSR Scan result", result);
            }
            catch (ApplicationException ex)
            {
                Invoke(new MethodInvoker(delegate
                {
                    SetButtonsState();
                    HandleError(ex.Message);
                }));
            }
        }

        private void ButtonMsrRead_Click(object sender, EventArgs e)
        {
            TryConvertToUint(textBoxMsrAddress.Text, out uint msr);
            uint eax = default, edx = default;
            if (cpu.ReadMsr(msr, ref eax, ref edx))
            {
                textBoxMsrEdx.Text = $"0x{edx:X8}";
                textBoxMsrEax.Text = $"0x{eax:X8}";
            }
        }

        private void ButtonMsrWrite_Click(object sender, EventArgs e)
        {
            TryConvertToUint(textBoxMsrEdx.Text, out uint edx);
            TryConvertToUint(textBoxMsrEax.Text, out uint eax);
            TryConvertToUint(textBoxMsrAddress.Text, out uint msr);

            if (!cpu.WriteMsr(msr, eax, edx))
            {
                HandleError($@"Error writing MSR {textBoxMsrAddress.Text}!");
                return;
            }

            SetStatusText("Write OK.");
        }

        private void ButtonMsrScan_Click(object sender, EventArgs e)
        {
            RunBackgroundTask(ReadMsr_Task, Scan_WorkerCompleted, "MSR scan");
        }

        private void ReadCPUID_Task(object sender, DoWorkEventArgs e)
        {
            try
            {
                Invoke(new MethodInvoker(delegate
                {
                    SetStatusText("Scanning CPUID range, please wait...");
                }));

                string result = "CPUID       EAX        EBX        ECX        EDX" + Environment.NewLine;
                uint LFuncStd = 0, LFuncExt = 0;
                uint eax = 0, ebx = 0, ecx = 0, edx = 0;

                if (cpu.Cpuid(0x00000000, ref eax, ref ebx, ref ecx, ref edx))
                    LFuncStd = eax;

                if (cpu.Cpuid(0x80000000, ref eax, ref ebx, ref ecx, ref edx))
                    LFuncExt = eax - 0x80000000;

                for (uint i = 0; i <= LFuncStd; ++i)
                {
                    var index = 0x00000000 + i;
                    cpu.Cpuid(index, ref eax, ref ebx, ref ecx, ref edx);
                    result += $"0x{index:X8}: 0x{eax:X8} 0x{ebx:X8} 0x{ecx:X8} 0x{edx:X8}" + Environment.NewLine;
                }

                for (uint i = 0; i <= LFuncExt; ++i)
                {
                    var index = 0x80000000 + i;
                    cpu.Cpuid(index, ref eax, ref ebx, ref ecx, ref edx);
                    result += $"0x{index:X8}: 0x{eax:X8} 0x{ebx:X8} 0x{ecx:X8} 0x{edx:X8}" + Environment.NewLine;
                }

                ShowResultForm("CPUID Scan result", result);
            }
            catch (ApplicationException ex)
            {
                Invoke(new MethodInvoker(delegate
                {
                    SetButtonsState();
                    HandleError(ex.Message);
                }));
            }
        }

        private void ButtonCPUIDRead_Click(object sender, EventArgs e)
        {
            TryConvertToUint(textBoxCPUIDAddress.Text, out uint index);
            uint eax = 0, ebx = 0, ecx = 0, edx = 0;
            if (cpu.Cpuid(index, ref eax, ref ebx, ref ecx, ref edx))
            {
                textBoxCPUIDeax.Text = $"0x{eax:X8}";
                textBoxCPUIDebx.Text = $"0x{ebx:X8}";
                textBoxCPUIDecx.Text = $"0x{ecx:X8}";
                textBoxCPUIDedx.Text = $"0x{edx:X8}";
            }
        }

        private void ButtonCPUIDScan_Click(object sender, EventArgs e)
        {
            RunBackgroundTask(ReadCPUID_Task, Scan_WorkerCompleted, "CPUID scan");
        }

        private void ButtonPMTable_Click(object sender, EventArgs e)
        {
            if (cpu.Status == IODriver.LibStatus.OK)
                new Thread(() => new PowerTableMonitor(cpu).ShowDialog()).Start();
            else
                HandleError("IO driver is not responding or not loaded.");
        }

        private void ButtonSMUMonitor_Click(object sender, EventArgs e)
        {
            TryConvertToUint(textBoxCMDAddress.Text, out uint addrMsg);
            TryConvertToUint(textBoxRSPAddress.Text, out uint addrRsp);
            TryConvertToUint(textBoxARGAddress.Text, out uint addrArg);

            new Thread(() => new SMUMonitor(cpu, addrMsg, addrArg, addrRsp).ShowDialog()).Start();
        }

        private void ComboBoxMailboxSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            MailboxListItem item = comboBoxMailboxSelect.SelectedItem as MailboxListItem;
            InitTestMailbox(item.msgAddr, item.rspAddr, item.argAddr);
        }

        private void SettingsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            ExitApplication();
        }

        private void buttonGetCO_Click(object sender, EventArgs e)
        {
            InitPBO();

            if (HasLimitControls)
            {
                // Refresh re-reads the PPT/TDC/EDC/scalar values too, dropping unapplied edits like it does for the margins
                RefreshPowerLimits(true);
            }
        }

        private uint EncodeCoreMarginBitmask(int coreIndex, int coresPerCCD = 8)
        {
            if (cpu.smu.SMU_TYPE >= SMU.SmuType.TYPE_APU0 && cpu.smu.SMU_TYPE <= SMU.SmuType.TYPE_APU2)
            {
                return (uint)coreIndex;
            }

            ApobCoreMapCore apobCore = GetApobCore(coreIndex);
            if (apobCore != null)
            {
                uint? apobMask = cpu.MakeCoreMaskForLogicalCore(apobCore.LogicalIndex);
                if (apobMask != null)
                    return apobMask.Value;
            }

            int ccdIndex = Convert.ToInt32(coreIndex / coresPerCCD);
            int localCoreIndex = coreIndex % coresPerCCD;

            int ccdMask = ccdIndex << 8;
            int mask = ccdMask | localCoreIndex;

            return (uint)(mask << 20);
        }

        // Writes the Curve Optimizer margins that have been changed
        private void ApplyCO(List<string> applied, List<string> failed)
        {
            if (!IsCurveOptimizerSupported)
            {
                HandleError("Curve Optimizer is not supported on this CPU.");
                return;
            }

            var failedCores = new List<int>();
            for (var i = 0; i < GetPhysicalCoreCount(); i++)
            {
                if (!IsCoreEnabled(i))
                    continue;

                NumericUpDown control = GetCOControl(i);
                if (control == null || !control.Enabled || !IsEdited(control))
                    continue;

                if (!cpu.SetPsmMarginSingleCore(EncodeCoreMarginBitmask(i), Convert.ToInt32(control.Value)))
                    failedCores.Add(i);
            }

            if (failedCores.Count == 0)
                applied.Add("Curve Optimizer");
            else
                failed.Add($"Curve Optimizer (core {string.Join(", ", failedCores)})");
        }

        // Writes FMax if it has been changed
        private void ApplyFMax(List<string> applied, List<string> failed)
        {
            uint value = (uint)numericUpDownFmax.Value;
            string command = $"Set FMax {value} MHz";

            if (cpu.SetFMax(value))
            {
                LogCommand(command, "OK", LogLevel.Success);
                applied.Add("FMax");
            }
            else
            {
                LogCommand(command, "Failed", LogLevel.Error);
                failed.Add("FMax");
            }
        }

        // The single Apply button of the PBO tab: writes everything that has been changed (highlighted) and nothing else
        private void ButtonApplyPbo_Click(object sender, EventArgs e)
        {
            var applied = new List<string>();
            var failed = new List<string>();

            bool coEdited = coControls.Values.Any(c => c.Enabled && IsEdited(c));
            bool fmaxEdited = numericUpDownFmax.Enabled && IsEdited(numericUpDownFmax);

            if (coEdited)
                ApplyCO(applied, failed);

            if (fmaxEdited)
                ApplyFMax(applied, failed);

            // Show what the hardware reports now (margins and FMax)
            if (coEdited || fmaxEdited)
                InitPBO();

            ApplyLimitsEdited(applied, failed);
            UpdateDiscardButtons();

            if (failed.Count > 0)
                HandleError($"Failed to set: {string.Join(", ", failed)}");
            else if (applied.Count > 0)
                SetStatusText($"Applied: {string.Join(", ", applied)}.");
            else
                SetStatusText("Nothing to apply, no values have been changed.");
        }

        private string GetWmiInstanceName()
        {
            try
            {
                instanceName = WMI.GetInstanceName(wmiScope, wmiAMDACPI);
            }
            catch
            {
                // ignored
            }

            return instanceName;
        }

        private void PopulateWmiFunctions()
        {
            try
            {
                instanceName = GetWmiInstanceName();
                classInstance = new ManagementObject(wmiScope,
                    $"{wmiAMDACPI}.InstanceName='{instanceName}'",
                    null);

                // Get function names with their IDs
                string[] functionObjects = { "GetObjectID", "GetObjectID2" };
                var index = 1;

                foreach (var functionObject in functionObjects)
                {
                    try
                    {
                        pack = WMI.InvokeMethodAndGetValue(classInstance, functionObject, "pack", null, 0);

                        if (pack != null)
                        {
                            var ID = (uint[])pack.GetPropertyValue("ID");
                            var IDString = (string[])pack.GetPropertyValue("IDString");
                            var Length = (byte)pack.GetPropertyValue("Length");

                            for (var i = 0; i < Length; ++i)
                            {
                                if (IDString[i] == "")
                                    break;

                                WmiCmdListItem item = new WmiCmdListItem($"{IDString[i] + ": "}{ID[i]:X8}", ID[i], !IDString[i].StartsWith("Get"));
                                comboBoxAvailableCommands.Items.Add(item);
                            }
                        }
                        else
                        {
                            comboBoxAvailableCommands.Items.Add("<FAILED>");
                        }

                        FitDropDownWidth(comboBoxAvailableCommands);
                        comboBoxAvailableCommands.SelectedIndex = 0;
                    }
                    catch
                    {
                        // ignored
                    }

                    index++;
                }
            }
            catch
            {
                // ignored
            }
        }

        private void ComboBoxAvailableCommands_SelectedIndexChanged(object sender, EventArgs e)
        {
            WmiCmdListItem command = comboBoxAvailableCommands.SelectedItem as WmiCmdListItem;

            comboBoxAvailableValues.Items.Clear();
            comboBoxAvailableValues.Enabled = false;
            textBoxWmiArgument.Text = "";
            textBoxWmiArgument.Enabled = false;

            if (command.isSet) {
                // Get possible values (index) of a memory option in BIOS
                var dvaluesPack = WMI.InvokeMethodAndGetValue(classInstance, "Getdvalues", "pack", "ID", command.value);
                if (dvaluesPack != null)
                {
                    uint[] DValuesBuffer = (uint[])dvaluesPack.GetPropertyValue("DValuesBuffer");
                    Console.WriteLine(command.text);
                    foreach (uint value in DValuesBuffer)
                    {
                        if (value != 0)
                        {
                            WmiCmdListItem item = new WmiCmdListItem(value.ToString(), value);
                            Console.WriteLine(value);
                            comboBoxAvailableValues.Items.Add(item);
                        }
                    }
                    Console.WriteLine("------------------------");

                    if (comboBoxAvailableValues.Items.Count > 0)
                        comboBoxAvailableValues.Enabled = true;
                    else
                        comboBoxAvailableValues.Items.Add("No values available for this command");
                }
                textBoxWmiArgument.Enabled = true;
            }
            else
            {
                comboBoxAvailableValues.Items.Add("Get commands don't support values");
            }

            FitDropDownWidth(comboBoxAvailableValues);
            comboBoxAvailableValues.SelectedIndex = 0;
        }

        // A long item must not make the combo box, and with it the page, wider than the window: the drop-down list
        // gets the extra width instead
        private static void FitDropDownWidth(ComboBox combo)
        {
            int width = combo.Width;

            foreach (object item in combo.Items)
                width = Math.Max(width, TextRenderer.MeasureText(combo.GetItemText(item), combo.Font).Width + SystemInformation.VerticalScrollBarWidth + 8);

            combo.DropDownWidth = width;
        }

        private void ComboBoxAvailableValues_SelectedIndexChanged(object sender, EventArgs e)
        {
            WmiCmdListItem command = comboBoxAvailableCommands.SelectedItem as WmiCmdListItem;
            if (command.isSet && comboBoxAvailableValues.Enabled)
                textBoxWmiArgument.Text = comboBoxAvailableValues.Text;
            else
                textBoxWmiArgument.Text = "";
        }

        private void ButtonWmiCmdSend_Click(object sender, EventArgs e)
        {
            WmiCmdListItem command = comboBoxAvailableCommands.SelectedItem as WmiCmdListItem;
            uint value = 0;
            if (command.isSet)
            {
                string text = textBoxWmiArgument.Text;
                //if (text.StartsWith("0x"))
                {
                    //TryConvertToUint(text, out value);
                }
                //else
                {
                    value = uint.Parse(text);
                }
            }

            if (value >= 0 && value < 0x10000)
            {
                var response = WMI.RunCommand(classInstance, command.value, value);
                string bytes = string.Join(Environment.NewLine,
                    response.Select((b, i) => new { b, i })
                            .GroupBy(x => x.i / 8)
                            .Select(g => string.Join(" ", g.Select(x => "0x" + x.b.ToString("X2")))));
                LogCommand($"AMD ACPI: {command.text}" + (command.isSet ? $" (argument {value})" : ""), bytes);
            }
        }

        private void ButtonBCLKApply_Click(object sender, EventArgs e)
        {
            double targetBclk = double.Parse(numericUpDownBclk.Text);
            cpu.bclk?.SetBclk(targetBclk);

            double? currentBclk = cpu.bclk?.GetBclk();
            labelBCLK.Text = currentBclk + " MHz";
            numericUpDownBclk.Text = $"{currentBclk}";
        }

        private void BulkMarginChangeHandler(int ccd, int step = 1)
        {
            int startCore = ccd * 8;
            int endCore = Math.Min(startCore + 8, GetPhysicalCoreCount());

            for (var i = startCore; i < endCore; ++i)
            {
                NumericUpDown control = GetCOControl(i);
                if (control != null && control.Enabled && IsCoreEnabled(i))
                {
                    decimal newValue = control.Value + step;
                    newValue = Math.Max(control.Minimum, Math.Min(control.Maximum, newValue));
                    control.Value = newValue;
                }
            }
        }

        private void Button_ccd0_inc_Click(object sender, EventArgs e)
        {
            BulkMarginChangeHandler(0, 1);
        }

        private void Button_ccd1_inc_Click(object sender, EventArgs e)
        {
            BulkMarginChangeHandler(1, 1);
        }

        private void Button_ccd0_dec_Click(object sender, EventArgs e)
        {
            BulkMarginChangeHandler(0, -1);
        }

        private void Button_ccd1_dec_Click(object sender, EventArgs e)
        {
            BulkMarginChangeHandler(1, -1);
        }

        private void ButtonCpuidDecode_Click(object sender, EventArgs e)
        {
            TryConvertToUint(textBoxCpuid.Text.Trim(), out uint eax);

            Cpu.CPUInfo info = new Cpu.CPUInfo
            {
                cpuid = eax
            };
            info.family = (Family)(((info.cpuid & 0xf00) >> 8) + ((info.cpuid & 0xff00000) >> 20));
            info.baseModel = (info.cpuid & 0xf0) >> 4;
            info.extModel = (info.cpuid & 0xf0000) >> 16;
            info.model = info.baseModel + info.extModel * 0x10;
            info.stepping = eax & 0xf;

            string responseString =
                $"family: {info.family} ({(uint)info.family:X2}h)" +
                Environment.NewLine +
                $"base model: 0x{info.baseModel:X1}" +
                Environment.NewLine +
                $"ext. model: 0x{info.extModel:X1}" +
                Environment.NewLine +
                $"model: 0x{info.model:X2}" +
                Environment.NewLine +
                $"stepping: {info.stepping}";

            LogCommand($"CPUID decode 0x{info.cpuid:X8}", responseString);
        }

        private void BtnSaveCOProfile_Click(object sender, EventArgs e)
        {
            List<string> lines = new List<string>();

            if (IsCurveOptimizerSupported)
            {
                for (var i = 0; i < GetPhysicalCoreCount(); i++)
                {
                    NumericUpDown control = GetCOControl(i);
                    if (control != null && control.Enabled)
                        lines.Add(string.Format(CultureInfo.InvariantCulture, "[{0},{1}]", i, Convert.ToInt32(control.Value)));
                }
            }

            // FMax and the power limits: only values that are known (read from the CPU, or entered)
            foreach (KeyValuePair<string, NumericUpDown> entry in PboLimitProfileControls)
            {
                if (entry.Value.Enabled && HasValue(entry.Value))
                    lines.Add(string.Format(CultureInfo.InvariantCulture, "{0}={1}", entry.Key, entry.Value.Value));
            }

            if (lines.Count == 0)
            {
                SetStatusText("Nothing to save.");
                return;
            }

            try
            {
                // Keep the Curve Shaper section of the profile; this button only owns the PBO part
                lines.AddRange(ReadProfileLines().Where(l => l.StartsWith(CsProfilePrefix)));

                Directory.CreateDirectory(profilesPath);
                File.WriteAllLines(defaultsPath, lines);

                SetStatusText("PBO profile saved.");
                Log($"Profile saved in {defaultsPath}", LogLevel.Success);
            }
            catch (Exception)
            {
                HandleError("Could not save profile to file!");
            }
        }

        // FMax and power limits as they are stored in the profile file: "fmax=<MHz>", "ppt=<W>", "tdc=<A>", "edc=<A>",
        // "scalar=<x>". Older versions ignore the keys they do not know.
        private IEnumerable<KeyValuePair<string, NumericUpDown>> PboLimitProfileControls => new[]
        {
            new KeyValuePair<string, NumericUpDown>("fmax", numericUpDownFmax),
            new KeyValuePair<string, NumericUpDown>("ppt", numericUpDownPpt),
            new KeyValuePair<string, NumericUpDown>("tdc", numericUpDownTdc),
            new KeyValuePair<string, NumericUpDown>("edc", numericUpDownEdc),
            new KeyValuePair<string, NumericUpDown>("scalar", numericUpDownScalar),
        };

        // Curve Shaper section of the profile: one line per frequency tier, "cs=<tier>,<low>,<medium>,<high>".
        // Older versions ignore these lines.
        private const string CsProfilePrefix = "cs=";

        private List<string> ReadProfileLines()
        {
            return File.Exists(defaultsPath) ? File.ReadAllLines(defaultsPath).ToList() : new List<string>();
        }

        private List<int[]> LoadCSProfile()
        {
            List<int[]> rows = new List<int[]>();
            int tiers = CsControls.GetLength(0);

            foreach (string line in ReadProfileLines())
            {
                if (!line.StartsWith(CsProfilePrefix))
                    continue;

                string[] parts = line.Substring(CsProfilePrefix.Length).Replace(" ", "").Split(',');
                if (parts.Length != 4)
                    continue;

                int[] row = new int[4];
                bool ok = true;
                for (int i = 0; i < 4; i++)
                    ok &= int.TryParse(parts[i], NumberStyles.Integer, CultureInfo.InvariantCulture, out row[i]);

                if (ok && row[0] >= 0 && row[0] < tiers)
                    rows.Add(row);
            }

            return rows;
        }

        private void ApplyCSProfile(out int applied, out int failed)
        {
            applied = 0;
            failed = 0;

            if (!IsCurveShaperSupported)
                return;

            NumericUpDown[,] controls = CsControls;
            foreach (int[] row in LoadCSProfile())
            {
                int tier = row[0];
                SMU.Status status = cpu.SetCurveShaperMargin(
                    marginHigh: ClampMargin(controls[tier, 2], row[3]),
                    marginMedium: ClampMargin(controls[tier, 1], row[2]),
                    marginLow: ClampMargin(controls[tier, 0], row[1]),
                    frequencyTier: tier);

                if (status == SMU.Status.OK)
                {
                    applied++;
                }
                else
                {
                    failed++;
                    Console.WriteLine($"Failed to apply saved Curve Shaper margins for tier {tier}: {status}");
                }
            }
        }

        private static int ClampMargin(NumericUpDown control, int value)
        {
            return (int)Math.Max(control.Minimum, Math.Min(control.Maximum, value));
        }

        private void BtnSaveCSProfile_Click(object sender, EventArgs e)
        {
            try
            {
                NumericUpDown[,] controls = CsControls;

                // Keep everything that is not Curve Shaper (CO margins, FMax)
                List<string> lines = ReadProfileLines().Where(l => !l.StartsWith(CsProfilePrefix)).ToList();

                for (int tier = 0; tier < controls.GetLength(0); tier++)
                {
                    lines.Add(string.Format(CultureInfo.InvariantCulture, "{0}{1},{2},{3},{4}",
                        CsProfilePrefix, tier,
                        (int)controls[tier, 0].Value, (int)controls[tier, 1].Value, (int)controls[tier, 2].Value));
                }

                Directory.CreateDirectory(profilesPath);
                File.WriteAllLines(defaultsPath, lines);

                SetStatusText("Curve Shaper margins saved to profile.");
                Log($"Curve Shaper margins saved in {defaultsPath}", LogLevel.Success);
            }
            catch (Exception)
            {
                HandleError("Could not save profile to file!");
            }
        }

        private void BtnLoadCSProfile_Click(object sender, EventArgs e)
        {
            List<int[]> rows;
            try
            {
                rows = LoadCSProfile();
            }
            catch (Exception)
            {
                HandleError("Could not load saved profile!");
                return;
            }

            if (rows.Count == 0)
            {
                SetStatusText("No Curve Shaper margins saved in the profile.");
                return;
            }

            NumericUpDown[,] controls = CsControls;
            foreach (int[] row in rows)
            {
                for (int band = 0; band < 3; band++)
                    controls[row[0], band].Value = ClampMargin(controls[row[0], band], row[band + 1]);
            }

            SetStatusText("Curve Shaper margins loaded from profile. Press Apply to write them to the SMU.");
            Log($"Saved Curve Shaper margins loaded from {defaultsPath}", LogLevel.Success);
        }

        // Everything saved in the profile file except Curve Shaper (see LoadCSProfile)
        private sealed class PboProfile
        {
            public readonly List<Tuple<int, int>> Margins = new List<Tuple<int, int>>();
            public decimal? FMax;
            public decimal? Ppt;
            public decimal? Tdc;
            public decimal? Edc;
            public decimal? Scalar;
        }

        private PboProfile LoadPboProfile()
        {
            PboProfile profile = new PboProfile();
            try
            {
                if (!Directory.Exists(profilesPath))
                {
                    MessageBox.Show("Profiles directory does not exist, created one for you.");
                    Directory.CreateDirectory(profilesPath);
                }

                // load from file if it exists
                if (File.Exists(defaultsPath))
                {
                    foreach (var line in File.ReadAllLines(defaultsPath))
                    {
                        if (line.StartsWith("["))
                        {
                            var values = line.Replace("[", "").Replace("]", "").Replace(" ", "").Split(',');
                            Int32.TryParse(values[0], NumberStyles.Integer, CultureInfo.InvariantCulture, out int index);
                            Int32.TryParse(values[1], NumberStyles.Integer, CultureInfo.InvariantCulture, out int margin);
                            profile.Margins.Add(new Tuple<int, int>(index, margin));
                            continue;
                        }

                        int separator = line.IndexOf('=');
                        if (separator <= 0 || !decimal.TryParse(line.Substring(separator + 1), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal value))
                            continue;

                        switch (line.Substring(0, separator).Trim().ToLowerInvariant())
                        {
                            case "fmax": profile.FMax = value; break;
                            case "ppt": profile.Ppt = value; break;
                            case "tdc": profile.Tdc = value; break;
                            case "edc": profile.Edc = value; break;
                            case "scalar": profile.Scalar = value; break;
                        }
                    }
                }
                else
                {
                    HandleError("No CO profile saved.");
                }
            }
            catch (Exception)
            {
                HandleError("Could not load saved profile!");
            }

            return profile;
        }

        // Puts a saved value in the control. It counts as an edit (highlighted) until it is applied.
        private static bool LoadProfileValue(NumericUpDown control, decimal? value)
        {
            if (!value.HasValue || !control.Enabled)
                return false;

            control.Value = ClampValue(control, value.Value);
            return true;
        }

        private void BtnLoadCOProfile_Click(object sender, EventArgs e)
        {
            PboProfile profile = LoadPboProfile();
            int loaded = 0;

            if (IsCurveOptimizerSupported)
            {
                foreach (Tuple<int, int> margin in profile.Margins)
                {
                    NumericUpDown control = GetCOControl(margin.Item1);
                    if (control != null && control.Enabled)
                    {
                        control.Value = margin.Item2;
                        loaded++;
                    }
                }
            }

            foreach (var entry in new[]
            {
                Tuple.Create(numericUpDownFmax, profile.FMax),
                Tuple.Create(numericUpDownPpt, profile.Ppt),
                Tuple.Create(numericUpDownTdc, profile.Tdc),
                Tuple.Create(numericUpDownEdc, profile.Edc),
                Tuple.Create(numericUpDownScalar, profile.Scalar),
            })
            {
                if (LoadProfileValue(entry.Item1, entry.Item2))
                    loaded++;
            }

            if (loaded > 0)
            {
                SetStatusText("Profile loaded. Press Apply to write the changed values to the SMU.");
                Log($"Saved PBO profile loaded from {defaultsPath}", LogLevel.Success);
            }
        }

        static bool TaskExists(string taskName)
        {
            // Open the task service
            using (TaskService taskService = new TaskService())
            {
                // Attempt to retrieve the task
                Task task = taskService.GetTask(taskName);
                return task != null;
            }
        }

        // Number of triggers a current-version task must have: startup, logon, resume from sleep
        private const int StartupTaskTriggerCount = 3;

        // Rights that let an account modify, replace or take over a file or folder
        private const FileSystemRights ModifyRights =
            FileSystemRights.WriteData | FileSystemRights.AppendData | FileSystemRights.Delete |
            FileSystemRights.DeleteSubdirectoriesAndFiles | FileSystemRights.ChangePermissions |
            FileSystemRights.TakeOwnership;

        private static bool IsTrustedPrincipal(SecurityIdentifier sid)
        {
            if (sid.IsWellKnown(WellKnownSidType.LocalSystemSid) ||
                sid.IsWellKnown(WellKnownSidType.BuiltinAdministratorsSid) ||
                sid.IsWellKnown(WellKnownSidType.CreatorOwnerSid))
                return true;

            // NT SERVICE\TrustedInstaller
            return sid.Value.StartsWith("S-1-5-80-956008885-");
        }

        private static bool HasUntrustedWriteAccess(AuthorizationRuleCollection rules, out string who)
        {
            who = null;
            foreach (FileSystemAccessRule rule in rules)
            {
                if (rule.AccessControlType != AccessControlType.Allow || (rule.FileSystemRights & ModifyRights) == 0)
                    continue;

                SecurityIdentifier sid = (SecurityIdentifier)rule.IdentityReference;
                if (IsTrustedPrincipal(sid))
                    continue;

                try { who = sid.Translate(typeof(NTAccount)).Value; }
                catch (Exception) { who = sid.Value; }
                return true;
            }
            return false;
        }

        // The startup task runs this executable as SYSTEM. If anyone other than SYSTEM/Administrators
        // can modify the executable or the folder it loads files from, they can run code as SYSTEM.
        static bool IsExecutableLocationSecure(string executablePath, out string reason)
        {
            reason = null;
            try
            {
                string who;
                string dir = Path.GetDirectoryName(executablePath);

                if (HasUntrustedWriteAccess(new FileInfo(executablePath).GetAccessControl()
                    .GetAccessRules(true, true, typeof(SecurityIdentifier)), out who))
                {
                    reason = $"The program file can be modified by \"{who}\".";
                    return false;
                }

                if (HasUntrustedWriteAccess(new DirectoryInfo(dir).GetAccessControl()
                    .GetAccessRules(true, true, typeof(SecurityIdentifier)), out who))
                {
                    reason = $"The folder \"{dir}\" can be modified by \"{who}\".";
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                reason = $"The permissions of the program folder could not be verified ({ex.Message}).";
                return false;
            }
        }

        static bool TaskIsUpToDate(string taskName)
        {
            using (TaskService taskService = new TaskService())
            {
                Task task = taskService.GetTask(taskName);
                return task != null
                    && task.Definition.Triggers.Count >= StartupTaskTriggerCount
                    && task.Definition.Principal.LogonType == TaskLogonType.ServiceAccount;
            }
        }

        static void AddTaskToScheduler(string taskName, string executablePath, int delaySeconds = 0)
        {
            // Create a new task service
            using (TaskService taskService = new TaskService())
            {
                // Create a new task definition
                TaskDefinition taskDefinition = taskService.NewTask();

                // Set the task properties
                taskDefinition.RegistrationInfo.Description = "Run Ryzen SMU Debug Tool on system startup, user logon and resume from sleep to apply CO profile. Automatically created by RyzenSDT. Remove manually or from the checkbox in PBO tab.";
                taskDefinition.Principal.UserId = "SYSTEM";
                taskDefinition.Principal.RunLevel = TaskRunLevel.Highest;
                taskDefinition.Principal.LogonType = TaskLogonType.ServiceAccount;

                // Do not skip or kill the task on battery power (laptops), and do not time it out
                taskDefinition.Settings.DisallowStartIfOnBatteries = false;
                taskDefinition.Settings.StopIfGoingOnBatteries = false;
                taskDefinition.Settings.ExecutionTimeLimit = TimeSpan.Zero;

                // Trigger 1: system startup
                BootTrigger bootTrigger = new BootTrigger();
                bootTrigger.Delay = TimeSpan.FromSeconds(delaySeconds);
                taskDefinition.Triggers.Add(bootTrigger);

                // Trigger 2: user logon
                LogonTrigger logonTrigger = new LogonTrigger();
                logonTrigger.Delay = TimeSpan.FromSeconds(delaySeconds);
                taskDefinition.Triggers.Add(logonTrigger);

                // Trigger 3: resume from sleep/hibernate (System log, Power-Troubleshooter, event ID 1)
                EventTrigger resumeTrigger = new EventTrigger("System", "Microsoft-Windows-Power-Troubleshooter", 1);
                resumeTrigger.Delay = TimeSpan.FromSeconds(delaySeconds);
                taskDefinition.Triggers.Add(resumeTrigger);

                // Create an action that runs the specified executable
                ExecAction execAction = new ExecAction(executablePath, "--applyprofile");
                taskDefinition.Actions.Add(execAction);

                // Register the task in the root folder of the Task Scheduler (replaces an existing one)
                taskService.RootFolder.RegisterTaskDefinition(
                    taskName,
                    taskDefinition,
                    TaskCreation.CreateOrUpdate,
                    "SYSTEM",
                    null,
                    TaskLogonType.ServiceAccount);
            }
        }

        static void RemoveTaskFromScheduler(string taskName)
        {
            // Open the task service
            using (TaskService taskService = new TaskService())
            {
                // Delete the task from the Task Scheduler
                taskService.RootFolder.DeleteTask(taskName, false);
            }
        }

        private bool SetStartup(bool isChecked = false)
        {
            /*using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                if (isChecked && key.GetValue("RyzenSDT") == null)
                {
                    key.SetValue("RyzenSDT", Application.ExecutablePath + " --applyprofile");
                }
                else if (!isChecked && key.GetValue("RyzenSDT") != null)
                {
                    key.DeleteValue("RyzenSDT", false);
                }
            }*/

            if (isChecked)
            {
                bool exists = TaskExists("RyzenSDT");
                if (exists && TaskIsUpToDate("RyzenSDT"))
                    return true;

                if (!IsExecutableLocationSecure(Application.ExecutablePath, out string reason))
                {
                    DialogResult answer = MessageBox.Show(
                        "The startup task runs as SYSTEM (the most privileged account) at every boot, logon and resume from sleep.\n\n" +
                        reason + "\n\n" +
                        "Any non-administrator that can modify these files could make Windows run its own code as SYSTEM.\n\n" +
                        "To be safe, move SMU Debug Tool to a folder only administrators can write to (for example under C:\\Program Files) and enable this option again.\n\n" +
                        "Create the startup task anyway?",
                        "Security warning",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning,
                        MessageBoxDefaultButton.Button2);

                    if (answer != DialogResult.Yes)
                    {
                        // Leave an existing (older) task as it is; do not create a new one
                        return exists;
                    }
                }

                // Create the task, or upgrade one made by an older version (logon-only, interactive user)
                AddTaskToScheduler("RyzenSDT", Application.ExecutablePath, 0);
                return true;
            }

            if (TaskExists("RyzenSDT"))
            {
                RemoveTaskFromScheduler("RyzenSDT");
            }
            return false;
        }

        private void CheckBoxApplyCOStartup_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox box = (CheckBox)sender;
            bool requested = box.Checked;
            bool active = SetStartup(requested);
            box.Checked = active;

            if (requested && !active)
                Log("Startup task was not created.", LogLevel.Warning);
            else
                Log("Startup settings saved.", LogLevel.Success);
        }

        private sealed class NotificationLevelItem
        {
            public DriverCleaner.NotificationLevel Level { get; }
            public string Text { get; }

            public NotificationLevelItem(DriverCleaner.NotificationLevel level, string text)
            {
                Level = level;
                Text = text;
            }

            public override string ToString() => Text;
        }

        private void InitDriverNotificationCombo()
        {
            comboBoxDriverNotifications.Items.Clear();
            foreach (DriverCleaner.NotificationLevel level in Enum.GetValues(typeof(DriverCleaner.NotificationLevel)))
            {
                var attr = typeof(DriverCleaner.NotificationLevel).GetField(level.ToString())
                    .GetCustomAttributes(typeof(DescriptionAttribute), false)
                    .OfType<DescriptionAttribute>().FirstOrDefault();
                comboBoxDriverNotifications.Items.Add(new NotificationLevelItem(level, attr?.Description ?? level.ToString()));
            }

            int saved = AppSettings.Instance.AutoUninstallDriverNotificationLevel;
            NotificationLevelItem selected = comboBoxDriverNotifications.Items.Cast<NotificationLevelItem>()
                .FirstOrDefault(i => (int)i.Level == saved);
            comboBoxDriverNotifications.SelectedItem = selected ?? comboBoxDriverNotifications.Items[comboBoxDriverNotifications.Items.Count - 1];

            comboBoxDriverNotifications.Enabled = checkBoxAutoUninstallDriver.Checked;
            formToolTip.SetToolTip(comboBoxDriverNotifications, "Which tray notifications to show while the driver is being removed.");
        }

        private void ComboBoxDriverNotifications_SelectedIndexChanged(object sender, EventArgs e)
        {
            NotificationLevelItem item = comboBoxDriverNotifications.SelectedItem as NotificationLevelItem;
            if (item == null || (int)item.Level == AppSettings.Instance.AutoUninstallDriverNotificationLevel)
                return;

            AppSettings.Instance.AutoUninstallDriverNotificationLevel = (int)item.Level;
            AppSettings.Instance.Save();
        }

        private void CheckBoxAutoUninstallDriver_CheckedChanged(object sender, EventArgs e)
        {
            AppSettings.Instance.AutoUninstallDriver = (sender as CheckBox).Checked;
            AppSettings.Instance.Save();
            comboBoxDriverNotifications.Enabled = (sender as CheckBox).Checked;
        }

        private void tableLayoutPanel14_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ButtonApplyCoreMap_Click(object sender, EventArgs e)
        {
            uint ccd0 = 0x8000;
            uint ccd1 = 0x8100;

            for (int i = 0; i < 8; i++)
            {
                CheckBox control = (CheckBox)Controls.Find($"checkBox{i}", true)[0];
                if (control != null && control.Enabled)
                {
                    if (!control.Checked)
                    {
                        int logicalIndex = Convert.ToInt32(control.Tag as string);
                        ccd0 = Utils.SetBits(ccd0, logicalIndex, 1, 1);
                    }
                }
            }

            for (int i = 0; i < 8; i++)
            {
                CheckBox control = (CheckBox)Controls.Find($"checkBox{i + 8}", true)[0];
                if (control != null && control.Enabled)
                {
                    if (!control.Checked)
                    {
                        int logicalIndex = Convert.ToInt32(control.Tag as string);
                        ccd1 = Utils.SetBits(ccd1, logicalIndex, 1, 1);
                    }
                }
            }

            var cmdItem = comboBoxAvailableCommands.Items
                     .OfType<WmiCmdListItem>()
                     .FirstOrDefault(item => item.text.Contains("Software Downcore Config"));

            if (cmdItem != null) {
                WMI.RunCommand(classInstance, cmdItem.value, ccd0);
                WMI.RunCommand(classInstance, cmdItem.value, ccd1);
            }

            cmdItem = comboBoxAvailableCommands.Items
                     .OfType<WmiCmdListItem>()
                     .FirstOrDefault(item => item.text.Contains("Set SMTEn"));

            if (cmdItem != null)
            {
                WMI.RunCommand(classInstance, cmdItem.value, checkBoxSMT.Checked ? 1u : 0);
            }

            ConfirmWindowsRestart();
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            var cmdItem = comboBoxAvailableCommands.Items
                     .OfType<WmiCmdListItem>()
                     .FirstOrDefault(item => item.text.Contains("Software Downcore Config"));

            if (cmdItem != null)
            {
                WMI.RunCommand(classInstance, cmdItem.value, 0x8000);
                WMI.RunCommand(classInstance, cmdItem.value, 0x81FF);
            }

            cmdItem = comboBoxAvailableCommands.Items
                     .OfType<WmiCmdListItem>()
                     .FirstOrDefault(item => item.text.Contains("Set SMTEn"));

            if (cmdItem != null)
            {
                WMI.RunCommand(classInstance, cmdItem.value, 0);
            }

            ConfirmWindowsRestart();
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            var cmdItem = comboBoxAvailableCommands.Items
                     .OfType<WmiCmdListItem>()
                     .FirstOrDefault(item => item.text.Contains("Software Downcore Config"));

            if (cmdItem != null)
            {
                WMI.RunCommand(classInstance, cmdItem.value, 0x8000);
                WMI.RunCommand(classInstance, cmdItem.value, 0x8100);
            }

            cmdItem = comboBoxAvailableCommands.Items
                     .OfType<WmiCmdListItem>()
                     .FirstOrDefault(item => item.text.Contains("Set SMTEn"));

            if (cmdItem != null)
            {
                WMI.RunCommand(classInstance, cmdItem.value, 1);
            }

            ConfirmWindowsRestart();
        }

        private void ConfirmWindowsRestart()
        {
            var result = MessageBox.Show(
                "A restart is required to apply the changes. Would you like to restart now?",
                "Confirm Restart",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    // Restart Windows
                    Process.Start(new ProcessStartInfo("shutdown", "/r /t 0")
                    {
                        CreateNoWindow = true,
                        UseShellExecute = false
                    });
                }
                catch (Exception ex)
                {
                    HandleError($"Failed to restart: {ex.Message}");
                }
            }
        }

        private void RadioButtonManualCoreControl_CheckedChanged(object sender, EventArgs e)
        {
            bool manual = radioButtonManualCoreControl.Checked == true;
            panelManualCoreControl.Enabled = manual;
            panelX3D.Enabled = !manual;
        }

        private void ButtonPCIRangeMonitor_Click(object sender, EventArgs e)
        {
            TryConvertToUint(textBoxPciStartReg.Text, out uint startAddress);
            TryConvertToUint(textBoxPciEndReg.Text, out uint endAddress);

            new Thread(() => new PCIRangeMonitor(cpu, startAddress, endAddress).ShowDialog()).Start();
        }

        private void ButtonDump_Click(object sender, EventArgs e)
        {
            string name = textBoxDumpName.Text.Trim();

            if (string.IsNullOrWhiteSpace(name))
            {
                HandleError("Please specify a valid file name!");
                return;
            }

            if (File.Exists(name))
            {
                var result = MessageBox.Show(
                    $"File {name} already exists. Overwrite?",
                    "Confirm Overwrite",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);
                if (result != DialogResult.Yes)
                {
                    return;
                }
            }

            try
            {
                TryConvertToUint(textBoxDumpStartAddress.Text.Trim(), out uint startAddress);
                TryConvertToUint(textBoxDumpEndAddress.Text.Trim(), out uint endAddress);

                SetStatusText(name + ": Dumping memory, please wait...");
                
                var stopwatch = Stopwatch.StartNew();
                MemoryDumper.Dump32BitAddressSpaceAsBytes(name, startAddress, endAddress);
                stopwatch.Stop();
                
                string elapsedTime = $"{stopwatch.Elapsed.TotalSeconds:F2}";
                SetStatusText(name + $": Dump complete. ({elapsedTime}s)");
                MessageBox.Show($"Memory dump completed successfully to file: {name}\n\nTime elapsed: {elapsedTime}s", "Dump Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                HandleError("Invalid address format!");
                return;
            }
        }

        private void ButtonRefreshCS_Click(object sender, EventArgs e)
        {
            InitCS(showStatus: true);
        }

        private void ButtonResetCS_Click(object sender, EventArgs e)
        {
            ResetToBaseline(CsControls.Cast<NumericUpDown>());
            SetStatusText("Curve Shaper edits reverted.");
        }

        private void ButtonZeroCS_Click(object sender, EventArgs e)
        {
            foreach (NumericUpDown nud in CsControls)
                nud.Value = 0;
        }

        private void ButtonApplyCS_Click(object sender, EventArgs e)
        {
            var errorMessages = new List<string>();
            NumericUpDown[,] controls = CsControls;

            for (int tier = 0; tier < controls.GetLength(0); tier++)
            {
                // Only the tiers with a changed value are written
                if (!HasPendingEdits(new[] { controls[tier, 0], controls[tier, 1], controls[tier, 2] }))
                    continue;

                SMU.Status status = cpu.SetCurveShaperMargin(
                    marginHigh: (int)controls[tier, 2].Value,
                    marginMedium: (int)controls[tier, 1].Value,
                    marginLow: (int)controls[tier, 0].Value,
                    frequencyTier: tier);

                if (status != SMU.Status.OK)
                    errorMessages.Add($"Failed to set Curve Shaper margins for frequency tier {tier} ({CsTierNames[tier]}): {status}");
            }

            if (errorMessages.Count == 0)
            {
                SetStatusText("Curve Shaper margins applied successfully.");
                Log("Curve Shaper margins applied successfully.", LogLevel.Success);
            }
            else
            {
                Log(string.Join(Environment.NewLine, errorMessages), LogLevel.Error);
                SetStatusText("One or more errors occurred while applying Curve Shaper margins.");
            }

            InitCS();
        }
    }

    /// <summary>
    /// TabControl that reserves no space for its tab headers. Pages are selected from code
    /// or from a separate control (see SettingsForm.InitTabStrip).
    /// </summary>
    internal class HiddenHeaderTabControl : TabControl
    {
        private const int TCM_ADJUSTRECT = 0x1328;

        protected override void WndProc(ref Message m)
        {
            // Report that the headers take no room, so the pages fill the whole control
            if (m.Msg == TCM_ADJUSTRECT && !DesignMode)
            {
                m.Result = (IntPtr)1;
                return;
            }

            base.WndProc(ref m);
        }
    }
}
