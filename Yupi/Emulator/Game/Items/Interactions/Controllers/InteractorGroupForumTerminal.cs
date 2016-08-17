using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Game.Items.Interactions.Models;
using Yupi.Emulator.Game.Items.Interfaces;

namespace Yupi.Emulator.Game.Items.Interactions.Controllers
{
     public class InteractorGroupForumTerminal : FurniInteractorModel
    {
        public override void OnTrigger(GameClient session, RoomItem item, int request, bool hasRights)
        {
            uint.Parse(item.ExtraData);
        }
    }
}