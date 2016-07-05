using System;
using System.Collections.Generic;
using Yupi.Emulator.Game.Items.Interactions.Enums;
using Yupi.Emulator.Game.Items.Interfaces;
using Yupi.Emulator.Game.Items.Wired.Interfaces;
using Yupi.Emulator.Game.Rooms;
using Yupi.Emulator.Game.Rooms.User;



namespace Yupi.Emulator.Game.Items.Wired.Handlers.Effects
{
    public class GiveReward : IWiredItem
    {
        private readonly List<Interaction> _mBanned;

        public GiveReward(RoomItem item, Room room)
        {
            Item = item;
            Room = room;
            OtherString = string.Empty;
            OtherExtraString = string.Empty;
            OtherExtraString2 = string.Empty;
            _mBanned = new List<Interaction>();
        }

        public Interaction Type => Interaction.ActionGiveReward;

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
            if (stuff[0] == null)
                return false;

            RoomUser user = (RoomUser) stuff[0];

            if (stuff[1] == null)
                return false;

            Interaction item = (Interaction) stuff[1];

            if (_mBanned.Contains(item))
                return false;

            if (user == null)
                return false;

            if (OtherExtraString2 == null)
                return false;

            int amountLeft = Convert.ToInt32(OtherExtraString2);

            bool unique = OtherBool;

            bool premied = false;

            if (amountLeft == 1)
            {
				user.GetClient().Router.GetComposer<WiredRewardAlertMessageComposer> ().Compose (user.GetClient(), 0);
                return true;
            }

            foreach (string dataStr in OtherString.Split(';'))
            {
                string[] dataArray = dataStr.Split(',');

                bool isbadge = dataArray[0] == "0";
                string code = dataArray[1];
                int percentage = int.Parse(dataArray[2]);

                int random = Yupi.GetRandomNumber(0, 100);
				            
                if (!unique && percentage < random)
                    continue;

                premied = true;

                if (isbadge)
                {
                    if (user.GetClient().GetHabbo().GetBadgeComponent().HasBadge(code))
                    {
						user.GetClient().Router.GetComposer<WiredRewardAlertMessageComposer> ().Compose (user.GetClient(), 1);    
                    }
                    else
                    {
                        user.GetClient()
                            .GetHabbo()
                            .GetBadgeComponent()
                            .GiveBadge(code, true, user.GetClient(), true);
						user.GetClient().Router.GetComposer<WiredRewardAlertMessageComposer> ().Compose (user.GetClient(), 7);     
                    }
                }
                else //item or effect
                {
                    Item roomItem = Yupi.GetGame().GetItemManager().GetItem(uint.Parse(code));

                    if (roomItem == null)
                        continue;

                    if (roomItem.Type == 'e') // is effect
                        user.GetClient()
                            .GetHabbo()
                            .GetAvatarEffectsInventoryComponent()
                            .AddNewEffect(roomItem.SpriteId, 3600, 1);
                    else
                    {
                        user.GetClient()
                            .GetHabbo()
                            .GetInventoryComponent()
                            .AddNewItem(0u, roomItem.Name, "0", 0u, true, false, 0, 0);

						user.GetClient().Router.GetComposer<UpdateInventoryMessageComposer> ().Compose (user.GetClient());
                    }

					user.GetClient().Router.GetComposer<WiredRewardAlertMessageComposer> ().Compose (user.GetClient(), 6);
                }
            }

            if (!premied)
            {
				user.GetClient().Router.GetComposer<WiredRewardAlertMessageComposer> ().Compose (user.GetClient(), 4);   
            }
            else if (amountLeft > 1)
            {
                amountLeft--;
                OtherExtraString2 = amountLeft.ToString();
            }

            return true;
        }
    }
}