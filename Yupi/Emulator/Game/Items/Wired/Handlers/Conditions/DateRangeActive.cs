using System.Collections.Generic;
using Yupi.Game.Items.Interactions.Enums;
using Yupi.Game.Items.Interfaces;
using Yupi.Game.Items.Wired.Interfaces;
using Yupi.Game.Rooms;

namespace Yupi.Game.Items.Wired.Handlers.Conditions
{
    internal class DateRangeActive : IWiredItem
    {
        public DateRangeActive(RoomItem item, Room room)
        {
            Item = item;
            Room = room;
            Items = new List<RoomItem>();
            OtherString = string.Empty;
        }

        public Interaction Type => Interaction.ConditionDateRangeActive;

        public RoomItem Item { get; set; }

        public Room Room { get; set; }

        public List<RoomItem> Items { get; set; }

        public string OtherString { get; set; }

        public string OtherExtraString
        {
            get { return ""; }
            set { }
        }

        public string OtherExtraString2
        {
            get { return ""; }
            set { }
        }

        public bool OtherBool
        {
            get { return true; }
            set { }
        }

        public int Delay
        {
            get { return 0; }
            set { }
        }

        public bool Execute(params object[] stuff)
        {
            int date1;
            int date2 = 0;

            string[] strArray = OtherString.Split(',');

            if (string.IsNullOrWhiteSpace(strArray[0]))
                return false;

            int.TryParse(strArray[0], out date1);

            if (strArray.Length > 1)
                int.TryParse(strArray[1], out date2);

            if (date1 == 0)
                return false;

            int currentTimestamp = Yupi.GetUnixTimeStamp();

            return date2 < 1 ? currentTimestamp >= date1 : currentTimestamp >= date1 && currentTimestamp <= date2;
        }
    }
}