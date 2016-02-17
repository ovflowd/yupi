using Yupi.Game.GameClients.Interfaces;
using Yupi.Game.Items.Interactions.Models;
using Yupi.Game.Items.Interfaces;
using Yupi.Game.Rooms.Items.Games.Teams.Enums;
using Yupi.Game.Rooms.User;

namespace Yupi.Game.Items.Interactions.Controllers
{
    internal class InteractorFreezeTile : FurniInteractorModel
    {
        public override void OnTrigger(GameClient session, RoomItem item, int request, bool hasRights)
        {
            if (session == null || session.GetHabbo() == null || item.InteractingUser > 0U)
                return;

            string pName = session.GetHabbo().UserName;
            RoomUser roomUserByHabbo = item.GetRoom().GetRoomUserManager().GetRoomUserByHabbo(pName);
            roomUserByHabbo.GoalX = item.X;
            roomUserByHabbo.GoalY = item.Y;

            if (roomUserByHabbo.Team != Team.None)
                roomUserByHabbo.ThrowBallAtGoal = true;
        }
    }
}