using System;
using System.Collections.Generic;
using Yupi.Game.Items.Interactions.Enums;
using Yupi.Game.Items.Interfaces;
using Yupi.Game.Items.Wired.Interfaces;
using Yupi.Game.Rooms;
using Yupi.Game.Rooms.User;

namespace Yupi.Game.Items.Wired.Handlers.Effects
{
    public class BotMove : IWiredItem
    {
        public BotMove(RoomItem item, Room room)
        {
            Item = item;
            Items = new List<RoomItem>();
            Room = room;
            OtherString = string.Empty;
            OtherExtraString = string.Empty;
            OtherExtraString2 = string.Empty;
        }

        public Interaction Type => Interaction.ActionBotMove;

        public RoomItem Item { get; set; }

        public Room Room { get; set; }

        public List<RoomItem> Items { get; set; }

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
            RoomUser bot = Room.GetRoomUserManager().GetBotByName(OtherString);

            if (bot == null)
                return false;

            Random rnd = new Random();
            RoomItem goal = Items[rnd.Next(Items.Count)];
            bot.MoveTo(goal.X, goal.Y);

            return true;
        }
    }
}