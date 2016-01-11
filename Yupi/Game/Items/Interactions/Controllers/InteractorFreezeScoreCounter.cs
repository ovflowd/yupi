using Yupi.Game.GameClients.Interfaces;
using Yupi.Game.Items.Interactions.Models;
using Yupi.Game.Items.Interfaces;
using Yupi.Game.Rooms.Items.Games.Teams.Enums;

namespace Yupi.Game.Items.Interactions.Controllers
{
    internal class InteractorFreezeScoreCounter : FurniInteractorModel
    {
        public override void OnPlace(GameClient session, RoomItem item)
        {
            if (item.Team == Team.None)
                return;

            item.ExtraData = item.GetRoom().GetGameManager().Points[(int) item.Team].ToString();
            item.UpdateState(false, true);
        }

        public override void OnTrigger(GameClient session, RoomItem item, int request, bool hasRights)
        {
            if (!hasRights)
                return;

            item.GetRoom().GetGameManager().Points[(int) item.Team] = 0;
            item.ExtraData = "0";
            item.UpdateState();
        }
    }
}