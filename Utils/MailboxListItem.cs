using ZenStates.Core;

namespace ZenStatesDebugTool
{
    public class MailboxListItem
    {
        public uint msgAddr { get; }
        public uint rspAddr { get; }
        public uint argAddr { get; }
        public string label { get; }

        public MailboxListItem(string label, SmuAddressSet addressSet)
        {
            this.label = label;
            msgAddr = addressSet.MsgAddress;
            rspAddr = addressSet.RspAddress;
            argAddr = addressSet.ArgAddress;
        }

        public MailboxListItem(string label, Mailbox mailbox)
        {
            this.label = label;
            msgAddr = mailbox.SMU_ADDR_MSG;
            rspAddr = mailbox.SMU_ADDR_RSP;
            argAddr = mailbox.SMU_ADDR_ARG;
        }

        public override string ToString()
        {
            return this.label;
        }
    }
}