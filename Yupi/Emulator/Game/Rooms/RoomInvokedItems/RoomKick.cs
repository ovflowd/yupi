namespace Yupi.Emulator.Game.Rooms.RoomInvokedItems
{
     public struct RoomKick
    {
     public string Alert;
     public int MinRank;

        public RoomKick(string alert, int minRank)
        {
            Alert = alert;
            MinRank = minRank;
        }
    }
}