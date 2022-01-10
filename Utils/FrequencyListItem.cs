namespace ZenStatesDebugTool
{
    public class FrequencyListItem
    {
        public double multi { get; }
        public string display { get; }

        public FrequencyListItem(double multi, string display)
        {
            this.multi = multi;
            this.display = display;
        }

        public override string ToString()
        {
            return this.display;
        }
    }
}