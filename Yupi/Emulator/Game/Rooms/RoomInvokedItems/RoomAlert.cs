namespace Yupi.Emulator.Game.Rooms.RoomInvokedItems
{
    internal class RoomAlert
    {
        internal string Message;
        internal int Minrank;

        public RoomAlert(string message, int minrank)
        {
            Message = message;
            Minrank = minrank;
        }
    }
}