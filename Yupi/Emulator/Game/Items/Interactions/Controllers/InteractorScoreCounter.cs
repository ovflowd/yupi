using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Game.Items.Interactions.Models;
using Yupi.Emulator.Game.Items.Interfaces;
using Yupi.Emulator.Game.Rooms.Items.Games.Teams.Enums;

namespace Yupi.Emulator.Game.Items.Interactions.Controllers
{
     public class InteractorScoreCounter : FurniInteractorModel
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

            int num;

            int.TryParse(item.ExtraData, out num);

            switch (request)
            {
                case 1:
                    num++;
                    break;

                case 2:
                    num--;
                    break;

                case 3:
                    num = 0;
                    break;
            }

            item.ExtraData = num.ToString();
            item.UpdateState(false, true);
        }

        public override void OnWiredTrigger(RoomItem item)
        {
            int num;
            int.TryParse(item.ExtraData, out num);

            num++;
            item.ExtraData = num.ToString();
            item.UpdateState(false, true);
        }
    }
}