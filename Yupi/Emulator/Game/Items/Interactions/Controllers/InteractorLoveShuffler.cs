using Yupi.Game.GameClients.Interfaces;
using Yupi.Game.Items.Interactions.Models;
using Yupi.Game.Items.Interfaces;

namespace Yupi.Game.Items.Interactions.Controllers
{
    internal class InteractorLoveShuffler : FurniInteractorModel
    {
        public override void OnPlace(GameClient session, RoomItem item)
        {
            item.ExtraData = "-1";
            item.UpdateNeeded = true;
        }

        public override void OnRemove(GameClient session, RoomItem item)
        {
            item.ExtraData = "-1";
        }

        public override void OnTrigger(GameClient session, RoomItem item, int request, bool hasRights)
        {
            if (!hasRights)
                return;

            if (item.ExtraData == "0")
                return;

            item.ExtraData = "0";
            item.UpdateState(false, true);
            item.ReqUpdate(10, true);
        }

        public override void OnWiredTrigger(RoomItem item)
        {
            if (item.ExtraData == "0")
                return;

            item.ExtraData = "0";
            item.UpdateState(false, true);
            item.ReqUpdate(10, true);
        }
    }
}