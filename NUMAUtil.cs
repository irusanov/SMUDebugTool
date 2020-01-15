using System;
using System.Runtime.InteropServices;

namespace ZenStatesDebugTool
{
    public class NUMAUtil
    {
        public ulong HighestNumaNode
        {
            get
            {
                ulong n = 0;
                GetNumaHighestNodeNumber(ref n);
                return n;
            }
        }

        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        private struct _GROUP_AFFINITY
        {
            public UIntPtr Mask;
            [MarshalAs(UnmanagedType.U2)]
            public ushort Group;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.U2)]
            public ushort[] Reserved;
        }

        [DllImport("kernel32", SetLastError = true)]
        private static extern Boolean SetThreadGroupAffinity(
            IntPtr hThread,
            ref _GROUP_AFFINITY GroupAffinity,
            ref _GROUP_AFFINITY PreviousGroupAffinity);

        [DllImport("kernel32", SetLastError = true)]
        private static extern IntPtr GetCurrentThread();

        [DllImport("kernel32", SetLastError = true)]
        private static extern Boolean GetNumaHighestNodeNumber(ref ulong HighestNodeNumer);

        /// <summary>
        /// Sets the processor group and the processor cpu affinity of the current thread.
        /// </summary>
        /// <param name="group">A processor group number.</param>
        /// <param name="cpus">A list of CPU numbers. The values should be
        /// between 0 and <see cref="Environment.ProcessorCount"/>.</param>
        public void SetThreadProcessorAffinity(ushort groupId, params int[] cpus)
        {
            if (cpus == null) throw new ArgumentNullException(nameof(cpus));
            if (cpus.Length == 0) throw new ArgumentException("You must specify at least one CPU.", nameof(cpus));

            // Supports up to 64 processors
            long cpuMask = 0;
            foreach (var cpu in cpus)
            {
                if (cpu < 0 || cpu >= Environment.ProcessorCount)
                    throw new ArgumentException("Invalid CPU number.");

                cpuMask |= 1L << cpu;
            }

            var hThread = GetCurrentThread();
            var previousAffinity = new _GROUP_AFFINITY { Reserved = new ushort[3] };
            var newAffinity = new _GROUP_AFFINITY
            {
                Group = groupId,
                Mask = new UIntPtr((ulong)cpuMask),
                Reserved = new ushort[3]
            };

            SetThreadGroupAffinity(hThread, ref newAffinity, ref previousAffinity);
        }
    }
}