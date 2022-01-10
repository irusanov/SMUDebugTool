namespace ZenStatesDebugTool
{
    public class CoreListItem
    {
        public int CCD { get; }
        public int CCX { get; }
        public int CORE { get; }

        public CoreListItem(int ccd, int ccx, int core)
        {
            this.CCD = ccd;
            this.CCX = ccx;
            this.CORE = core;
        }

        public override string ToString()
        {
            return string.Format("Core {0}", (object)(this.CORE));
        }
    }
}