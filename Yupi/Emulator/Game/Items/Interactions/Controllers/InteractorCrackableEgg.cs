using System;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Game.Items.Interactions.Models;
using Yupi.Emulator.Game.Items.Interfaces;
using Yupi.Emulator.Game.Rooms.User;
using Yupi.Emulator.Game.Rooms.User.Path;

namespace Yupi.Emulator.Game.Items.Interactions.Controllers
{
     class InteractorCrackableEgg : FurniInteractorModel
    {
        public override void OnTrigger(GameClient session, RoomItem item, int request, bool hasRights)
        {
            RoomUser roomUser = null;
            if (session != null)
                roomUser = item.GetRoom().GetRoomUserManager().GetRoomUserByHabbo(session.GetHabbo().Id);

            if (roomUser == null)
                return;

            if (Gamemap.TilesTouching(item.X, item.Y, roomUser.X, roomUser.Y))
            {
                int cracks = 0;

                if (Yupi.IsNum(item.ExtraData))
                    cracks = Convert.ToInt16(item.ExtraData);

                cracks++;
                item.ExtraData = Convert.ToString(cracks);
                item.UpdateState(false, true);
                return;
            }

            roomUser.MoveTo(item.SquareInFront);
        }
    }
}