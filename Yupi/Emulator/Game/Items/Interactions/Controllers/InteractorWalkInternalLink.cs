using System;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Game.Items.Interactions.Models;
using Yupi.Emulator.Game.Items.Interfaces;
using Yupi.Emulator.Game.Rooms.User;
using Yupi.Emulator.Messages;
using Yupi.Emulator.Messages.Buffers;

namespace Yupi.Emulator.Game.Items.Interactions.Controllers
{
     public class InteractorWalkInternalLink : FurniInteractorModel
    {
        public override void OnUserWalk(GameClient session, RoomItem item, RoomUser user)
        {
            if (item == null || user == null)
                return;

            string[] data = item.ExtraData.Split(Convert.ToChar(9));

            if (item.ExtraData == "" || data.Length < 4)
                return;

            SimpleServerMessageBuffer messageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("InternalLinkMessageComposer"));

            messageBuffer.AppendString(data[3]);
            session.SendMessage(messageBuffer);
        }
    }
}