using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Game.Items.Interactions.Interfaces;
using Yupi.Emulator.Game.Items.Interfaces;
using Yupi.Emulator.Game.Rooms.User;

namespace Yupi.Emulator.Game.Items.Interactions.Models
{
     class FurniInteractorModel : IFurniInteractor
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