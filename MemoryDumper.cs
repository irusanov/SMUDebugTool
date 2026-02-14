using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;


namespace ZenStatesDebugTool
{
    public static class MemoryDumper
    {
        public static void Dump32BitAddressSpaceAsBytes(string outputPath, uint startAddress, uint endAddress)
        {
            //const uint StartAddress = 0xC0000000;
            //const uint EndAddress = 0xFFFFFFFF;
            const uint Step = 4; // Read DWORDs


            Thread thread = new Thread(() =>
            {
                using (FileStream fs = new FileStream(
                outputPath,
                FileMode.Create,
                FileAccess.Write,
                FileShare.None,
                bufferSize: 1024 * 1024))
                {
                    uint data = 0;
                    byte[] bytes = new byte[4];

                    for (uint addr = startAddress; addr <= endAddress; addr += Step)
                    {
                        bool ok = false;

                        try
                        {
                            ok = CpuSingleton.Instance.io.GetPhysLong((UIntPtr)addr, out data);
                        }
                        catch
                        {
                            ok = false;
                        }

                        if (ok)
                        {
                            // Little-endian split
                            bytes[0] = (byte)(data & 0xFF);
                            bytes[1] = (byte)((data >> 8) & 0xFF);
                            bytes[2] = (byte)((data >> 16) & 0xFF);
                            bytes[3] = (byte)((data >> 24) & 0xFF);
                        }
                        else
                        {
                            // Mark unreadable regions clearly
                            bytes[0] = 0xFF;
                            bytes[1] = 0xFF;
                            bytes[2] = 0xFF;
                            bytes[3] = 0xFF;
                        }

                        fs.Write(bytes, 0, 4);

                        if (addr > endAddress - Step)
                            break;
                    }
                }
            })
            {
                IsBackground = true
            };
            thread.Start();
            thread.Join();
        }
    }
}