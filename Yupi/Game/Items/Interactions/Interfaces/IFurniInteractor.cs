using Yupi.Game.GameClients.Interfaces;
using Yupi.Game.Items.Interfaces;
using Yupi.Game.Rooms.User;

namespace Yupi.Game.Items.Interactions.Interfaces
{
    internal interface IFurniInteractor
    {
        void OnPlace(GameClient session, RoomItem item);

        void OnRemove(GameClient session, RoomItem item);

        void OnTrigger(GameClient session, RoomItem item, int request, bool hasRights);

        void OnUserWalk(GameClient session, RoomItem item, RoomUser user);

        void OnWiredTrigger(RoomItem item);
    }
}