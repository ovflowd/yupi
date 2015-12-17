using Yupi.Game.GameClients.Interfaces;
using Yupi.Game.Items.Interactions.Enums;
using Yupi.Game.Items.Interactions.Models;
using Yupi.Game.Items.Interfaces;

namespace Yupi.Game.Items.Interactions.Controllers
{
    internal class InteractorGate : FurniInteractorModel
    {
        public override void OnTrigger(GameClient session, RoomItem item, int request, bool hasRights)
        {
            if (!hasRights)
                return;

            if (item?.GetBaseItem() == null || item.GetBaseItem().InteractionType != Interaction.Gate)
                return;

            uint modes = item.GetBaseItem().Modes - 1;

            if (modes <= 0)
                item.UpdateState(false, true);

            if (item.GetRoom() == null || item.GetRoom().GetGameMap() == null ||
                item.GetRoom().GetGameMap().SquareHasUsers(item.X, item.Y))
                return;

            int currentMode;
            int.TryParse(item.ExtraData, out currentMode);
            int newMode;

            if (currentMode <= 0)
                newMode = 1;
            else if (currentMode >= modes)
                newMode = 0;
            else
                newMode = currentMode + 1;

            if (newMode == 0 && !item.GetRoom().GetGameMap().ItemCanBePlacedHere(item.X, item.Y))
                return;

            item.ExtraData = newMode.ToString();
            item.UpdateState();
            item.GetRoom().GetGameMap().UpdateMapForItem(item);
            item.GetRoom()
                .GetWiredHandler()
                .ExecuteWired(Interaction.TriggerStateChanged,
                    item.GetRoom().GetRoomUserManager().GetRoomUserByHabbo(session.GetHabbo().Id), item);
        }

        public override void OnWiredTrigger(RoomItem item)
        {
            uint num = item.GetBaseItem().Modes - 1;

            if (num <= 0)
                item.UpdateState(false, true);

            if (item.GetRoom() == null || item.GetRoom().GetGameMap() == null ||
                item.GetRoom().GetGameMap().SquareHasUsers(item.X, item.Y))
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

            if (num3 == 0 && !item.GetRoom().GetGameMap().ItemCanBePlacedHere(item.X, item.Y))
                return;

            item.ExtraData = num3.ToString();
            item.UpdateState();
            item.GetRoom().GetGameMap().UpdateMapForItem(item);
        }
    }
}