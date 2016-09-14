// ---------------------------------------------------------------------------------
// <copyright file="EventAlert.cs" company="https://github.com/sant0ro/Yupi">
//   Copyright (c) 2016 Claudio Santoro, TheDoctor
// </copyright>
// <license>
//   Permission is hereby granted, free of charge, to any person obtaining a copy
//   of this software and associated documentation files (the "Software"), to deal
//   in the Software without restriction, including without limitation the rights
//   to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//   copies of the Software, and to permit persons to whom the Software is
//   furnished to do so, subject to the following conditions:
//
//   The above copyright notice and this permission notice shall be included in
//   all copies or substantial portions of the Software.
//
//   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//   IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//   FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//   AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//   LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//   OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//   THE SOFTWARE.
// </license>
// ---------------------------------------------------------------------------------
/* copyright */
using Yupi.Emulator.Game.Commands.Interfaces;
using Yupi.Emulator.Game.GameClients.Interfaces;



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