using System;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using ZenStates.Core;

namespace ZenStatesDebugTool
{
    public partial class PCIRangeMonitor : Form
    {
        private readonly Cpu CPU;
        readonly System.Windows.Forms.Timer RefreshTimer = new System.Windows.Forms.Timer();
        private readonly BindingList<AddressMonitorItem> list = new BindingList<AddressMonitorItem>();
        private readonly uint StartAddress;
        private readonly uint EndAddress;
        private class AddressMonitorItem
        {
            public string Address { get; set; }
            public string Value { get; set; }
            public string ValueFloat { get; set; }
            public string ValueBin { get; set; }
        }

        private BindingList<AddressMonitorItem> RefreshList()
        {
            BindingList<AddressMonitorItem> l = new BindingList<AddressMonitorItem>();
            Thread refreshThread = new Thread(() =>
            {
                for (var i = StartAddress; i < EndAddress; i += 4)
                {
                    uint value = 0;

                    CPU.ReadDwordEx(i, ref value);
                    var floatValue = Convert.ToSingle(value);

                    //if (floatValue > 1f && floatValue < 2f)
                    {
                        l.Add(new AddressMonitorItem
                        {
                            Address = $"0x{i:X8}",
                            Value = $"0x{value:X8}",
                            ValueFloat = $"{floatValue:F4}",
                            ValueBin = $"{Convert.ToString(value, 2).PadLeft(32, '0')}"
                        });
                    }
                }
            });
            refreshThread.IsBackground = true;
            refreshThread.Start();
            refreshThread.Join();
            return l;
        }

        private void RefreshData()
        {
            Thread refreshThread = new Thread(() =>
            {
                var l = RefreshList();

                foreach (var item in list)
                {
                    var newItem = l.FirstOrDefault(x => x.Address == item.Address);
                    if (newItem != null && item.Value != newItem.Value)
                    {
                        item.Value = newItem.Value;
                        item.ValueFloat = newItem.ValueFloat;
                        item.ValueBin = newItem.ValueBin;
                        var rowIndex = list.IndexOf(item);
                        dataGridViewPCIRange.Rows[rowIndex].DefaultCellStyle.BackColor = System.Drawing.Color.LightGoldenrodYellow;
                    }
                }

                dataGridViewPCIRange.Invoke((MethodInvoker)delegate
                {
                    dataGridViewPCIRange.Refresh();
                });
            });

            refreshThread.IsBackground = true;
            refreshThread.Start();
        }

        public PCIRangeMonitor(Cpu cpu, uint startAddress, uint endAddress)
        {
            CPU = cpu;
            StartAddress = startAddress;
            EndAddress = endAddress;

            RefreshTimer.Interval = 500;
            RefreshTimer.Tick += new EventHandler(RefreshTimer_Tick);

            InitializeComponent();

            list.Clear();
            list = RefreshList();
            dataGridViewPCIRange.DataSource = list;
        }

        private void RefreshTimer_Tick(object sender, EventArgs e)
        {
            Console.WriteLine("refreshing");
            RefreshData();
        }

        private void PCIRangeMonitor_FormClosing(object sender, FormClosingEventArgs e)
        {
            RefreshTimer.Stop();
        }

        private void PCIRangeMonitor_Shown(object sender, EventArgs e)
        {
            RefreshTimer.Start();
        }
    }
}
