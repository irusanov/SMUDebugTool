using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZenStates.Core;

namespace ZenStatesDebugTool
{
    public partial class PowerTableMonitor : Form
    {
        private readonly PowerTable PowerTable;
        private uint dramBaseAddress = 0;
        private readonly UIntPtr dramPtr;
        private readonly Ops OPS;
        private readonly uint[] table = new uint[PowerTable.tableSize / 4];
        readonly Timer PowerCfgTimer = new Timer();
        private readonly BindingList<PowerMonitorItem> list = new BindingList<PowerMonitorItem>();
        private class PowerMonitorItem
        {
            public string Index { get; set; }
            public string Offset { get; set; }
            public string Value { get; set; }
        }

        private void ReadPowerConfig()
        {
            if (dramBaseAddress > 0)
            {
                try
                {
                    uint data = 0;

                    if (OPS.TransferTableToDram() != SMU.Status.OK)
                        OPS.TransferTableToDram(); // retry

                    for (int i = 0; i < table.Length; ++i)
                    {
                        InteropMethods.GetPhysLong(dramPtr + (i * 0x4), out data);
                        table[i] = data;
                    }

                    if (table.Any(v => v != 0))
                        PowerTable.Table = table;
                }
                catch (EntryPointNotFoundException ex)
                {
                    throw new ApplicationException(ex.Message);
                }
                catch (DllNotFoundException ex)
                {
                    throw new ApplicationException(ex.Message);
                }
            }
        }

        private void FillInData(uint[] table)
        {
            list.Clear();

            for (var i = 0; i < table.Length; i++)
            {
                var bytes = BitConverter.GetBytes(table[i]);
                list.Add(new PowerMonitorItem {
                    Index = $"{i:D4}",
                    Offset = $"0x{(i*4):X4}",
                    Value = $"{BitConverter.ToSingle(bytes, 0):F6}"
                });;
            }
        }

        private void RefreshData(uint[] table)
        {
            int index = 0;

            foreach (var item in list)
            {
                var bytes = BitConverter.GetBytes(table[index]);
                item.Value = $"{BitConverter.ToSingle(bytes, 0):F6}";
                index++;
            }

            dataGridView1.Refresh();
        }

        private void PowerCfgTimer_Tick(object sender, EventArgs e)
        {
            Console.WriteLine("refreshing");
            ReadPowerConfig();
            RefreshData(PowerTable.Table);
        }

        public PowerTableMonitor(Ops ops)
        {
            OPS = ops;
            PowerTable = new PowerTable(OPS.cpuinfo.smu.SMU_TYPE);
            PowerCfgTimer.Interval = 2000;
            PowerCfgTimer.Tick += new EventHandler(PowerCfgTimer_Tick);

            InitializeComponent();

            dataGridView1.DataSource = list;
            
            // Get first base address
            dramBaseAddress = (uint)(OPS.GetDramBaseAddress() & 0xFFFFFFFF);
            if (dramBaseAddress > 0)
            {
                dramPtr = new UIntPtr(dramBaseAddress);
                ReadPowerConfig();
            }

            FillInData(PowerTable.Table);
        }

        private void PowerTableMonitor_FormClosing(object sender, FormClosingEventArgs e)
        {
            PowerCfgTimer.Stop();
        }

        private void PowerTableMonitor_Shown(object sender, EventArgs e)
        {
            PowerCfgTimer.Start();
        }

        private void ButtonApply_Click(object sender, EventArgs e)
        {
            PowerCfgTimer.Interval = Convert.ToInt32(numericUpDownInterval.Value);
        }
    }
}
