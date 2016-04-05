namespace Yupi.Emulator.Game.Rooms.RoomInvokedItems
{
     struct RoomKick
    {
         string Alert;
         int MinRank;

        public RoomKick(string alert, int minRank)
        {
            Alert = alert;
            MinRank = minRank;
        }
    }
}