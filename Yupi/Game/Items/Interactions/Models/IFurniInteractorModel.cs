using Yupi.Game.GameClients.Interfaces;
using Yupi.Game.Items.Interactions.Interfaces;
using Yupi.Game.Items.Interfaces;
using Yupi.Game.Rooms.User;

namespace Yupi.Game.Items.Interactions.Models
{
    internal class FurniInteractorModel : IFurniInteractor
    {
        public virtual void OnPlace(GameClient session, RoomItem item)
        {
        }

        public virtual void OnRemove(GameClient session, RoomItem item)
        {
        }

        public virtual void OnTrigger(GameClient session, RoomItem item, int request, bool hasRights)
        {
        }

        public virtual void OnUserWalk(GameClient session, RoomItem item, RoomUser user)
        {
        }

        public virtual void OnWiredTrigger(RoomItem item)
        {
        }
    }
}