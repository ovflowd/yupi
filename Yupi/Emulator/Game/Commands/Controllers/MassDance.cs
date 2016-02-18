using System.Collections.Generic;
using Yupi.Emulator.Game.Commands.Interfaces;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Game.Rooms;
using Yupi.Emulator.Game.Rooms.User;
using Yupi.Emulator.Messages;
using Yupi.Emulator.Messages.Buffers;
using Yupi.Emulator.Messages.Parsers;

namespace Yupi.Emulator.Game.Commands.Controllers
{
    /// <summary>
    ///     Class MassDance. This class cannot be inherited.
    /// </summary>
    internal sealed class MassDance : Command
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="MassDance" /> class.
        /// </summary>
        public MassDance()
        {
            MinRank = 7;
            Description = "Enable dance Id for the whole room.";
            Usage = ":massdance [danceId(0 - 4)]";
            MinParams = 1;
        }

        public override bool Execute(GameClient session, string[] pms)
        {
            ushort danceId;
            ushort.TryParse(pms[0], out danceId);

            if (danceId > 4)
            {
                session.SendWhisper(Yupi.GetLanguage().GetVar("command_dance_wrong_syntax"));
                return true;
            }
            Room room = Yupi.GetGame().GetRoomManager().GetRoom(session.GetHabbo().CurrentRoomId);
            HashSet<RoomUser> roomUsers = room.GetRoomUserManager().GetRoomUsers();

            foreach (RoomUser roomUser in roomUsers)
            {
                SimpleServerMessageBuffer messageBuffer =
                    new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingRequest("DanceStatusMessageComposer"));
                messageBuffer.AppendInteger(roomUser.VirtualId);
                messageBuffer.AppendInteger(danceId);
                room.SendMessage(messageBuffer);
                roomUser.DanceId = danceId;
            }
            return true;
        }
    }
}