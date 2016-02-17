using Yupi.Emulator.Game.Commands.Interfaces;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Game.Rooms;
using Yupi.Emulator.Messages;
using Yupi.Emulator.Messages.Parsers;

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
                ServerMessage serverMessage = new ServerMessage();
                serverMessage.Init(PacketLibraryManager.OutgoingRequest("WhisperMessageComposer"));
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