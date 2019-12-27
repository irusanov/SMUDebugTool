using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using OpenLibSys;

namespace ZenStatesDebugTool
{
    public partial class SettingsForm : Form, IDisposable
    {
        private static readonly int Threads = Convert.ToInt32(Environment.GetEnvironmentVariable("NUMBER_OF_PROCESSORS"));
        public Ols ols;
        private SMU.CPUType cpuType = SMU.CPUType.Unsupported;
        private SMU smu;
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

            // uint test = Convert.ToUInt32(textBoxCMDAddress.Text, 16);
            // this.labelStatus.Text = Convert.ToString(test, 16).ToUpper();
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

        private void ResetFields(SMU smuSettings)
        {
            textBoxCMDAddress.Text = this.cpuType > SMU.CPUType.DEBUG ? "0x" + Convert.ToString(smuSettings.SMU_ADDR_MSG, 16).ToUpper() : "0x0";
            textBoxRSPAddress.Text = this.cpuType > SMU.CPUType.DEBUG ? "0x" + Convert.ToString(smuSettings.SMU_ADDR_RSP, 16).ToUpper() : "0x0";
            textBoxARGAddress.Text = this.cpuType > SMU.CPUType.DEBUG ? "0x" + Convert.ToString(smuSettings.SMU_ADDR_ARG0, 16).ToUpper() : "0x0";

            textBoxCMD.Text = "0x1";
            textBoxARG0.Text = "0x0";
            textBoxARG1.Text = "0x0";
        }

        private void InitForm()
        {
            this.smu = GetMaintainedSettings.GetByType(this.cpuType);
            this.labelStatus.Text = Convert.ToString(this.cpuType) + ". Ready.";
            ResetFields(smu);
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

        private bool SmuReadReg(UInt32 addr, ref UInt32 data)
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
            UInt16 timeout = 1000;
            UInt32 data = 0;
            while ((!res || data != 1) && --timeout > 0)
            {
                res = SmuReadReg(SMU_ADDR_RSP, ref data);
            }

            if (timeout == 0 || data != 1) res = false;

            return res;
        }

        private bool SmuRead(UInt32 msg, ref UInt32 data)
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

        private bool SmuWrite(uint msg, uint arg0, uint arg1)
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
                res = SmuWriteReg(SMU_ADDR_ARG0, arg0);

                if (res)
                {
                    SmuWriteReg(SMU_ADDR_ARG1, arg1);
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

        public uint ReadDword(uint value)
        {
            ols.WritePciConfigDword(smu.SMU_PCI_ADDR, (byte)smu.SMU_OFFSET_ADDR, value);
            Thread.Sleep(5000);
            return ols.ReadPciConfigDword(smu.SMU_PCI_ADDR, (byte)smu.SMU_OFFSET_DATA);
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
                uint arg1 = Convert.ToUInt32(textBoxARG1.Text, 16);

                Console.WriteLine("MSG Address:  0x" + Convert.ToString(SMU_ADDR_MSG, 16).ToUpper());
                Console.WriteLine("RSP Address:  0x" + Convert.ToString(SMU_ADDR_RSP, 16).ToUpper());
                Console.WriteLine("ARG0 Address: 0x" + Convert.ToString(SMU_ADDR_ARG0, 16).ToUpper());
                Console.WriteLine("ARG1 Address: 0x" + Convert.ToString(SMU_ADDR_ARG0, 16).ToUpper());
                Console.WriteLine("ARG0        : 0x" + Convert.ToString(arg0, 16).ToUpper());
                Console.WriteLine("ARG1        : 0x" + Convert.ToString(arg1, 16).ToUpper());

                if (SmuWrite(command, arg0, arg1))
                {
                    // Read response
                    uint data = 0;

                    if (SmuReadReg(SMU_ADDR_RSP, ref data))
                    {
                        string responseString = "0x" + Convert.ToString(data, 16).ToUpper();
                        labelStatus.Text = GetSMUStatus.GetByType(data);
                        Console.WriteLine("CMD Status: " + responseString);

                        SmuReadReg(SMU_ADDR_ARG0, ref data);
                        responseString = $"HEX: 0x{Convert.ToString(data, 16).ToUpper()}\n" +
                                         $"INT: {Convert.ToString(data, 10).ToUpper()}";
                        Console.WriteLine("Response: " + responseString);
                        MessageBox.Show(responseString, "Response");
                    }
                    else
                    {
                        labelStatus.Text = "Error reading response";
                    }
                } 
                else
                {
                    labelStatus.Text = "Error SMU write";
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void buttonDefaults_Click(object sender, EventArgs e)
        {
            this.InitForm();
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            try
            {
                ApplySettings();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error reading response");
                labelStatus.Text = "Error";
            }
        }
    }
}
