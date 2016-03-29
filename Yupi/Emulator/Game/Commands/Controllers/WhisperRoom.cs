using Yupi.Emulator.Game.Commands.Interfaces;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Game.Rooms;
using Yupi.Emulator.Messages;
using Yupi.Emulator.Messages.Buffers;

namespace Yupi.Emulator.Game.Commands.Controllers
{
    internal sealed class WhisperRoom : Command
    {
        public WhisperRoom()
        {
            MinRank = 6;
            Description = "Susurrar para o Quarto Todo";
            Usage = ":whisperroom [MESSAGE]";
            MinParams = -1;
        }

        public override bool Execute(GameClient session, string[] pms)
        {
            Room room = session.GetHabbo().CurrentRoom;
            string message = string.Join(" ", pms);
            foreach (GameClient client in Yupi.GetGame().GetClientManager().Clients.Values)
            {
                SimpleServerMessageBuffer simpleServerMessageBuffer = new SimpleServerMessageBuffer();
                simpleServerMessageBuffer.Init(PacketLibraryManager.OutgoingHandler("WhisperMessageComposer"));
                simpleServerMessageBuffer.AppendInteger(room.RoomId);
                simpleServerMessageBuffer.AppendString(message);
                simpleServerMessageBuffer.AppendInteger(0);
                simpleServerMessageBuffer.AppendInteger(36);
                simpleServerMessageBuffer.AppendInteger(0);
                simpleServerMessageBuffer.AppendInteger(-1);
                client.SendMessage(simpleServerMessageBuffer);
            }
            return true;
        }
    }
}