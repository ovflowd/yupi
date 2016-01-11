using System.Timers;
using Yupi.Game.GameClients.Interfaces;
using Yupi.Game.Items.Interactions.Models;
using Yupi.Game.Items.Interfaces;
using Yupi.Game.Rooms.User;

namespace Yupi.Game.Items.Interactions.Controllers
{
    internal class InteractorVikingCotie : FurniInteractorModel
    {
        private RoomItem _mItem;

        public override void OnTrigger(GameClient session, RoomItem item, int request, bool hasRights)
        {
            RoomUser user = item.GetRoom().GetRoomUserManager().GetRoomUserByHabbo(session.GetHabbo().Id);

            if (user == null)
                return;

            if (user.CurrentEffect != 172 && user.CurrentEffect != 5 && user.CurrentEffect != 173)
                return;

            if (item.ExtraData != "5")
            {
                if (item.VikingCotieBurning)
                    return;

                item.ExtraData = "1";
                item.UpdateState();

                item.VikingCotieBurning = true;

                GameClient clientByUsername =
                    Yupi.GetGame().GetClientManager().GetClientByUserName(item.GetRoom().RoomData.Owner);

                if (clientByUsername != null)
                {
                    if (clientByUsername.GetHabbo().UserName != item.GetRoom().RoomData.Owner)
                        clientByUsername.SendNotif(string.Format(Yupi.GetLanguage().GetVar("viking_burn_started"),
                            user.GetUserName()));
                }

                _mItem = item;

                Timer timer = new Timer(5000);
                timer.Elapsed += OnElapse;
                timer.Enabled = true;
            }
            else
                session.SendNotif(Yupi.GetLanguage().GetVar("user_viking_error"));
        }

        private void OnElapse(object sender, ElapsedEventArgs e)
        {
            if (_mItem == null)
                return;

            switch (_mItem.ExtraData)
            {
                case "1":
                    _mItem.ExtraData = "2";
                    _mItem.UpdateState();
                    return;

                case "2":
                    _mItem.ExtraData = "3";
                    _mItem.UpdateState();
                    return;

                case "3":
                    _mItem.ExtraData = "4";
                    _mItem.UpdateState();
                    return;

                case "4":
                    ((Timer) sender).Stop();
                    _mItem.ExtraData = "5";
                    _mItem.UpdateState();
                    return;
            }
        }
    }
}