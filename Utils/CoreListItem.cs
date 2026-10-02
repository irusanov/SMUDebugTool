namespace ZenStatesDebugTool
{
    public class CoreListItem
    {
        public int CCD { get; }
        public int CCX { get; }
        public int CORE { get; }
        public int LogicalIndex { get; }
        public uint Mask { get; }

        public CoreListItem(int ccd, int ccx, int core, int logicalIndex, uint mask)
        {
            this.CCD = ccd;
            this.CCX = ccx;
            this.CORE = core;
            this.LogicalIndex = logicalIndex;
            this.Mask = mask;
        }

        public override string ToString()
        {
            return string.Format("Core {0}", (object)(this.LogicalIndex));
        }
    }
}