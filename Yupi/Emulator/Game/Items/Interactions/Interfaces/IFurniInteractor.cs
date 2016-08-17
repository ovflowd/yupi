using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Game.Items.Interfaces;
using Yupi.Emulator.Game.Rooms.User;

namespace Yupi.Emulator.Game.Items.Interactions.Interfaces
{
     public interface IFurniInteractor
    {
        void OnPlace(GameClient session, RoomItem item);

        void OnRemove(GameClient session, RoomItem item);

        void OnTrigger(GameClient session, RoomItem item, int request, bool hasRights);

        void OnUserWalk(GameClient session, RoomItem item, RoomUser user);

        void OnWiredTrigger(RoomItem item);
    }
}