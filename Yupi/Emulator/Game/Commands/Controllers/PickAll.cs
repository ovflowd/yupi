using System.Collections.Generic;
using Yupi.Game.Commands.Interfaces;
using Yupi.Game.GameClients.Interfaces;
using Yupi.Game.Items.Interfaces;
using Yupi.Game.Rooms;

namespace Yupi.Game.Commands.Controllers
{
    /// <summary>
    ///     Class PickAll. This class cannot be inherited.
    /// </summary>
    internal sealed class PickAll : Command
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="PickAll" /> class.
        /// </summary>
        public PickAll()
        {
            MinRank = -2;
            Description = "Picks up all the items in your room.";
            Usage = ":pickall";
            MinParams = 0;
        }

        public override bool Execute(GameClient session, string[] pms)
        {
            Room room = session.GetHabbo().CurrentRoom;
            List<RoomItem> roomItemList = room.GetRoomItemHandler().RemoveAllFurniture(session);
            if (session.GetHabbo().GetInventoryComponent() == null)
            {
                return true;
            }
            // session.GetHabbo().GetInventoryComponent().AddItemArray(roomItemList);
            //session.GetHabbo().GetInventoryComponent().UpdateItems(false);
            room.GetRoomItemHandler().RemoveItemsByOwner(ref roomItemList, ref session);
            return true;
        }
    }
}