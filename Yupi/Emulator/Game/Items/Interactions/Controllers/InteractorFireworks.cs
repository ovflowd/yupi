using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Game.Items.Interactions.Models;
using Yupi.Emulator.Game.Items.Interfaces;

namespace Yupi.Emulator.Game.Items.Interactions.Controllers
{
     public class InteractorFireworks : FurniInteractorModel
    {
        public override void OnPlace(GameClient session, RoomItem item)
        {
            item.ExtraData = "1";
        }

        public override void OnRemove(GameClient session, RoomItem item)
        {
            item.ExtraData = "1";
        }

        public override void OnTrigger(GameClient session, RoomItem item, int request, bool hasRights)
        {
            if (item.ExtraData == "" || item.ExtraData == "0")
            {
                item.ExtraData = "1";
                item.UpdateState();
                return;
            }
            if (item.ExtraData == "1")
                item.ExtraData = "2";
        }
    }
}