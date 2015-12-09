using Yupi.Game.Commands.Interfaces;
using Yupi.Game.GameClients.Interfaces;
using Yupi.Messages;
using Yupi.Messages.Parsers;

namespace Yupi.Game.Commands.Controllers
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
            var room = session.GetHabbo().CurrentRoom;
            var message = string.Join(" ", pms);
            foreach (var client in Yupi.GetGame().GetClientManager().Clients.Values)
            {
                var serverMessage = new ServerMessage();
                serverMessage.Init(LibraryParser.OutgoingRequest("WhisperMessageComposer"));
                serverMessage.AppendInteger(room.RoomId);
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