using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Game.Items.Interactions.Models;
using Yupi.Emulator.Game.Items.Interfaces;

namespace Yupi.Emulator.Game.Items.Interactions.Controllers
{
     class InteractorScoreboard : FurniInteractorModel
    {
        public override void OnTrigger(GameClient session, RoomItem item, int request, bool hasRights)
        {
            if (!hasRights)
                return;

            int num;
            int.TryParse(item.ExtraData, out num);

            switch (request)
            {
                case 1:
                    if (item.PendingReset && num > 0)
                    {
                        num = 0;
                        item.PendingReset = false;
                    }
                    else
                    {
                        num += 60;
                        item.UpdateNeeded = false;
                    }
                    break;

                case 2:
                    item.UpdateNeeded = !item.UpdateNeeded;
                    item.PendingReset = true;
                    break;
            }

            item.ExtraData = num.ToString();
            item.UpdateState();
        }

        public override void OnWiredTrigger(RoomItem item)
        {
            int num;
            int.TryParse(item.ExtraData, out num);

            num += 60;

            item.UpdateNeeded = false;
            item.ExtraData = num.ToString();
            item.UpdateState();
        }
    }
}