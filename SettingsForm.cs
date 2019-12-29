using OpenLibSys;
using System;
using System.Management;
using System.Threading;
using System.ComponentModel;
using System.Windows.Forms;

namespace ZenStatesDebugTool
{
    public partial class SettingsForm : Form, IDisposable
    {
        private static readonly int Threads = Convert.ToInt32(Environment.GetEnvironmentVariable("NUMBER_OF_PROCESSORS"));
        private BackgroundWorker backgroundWorker1;
        private readonly Ols ols;
        private SMU.CPUType cpuType = SMU.CPUType.Unsupported;
        private SMU smu;

        private string mbVendor;
        private string mbName;
        private string cpuName;
        private string biosVersion;
        private uint smuVersion;

        readonly Mutex hMutexPci;

        private uint SMU_ADDR_MSG = 0;
        private uint SMU_ADDR_RSP = 0;
        private uint SMU_ADDR_ARG0 = 0;
        private uint SMU_ADDR_ARG1 = 0;

        public SettingsForm()
        {
            InitializeComponent();

            ols = new Ols();
            hMutexPci = new Mutex();

            try
            {
                CheckOlsStatus();
            } 
            catch (ApplicationException ex)
            {
                MessageBox.Show(ex.Message, "Error");
                this.Dispose();
                Application.Exit();
            }

            // CPU Check. Compare family, model, ext family, ext model. Ignore stepping/revision
            switch (GetCpuInfo() & 0xFFFFFFF0)
            {
                case 0x00800F10: // CPU \ Zen \ Summit Ridge \ ZP - B0 \ 14nm
                case 0x00800F00: // CPU \ Zen \ Summit Ridge \ ZP - A0 \ 14nm
                    this.cpuType = SMU.CPUType.SummitRidge;
                    break;
                case 0x00800F80: // CPU \ Zen + \ Pinnacle Ridge \ Colfax 12nm
                    this.cpuType = SMU.CPUType.PinnacleRidge;
                    break;
                case 0x00810F80: // APU \ Zen + \ Picasso \ 12nm
                    this.cpuType = SMU.CPUType.Picasso;
                    break;
                case 0x00810F00: // APU \ Zen \ Raven Ridge \ RV - A0 \ 14nm
                case 0x00810F10: // APU \ Zen \ Raven Ridge \ 14nm
                case 0x00820F00: // APU \ Zen \ Raven Ridge 2 \ RV2 - A0 \ 14nm
                    this.cpuType = SMU.CPUType.RavenRidge;
                    break;
                case 0x00870F10: // CPU \ Zen2 \ Matisse \ MTS - B0 \ 7nm + 14nm I/ O Die
                case 0x00870F00: // CPU \ Zen2 \ Matisse \ MTS - A0 \ 7nm + 14nm I/ O Die
                    this.cpuType = SMU.CPUType.Matisse;
                    break;
                case 0x00830F00:
                case 0x00830F10: // CPU \ Epyc 2 \ Rome \ Treadripper 2 \ Castle Peak 7nm
                    this.cpuType = SMU.CPUType.Rome;
                    break;
                case 0x00850F00:
                case 0x00850F10: // APU \ Renoir \ Fenghuang
                    this.cpuType = SMU.CPUType.Renoir;
                    break;
                default:
                    this.cpuType = SMU.CPUType.Unsupported;
#if DEBUG
                    this.cpuType = SMU.CPUType.DEBUG;
#endif
                    break;
            }

            InitForm();
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

        private void ResetSettings(SMU smuSettings)
        {
            SMU_ADDR_MSG = smuSettings.SMU_ADDR_MSG;
            SMU_ADDR_RSP = smuSettings.SMU_ADDR_RSP;
            SMU_ADDR_ARG0 = smuSettings.SMU_ADDR_ARG0;
            SMU_ADDR_ARG1 = SMU_ADDR_ARG0 + 0x4;

            textBoxCMDAddress.Text = this.cpuType > SMU.CPUType.DEBUG ? $"0x{Convert.ToString(SMU_ADDR_MSG, 16).ToUpper()}" : "0x0";
            textBoxRSPAddress.Text = this.cpuType > SMU.CPUType.DEBUG ? $"0x{Convert.ToString(SMU_ADDR_RSP, 16).ToUpper()}" : "0x0";
            textBoxARGAddress.Text = this.cpuType > SMU.CPUType.DEBUG ? $"0x{Convert.ToString(SMU_ADDR_ARG0, 16).ToUpper()}" : "0x0";

            textBoxCMD.Text = "0x1";
            textBoxARG0.Text = "0x0";
        }

        private void SetSystemInfo()
        {
            cpuInfoLabel.Text = cpuName;
            mbVendorInfoLabel.Text = mbVendor;
            mbModelInfoLabel.Text = mbName;
            biosInfoLabel.Text = biosVersion;
            smuInfoLabel.Text = GetSmuVersionString(smuVersion);
        }

        private void InitForm()
        {
            this.smu = GetMaintainedSettings.GetByType(this.cpuType);
            ResetSettings(smu);
            InitSystemInfo();
            SetSystemInfo();

            SetCmdStatus($"{Convert.ToString(this.cpuType)}. Ready.");
        }

        private bool SmuWriteReg(UInt32 addr, UInt32 data)
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
            Thread.Sleep(5000);
            return ols.ReadPciConfigDword(smu.SMU_PCI_ADDR, (byte)smu.SMU_OFFSET_DATA);
        }

        private void SetCmdStatus(string status)
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

        private static string GetSmuVersionString(uint version)
        {
            string[] versionString = new string[3];
            versionString[0] = ((version & 0x00FF0000) >> 16).ToString("D2");
            versionString[1] = ((version & 0x0000FF00) >> 8).ToString("D2");
            versionString[2] = (version & 0x000000FF).ToString("D2");

            return string.Join(".", versionString);
        }

        private void InitSystemInfo()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_BaseBoard");
            foreach (ManagementObject obj in searcher.Get())
            {
                mbVendor = (string)obj["Manufacturer"];
                mbVendor = mbVendor.Trim();
                mbName = (string)obj["Product"];
                mbName = mbName.Trim();
            }
            if (searcher != null) searcher.Dispose();

            searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Processor");
            foreach (ManagementObject obj in searcher.Get())
            {
                cpuName = (string)obj["Name"];
                cpuName = cpuName.Replace("(R)", "");
                cpuName = cpuName.Replace("(TM)", "");
                cpuName = cpuName.Trim();
            }
            if (searcher != null) searcher.Dispose();

            searcher = new ManagementObjectSearcher("SELECT * FROM Win32_BIOS");
            foreach (ManagementObject obj in searcher.Get())
            {
                biosVersion = (string)obj["SMBIOSBIOSVersion"];
                biosVersion = biosVersion.Trim();
            }
            if (searcher != null) searcher.Dispose();

            smuVersion = GetSmuVersion();
        }

        private static void HandleError(Exception ex, string title = "Error")
        {
            MessageBox.Show(ex.Message, title);
        }

        private void ShowResult(uint data)
        {
            string responseString = $"HEX: 0x{Convert.ToString(data, 16).ToUpper()}\n" +
                                    $"INT: {Convert.ToString(data, 10).ToUpper()}";
            Console.WriteLine($"Response: {responseString}");
            MessageBox.Show(responseString, "Response");
        }

        private void ApplySettings()
        {
            try
            {
                SMU_ADDR_MSG = Convert.ToUInt32(textBoxCMDAddress.Text, 16);
                SMU_ADDR_RSP = Convert.ToUInt32(textBoxRSPAddress.Text, 16);
                SMU_ADDR_ARG0 = Convert.ToUInt32(textBoxARGAddress.Text, 16);
                SMU_ADDR_ARG1 = SMU_ADDR_ARG0 + 0x4;

                uint command = Convert.ToUInt32(textBoxCMD.Text, 16);
                uint arg0 = Convert.ToUInt32(textBoxARG0.Text, 16);

                Console.WriteLine("MSG Address:  0x" + Convert.ToString(SMU_ADDR_MSG, 16).ToUpper());
                Console.WriteLine("RSP Address:  0x" + Convert.ToString(SMU_ADDR_RSP, 16).ToUpper());
                Console.WriteLine("ARG0 Address: 0x" + Convert.ToString(SMU_ADDR_ARG0, 16).ToUpper());
                Console.WriteLine("ARG1 Address: 0x" + Convert.ToString(SMU_ADDR_ARG0, 16).ToUpper());
                Console.WriteLine("ARG0        : 0x" + Convert.ToString(arg0, 16).ToUpper());

                if (SmuWrite(command, arg0))
                {
                    // Read response
                    uint data = 0;

                    if (SmuReadReg(SMU_ADDR_RSP, ref data))
                    {
                        string responseString = "0x" + Convert.ToString(data, 16).ToUpper();
                        SetCmdStatus(GetSMUStatus.GetByType(data));
                        Console.WriteLine("CMD Status: " + responseString);

                        SmuReadReg(SMU_ADDR_ARG0, ref data);
                        ShowResult(data);
                    }
                    else
                    {
                        SetCmdStatus("Error reading response");
                    }
                } 
                else
                {
                    SetCmdStatus("Error SMU write");
                }

            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
        }

        private void ButtonDefaults_Click(object sender, EventArgs e)
        {
            this.InitForm();
        }

        private void ButtonApply_Click(object sender, EventArgs e)
        {
            try
            {
                ApplySettings();
            }
            catch (Exception ex)
            {
                HandleError(ex, "Error reading response");
                SetCmdStatus("Error");
            }
        }

        private void BackgroundWorkerReadPci_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                uint address = Convert.ToUInt32(textBoxPciAddress.Text.Trim(), 16);
                uint data = ReadDword(address);
                e.Result = data;
            }
            catch
            {
                this.Invoke(new MethodInvoker(delegate ()
                {
                    buttonPciRead.Enabled = true;
                    textBoxPciAddress.Enabled = true;
                    SetCmdStatus("Error");
                }));
                e.Result = 0;
            }
        }

        private void BackgroundWorkerReadPci_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            uint data = (uint)e.Result;

            buttonPciRead.Enabled = true;
            textBoxPciAddress.Enabled = true;
            SetCmdStatus("OK");
            ShowResult(data);
        }

        private void ButtonPciRead_Click(object sender, EventArgs e)
        {
            try
            {
                SetCmdStatus("Reading, please wait...");
                buttonPciRead.Enabled = false;
                textBoxPciAddress.Enabled = false;
                backgroundWorker1 = new BackgroundWorker();
                backgroundWorker1.DoWork += new DoWorkEventHandler(BackgroundWorkerReadPci_DoWork);
                backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BackgroundWorkerReadPci_RunWorkerCompleted);
                backgroundWorker1.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                SetCmdStatus("Error");
                buttonPciRead.Enabled = true;
                textBoxPciAddress.Enabled = true;
                HandleError(ex);
            }
        }
    }
}
