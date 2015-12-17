using System.Linq;
using Yupi.Game.GameClients.Interfaces;
using Yupi.Game.Items.Interactions.Enums;
using Yupi.Game.Items.Interactions.Models;
using Yupi.Game.Items.Interfaces;
using Yupi.Game.Rooms;
using Yupi.Game.Rooms.User;

namespace Yupi.Game.Items.Interactions.Controllers
{
    internal class InteractorGenericSwitch : FurniInteractorModel
    {
        public override void OnTrigger(GameClient session, RoomItem item, int request, bool hasRights)
        {
            uint num = item.GetBaseItem().Modes - 1;

            if (session == null || !hasRights || num <= 0 || item.GetBaseItem().InteractionType == Interaction.Pinata)
                return;

            int num2;
            int.TryParse(item.ExtraData, out num2);
            int num3;

            if (num2 <= 0)
                num3 = 1;
            else
            {
                if (num2 >= num)
                    num3 = 0;
                else
                    num3 = num2 + 1;
            }

            item.ExtraData = num3.ToString();
            item.UpdateState();
            item.GetRoom()
                .GetWiredHandler()
                .ExecuteWired(Interaction.TriggerStateChanged,
                    item.GetRoom().GetRoomUserManager().GetRoomUserByHabbo(session.GetHabbo().Id), item);

            if (!item.GetBaseItem().StackMultipler)
                return;

            Room room = item.GetRoom();

            foreach (
                RoomUser current in
                    room.GetRoomUserManager().UserList.Values.Where(current => current.Statusses.ContainsKey("sit")))
                room.GetRoomUserManager().UpdateUserStatus(current, true);
        }

        public override void OnWiredTrigger(RoomItem item)
        {
            uint num = item.GetBaseItem().Modes - 1;

            if (num == 0)
                return;

            int num2;

            if (!int.TryParse(item.ExtraData, out num2))
                return;

            int num3;

            if (num2 <= 0)
                num3 = 1;
            else
            {
                if (num2 >= num)
                    num3 = 0;
                else
                    num3 = num2 + 1;
            }

            item.ExtraData = num3.ToString();
            item.UpdateState();

            if (!item.GetBaseItem().StackMultipler)
                return;

            Room room = item.GetRoom();

            foreach (
                RoomUser current in
                    room.GetRoomUserManager().UserList.Values.Where(current => current.Statusses.ContainsKey("sit")))
                room.GetRoomUserManager().UpdateUserStatus(current, true);
        }
    }
}