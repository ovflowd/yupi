using System;
using Yupi.Emulator.Game.Rooms.Events.Models;
using Yupi.Emulator.Messages;
using Yupi.Emulator.Messages.Buffers;

namespace Yupi.Emulator.Game.Rooms.Events.Composers
{
    class RoomEventComposer
    {
         static SimpleServerMessageBuffer Compose(RoomEvent roomEvent, Room room)
        {
            SimpleServerMessageBuffer simpleServerMessageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("RoomEventMessageComposer"));

            simpleServerMessageBuffer.AppendInteger(room.RoomData.Id);
            simpleServerMessageBuffer.AppendInteger(room.RoomData.OwnerId);
            simpleServerMessageBuffer.AppendString(room.RoomData.Owner);
            simpleServerMessageBuffer.AppendInteger(1);
            simpleServerMessageBuffer.AppendInteger(1);
            simpleServerMessageBuffer.AppendString(roomEvent.Name);
            simpleServerMessageBuffer.AppendString(roomEvent.Description);
            simpleServerMessageBuffer.AppendInteger(0);
            simpleServerMessageBuffer.AppendInteger((int)Math.Floor((roomEvent.Time - Yupi.GetUnixTimeStamp()) / 60.0));

            simpleServerMessageBuffer.AppendInteger(roomEvent.Category);

            return simpleServerMessageBuffer;
        }
    }
}
