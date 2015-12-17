using Yupi.Game.Commands.Interfaces;
using Yupi.Game.GameClients.Interfaces;
using Yupi.Messages;
using Yupi.Messages.Parsers;

namespace Yupi.Game.Commands.Controllers
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
                ServerMessage serverMessage = new ServerMessage();
                serverMessage.Init(LibraryParser.OutgoingRequest("WhisperMessageComposer"));
                serverMessage.AppendInteger(client.CurrentRoomUserId);
                serverMessage.AppendString(message);
                serverMessage.AppendInteger(0);
                serverMessage.AppendInteger(36);
                serverMessage.AppendInteger(0);
                serverMessage.AppendInteger(-1);
                client.SendMessage(serverMessage);
            }
            return true;
        }
    }
}