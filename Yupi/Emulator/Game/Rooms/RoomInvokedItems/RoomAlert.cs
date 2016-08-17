namespace Yupi.Emulator.Game.Rooms.RoomInvokedItems
{
     public class RoomAlert
    {
     public string Message;
     public int Minrank;

        public RoomAlert(string message, int minrank)
        {
            Message = message;
            Minrank = minrank;
        }
    }
}