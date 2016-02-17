using Yupi.Game.GameClients.Interfaces;
using Yupi.Game.Items.Interactions.Models;
using Yupi.Game.Items.Interfaces;

namespace Yupi.Game.Items.Interactions.Controllers
{
    internal class InteractorRoller : FurniInteractorModel
    {
        public override void OnTrigger(GameClient session, RoomItem item, int request, bool hasRights)
        {
            item.GetRoom().GetRoomItemHandler().GotRollers = true;
        }
    }
}