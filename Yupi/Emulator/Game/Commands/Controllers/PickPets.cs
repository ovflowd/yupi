using System.Linq;
using Yupi.Emulator.Game.Commands.Interfaces;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Game.Pets;
using Yupi.Emulator.Game.Rooms;

namespace Yupi.Emulator.Game.Commands.Controllers
{
    /// <summary>
    ///     Class PickPets. This class cannot be inherited.
    /// </summary>
     public sealed class PickPets : Command
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

			session.Router.GetComposer<SerializePetInventory>().Compose(session, session.GetHabbo().GetInventoryComponent()._inventoryPets);
            return true;
        }
    }
}