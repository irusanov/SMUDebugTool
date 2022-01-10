namespace ZenStatesDebugTool
{
   public class SmuAddressSet
    {
        public uint MsgAddress;
        public uint RspAddress;
        public uint ArgAddress;

        public SmuAddressSet()
        {
            MsgAddress = 0;
            RspAddress = 0;
            ArgAddress = 0;
        }

        public SmuAddressSet(uint msgAddress, uint rspAddress, uint argAddress)
        {
            MsgAddress = msgAddress;
            RspAddress = rspAddress;
            ArgAddress = argAddress;
        }
    }
}
