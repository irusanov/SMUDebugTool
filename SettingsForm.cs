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
using ZenStatesDebugTool.Properties;

namespace ZenStatesDebugTool
{
    public partial class SettingsForm : Form
    {
        private static readonly int Threads = Convert.ToInt32(Environment.GetEnvironmentVariable("NUMBER_OF_PROCESSORS"));
        private BackgroundWorker backgroundWorker1;
        private NUMAUtil _numaUtil;
        private readonly Ols ols;
        private SMU.CPUType cpuType = SMU.CPUType.Unsupported;
        private SMU smu;
        private SystemInfo SI;
        List<SmuAddressSet> matches;

        readonly Mutex hMutexPci;

        private uint SMU_ADDR_MSG;
        private uint SMU_ADDR_RSP;
        private uint SMU_ADDR_ARG0;
        private uint SMU_ADDR_ARG1;

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

            ols = new Ols();
            hMutexPci = new Mutex();

            _numaUtil = new NUMAUtil();
            textBoxResult.Text = $@"Detected NUMA nodes. ({_numaUtil.HighestNumaNode + 1})" + textBoxResult.Text;

            try
            {
                CheckOlsStatus();
                InitForm();
            }
            catch (ApplicationException ex)
            {
                MessageBox.Show(ex.Message, Resources.Error);
                Dispose();
                Application.Exit();
            }
        }

        private void CheckOlsStatus()
        {
            // Check support library status
            switch (ols.GetStatus())
            {
                case (uint)Ols.Status.NO_ERROR:
                    break;
                case (uint)Ols.Status.DLL_NOT_FOUND:
                    throw new ApplicationException("WinRing DLL_NOT_FOUND");
                case (uint)Ols.Status.DLL_INCORRECT_VERSION:
                    throw new ApplicationException("WinRing DLL_INCORRECT_VERSION");
                case (uint)Ols.Status.DLL_INITIALIZE_ERROR:
                    throw new ApplicationException("WinRing DLL_INITIALIZE_ERROR");
            }

            // Check WinRing0 status
            switch (ols.GetDllStatus())
            {
                case (uint)Ols.OlsDllStatus.OLS_DLL_NO_ERROR:
                    break;
                case (uint)Ols.OlsDllStatus.OLS_DLL_DRIVER_NOT_LOADED:
                    throw new ApplicationException("WinRing OLS_DRIVER_NOT_LOADED");
                case (uint)Ols.OlsDllStatus.OLS_DLL_UNSUPPORTED_PLATFORM:
                    throw new ApplicationException("WinRing OLS_UNSUPPORTED_PLATFORM");
                case (uint)Ols.OlsDllStatus.OLS_DLL_DRIVER_NOT_FOUND:
                    throw new ApplicationException("WinRing OLS_DLL_DRIVER_NOT_FOUND");
                case (uint)Ols.OlsDllStatus.OLS_DLL_DRIVER_UNLOADED:
                    throw new ApplicationException("WinRing OLS_DLL_DRIVER_UNLOADED");
                case (uint)Ols.OlsDllStatus.OLS_DLL_DRIVER_NOT_LOADED_ON_NETWORK:
                    throw new ApplicationException("WinRing DRIVER_NOT_LOADED_ON_NETWORK");
                case (uint)Ols.OlsDllStatus.OLS_DLL_UNKNOWN_ERROR:
                    throw new ApplicationException("WinRing OLS_DLL_UNKNOWN_ERROR");
            }
        }

        private uint GetCpuInfo()
        {
            uint eax = 0, ebx = 0, ecx = 0, edx = 0;
            ols.CpuidPx(0x00000000, ref eax, ref ebx, ref ecx, ref edx, (UIntPtr)1);
            if (ols.CpuidPx(0x00000001, ref eax, ref ebx, ref ecx, ref edx, (UIntPtr)1) == 1)
            {
                return eax;
            }
            return 0;
        }

        private void InitSystemInfo()
        {
            SI = new SystemInfo
            {
                CpuId = GetCpuInfo()
            };

            // CPU Check. Compare family, model, ext family, ext model. Ignore stepping/revision
            switch (SI.CpuId & 0xFFFFFFF0)
            {
                case 0x00800F10: // CPU \ Zen \ Summit Ridge \ ZP - B0 \ 14nm
                case 0x00800F00: // CPU \ Zen \ Summit Ridge \ ZP - A0 \ 14nm
                    cpuType = SMU.CPUType.SummitRidge;
                    break;
                case 0x00800F80: // CPU \ Zen + \ Pinnacle Ridge \ Colfax 12nm
                    cpuType = SMU.CPUType.PinnacleRidge;
                    break;
                case 0x00810F80: // APU \ Zen + \ Picasso \ 12nm
                    cpuType = SMU.CPUType.Picasso;
                    break;
                case 0x00810F00: // APU \ Zen \ Raven Ridge \ RV - A0 \ 14nm
                case 0x00810F10: // APU \ Zen \ Raven Ridge \ 14nm
                case 0x00820F00: // APU \ Zen \ Raven Ridge 2 \ RV2 - A0 \ 14nm
                    cpuType = SMU.CPUType.RavenRidge;
                    break;
                case 0x00870F10: // CPU \ Zen2 \ Matisse \ MTS - B0 \ 7nm + 14nm I/ O Die
                case 0x00870F00: // CPU \ Zen2 \ Matisse \ MTS - A0 \ 7nm + 14nm I/ O Die
                    cpuType = SMU.CPUType.Matisse;
                    break;
                case 0x00830F00:
                case 0x00830F10: // CPU \ Epyc 2 \ Rome \ Treadripper 2 \ Castle Peak 7nm
                    cpuType = SMU.CPUType.Rome;
                    break;
                case 0x00850F00:
                    cpuType = SMU.CPUType.Fenghuang;
                    break;
                case 0x00850F10: // APU \ Renoir
                    cpuType = SMU.CPUType.Renoir;
                    break;
                default:
                    cpuType = SMU.CPUType.Unsupported;
#if DEBUG
                    cpuType = SMU.CPUType.DEBUG;
#endif
                    break;
            }

            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_BaseBoard");
            foreach (ManagementObject obj in searcher.Get())
            {
                SI.MbVendor = ((string)obj["Manufacturer"]).Trim();
                SI.MbName = ((string)obj["Product"]).Trim();
            }
            if (searcher != null) searcher.Dispose();

            searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Processor");
            foreach (ManagementObject obj in searcher.Get())
            {
                var cpuName = (string)obj["Name"];
                cpuName = cpuName.Replace("(R)", "");
                cpuName = cpuName.Replace("(TM)", "");
                cpuName = cpuName.Trim();

                SI.CpuName = cpuName;
            }
            if (searcher != null) searcher.Dispose();

            searcher = new ManagementObjectSearcher("SELECT * FROM Win32_BIOS");
            foreach (ManagementObject obj in searcher.Get())
            {
                SI.BiosVersion = ((string)obj["SMBIOSBIOSVersion"]).Trim();
            }
            if (searcher != null) searcher.Dispose();
        }

        private void ResetSettings(SMU smuSettings)
        {
            SMU_ADDR_MSG = smuSettings.SMU_ADDR_MSG;
            SMU_ADDR_RSP = smuSettings.SMU_ADDR_RSP;
            SMU_ADDR_ARG0 = smuSettings.SMU_ADDR_ARG0;
            SMU_ADDR_ARG1 = SMU_ADDR_ARG0 + 0x4;

            textBoxCMDAddress.Text = cpuType > SMU.CPUType.DEBUG ? $"0x{Convert.ToString(SMU_ADDR_MSG, 16).ToUpper()}" : "0x0";
            textBoxRSPAddress.Text = cpuType > SMU.CPUType.DEBUG ? $"0x{Convert.ToString(SMU_ADDR_RSP, 16).ToUpper()}" : "0x0";
            textBoxARGAddress.Text = cpuType > SMU.CPUType.DEBUG ? $"0x{Convert.ToString(SMU_ADDR_ARG0, 16).ToUpper()}" : "0x0";

            textBoxCMD.Text = "0x1";
            textBoxARG0.Text = "0x0";
        }

        private void DisplaySystemInfo()
        {
            cpuInfoLabel.Text = SI.CpuName;
            mbVendorInfoLabel.Text = SI.MbVendor;
            mbModelInfoLabel.Text = SI.MbName;
            biosInfoLabel.Text = SI.BiosVersion;
            smuInfoLabel.Text = SI.GetSmuVersionString();
            cpuIdLabel.Text = $"{SI.GetCpuIdString()} ({cpuType.ToString()})";
        }

        private void InitForm()
        {
            InitSystemInfo();
            smu = GetMaintainedSettings.GetByType(cpuType);
            ResetSettings(smu);
            smu.Version = GetSmuVersion();
            SI.SmuVersion = smu.Version;
            DisplaySystemInfo();

            pstateIdBox.SelectedIndex = 0;

            pstateDid.KeyDown += pstateFidDid_KeyDown;
            pstateDid.KeyPress += pstateFidDid_KeyPress;
            pstateDid.KeyUp += pstateFidDid_KeyUp;
            pstateFid.KeyDown += pstateFidDid_KeyDown;
            pstateFid.KeyPress += pstateFidDid_KeyPress;
            pstateFid.KeyUp += pstateFidDid_KeyUp;

            SetStatusText($"{cpuType}. Ready.");
        }

        private bool SmuWriteReg(uint addr, uint data)
        {
            int res = 0;

            // Clear response
            res = ols.WritePciConfigDwordEx(smu.SMU_PCI_ADDR, smu.SMU_OFFSET_ADDR, addr);
            if (res == 1)
            {
                res = ols.WritePciConfigDwordEx(smu.SMU_PCI_ADDR, smu.SMU_OFFSET_DATA, data);
            }

            return (res == 1);
        }

        private bool SmuReadReg(uint addr, ref uint data)
        {
            int res = 0;

            // Clear response
            res = ols.WritePciConfigDwordEx(smu.SMU_PCI_ADDR, smu.SMU_OFFSET_ADDR, addr);
            if (res == 1)
            {
                res = ols.ReadPciConfigDwordEx(smu.SMU_PCI_ADDR, smu.SMU_OFFSET_DATA, ref data);
            }

            return (res == 1);
        }

        private bool SmuWaitDone()
        {
            bool res = false;
            ushort timeout = 1000;
            uint data = 0;
            while ((!res || data != 1) && --timeout > 0)
            {
                res = SmuReadReg(SMU_ADDR_RSP, ref data);
            }

            if (timeout == 0 || data != 1) res = false;

            return res;
        }

        private bool SmuRead(uint msg, ref uint data)
        {
            bool res;

            // Clear response
            res = SmuWriteReg(SMU_ADDR_RSP, 0);

            if (res)
            {
                // Send message
                res = SmuWriteReg(SMU_ADDR_MSG, msg);
                if (res)
                {
                    // Check completion
                    res = SmuWaitDone();

                    if (res)
                    {
                        res = SmuReadReg(SMU_ADDR_ARG0, ref data);
                    }
                }
            }

            return res;
        }

        private bool SmuWrite(uint msg, uint value)
        {
            bool res;

            // Mutex
            res = hMutexPci.WaitOne(5000);

            // Clear response
            if (res)
            {
                res = SmuWriteReg(SMU_ADDR_RSP, 0);
            }

            if (res)
            {
                // Write data
                res = SmuWriteReg(SMU_ADDR_ARG0, value);

                if (res)
                {
                    SmuWriteReg(SMU_ADDR_ARG1, 0);
                }
                // Send message
                res = SmuWriteReg(SMU_ADDR_MSG, msg);

                if (res)
                {
                    res = SmuWaitDone();
                }
            }

            hMutexPci.ReleaseMutex();

            return res;
        }

        private uint ReadDword(uint value)
        {
            ols.WritePciConfigDword(smu.SMU_PCI_ADDR, (byte)smu.SMU_OFFSET_ADDR, value);
            // Thread.Sleep(5000);
            return ols.ReadPciConfigDword(smu.SMU_PCI_ADDR, (byte)smu.SMU_OFFSET_DATA);
        }

        private void SetStatusText(string status)
        {
            labelStatus.Text = status;
            Console.WriteLine($"CMD Status: {status}");
        }

        private uint GetSmuVersion()
        {
            uint version = 0;

            SmuRead(smu.SMC_MSG_GetSmuVersion, ref version);
            return version;
        }

        private void SetButtonsState(bool enabled = true)
        {
            buttonApply.Enabled = enabled;
            buttonDefaults.Enabled = enabled;
            buttonProbe.Enabled = enabled;
            buttonPciRead.Enabled = enabled;
            buttonExport.Enabled = enabled;
            textBoxCMDAddress.Enabled = enabled;
            textBoxRSPAddress.Enabled = enabled;
            textBoxARGAddress.Enabled = enabled;
            textBoxCMD.Enabled = enabled;
            textBoxARG0.Enabled = enabled;
            textBoxPciAddress.Enabled = enabled;
            textBoxPciValue.Enabled = enabled;
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

        private void HandleError(Exception ex, string title = "Error")
        {
            SetStatusText(Resources.Error);
            MessageBox.Show(ex.Message, title);
        }

        private void ShowResultMessageBox(uint data)
        {
            string responseString =
                $"HEX: 0x{Convert.ToString(data, 16).ToUpper()}" +
                Environment.NewLine +
                $"INT: {Convert.ToString(data, 10).ToUpper()}" +
                Environment.NewLine +
                Environment.NewLine;
            Console.WriteLine($"Response: {responseString}");
            // MessageBox.Show(responseString, "Response");
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

        private void ApplySettings()
        {
            try
            {
                TryConvertToUint(textBoxCMDAddress.Text, out SMU_ADDR_MSG);
                TryConvertToUint(textBoxRSPAddress.Text, out SMU_ADDR_RSP);
                TryConvertToUint(textBoxARGAddress.Text, out SMU_ADDR_ARG0);
                SMU_ADDR_ARG1 = SMU_ADDR_ARG0 + 0x4;

                TryConvertToUint(textBoxCMD.Text, out uint command);
                TryConvertToUint(textBoxARG0.Text, out uint arg0);

                Console.WriteLine("MSG Address:  0x" + Convert.ToString(SMU_ADDR_MSG, 16).ToUpper());
                Console.WriteLine("RSP Address:  0x" + Convert.ToString(SMU_ADDR_RSP, 16).ToUpper());
                Console.WriteLine("ARG0 Address: 0x" + Convert.ToString(SMU_ADDR_ARG0, 16).ToUpper());
                Console.WriteLine("ARG0        : 0x" + Convert.ToString(arg0, 16).ToUpper());

                if (SmuWrite(command, arg0))
                {
                    // Read response
                    uint data = 0;

                    if (SmuReadReg(SMU_ADDR_RSP, ref data))
                    {
                        string responseString = "0x" + Convert.ToString(data, 16).ToUpper();
                        SetStatusText(GetSMUStatus.GetByType((SMU.Status)data));

                        SmuReadReg(SMU_ADDR_ARG0, ref data);
                        ShowResultMessageBox(data);
                    }
                    else
                    {
                        SetStatusText("Error reading response");
                    }
                }
                else
                {
                    SetStatusText("Error SMU write");
                }
            }
            catch (ApplicationException ex)
            {
                HandleError(ex);
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
                HandleError(ex, "Error reading response");
            }
        }

        private void HandlePciReadBtnClick()
        {
            try
            {
                SetStatusText("Reading, please wait...");
                SetButtonsState(false);

                TryConvertToUint(textBoxPciAddress.Text, out uint address);
                uint data = ReadDword(address);

                textBoxPciValue.Text = $"0x{data.ToString("X8")}";

                SetButtonsState();
                SetStatusText(GetSMUStatus.GetByType(SMU.Status.OK));
                ShowResult(data);
            }
            catch (ApplicationException ex)
            {
                SetButtonsState();
                HandleError(ex);
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

                if (SmuWriteReg(address, data))
                {
                    SetStatusText("Write OK.");
                }
                else
                {
                    SetStatusText(Resources.Error);
                }

                SetButtonsState();
            }
            catch (ApplicationException ex)
            {
                SetButtonsState();
                HandleError(ex);
            }
        }

        private void ButtonPciRead_Click(object sender, EventArgs e)
        {
            HandlePciReadBtnClick();
        }

        private void TextBoxPciAddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                HandlePciReadBtnClick();
            }
        }

        private void ButtonPciWrite_Click(object sender, EventArgs e)
        {
            HandlePciWriteBtnClick();
        }

        private void TextBoxPciValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                HandlePciWriteBtnClick();
            }
        }

        private SMU.Status TrySettings(uint msgAddr, uint rspAddr, uint argAddress, uint cmd, uint value)
        {
            SMU.Status status = SMU.Status.FAILED;

            try
            {
                SMU_ADDR_MSG = msgAddr;
                SMU_ADDR_RSP = rspAddr;
                SMU_ADDR_ARG0 = argAddress;

                if (SmuWrite(cmd, value))
                {
                    // Read response
                    status = (SMU.Status)ReadDword(SMU_ADDR_RSP);
                }
            }
            catch (ApplicationException ex)
            {
                HandleError(ex);
            }

            return status;
        }

        private void BackgroundWorkerTrySettings_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                matches = new List<SmuAddressSet>();

                uint SMU_START = 0x03B10500;
                uint SMU_END = 0x03B10AFF;
                byte RSP_OFFSET = 0x4C;
                byte RSP_OFFSET_APU = 0x60; // Picasso & Fenghuang

                if (
                    cpuType == SMU.CPUType.Picasso
                 || cpuType == SMU.CPUType.Fenghuang
                 || cpuType == SMU.CPUType.RavenRidge
                )
                {
                    RSP_OFFSET = RSP_OFFSET_APU;
                }

                while (SMU_START <= SMU_END)
                {
                    uint smuRspAddress = SMU_START + RSP_OFFSET;

                    if (ReadDword(SMU_START) != 0xFFFFFFFF)
                    {
                        // Check if CMD-RSP pair returns correct status, while using a placeholder ARG address
                        if (TrySettings(SMU_START, smuRspAddress, 0xF, smu.SMC_MSG_TestMessage, 0x0) == SMU.Status.OK)
                        {
                            // Send smu version command, so the corresponding ARG0 address changes its value
                            TrySettings(SMU_START, smuRspAddress, 0xF, smu.SMC_MSG_GetSmuVersion, 0x0);
                            bool match = false;

                            SMU_ADDR_ARG0 = SMU_ADDR_RSP + 4;

                            // Scan for ARG address
                            while ((SMU_ADDR_ARG0 <= SMU_END) && !match)
                            {
                                // Check if smu version major is in range
                                var currentRegValue = (ReadDword(SMU_ADDR_ARG0) & 0x00FF0000) >> 16;
                                if (currentRegValue > 1 && currentRegValue <= 99)
                                {
                                    // Send test message with an argument, using the potential ARG0 address
                                    var argValue = (uint)matches.Count * 2 + 99;
                                    TrySettings(SMU_START, smuRspAddress, SMU_ADDR_ARG0, smu.SMC_MSG_TestMessage, argValue);
                                    currentRegValue = ReadDword(SMU_ADDR_ARG0);
                                    Console.WriteLine($"REG: 0x{Convert.ToString(SMU_ADDR_ARG0, 16).ToUpper()} Value: 0x{Convert.ToString(currentRegValue, 16).ToUpper()}");

                                    // Check the address for expected value (argument + 1)
                                    if (currentRegValue == argValue + 1)
                                    {
                                        match = true;
                                        matches.Add(new SmuAddressSet(SMU_START, smuRspAddress, SMU_ADDR_ARG0));

                                        string responseString =
                                            $"CMD:  0x{Convert.ToString(SMU_START, 16).ToUpper()}" +
                                            Environment.NewLine +
                                            $"RSP:  0x{Convert.ToString(smuRspAddress, 16).ToUpper()}" +
                                            Environment.NewLine +
                                            $"ARG:  0x{Convert.ToString(SMU_ADDR_ARG0, 16).ToUpper()}" +
                                            Environment.NewLine +
                                            Environment.NewLine;

                                        SMU_ADDR_ARG0 += 20;

                                        Invoke(new MethodInvoker(delegate
                                        {
                                            textBoxResult.Text += responseString;
                                        }));
                                    }
                                }

                                SMU_ADDR_ARG0 += 0x4;
                            }
                        }
                    }

                    SMU_START += 0x4;
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

        private void BackgroundWorkerTrySettings_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            SetButtonsState();
            SetStatusText("Scan Complete.");
        }

        private void ScanSmu(RunWorkerCompletedEventHandler completedHandler)
        {
            try
            {
                SetStatusText("Scanning addresses, please wait...");
                SetButtonsState(false);
                textBoxResult.Clear();

                backgroundWorker1 = new BackgroundWorker();
                backgroundWorker1.DoWork += BackgroundWorkerTrySettings_DoWork;
                backgroundWorker1.RunWorkerCompleted += completedHandler;
                backgroundWorker1.RunWorkerAsync();
            }
            catch (ApplicationException ex)
            {
                SetStatusText(Resources.Error);
                SetButtonsState();
                HandleError(ex);
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
            {
                ScanSmu(BackgroundWorkerTrySettings_RunWorkerCompleted);
            }
        }

        private void TabControl1_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPage == tabPageInfo)
            {
                splitContainer1.Panel2Collapsed = true;
            }
            else if (splitContainer1.Panel2Collapsed)
            {
                splitContainer1.Panel2Collapsed = false;
            }
        }

        public string GenerateReportJson()
        {
            StringWriter sw = new StringWriter();
            JsonTextWriter writer = new JsonTextWriter(sw);
            writer.Formatting = Formatting.Indented;

            // {
            writer.WriteStartObject();

            writer.WritePropertyName("AppVersion");
            writer.WriteValue(Application.ProductVersion);

            writer.WritePropertyName("OSVersion");
            writer.WriteValue(new ComputerInfo().OSFullName);

            writer.WritePropertyName("CpuId");
            writer.WriteValue(SI.GetCpuIdString());

            writer.WritePropertyName("CpuCodeName");
            writer.WriteValue(cpuType.ToString());

            writer.WritePropertyName("CpuName");
            writer.WriteValue(SI.CpuName);

            writer.WritePropertyName("MbVendor");
            writer.WriteValue(SI.MbVendor);

            writer.WritePropertyName("MbName");
            writer.WriteValue(SI.MbName);

            writer.WritePropertyName("BiosVersion");
            writer.WriteValue(SI.BiosVersion);

            writer.WritePropertyName("SmuVersion");
            writer.WriteValue(SI.GetSmuVersionString());

            // "SmuAddresses:"
            writer.WritePropertyName("Mailboxes");
            writer.WriteStartArray();
            foreach (SmuAddressSet set in matches)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("MsgAddress");
                writer.WriteValue($"0x{set.MsgAddress.ToString("X8")}");
                writer.WritePropertyName("RspAddress");
                writer.WriteValue($"0x{set.RspAddress.ToString("X8")}");
                writer.WritePropertyName("ArgAddress");
                writer.WriteValue($"0x{set.ArgAddress.ToString("X8")}");
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
            {
                File.Delete(fileName);
            }

            using (var sw = new StreamWriter(fileName, true))
            {
                sw.WriteLine(GenerateReportJson());
            }

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
            ScanSmu(BackgroundWorkerReport_RunWorkerCompleted);
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
            if (ols.RdmsrTx(Convert.ToUInt32(Convert.ToInt64(0xC0010064) + pstateId), ref eax, ref edx, (UIntPtr)(1 << 10)) != 1)
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

            var frequency = ((CpuFid / CpuDfsId) * 200) + "";

            pstateDid.Text = Convert.ToString(CpuDfsId, 10);
            pstateFid.Text = Convert.ToString(CpuFid, 10);
            pstateFrequency.Text = frequency + "MHz";

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

            if (ols.RdmsrTx(Convert.ToUInt32(Convert.ToInt64(0xC0010064) + pstateId), ref eax, ref edx, (UIntPtr)(1 << 10)) != 1)
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

        private bool WritePstateClick(int pstateId, uint eax, uint edx, int numanode = 0)
        {
            if (_numaUtil.HighestNumaNode > 0) _numaUtil.SetThreadProcessorAffinity((ushort)(numanode + 1), Enumerable.Range(0, Environment.ProcessorCount).ToArray());
            if (ols.Wrmsr(Convert.ToUInt32(Convert.ToInt64(0xC0010064) + pstateId), eax, edx) != 1) //, (UIntPtr) ((1 << i) - 1)
            {
                SetStatusText($@"Error writing PState {pstateId}!");
                return false;
            }

            return true;
        }
    }
}
