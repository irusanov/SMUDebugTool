using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Globalization;
using ZenStates.Core;

namespace ZenStatesDebugTool
{
    public partial class PowerTableMonitor : Form
    {
        private readonly Cpu CPU;
        readonly Timer PowerCfgTimer = new Timer();
        private readonly BindingList<PowerMonitorItem> list = new BindingList<PowerMonitorItem>();
        private class PowerMonitorItem
        {
            public string Index { get; set; }
            public string Offset { get; set; }
            public string Value { get; set; }
            public string Max { get; set; }
        }

        private void FillInData(float[] table)
        {
            list.Clear();

            for (var i = 0; i < table.Length; i++)
            {
                var valueStr = table[i].ToString("F6", CultureInfo.InvariantCulture);
                list.Add(new PowerMonitorItem
                {
                    Index = $"{i:D4}",
                    Offset = $"0x{(i * 4):X4}",
                    Value = valueStr,
                    Max = valueStr
                });
            }
        }

        private void RefreshData(float[] table)
        {
            int index = 0;

            foreach (var item in list)
            {
                var current = table[index];
                var currentStr = current.ToString("F6", CultureInfo.InvariantCulture);

                // Update value string
                item.Value = currentStr;

                // Parse existing max; if parse fails, treat as 0
                float existingMax = 0f;
                if (!string.IsNullOrWhiteSpace(item.Max) &&
                    !float.TryParse(item.Max, NumberStyles.Float, CultureInfo.InvariantCulture, out existingMax))
                {
                    existingMax = 0f;
                }

                // Update max if needed
                if (current > existingMax)
                {
                    item.Max = currentStr;
                }

                index++;
            }

            dataGridView1.Refresh();
        }

        private void PowerCfgTimer_Tick(object sender, EventArgs e)
        {
            Console.WriteLine("refreshing");
            if (CPU.RefreshPowerTable() == SMU.Status.OK)
                RefreshData(CPU.powerTable.Table);
        }

        public PowerTableMonitor(Cpu cpu)
        {
            CPU = cpu;
            cpu.RefreshPowerTable();

            PowerCfgTimer.Interval = 2000;
            PowerCfgTimer.Tick += new EventHandler(PowerCfgTimer_Tick);

            InitializeComponent();

            dataGridView1.DataSource = list;

            FillInData(cpu.powerTable.Table);
        }

        private void PowerTableMonitor_FormClosing(object sender, FormClosingEventArgs e)
        {
            PowerCfgTimer.Stop();
            //CPU.powerTable.Dispose();
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
