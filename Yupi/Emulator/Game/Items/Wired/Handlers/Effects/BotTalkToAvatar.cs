using System.Collections.Generic;
using Yupi.Emulator.Game.Items.Interactions.Enums;
using Yupi.Emulator.Game.Items.Interfaces;
using Yupi.Emulator.Game.Items.Wired.Interfaces;
using Yupi.Emulator.Game.Rooms;
using Yupi.Emulator.Game.Rooms.User;



namespace Yupi.Emulator.Game.Items.Wired.Handlers.Effects
{
    public class BotTalkToAvatar : IWiredItem
    {
        public BotTalkToAvatar(RoomItem item, Room room)
        {
            Item = item;
            Room = room;
            OtherString = string.Empty;
            OtherExtraString = string.Empty;
            OtherExtraString2 = string.Empty;
        }

        public Interaction Type => Interaction.ActionBotTalkToAvatar;

        public RoomItem Item { get; set; }

        public Room Room { get; set; }

        public List<RoomItem> Items
        {
            get { return new List<RoomItem>(); }
            set { }
        }

        public int Delay
        {
            get { return 0; }
            set { }
        }

        public string OtherString { get; set; }

        public string OtherExtraString { get; set; }

        public string OtherExtraString2 { get; set; }

        public bool OtherBool { get; set; }

        public bool Execute(params object[] stuff)
        {
            RoomUser roomUser = (RoomUser) stuff[0];
            RoomUser bot = Room.GetRoomUserManager().GetBotByName(OtherString);

            if (bot == null)
                return false;

            if (OtherBool)
            {
                SimpleServerMessageBuffer whisp = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("WhisperMessageComposer"));
                whisp.AppendInteger(bot.VirtualId);
                whisp.AppendString(OtherExtraString);
                whisp.AppendInteger(0);
                whisp.AppendInteger(2);
                whisp.AppendInteger(0);
                whisp.AppendInteger(-1);
                roomUser.GetClient().SendMessage(whisp);
            }
            else
                bot.Chat(null, roomUser.GetUserName() + " : " + OtherExtraString, false, 0);

            return true;
        }
    }
}