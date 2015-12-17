using System.Linq;
using Yupi.Game.Commands.Interfaces;
using Yupi.Game.GameClients.Interfaces;
using Yupi.Game.Pets;
using Yupi.Game.Rooms;

namespace Yupi.Game.Commands.Controllers
{
    /// <summary>
    ///     Class PickPets. This class cannot be inherited.
    /// </summary>
    internal sealed class PickPets : Command
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="PickPets" /> class.
        /// </summary>
        public PickPets()
        {
            MinRank = -1;
            Description = "Picks up all the pets in your room.";
            Usage = ":pickpets";
            MinParams = 0;
        }

        public override bool Execute(GameClient session, string[] pms)
        {
            Room room = session.GetHabbo().CurrentRoom;
            foreach (
                Pet pet in
                    room.GetRoomUserManager().GetPets().Where(pet => pet.OwnerId == session.GetHabbo().Id))
            {
                session.GetHabbo().GetInventoryComponent().AddPet(pet);
                room.GetRoomUserManager().RemoveBot(pet.VirtualId, false);
            }
            session.SendMessage(session.GetHabbo().GetInventoryComponent().SerializePetInventory());
            return true;
        }
    }
}