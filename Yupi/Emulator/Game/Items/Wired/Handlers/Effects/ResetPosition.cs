using System.Collections.Generic;
using Yupi.Emulator.Game.Items.Interactions.Enums;
using Yupi.Emulator.Game.Items.Interfaces;
using Yupi.Emulator.Game.Items.Wired.Interfaces;
using Yupi.Emulator.Game.Rooms;



namespace Yupi.Emulator.Game.Items.Wired.Handlers.Effects
{
    public class ResetPosition : IWiredItem
    {
        public ResetPosition(RoomItem item, Room room)
        {
            Item = item;
            Room = room;
            OtherString = string.Empty;
            OtherExtraString = string.Empty;
            OtherExtraString2 = string.Empty;
            Delay = 0;
            Items = new List<RoomItem>();
        }

        public Interaction Type => Interaction.ActionPosReset;

        public RoomItem Item { get; set; }

        public Room Room { get; set; }

        public List<RoomItem> Items { get; set; }

        public int Delay { get; set; }

        public string OtherString { get; set; }

        public string OtherExtraString { get; set; }

        public string OtherExtraString2 { get; set; }

        public bool OtherBool { get; set; }

        public bool Execute(params object[] stuff)
        {
            if (Room == null)
                return false;

            if (string.IsNullOrWhiteSpace(OtherString) || string.IsNullOrWhiteSpace(OtherExtraString))
                return false;

            string[] booleans = OtherString.Split(',');

            if (booleans.Length < 3)
                return false;

            bool extraData = booleans[0] == "true";
            bool rot = booleans[1] == "true";
            bool position = booleans[2] == "true";

            foreach (string itemData in OtherExtraString.Split('/'))
            {
                if (string.IsNullOrWhiteSpace(itemData))
                    continue;

                string[] innerData = itemData.Split('|');
                uint itemId = uint.Parse(innerData[0]);

                RoomItem fItem = Room.GetRoomItemHandler().GetItem(itemId);

                if (fItem == null)
                    continue;

                string extraDataToSet = extraData ? innerData[1] : fItem.ExtraData;
                int rotationToSet = rot ? int.Parse(innerData[2]) : fItem.Rot;

                string[] positions = innerData[3].Split(',');

                int xToSet = position ? int.Parse(positions[0]) : fItem.X;
                int yToSet = position ? int.Parse(positions[1]) : fItem.Y;
                double zToSet = position ? double.Parse(positions[2]) : fItem.Z;


                SimpleServerMessageBuffer simpleServerMessageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("ItemAnimationMessageComposer"));
                simpleServerMessageBuffer.AppendInteger(fItem.X);
                simpleServerMessageBuffer.AppendInteger(fItem.Y);
                simpleServerMessageBuffer.AppendInteger(xToSet);
                simpleServerMessageBuffer.AppendInteger(yToSet);
                simpleServerMessageBuffer.AppendInteger(1);
                simpleServerMessageBuffer.AppendInteger(fItem.Id);
                simpleServerMessageBuffer.AppendString(fItem.Z.ToString(Yupi.CultureInfo));
                simpleServerMessageBuffer.AppendString(zToSet.ToString(Yupi.CultureInfo));
                simpleServerMessageBuffer.AppendInteger(0);
                Room.SendMessage(simpleServerMessageBuffer);

                Room.GetRoomItemHandler()
                    .SetFloorItem(null, fItem, xToSet, yToSet, rotationToSet, false, false, false, false, false);
                fItem.ExtraData = extraDataToSet;
                fItem.UpdateState();

                Room.GetGameMap().GenerateMaps();
            }

            return true;
        }
    }
}
