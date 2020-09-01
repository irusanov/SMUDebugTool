using Microsoft.VisualBasic.Devices;
using Newtonsoft.Json;
using OpenLibSys;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Management;
using System.Threading;
using System.Windows.Forms;
using ZenStates;
using ZenStatesDebugTool.Properties;

namespace ZenStatesDebugTool
{
    public partial class SettingsForm : Form
    {
        //private static readonly int Threads = Convert.ToInt32(Environment.GetEnvironmentVariable("NUMBER_OF_PROCESSORS"));
        private BackgroundWorker backgroundWorker1;
        private NUMAUtil _numaUtil;
        private readonly List<BiosACPIFunction> biosFunctions = new List<BiosACPIFunction>();
        private readonly Ops OPS = new Ops();
        private SystemInfo SI;
        List<SmuAddressSet> matches;
        private int _coreCount;

        readonly Mutex hMutexPci;

        private uint SMU_ADDR_MSG;
        private uint SMU_ADDR_RSP;
        private uint SMU_ADDR_ARG;

        private class SmuAddressSet
        {
            public uint MsgAddress;
            public uint RspAddress;
            public uint ArgAddress;

            public SmuAddressSet()
            {
                MsgAddress = 0;
                RspAddress = 0;
                ArgAddress = 0;
            }

            public SmuAddressSet(uint msgAddress, uint rspAddress, uint argAddress)
            {
                MsgAddress = msgAddress;
                RspAddress = rspAddress;
                ArgAddress = argAddress;
            }
        }

        public SettingsForm()
        {
            InitializeComponent();

            hMutexPci = new Mutex();

            _numaUtil = new NUMAUtil();
            textBoxResult.Text = $@"Detected NUMA nodes. ({_numaUtil.HighestNumaNode + 1})" + textBoxResult.Text;

            _coreCount = new ManagementObjectSearcher("Select * from Win32_Processor").Get().Cast<ManagementBaseObject>().Sum(item => int.Parse(item["NumberOfCores"].ToString()));

            try
            {
                OPS.CheckOlsStatus();
                InitForm();
            }
            catch (ApplicationException ex)
            {
                MessageBox.Show(ex.Message, Resources.Error);
                Dispose();
                ExitApplication();
            }
        }

        private static void ExitApplication()
        {
            if (Application.MessageLoop)
                Application.Exit();
            else
                Environment.Exit(1);
        }

        private void InitSystemInfo()
        {
            var cpufamily = OPS.GetCpuFamily();
            if (cpufamily != SMU.CpuFamily.FAMILY_17H && cpufamily != SMU.CpuFamily.FAMILY_19H)
            {
                MessageBox.Show("CPU is not supported.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ExitApplication();
            }

            int[] coreCount = OPS.GetCoreCount();
            SI = new SystemInfo
            {
                CpuId = OPS.GetCpuId(),
                CpuName = OPS.GetCpuName(),
                NodesPerProcessor = OPS.GetCpuNodes(),
                PackageType = OPS.GetPkgType(),
                PatchLevel = OPS.GetPatchLevel(),
                SmuVersion = OPS.Smu.Version,
                FusedCoreCount = coreCount[0],
                Threads = coreCount[1],
                CCDCount = OPS.GetCCDCount(),
                CodeName = $"{OPS.CpuType}",
            };

            SI.Model = (SI.CpuId & 0xff) >> 4;
            SI.ExtendedModel = SI.Model + ((SI.CpuId >> 12) & 0xF0);


            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_BaseBoard");
            foreach (ManagementObject obj in searcher.Get())
            {
                SI.MbVendor = ((string)obj["Manufacturer"]).Trim();
                SI.MbName = ((string)obj["Product"]).Trim();
            }
            if (searcher != null) searcher.Dispose();

            searcher = new ManagementObjectSearcher("SELECT * FROM Win32_BIOS");
            foreach (ManagementObject obj in searcher.Get())
            {
                SI.BiosVersion = ((string)obj["SMBIOSBIOSVersion"]).Trim();
            }
            if (searcher != null) searcher.Dispose();
        }

        private BiosACPIFunction GetFunctionByIdString(string name)
        {
            return biosFunctions.Find(x => x.IDString == name);
        }

        private void ResetSmuAddresses()
        {
            OPS.Smu.SMU_ADDR_MSG = SMU_ADDR_MSG;
            OPS.Smu.SMU_ADDR_RSP = SMU_ADDR_RSP;
            OPS.Smu.SMU_ADDR_ARG = SMU_ADDR_ARG;

            textBoxCMDAddress.Text = $"0x{Convert.ToString(SMU_ADDR_MSG, 16).ToUpper()}";
            textBoxRSPAddress.Text = $"0x{Convert.ToString(SMU_ADDR_RSP, 16).ToUpper()}";
            textBoxARGAddress.Text = $"0x{Convert.ToString(SMU_ADDR_ARG, 16).ToUpper()}";

            textBoxCMD.Text = "0x1";
            textBoxARG0.Text = "0x0";
        }

        private void DisplaySystemInfo()
        {
            cpuInfoLabel.Text = SI.CpuName;
            extendedModelInfoLabel.Text = $"{SI.ExtendedModel:X8}";
            packageTypeInfoLabel.Text = $"{SI.PackageType} ({(SMU.PackageType)SI.PackageType})";
            mbVendorInfoLabel.Text = SI.MbVendor;
            mbModelInfoLabel.Text = SI.MbName;
            biosInfoLabel.Text = SI.BiosVersion;
            smuInfoLabel.Text = SI.GetSmuVersionString();
            firmwareInfoLabel.Text = $"{SI.PatchLevel:X8}";
            cpuIdLabel.Text = $"{SI.GetCpuIdString()} ({OPS.CpuType})";
            configInfoLabel.Text = $"{SI.CCDCount} CCD / {SI.CCXCount} CCX / {SI.PhysicalCoreCount} physical cores";
        }

        private void InitForm()
        {
            InitSystemInfo();

            if (OPS.Smu.Version == 0)
            {
                MessageBox.Show("Error getting SMU version!\n" +
                    "Default SMU addresses are not responding to commands.",
                    "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Cache default addresses
            SMU_ADDR_MSG = OPS.Smu.SMU_ADDR_MSG;
            SMU_ADDR_RSP = OPS.Smu.SMU_ADDR_RSP;
            SMU_ADDR_ARG = OPS.Smu.SMU_ADDR_ARG;

            ResetSmuAddresses();
            DisplaySystemInfo();

            pstateIdBox.SelectedIndex = 0;

            pstateDid.KeyDown += pstateFidDid_KeyDown;
            pstateDid.KeyPress += pstateFidDid_KeyPress;
            pstateDid.KeyUp += pstateFidDid_KeyUp;
            pstateFid.KeyDown += pstateFidDid_KeyDown;
            pstateFid.KeyPress += pstateFidDid_KeyPress;
            pstateFid.KeyUp += pstateFidDid_KeyUp;

            PopulateFrequencyList(comboBoxACF.Items);
            PopulateFrequencyList(comboBoxSCF.Items);
            PopulateCCDList(comboBoxCore.Items);

            comboBoxCore.SelectedIndex = 0;

            int index = (int)((GetCurrentMulti() - 5.50) / 0.25);
            comboBoxACF.SelectedIndex = index;
            comboBoxSCF.SelectedIndex = index;

            var prochotEnabled = OPS.IsProchotEnabled();
            checkBoxPROCHOT.Checked = prochotEnabled;
            //checkBoxPROCHOT.Enabled = prochotEnabled;
            //buttonApplyPROCHOT.Enabled = prochotEnabled;

            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(checkBoxPROCHOT, "Disables temperature throttling. Can be useful on extreme cooling.");

            SetStatusText($"{OPS.CpuType}. Ready.");
        }

        // TODO: Detect OC Mode and return PState freq if on auto
        private double GetCurrentMulti()
        {
            uint eax = default, edx = default;
            if (OPS.Ols.Rdmsr(0xC0010293, ref eax, ref edx) != 1)
            {
                SetStatusText($@"Error getting current frequency!");
                return 0;
            }

            return 25 * (eax & 0xFF) / (12.5 * (eax >> 8 & 0x3F));
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
            for (int core = 0; core < this._coreCount; ++core)
                l.Add((object)new CoreListItem(core / 8, core / 4, core));
        }

        private void ApplyFrequencyAllCoreSetting(int frequency)
        {
            if (OPS.SendSmuCommand(OPS.Smu.SMU_MSG_SetOverclockFrequencyAllCores, Convert.ToUInt32(frequency)))
                SetStatusText(string.Format("Set frequency to {0} MHz!", (object)frequency));
            else
                HandleError("Error setting frequency!");
        }

        private void ApplyFrequencySingleCoreSetting(CoreListItem i, int frequency)
        {
            if (OPS.SendSmuCommand(OPS.Smu.SMU_MSG_SetOverclockFrequencyPerCore, Convert.ToUInt32(((i.CCD << 4 | i.CCX % 2 & 15) << 4 | i.CORE % 4 & 15) << 20 | frequency & 0xFFFFF)))
                SetStatusText(string.Format("Set core {0} frequency to {1} MHz!", (object)i, (object)frequency));
            else
                HandleError("Error setting frequency!");
        }

        private void EnableOCMode(bool prochotEnabled = true)
        {
            if (OPS.SendSmuCommand(OPS.Smu.SMU_MSG_EnableOcMode, prochotEnabled ? 0U : 0x1000000))
                SetStatusText(prochotEnabled ? "PROCHOT enabled." : "PROCHOT disabled.");
            else
                HandleError("Error setting OC Mode!");
        }

        private void DisableOCMode()
        {
            if (OPS.SendSmuCommand(OPS.Smu.SMU_MSG_DisableOcMode, 0U))
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
            MessageBox.Show(message, title);
        }

        private void ShowResultMessageBox(uint data)
        {
            uint[] d = { data };
            ShowResultMessageBox(d);
        }

        private void ShowResultMessageBox(uint[] data)
        {
            string responseString = "";
            string[] hexArray = new string[data.Length];
            string[] intArray = new string[data.Length];

            for (var i = 0; i < data.Length; i++)
            {
                hexArray[i] = $"0x{Convert.ToString(data[i], 16).ToUpper()}";
                intArray[i] = $"{Convert.ToString(data[i], 10).ToUpper()}";
            }

            responseString += "HEX: " + string.Join(", ", hexArray);
            responseString += Environment.NewLine;
            responseString += "INT: " + string.Join(", ", intArray);
            responseString += Environment.NewLine;
            responseString += Environment.NewLine;

            Console.WriteLine($"Response: {responseString}");
            textBoxResult.Text = responseString + textBoxResult.Text;
        }

        private void ShowResult(uint data)
        {
            string responseString =
                $"REG: {textBoxPciAddress.Text.Trim()}" +
                Environment.NewLine +
                $"HEX: 0x{Convert.ToString(data, 16).ToUpper()}" +
                Environment.NewLine +
                $"INT: {Convert.ToString(data, 10).ToUpper()}" +
                Environment.NewLine +
                Environment.NewLine;
            Console.WriteLine($"Response: {responseString}");
            textBoxResult.Text = responseString + textBoxResult.Text;
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
                uint[] args = new uint[6];
                string[] userArgs = textBoxARG0.Text.Trim().Split(',');

                TryConvertToUint(textBoxCMDAddress.Text, out uint addrMsg);
                TryConvertToUint(textBoxRSPAddress.Text, out uint addrRsp);
                TryConvertToUint(textBoxARGAddress.Text, out uint addrArg);
                TryConvertToUint(textBoxCMD.Text, out uint command);

                OPS.Smu.SMU_ADDR_MSG = addrMsg;
                OPS.Smu.SMU_ADDR_RSP = addrRsp;
                OPS.Smu.SMU_ADDR_ARG = addrArg;

                for (var i = 0; i < userArgs.Length; i++)
                {
                    if (i == args.Length)
                        break;

                    TryConvertToUint(userArgs[i], out uint temp);
                    args[i] = temp;
                }
                

                Console.WriteLine("MSG Address:  0x" + Convert.ToString(OPS.Smu.SMU_ADDR_MSG, 16).ToUpper());
                Console.WriteLine("RSP Address:  0x" + Convert.ToString(OPS.Smu.SMU_ADDR_RSP, 16).ToUpper());
                Console.WriteLine("ARG0 Address: 0x" + Convert.ToString(OPS.Smu.SMU_ADDR_ARG, 16).ToUpper());
                Console.WriteLine("ARG0        : 0x" + Convert.ToString(args[0], 16).ToUpper());

                SMU.Status status = OPS.SendSmuCommand(command, ref args);

                if (status == SMU.Status.OK)
                {
                    SetStatusText(GetSMUStatus.GetByType(status));
                    ShowResultMessageBox(args);
                }
                else
                {
                    SetStatusText(GetSMUStatus.GetByType(status));
                }
            }
            catch (ApplicationException ex)
            {
                HandleError(ex.Message);
            }
        }

        private void ButtonDefaults_Click(object sender, EventArgs e)
        {
            InitForm();
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
                uint data = OPS.ReadDword(address);

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

                if (OPS.SmuWriteReg(address, data))
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

        private SMU.Status TrySettings(uint msgAddr, uint rspAddr, uint argAddress, uint cmd, uint value)
        {
            uint[] args = new uint[6];
            args[0] = value;

            OPS.Smu.SMU_ADDR_MSG = msgAddr;
            OPS.Smu.SMU_ADDR_RSP = rspAddr;
            OPS.Smu.SMU_ADDR_ARG = argAddress;

            return OPS.SendSmuCommand(cmd, ref args);
        }

        private void ScanSmuRange(uint start, uint end, int step, byte offset)
        {
            matches = new List<SmuAddressSet>();

            while (start <= end)
            {
                uint smuRspAddress = start + offset;
                uint smuArgAddress = 0xF;

                if (OPS.ReadDword(start) != 0xFFFFFFFF)
                {
                    // Check if CMD-RSP pair returns correct status, while using a placeholder ARG address
                    if (TrySettings(start, smuRspAddress, smuArgAddress, OPS.Smu.SMU_MSG_TestMessage, 0x0) == SMU.Status.OK)
                    {
                        // Send smu version command, so the corresponding ARG0 address changes its value
                        TrySettings(start, smuRspAddress, smuArgAddress, OPS.Smu.SMU_MSG_GetSmuVersion, 0x0);
                        bool match = false;

                        smuArgAddress = smuRspAddress + 4;

                        // Scan for ARG address
                        while ((smuArgAddress <= end) && !match)
                        {
                            // Check if smu version major is in range
                            var currentRegValue = (OPS.ReadDword(smuArgAddress) & 0x00FF0000) >> 16;
                            Console.WriteLine($"REG: 0x{smuArgAddress:X8} Value: 0x{currentRegValue:X8}");
                            if (currentRegValue > 1 && currentRegValue <= 99)
                            {
                                // Send test message with an argument, using the potential ARG0 address
                                var argValue = (uint)matches.Count * 2 + 99;
                                TrySettings(start, smuRspAddress, smuArgAddress, OPS.Smu.SMU_MSG_TestMessage, argValue);
                                currentRegValue = OPS.ReadDword(smuArgAddress);
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
                                        $"ARG:  0x{smuArgAddress:X8}" +
                                        Environment.NewLine +
                                        Environment.NewLine;

                                    smuArgAddress += 20;

                                    Invoke(new MethodInvoker(delegate
                                    {
                                        textBoxResult.Text += responseString;
                                    }));
                                }
                            }

                            smuArgAddress += 0x4;
                        }
                    }
                }

                start += (uint)step;
            }
        }

        private void RunBackgroundTask(DoWorkEventHandler task, RunWorkerCompletedEventHandler completedHandler)
        {
            try
            {
                SetButtonsState(false);
                textBoxResult.Clear();

                backgroundWorker1 = new BackgroundWorker();
                backgroundWorker1.DoWork += task;
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
            ResetSmuAddresses();

            try
            {
                Invoke(new MethodInvoker(delegate
                {
                    SetStatusText("Scanning SMU addresses, please wait...");
                }));

                switch (OPS.CpuType)
                {
                    case SMU.CPUType.RavenRidge:
                    case SMU.CPUType.Picasso:
                    case SMU.CPUType.Fenghuang:
                    case SMU.CPUType.Renoir:
                        ScanSmuRange(0x03B10500, 0x03B10998, 8, 0x3C);
                        ScanSmuRange(0x03B10A00, 0x03B10AFF, 4, 0x60);
                        break;
                    case SMU.CPUType.PinnacleRidge:
                    case SMU.CPUType.SummitRidge:
                    case SMU.CPUType.Matisse:
                    case SMU.CPUType.Threadripper:
                        ScanSmuRange(0x03B10500, 0x03B10998, 8, 0x3C);
                        ScanSmuRange(0x03B10500, 0x03B10AFF, 4, 0x4C);
                        break;
                    case SMU.CPUType.Rome:
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
                RunBackgroundTask(BackgroundWorkerTrySettings_DoWork, SmuScan_WorkerCompleted);
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

            writer.WritePropertyName("CpuId");
            writer.WriteValue(SI.GetCpuIdString());

            writer.WritePropertyName("CpuCodeName");
            writer.WriteValue(OPS.CpuType.ToString());

            writer.WritePropertyName("CpuName");
            writer.WriteValue(SI.CpuName);

            writer.WritePropertyName("ExtendedModel");
            writer.WriteValue($"{SI.ExtendedModel:X8}");

            writer.WritePropertyName("PackageType");
            writer.WriteValue(SI.PackageType);

            writer.WritePropertyName("CCDCount");
            writer.WriteValue(SI.CCDCount);

            writer.WritePropertyName("CCXCount");
            writer.WriteValue(SI.CCXCount);

            writer.WritePropertyName("NumCoresInCCX");
            writer.WriteValue(SI.NumCoresInCCX);

            writer.WritePropertyName("FusedCoreCount");
            writer.WriteValue(SI.FusedCoreCount);

            writer.WritePropertyName("PhysicalCoreCount");
            writer.WriteValue(SI.PhysicalCoreCount);

            writer.WritePropertyName("Threads");
            writer.WriteValue(SI.Threads);

            writer.WritePropertyName("MbVendor");
            writer.WriteValue(SI.MbVendor);

            writer.WritePropertyName("MbName");
            writer.WriteValue(SI.MbName);

            writer.WritePropertyName("BiosVersion");
            writer.WriteValue(SI.BiosVersion);

            writer.WritePropertyName("Microcode");
            writer.WriteValue($"{SI.PatchLevel:X8}");

            writer.WritePropertyName("SmuVersion");
            writer.WriteValue(SI.GetSmuVersionString());

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

            ResetSmuAddresses();
            SetButtonsState();
            SetStatusText("Report Complete.");
            MessageBox.Show($"Report saved as {fileName}");
        }

        public static void CalculatePstateDetails(uint eax, ref uint IddDiv, ref uint IddVal, ref uint CpuVid, ref uint CpuDfsId, ref uint CpuFid)
        {
            IddDiv = eax >> 30;
            IddVal = eax >> 22 & 0xFF;
            CpuVid = eax >> 14 & 0xFF;
            CpuDfsId = eax >> 8 & 0x3F;
            CpuFid = eax & 0xFF;
        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            RunBackgroundTask(BackgroundWorkerTrySettings_DoWork, BackgroundWorkerReport_RunWorkerCompleted);
        }

        private bool nonNumberEntered;

        private void pstateFidDid_KeyDown(object sender, KeyEventArgs e)
        {
            nonNumberEntered = false;

            if (e.KeyCode < Keys.D0 || e.KeyCode > Keys.D9)
            {
                if (e.KeyCode < Keys.NumPad0 || e.KeyCode > Keys.NumPad9)
                {
                    if (e.KeyCode != Keys.Back)
                    {
                        nonNumberEntered = true;
                    }
                }
            }

            if (ModifierKeys == Keys.Shift)
            {
                nonNumberEntered = true;
            }
        }

        private void pstateFidDid_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (nonNumberEntered)
            {
                e.Handled = true;
            }
        }

        private void pstateFidDid_KeyUp(object sender, KeyEventArgs e)
        {
            var fid = string.IsNullOrEmpty(pstateFid.Text) ? 0 : int.Parse(pstateFid.Text);
            var did = string.IsNullOrEmpty(pstateDid.Text) ? 1 : int.Parse(pstateDid.Text);
            pstateFrequency.Text = (fid * 25 / (did * 12.5)) * 100 + "MHz";
        }

        private void btnPstateRead_Click(object sender, EventArgs e)
        {
            uint eax = default, edx = default;
            var pstateId = pstateIdBox.SelectedIndex;
            if (OPS.Ols.Rdmsr(Convert.ToUInt32(Convert.ToInt64(0xC0010064) + pstateId), ref eax, ref edx) != 1)
            {
                SetStatusText($@"Error reading PState {pstateId}!");
                return;
            }

            uint IddDiv = 0x0;
            uint IddVal = 0x0;
            uint CpuVid = 0x0;
            uint CpuDfsId = 0x0;
            uint CpuFid = 0x0;

            CalculatePstateDetails(eax, ref IddDiv, ref IddVal, ref CpuVid, ref CpuDfsId, ref CpuFid);

            pstateDid.Text = Convert.ToString(CpuDfsId, 10);
            pstateFid.Text = Convert.ToString(CpuFid, 10);
            pstateFrequency.Text = (CpuFid * 25 / (CpuDfsId * 12.5)) * 100 + "MHz";

            SetStatusText($@"PState {pstateId} successfully read.");

            pstateDid.ReadOnly = false;
            pstateFid.ReadOnly = false;
            btnPstateWrite.Enabled = true;
        }

        private void btnPstateWrite_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show(
                @"This will change the selected PState and your CPU frequency." +
                Environment.NewLine +
                @"Setting a high frequency could crash/damage your system." +
                Environment.NewLine +
                @"Do you want to continue?",
                @"Confirm PState change",
                MessageBoxButtons.OKCancel
            );

            if (confirmResult != DialogResult.OK) return;

            if (string.IsNullOrEmpty(pstateDid.Text) || string.IsNullOrEmpty(pstateFid.Text))
            {
                MessageBox.Show("Can't write because DID/FID is empty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var pstateId = pstateIdBox.SelectedIndex;
            uint eax = default, edx = default;
            uint IddDiv = 0x0;
            uint IddVal = 0x0;
            uint CpuVid = 0x0;
            uint CpuDfsId = 0x0;
            uint CpuFid = 0x0;

            if (OPS.Ols.Rdmsr(Convert.ToUInt32(Convert.ToInt64(0xC0010064) + pstateId), ref eax, ref edx) != 1)
            {
                SetStatusText($@"Error reading PState {pstateId}!");
                return;
            }

            CalculatePstateDetails(eax, ref IddDiv, ref IddVal, ref CpuVid, ref CpuDfsId, ref CpuFid);

            eax = (IddDiv & 0xFF) << 30 | (IddVal & 0xFF) << 22 | (CpuVid & 0xFF) << 14 | (uint.Parse(pstateDid.Text) & 0xFF) << 8 | uint.Parse(pstateFid.Text) & 0xFF;

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

            SetStatusText($@"Successfully written PState {pstateId}.");
        }

        // P0 fix C001_0015 HWCR[21]=1
        // Fixes timer issues when not using HPET
        public bool ApplyTscWorkaround()
        {
            uint eax = 0, edx = 0;

            if (OPS.Ols.Rdmsr(0xC0010015, ref eax, ref edx) != -1)
            {
                eax |= 0x200000;
                return OPS.Ols.Wrmsr(0xC0010015, eax, edx) != -1;
            }

            SetStatusText($@"Error applying TSC fix!");
            return false;
        }

        private bool WritePstateClick(int pstateId, uint eax, uint edx, int numanode = 0)
        {
            if (_numaUtil.HighestNumaNode > 0) _numaUtil.SetThreadProcessorAffinity((ushort)(numanode + 1), Enumerable.Range(0, Environment.ProcessorCount).ToArray());

            if (!ApplyTscWorkaround()) return false;

            if (OPS.Ols.Wrmsr(Convert.ToUInt32(Convert.ToInt64(0xC0010064) + pstateId), eax, edx) != 1)
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
                Invoke(new MethodInvoker(delegate
                {
                    SetStatusText("Scanning PCI addresses, please wait...");
                }));

                string result = "REG         Value" + Environment.NewLine;

                TryConvertToUint(textBoxPciStartReg.Text, out uint startReg);
                TryConvertToUint(textBoxPciEndReg.Text, out uint endReg);

                while (startReg <= endReg)
                {
                    var data = OPS.ReadDword(startReg);
                    result += $"0x{startReg:X8}: 0x{data:X8}" + Environment.NewLine;
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
            SetButtonsState();
            ResetSmuAddresses();
            SetStatusText("Scan Complete.");
        }

        private void ButtonPciScan_Click(object sender, EventArgs e)
        {
            RunBackgroundTask(PciScan_DoWork, Scan_WorkerCompleted);
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
            if (!checkBoxPROCHOT.Checked && OPS.IsProchotEnabled())
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
                    if (OPS.Ols.Rdmsr(startReg, ref eax, ref edx) == 1)
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
            if (OPS.Ols.Rdmsr(msr, ref eax, ref edx) == 1)
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

            if (OPS.Ols.Wrmsr(msr, eax, edx) != 1)
            {
                HandleError($@"Error writing MSR {textBoxMsrAddress.Text}!");
                return;
            }

            SetStatusText("Write OK.");
        }

        private void ButtonMsrScan_Click(object sender, EventArgs e)
        {
            RunBackgroundTask(ReadMsr_Task, Scan_WorkerCompleted);
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

                if (OPS.Ols.Cpuid(0x00000000, ref eax, ref ebx, ref ecx, ref edx) == 1)
                    LFuncStd = eax;

                if (OPS.Ols.Cpuid(0x80000000, ref eax, ref ebx, ref ecx, ref edx) == 1)
                    LFuncExt = eax - 0x80000000;

                for (uint i = 0; i <= LFuncStd; ++i)
                {
                    var index = 0x00000000 + i;
                    OPS.Ols.Cpuid(index, ref eax, ref ebx, ref ecx, ref edx);
                    result += $"0x{index:X8}: 0x{eax:X8} 0x{ebx:X8} 0x{ecx:X8} 0x{edx:X8}" + Environment.NewLine;
                }

                for (uint i = 0; i <= LFuncExt; ++i)
                {
                    var index = 0x80000000 + i;
                    OPS.Ols.Cpuid(index, ref eax, ref ebx, ref ecx, ref edx);
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
            if (OPS.Ols.Cpuid(index, ref eax, ref ebx, ref ecx, ref edx) == 1)
            {
                textBoxCPUIDeax.Text = $"0x{eax:X8}";
                textBoxCPUIDebx.Text = $"0x{ebx:X8}";
                textBoxCPUIDecx.Text = $"0x{ecx:X8}";
                textBoxCPUIDedx.Text = $"0x{edx:X8}";
            }
        }

        private void ButtonCPUIDScan_Click(object sender, EventArgs e)
        {
            RunBackgroundTask(ReadCPUID_Task, Scan_WorkerCompleted);
        }

        private void ButtonPMTable_Click(object sender, EventArgs e)
        {
            new Thread(() => new PowerTableMonitor(OPS).ShowDialog()).Start();
        }
    }
}
