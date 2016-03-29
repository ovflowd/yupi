using Yupi.Emulator.Game.Commands.Interfaces;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Messages;
using Yupi.Emulator.Messages.Buffers;

namespace Yupi.Emulator.Game.Commands.Controllers
{
    internal sealed class WhisperHotel : Command
    {
        public WhisperHotel()
        {
            MinRank = 7;
            Description = "Susurrar a Todo el Hotel";
            Usage = ":whisperhotel [MESSAGE]";
            MinParams = -1;
        }

        public override bool Execute(GameClient session, string[] pms)
        {
            string message = string.Join(" ", pms);
            if (string.IsNullOrEmpty(message)) return true;
            foreach (GameClient client in Yupi.GetGame().GetClientManager().Clients.Values)
            {
                SimpleServerMessageBuffer simpleServerMessageBuffer = new SimpleServerMessageBuffer();
                simpleServerMessageBuffer.Init(PacketLibraryManager.OutgoingHandler("WhisperMessageComposer"));
                simpleServerMessageBuffer.AppendInteger(client.CurrentRoomUserId);
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