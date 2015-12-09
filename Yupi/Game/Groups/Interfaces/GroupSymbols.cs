namespace Yupi.Game.Groups.Interfaces
{
    internal struct GroupSymbols
    {
        internal int Id;
        internal string Value1;
        internal string Value2;

        internal GroupSymbols(int id, string value1, string value2)
        {
            Id = id;
            Value1 = value1;
            Value2 = value2;
        }
    }
}