namespace Yupi.Emulator.Game.Rooms.RoomInvokedItems
{
     class RoomAlert
    {
         string Message;
         int Minrank;

        public RoomAlert(string message, int minrank)
        {
            Message = message;
            Minrank = minrank;
        }
    }
}