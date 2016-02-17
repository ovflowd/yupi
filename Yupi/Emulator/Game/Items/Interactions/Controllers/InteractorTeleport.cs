using Yupi.Game.GameClients.Interfaces;
using Yupi.Game.Items.Interactions.Models;
using Yupi.Game.Items.Interfaces;
using Yupi.Game.Rooms.User;

namespace Yupi.Game.Items.Interactions.Controllers
{
    internal class InteractorTeleport : FurniInteractorModel
    {
        public override void OnPlace(GameClient session, RoomItem item)
        {
            item.ExtraData = "0";

            if (item.InteractingUser != 0)
            {
                RoomUser user = item.GetRoom().GetRoomUserManager().GetRoomUserByHabbo(item.InteractingUser);

                if (user != null)
                {
                    user.ClearMovement();
                    user.AllowOverride = false;
                    user.CanWalk = true;
                }

                item.InteractingUser = 0;
            }

            if (item.InteractingUser2 != 0)
            {
                RoomUser user = item.GetRoom().GetRoomUserManager().GetRoomUserByHabbo(item.InteractingUser2);

                if (user != null)
                {
                    user.ClearMovement();
                    user.AllowOverride = false;
                    user.CanWalk = true;
                }

                item.InteractingUser2 = 0;
            }
        }

        public override void OnRemove(GameClient session, RoomItem item)
        {
            item.ExtraData = "0";

            if (item.InteractingUser != 0)
            {
                RoomUser user = item.GetRoom().GetRoomUserManager().GetRoomUserByHabbo(item.InteractingUser);

                user?.UnlockWalking();

                item.InteractingUser = 0;
            }

            if (item.InteractingUser2 != 0)
            {
                RoomUser user = item.GetRoom().GetRoomUserManager().GetRoomUserByHabbo(item.InteractingUser2);

                user?.UnlockWalking();

                item.InteractingUser2 = 0;
            }
        }

        public override void OnTrigger(GameClient session, RoomItem item, int request, bool hasRights)
        {
            if (item?.GetRoom() == null || session?.GetHabbo() == null)
                return;

            RoomUser user = item.GetRoom().GetRoomUserManager().GetRoomUserByHabbo(session.GetHabbo().Id);

            if (user != null)
            {
                if (user.Coordinate == item.Coordinate || user.Coordinate == item.SquareInFront)
                {
                    if (item.InteractingUser != 0)
                        return;

                    item.InteractingUser = user.GetClient().GetHabbo().Id;
                }
                else if (user.CanWalk)
                    user.MoveTo(item.SquareInFront);
            }
        }
    }
}