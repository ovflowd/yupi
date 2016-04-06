using Yupi.Emulator.Game.Commands.Interfaces;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Messages;
using Yupi.Emulator.Messages.Buffers;

namespace Yupi.Emulator.Game.Commands.Controllers
{
    /// <summary>
    ///     Class HotelAlert. This class cannot be inherited.
    /// </summary>
     public sealed class EventAlert : Command
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="EventAlert" /> class.
        /// </summary>
        public EventAlert()
        {
            MinRank = 5;
            Description = "Alerts to all hotel a event.";
            Usage = ":eventha";
            MinParams = -1;
        }

        public override bool Execute(GameClient session, string[] pms)
        {
            SimpleServerMessageBuffer messageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("SuperNotificationMessageComposer"));
            messageBuffer.AppendString("events");
            messageBuffer.AppendInteger(4);
            messageBuffer.AppendString("title");
            messageBuffer.AppendString("Temos um novo Evento");
            messageBuffer.AppendString("message");
            messageBuffer.AppendString(
                "Tem um novo evento acontecendo agora mesmo!\n\nO evento está sendo feito por:    <b>" +
                session.GetHabbo().UserName +
                "</b>\n\nCorra para participar antes que o quarto seja fechado! Clique em " +
                "<i>Ir para o Evento</i>\n\nE o " +
                "evento vai ser:\n\n<b>" + string.Join(" ", pms) + "</b>\n\nEstamos esperando você lá em!");
            messageBuffer.AppendString("linkUrl");
            messageBuffer.AppendString("event:navigator/goto/" + session.GetHabbo().CurrentRoomId);
            messageBuffer.AppendString("linkTitle");
            messageBuffer.AppendString("Ir para o Evento");

            /*foreach (var client in Yupi.GetGame().GetClientManager().Clients.Values)
            {
                if (client == null)
                    continue;
 
                if (session.GetHabbo().Id == client.GetHabbo().Id)
                {
                    client.SendWhisper("O Alerta de Evento foi Enviado com Sucesso", true);
                    continue;
                }
 
                if (client.GetHabbo().DisableEventAlert == false)
                    client.SendMessage(messageBuffer);
 
                //Thread.Sleep(10);
            }*/

            Yupi.GetGame().GetClientManager().QueueBroadcaseMessage(messageBuffer);
            return true;
        }
    }
}