namespace ZenStatesDebugTool
{
    public class WmiCmdListItem
    {
        public uint value { get; }
        public string text { get; }

        public bool isSet { get; }

        public WmiCmdListItem(string text, uint value, bool isSet = false)
        {
            this.text = text;
            this.value = value;
            this.isSet = isSet;
        }

        public override string ToString()
        {
            return this.text;
        }
    }
}