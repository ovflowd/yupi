using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Game.Items.Interactions.Enums;
using Yupi.Emulator.Game.Items.Interactions.Models;
using Yupi.Emulator.Game.Items.Interfaces;
using Yupi.Emulator.Game.Users;
using Yupi.Emulator.Messages;
using Yupi.Emulator.Messages.Buffers;

namespace Yupi.Emulator.Game.Items.Interactions.Controllers
{
     class InteractorHcGate : FurniInteractorModel
    {
        public override void OnTrigger(GameClient session, RoomItem item, int request, bool hasRights)
        {
            Habbo user = session.GetHabbo();
            bool ishc = user.Vip;

            if (!ishc)
            {
                SimpleServerMessageBuffer messageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("CustomUserNotificationMessageComposer"));
                messageBuffer.AppendInteger(3);
                session.SendMessage(messageBuffer);
                return;
            }

            if (item?.GetBaseItem() == null || item.GetBaseItem().InteractionType != Interaction.HcGate)
                return;

            uint modes = item.GetBaseItem().Modes - 1;

            if (modes <= 0)
                item.UpdateState(false, true);

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