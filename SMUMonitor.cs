using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;
using ZenStates.Core;

namespace ZenStatesDebugTool
{
    public partial class SMUMonitor : Form
    {
        private readonly Ops OPS;
        readonly System.Windows.Forms.Timer MonitorTimer = new System.Windows.Forms.Timer();
        private readonly BindingList<SmuMonitorItem> list = new BindingList<SmuMonitorItem>();
        private uint prevCmdValue;
        private readonly uint SMU_ADDR_MSG;
        private readonly uint SMU_ADDR_ARG;
        private readonly uint SMU_ADDR_RSP;

        private class SmuMonitorItem
        {
            public string Cmd { get; set; }
            public string Arg { get; set; }
            public string Rsp { get; set; }
        }

        public SMUMonitor(Ops ops, uint addrMsg, uint addrArg, uint addrRsp)
        {
            OPS = ops;
            SMU_ADDR_MSG = addrMsg;
            SMU_ADDR_ARG = addrArg;
            SMU_ADDR_RSP = addrRsp;
            MonitorTimer.Interval = 10;
            MonitorTimer.Tick += new EventHandler(MonitorTimer_Tick);

            InitializeComponent();

            labelCmdAddr.Text = $"0x{addrMsg:X8}";
            labelRspAddr.Text = $"0x{addrRsp:X8}";
            labelArgAddr.Text = $"0x{addrArg:X8}";

            dataGridView2.DataSource = list;
        }

        private void AddLine()
        {
            uint msg = 0;
            uint rsp = 0;
            uint arg = 0;

            OPS.SmuReadReg(SMU_ADDR_MSG, ref msg);

            if (msg != prevCmdValue)
            {
                prevCmdValue = msg;
                if (OPS.SmuReadReg(SMU_ADDR_RSP, ref rsp))
                    OPS.SmuReadReg(SMU_ADDR_ARG, ref arg);

                new Thread(() => {
                    list.Add(new SmuMonitorItem
                    {
                        Cmd = $"0x{msg:X2}",
                        Arg = $"0x{arg:X8}",
                        Rsp = $"0x{rsp:X2} {GetSMUStatus.GetByType((SMU.Status)rsp)}"
                    });

                    dataGridView2.FirstDisplayedScrollingRowIndex = list.Count - 1;
                }).Start();
            }
        }

        private void MonitorTimer_Tick(object sender, EventArgs e) => AddLine();

        private void SMUMonitor_FormClosing(object sender, FormClosingEventArgs e) => MonitorTimer.Stop();

        private void ButtonClear_Click(object sender, EventArgs e) => list.Clear();

        private void SMUMonitor_Shown(object sender, EventArgs e) => MonitorTimer.Start();

        private void ButtonApply_Click(object sender, EventArgs e)
        {
            if (MonitorTimer.Enabled)
            {
                MonitorTimer.Stop();
                buttonStartStop.Text = "Start";
            }
            else
            {
                prevCmdValue = 0;
                MonitorTimer.Start();
                buttonStartStop.Text = "Stop";
            }
        }
    }
}
