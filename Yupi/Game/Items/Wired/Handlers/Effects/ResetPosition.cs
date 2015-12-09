using System.Collections.Generic;
using Yupi.Game.Items.Interactions.Enums;
using Yupi.Game.Items.Interfaces;
using Yupi.Game.Items.Wired.Interfaces;
using Yupi.Game.Rooms;
using Yupi.Messages;
using Yupi.Messages.Parsers;

namespace Yupi.Game.Items.Wired.Handlers.Effects
{
    public class ResetPosition : IWiredItem
    {
        private readonly List<Interaction> _mBanned;

        public ResetPosition(RoomItem item, Room room)
        {
            Item = item;
            Room = room;
            OtherString = string.Empty;
            OtherExtraString = string.Empty;
            OtherExtraString2 = string.Empty;
            Delay = 0;
            Items = new List<RoomItem>();
            _mBanned = new List<Interaction>();
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

            var booleans = OtherString.Split(',');

            if (booleans.Length < 3)
                return false;

            var extraData = booleans[0] == "true";
            var rot = booleans[1] == "true";
            var position = booleans[2] == "true";

            foreach (var itemData in OtherExtraString.Split('/'))
            {
                if (string.IsNullOrWhiteSpace(itemData))
                    continue;

                var innerData = itemData.Split('|');
                var itemId = uint.Parse(innerData[0]);

                var fItem = Room.GetRoomItemHandler().GetItem(itemId);

                if (fItem == null)
                    continue;

                var extraDataToSet = (extraData) ? innerData[1] : fItem.ExtraData;
                var rotationToSet = (rot) ? int.Parse(innerData[2]) : fItem.Rot;

                var positions = innerData[3].Split(',');

                var xToSet = (position) ? int.Parse(positions[0]) : fItem.X;
                var yToSet = (position) ? int.Parse(positions[1]) : fItem.Y;
                var zToSet = (position) ? double.Parse(positions[2]) : fItem.Z;


                var serverMessage = new ServerMessage(LibraryParser.OutgoingRequest("ItemAnimationMessageComposer"));
                serverMessage.AppendInteger(fItem.X);
                serverMessage.AppendInteger(fItem.Y);
                serverMessage.AppendInteger(xToSet);
                serverMessage.AppendInteger(yToSet);
                serverMessage.AppendInteger(1);
                serverMessage.AppendInteger(fItem.Id);
                serverMessage.AppendString(fItem.Z.ToString(Yupi.CultureInfo));
                serverMessage.AppendString(zToSet.ToString(Yupi.CultureInfo));
                serverMessage.AppendInteger(0);
                Room.SendMessage(serverMessage);

                Room.GetRoomItemHandler().SetFloorItem(null, fItem, xToSet, yToSet, rotationToSet, false, false, false, false, false);
                fItem.ExtraData = extraDataToSet;
                fItem.UpdateState();

                Room.GetGameMap().GenerateMaps();
            }

            return true;
        }
    }
}