using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Game.Items.Interactions.Models;
using Yupi.Emulator.Game.Items.Interfaces;
using Yupi.Emulator.Game.Rooms.Items.Games.Teams.Enums;

namespace Yupi.Emulator.Game.Items.Interactions.Controllers
{
     class InteractorBanzaiScoreCounter : FurniInteractorModel
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