namespace Yupi.Core.Algorithms.GameField.Algorithm
{
    public class GametileUpdate
    {
        public GametileUpdate(int x, int y, byte value)
        {
            X = x;
            Y = y;
            Value = value;
        }

        public byte Value { get; private set; }

        public int Y { get; private set; }

        public int X { get; private set; }
    }
}