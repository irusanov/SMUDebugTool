using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Management;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using ZenStates.Core;
using ZenStates.Core.Common;
using ZenStates.Core.Hardware;
using ZenStates.Core.Hardware.DRAM.DDR5.Spd;
using DRAM = ZenStates.Core.Hardware.DRAM;

namespace ZenStatesDebugTool
{
    /// <summary>
    /// Builds the debug report from the debug functions of ZenStates-Core (the same sections, in the same format, as
    /// the ZenTimings report, so the core's report parsers can read it back), plus the SMU sections of this tool:
    /// the mailboxes with their message IDs, the mailboxes found by the SMU scan and the P-state MSRs.
    /// A section that fails is marked and the report goes on. It only reads; it does not run the SMU scan.
    /// </summary>
    internal sealed class DebugReportGenerator
    {
        private const string WmiAmdAcpi = "AMD_ACPI";
        private const string WmiScope = "root\\wmi";

        private readonly Cpu cpu;
        private readonly IList<SmuAddressSet> scannedMailboxes;
        private readonly StringBuilder result = new StringBuilder();

        /// <param name="scannedMailboxes">Mailboxes found by the SMU scan in this session, may be empty.</param>
        public DebugReportGenerator(Cpu cpu, IList<SmuAddressSet> scannedMailboxes)
        {
            this.cpu = cpu;
            this.scannedMailboxes = scannedMailboxes ?? new List<SmuAddressSet>();
        }

        public static string ProductHeader => $"{Application.ProductName} {Application.ProductVersion}";

        public string Generate()
        {
            result.Clear();

            try
            {
                Build();
            }
            catch (Exception ex)
            {
                AddLine();
                AddLine("<FAILED> " + ex);
            }

            return result.ToString();
        }

        private void AddHeading(string heading) => result.Append(ReportBuilder.Heading(heading));

        private void AddLine(string row = "") => result.Append(row + Environment.NewLine);

        // One section: a section that fails is marked and the report goes on
        private void AddReport(Func<string> report)
        {
            try
            {
                AddLine(report());
            }
            catch (Exception ex)
            {
                AddLine("<FAILED>");
                AddLine(ex.Message);
            }
        }

        private void AddSection(string heading, Action body)
        {
            AddHeading(heading);
            try
            {
                body();
            }
            catch (Exception ex)
            {
                AddLine("<FAILED>");
                AddLine(ex.Message);
            }
            AddLine();
        }

        private void Build()
        {
            DRAM.MemoryConfig memoryConfig = cpu.GetMemoryConfig();

            AddLine($"{ProductHeader} Debug Report");
            AddLine($"Core Version: {cpu.Version}");
            AddLine($"PawnIO Version: {ZenStates.Core.PawnIo.PawnIo.Version}");
            AddLine();

            AddSection("System Info", () =>
            {
                AddLine($"{"OS:",-19}{new ComputerInfo().OSFullName}");

                foreach (PropertyInfo property in cpu.systemInfo.GetType().GetProperties())
                {
                    string name = property.Name;
                    if (name == "Hardware" || name == "SensorGroups" || name == "SMBios")
                        continue;

                    object value = property.GetValue(cpu.systemInfo, null);
                    if (name == "CpuId" || name == "PatchLevel" || name == "SmuTableVersion")
                        AddLine($"{name + ":",-19}{value:X8}");
                    else if (name == "Model" || name == "ExtendedModel" || name == "BaseModel")
                        AddLine($"{name + ":",-19}{value} (0x{value:X})");
                    else
                        AddLine($"{name + ":",-19}{value}");
                }

                AddLine($"{"DRAM Base Address:",-19}{(long)cpu.powerTable.DramBaseAddress:X16}");
                AddLine($"{"MemType:",-19}{memoryConfig?.Type}");
            });

            AddHeading("CPU Topology");
            AddReport(GetTopologyReport);
            AddLine();

            AddSmuSections();

            // Memory modules. The SPD is read in the background at startup; a report taken right away waits for it.
            memoryConfig?.WaitForSpdInfo(5000);
            AddSection("Memory Modules", () =>
            {
                foreach (var module in memoryConfig.Modules)
                {
                    AddLine($"{module.BankLabel} | {module.DeviceLocator}");
                    AddLine($"-- Slot: {module.Slot}");
                    AddLine(module.Rank == DRAM.MemRank.DR ? "-- Dual Rank" : "-- Single Rank");
                    AddLine($"-- DCT Offset: 0x{module.DctOffset >> 20:X}");
                    AddLine($"-- Manufacturer: {module.Manufacturer}");
                    AddLine($"-- {module.PartNumber} {module.Capacity} {module.ClockSpeed}MHz");
                    AddLine($"-- {module.AddressConfig}");
                    AddLine();
                }
            });

            if (memoryConfig?.Type == DRAM.MemType.DDR5)
            {
                AddSection("SMBUS Memory Modules", () =>
                {
                    AddLine();
                    foreach (KeyValuePair<byte, Ddr5SpdInfo> kvp in memoryConfig.ReadAndDecodeAll() ?? new Dictionary<byte, Ddr5SpdInfo>())
                    {
                        AddLine(string.Format("DIMM at I2C address 0x{0:X2}", kvp.Key));
                        AddLine(kvp.Value.ToString());
                    }
                });
            }
            else if (memoryConfig?.Type == DRAM.MemType.LPDDR5)
            {
                AddSection("APOB Memory Modules (LPDDR5 SPD)", () =>
                {
                    AddLine();
                    var spd = memoryConfig.SpdInfo;
                    if (spd == null || spd.Count == 0)
                        AddLine("No SPD in the APOB.");
                    foreach (KeyValuePair<byte, Ddr5SpdInfo> kvp in spd ?? new Dictionary<byte, Ddr5SpdInfo>())
                    {
                        AddLine(string.Format("{0} {1}", kvp.Value.FromApob ? "APOB slot" : "DIMM at I2C address", kvp.Key));
                        AddLine(kvp.Value.ToString());
                    }
                });
            }
            else if (memoryConfig?.Type == DRAM.MemType.DDR4)
            {
                AddSection("SMBUS Memory Modules (DDR4 SPD)", () =>
                {
                    AddLine();
                    var modules = memoryConfig.ReadAndDecodeAllDdr4();
                    if (modules == null || modules.Count == 0)
                        AddLine("No module SPD could be read.");
                    foreach (var kvp in modules ?? new Dictionary<byte, DRAM.DDR4.Spd.Ddr4SpdInfo>())
                    {
                        AddLine(string.Format("DIMM at SPD address 0x{0:X2}", kvp.Key));
                        AddLine(kvp.Value.ToString());
                    }
                });

                AddSection("SMBUS Memory Module Thermal Sensors", () =>
                {
                    AddLine();
                    var sensors = memoryConfig.Ddr4ThermalSensors;
                    if (sensors == null || sensors.Count == 0)
                        AddLine("No module SPD found on the SMBus.");
                    foreach (var kvp in sensors ?? new Dictionary<byte, DRAM.DDR4.Thermal.Ddr4ThermalData>())
                    {
                        AddLine(string.Format("DIMM at SPD address 0x{0:X2}", kvp.Key));
                        AddLine(kvp.Value != null ? kvp.Value.ToString() : "  Thermal sensor: not available");
                    }
                });
            }

            AddChannels();

            AddSection("Memory Config", () =>
            {
                var timings = memoryConfig.Timings[0].Value;
                foreach (PropertyInfo property in timings.GetType().GetProperties())
                {
                    if (property.Name != "Item")
                        AddLine($"{property.Name + ":",-28}{timings[property.Name]}");
                }
            });

            AddReport(() => cpu.info.apob?.GetReport() ?? "APOB: not available");
            AddLine();
            AddReport(() => cpu.info.aod?.GetReport() ?? "AOD: not available");
            AddLine();

            // SMU power table (decoded), then the raw PM table as the PawnIO RyzenSMU module reads it
            AddReport(() => cpu.powerTable?.GetReport() ?? "SMU: Power Table not available");
            AddLine();
            AddReport(() => cpu.RyzenSmu?.GetReport() ?? "Ryzen SMU: not available");
            AddLine();

            // Sensor hardware of the core (SuperIO, SVI3, BCLK...); ASUS WMI has its own section below
            AddHeading("SuperIO");
            foreach (IHardware hardware in cpu.systemInfo?.Hardware ?? new List<IHardware>())
            {
                if (hardware.HardwareType == HardwareType.SuperIO)
                    AddReport(hardware.GetReport);
            }
            AddLine();

            foreach (IHardware hardware in cpu.systemInfo?.Hardware ?? new List<IHardware>())
            {
                if (hardware.HardwareType != HardwareType.SuperIO && hardware.HardwareType != HardwareType.AsusWmi)
                {
                    AddHeading($"Hardware: {hardware.HardwareType}");
                    AddReport(hardware.GetReport);
                }
            }

            AddReport(() => SystemInfo.SMBios.GetReport());

            AddSection("WMI: AMD_ACPI", () => AddLine(WMI.Query(WmiScope, WmiAmdAcpi) != null ? "OK" : "<FAILED>"));

            string instanceName = null;
            AddSection("WMI: Instance Name", () =>
            {
                instanceName = WMI.GetInstanceName(WmiScope, WmiAmdAcpi);
                AddLine(string.IsNullOrEmpty(instanceName) ? "<FAILED>" : instanceName);
            });

            if (!string.IsNullOrEmpty(instanceName))
                AddWmiFunctions(instanceName);

            AsusWmiHardware asusWmi = cpu.systemInfo?.GetAsusWmi();
            if (asusWmi != null)
            {
                AddHeading(AsusWmiHardware.GroupName);
                AddReport(() =>
                {
                    // The refresh reads only the sensor it needs, so read them all for the report
                    asusWmi.Update();
                    return asusWmi.GetReport().TrimEnd();
                });
                AddLine();
            }

            AddSection("SVI2: PCI Range", () =>
            {
                uint start = 0x0005A000;
                uint end = 0x0005A0FF;
                if (cpu.smu.SMU_TYPE == SMU.SmuType.TYPE_APU1 || cpu.smu.SMU_TYPE == SMU.SmuType.TYPE_APU2)
                {
                    start = 0x0006F000;
                    end = 0x0006F0FF;
                }

                for (uint address = start; address <= end; address += 4)
                {
                    uint data = 0xFFFFFFFF;
                    if (cpu.ReadDwordEx(address, ref data))
                        AddLine($"0x{address:X8}: 0x{data:X8}");
                }
            });

            // Strix / Krackan have no SMUFUSE block at 0x5D200; their fuses are looked for around 0x3820AB0
            if (cpu.ReadDword(0x0005D218) == 0xFFFFFFFF)
                AddRegisterRange("SMU: SMUFUSE 0x3820A00", 0x03820A00, 0x03820BFF);

            AddRegisterRange("SMU: SMUFUSE NBSMNIND", 0x0005D200, 0x0005D5FF);

            AddReport(() => Mmio.Instance.GetReport());
            AddLine();
            AddReport(() => cpu.bclk?.GetMeasurementReport() ?? string.Empty);
        }

        // ---- SMU Debug Tool sections ----

        private void AddSmuSections()
        {
            AddSection("SMU: Mailboxes", () =>
            {
                AddLine($"{"SMU Type:",-19}{cpu.smu.SMU_TYPE}");
                AddLine($"{"SMU Version:",-19}{cpu.systemInfo.SmuVersion}");
                AddLine($"{"Manual OC Mode:",-19}{SafeValue(() => cpu.GetOcMode() ? "On" : "Off")}");
                AddLine();

                AddMailbox("RSMU", cpu.smu.Rsmu);
                AddMailbox("MP1", cpu.smu.Mp1Smu);
                AddMailbox("HSMP", cpu.smu.Hsmp);
            });

            AddSection("SMU: Scanned Mailboxes", () =>
            {
                if (scannedMailboxes.Count == 0)
                    AddLine("No SMU scan in this session.");

                for (int i = 0; i < scannedMailboxes.Count; i++)
                {
                    SmuAddressSet set = scannedMailboxes[i];
                    AddLine($"Mailbox {i + 1}: MSG 0x{set.MsgAddress:X8}, RSP 0x{set.RspAddress:X8}, ARG 0x{set.ArgAddress:X8}");
                }
            });

            AddSection("MSR: P-States", () =>
            {
                for (uint i = 0; i < 8; i++)
                {
                    uint eax = 0, edx = 0;
                    uint msr = 0xC0010064 + i;
                    AddLine(cpu.ReadMsr(msr, ref eax, ref edx)
                        ? $"P{i} 0x{msr:X8}: 0x{edx:X8}{eax:X8}"
                        : $"P{i} 0x{msr:X8}: <FAILED>");
                }
            });
        }

        // The mailbox addresses and every message ID the core knows for it (SMU_ADDR_* and SMU_MSG_* fields)
        private void AddMailbox(string name, object mailbox)
        {
            AddLine($"-- {name}");
            if (mailbox == null)
            {
                AddLine("   not available");
                AddLine();
                return;
            }

            var addresses = new List<string>();
            var messages = new List<string>();
            foreach (FieldInfo field in mailbox.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance))
            {
                object value = field.GetValue(mailbox);
                if (!(value is uint number))
                    continue;

                if (field.Name.StartsWith("SMU_ADDR_"))
                    addresses.Add($"   {field.Name + ":",-40}0x{number:X8}");
                else if (field.Name.StartsWith("SMU_MSG_") && number != 0)
                    messages.Add($"   {field.Name + ":",-40}0x{number:X2}");
            }

            foreach (string line in addresses)
                AddLine(line);
            foreach (string line in messages)
                AddLine(line);
            AddLine();
        }

        private static string SafeValue(Func<string> value)
        {
            try
            {
                return value();
            }
            catch
            {
                return "<FAILED>";
            }
        }

        private void AddRegisterRange(string heading, uint start, uint end)
        {
            AddSection(heading, () =>
            {
                for (uint address = start; address <= end; address += 4)
                    AddLine($"0x{address:X8}: 0x{cpu.ReadDword(address):X8}");
            });
        }

        private void AddChannels()
        {
            AddSection("Memory Channels Info", () =>
            {
                AddLine("-- UMC Configuration");
                for (uint i = 0; i < 0xC; i++)
                {
                    uint reg = (i << 20) | 0x50100;
                    AddLine($"0x{reg:X8}: 0x{cpu.ReadDword(reg):X8}");
                }

                AddLine();

                for (uint i = 0; i < 0xC; i++)
                {
                    try
                    {
                        uint offset = i << 20;
                        bool channel = ZenStates.Core.Utils.GetBits(cpu.ReadDword(offset | 0x50DF0), 19, 1) == 0;
                        bool dimm1 = ZenStates.Core.Utils.GetBits(cpu.ReadDword(offset | 0x50000), 0, 1) == 1;
                        bool dimm2 = ZenStates.Core.Utils.GetBits(cpu.ReadDword(offset | 0x50008), 0, 1) == 1;
                        bool enabled = channel && (dimm1 || dimm2);

                        AddLine($"Channel{i}: {enabled}");
                        if (!enabled)
                            continue;

                        AddLine("-- UMC Registers");
                        for (uint reg = offset | 0x50000; reg <= (offset | 0x50FFC); reg += 4)
                            AddLine($"   0x{reg:X8}: 0x{cpu.ReadDword(reg):X8}");
                    }
                    catch
                    {
                        AddLine($"Channel{i}: <FAILED>");
                    }
                }
            });
        }

        private void AddWmiFunctions(string instanceName)
        {
            try
            {
                using (var classInstance = new ManagementObject(WmiScope, $"{WmiAmdAcpi}.InstanceName='{instanceName}'", null))
                {
                    string[] functionObjects = { "GetObjectID", "GetObjectID2" };
                    for (int index = 0; index < functionObjects.Length; index++)
                    {
                        AddSection($"WMI: Bios Functions {index + 1}", () =>
                        {
                            using (var pack = WMI.InvokeMethodAndGetValue(classInstance, functionObjects[index], "pack", null, 0))
                            {
                                if (pack == null)
                                {
                                    AddLine("<FAILED>");
                                    return;
                                }

                                var ids = (uint[])pack.GetPropertyValue("ID");
                                var idStrings = (string[])pack.GetPropertyValue("IDString");
                                var length = (byte)pack.GetPropertyValue("Length");

                                for (int i = 0; i < length; ++i)
                                {
                                    if (idStrings[i] == "")
                                        break;
                                    AddLine($"{idStrings[i] + ":",-30}{ids[i]:X8}");
                                }
                            }
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                AddLine($"<FAILED: {ex.Message}>");
            }
        }

        /// <summary>How the CCD / CCX / core counts were found (CPUID, fuses), as in the ZenTimings report.</summary>
        private string GetTopologyReport()
        {
            Cpu.CpuTopology t = cpu.info.topology;
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{"CCDs:",-19}{t.ccds} (enabled 0x{t.ccdEnableMap:X2}, disabled 0x{t.ccdDisableMap:X2})");
            if (t.fuse1 != 0)
                sb.AppendLine($"{"CCD fuses:",-19}0x{t.fuse1:X8} = 0x{t.ccdsPresent:X8}, 0x{t.fuse2:X8} = 0x{t.ccdsDown:X8}");

            string ccxCores = t.ccxCoreCounts != null && t.ccxCoreCounts.Length > 0
                ? string.Join(", ", Array.ConvertAll(t.ccxCoreCounts, c => c.ToString(CultureInfo.InvariantCulture)))
                : "unknown";
            sb.AppendLine($"{"CCXs:",-19}{t.ccxs} (cores per CCX from CPUID: {ccxCores})");
            sb.AppendLine($"{"Cores:",-19}{t.cores} of {t.physicalCores} slots, {t.threadsPerCore} thread(s) each, {t.cpuNodes} node(s)");

            if (t.coreFuseAddress != 0)
            {
                sb.AppendLine($"{"Core fuse:",-19}0x{t.coreFuseAddress:X8}, mask at bit {t.coreFuseShift}");
                for (int i = 0; t.coreFuseValues != null && i < t.coreFuseValues.Length; i++)
                {
                    string disabled = t.coreDisableMap != null && i < t.coreDisableMap.Length
                        ? $", disabled cores 0x{t.coreDisableMap[i]:X2}"
                        : "";
                    string label = "  CCD " + i + ":";
                    sb.AppendLine($"{label,-19}0x{t.coreFuseValues[i]:X8}{disabled}");
                }
            }
            else
            {
                sb.AppendLine($"{"Core fuse:",-19}not known for this processor");
            }

            if (!string.IsNullOrEmpty(t.fuseNote))
                sb.AppendLine($"{"Note:",-19}{t.fuseNote}");

            return sb.ToString().TrimEnd();
        }
    }
}
