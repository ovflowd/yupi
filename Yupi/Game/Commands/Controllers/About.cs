using System.Text;
using Yupi.Game.Commands.Interfaces;
using Yupi.Game.GameClients.Interfaces;
using Yupi.Messages;
using Yupi.Messages.Parsers;

namespace Yupi.Game.Commands.Controllers
{
    /// <summary>
    ///     Class About. This class cannot be inherited.
    /// </summary>
    internal sealed class About : Command
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="About" /> class.
        /// </summary>
        public About()
        {
            MinRank = 1;
            Description = "Shows information about the server.";
            Usage = ":about";
            MinParams = 0;
        }

        public override bool Execute(GameClient client, string[] pms)
        {
            ServerMessage message =
                new ServerMessage(LibraryParser.OutgoingRequest("SuperNotificationMessageComposer"));

            message.AppendString("Yupi");
            message.AppendInteger(4);
            message.AppendString("title");
            message.AppendString("Yupi");
            message.AppendString("message");
            StringBuilder info = new StringBuilder();
            info.Append("<h5><b>Yupi - Based on Azure Emulator</b><h5></br></br>");
            info.Append("<br />");
            info.AppendFormat(
                "<b><br />Developed by:</b> <br />Kessiler Rodrigues (Kessiler)<br />Claudio Santoro (sant0ro/bi0s) <br />Rafael Oliveira (iPlezier) <br /><br /> ");
            info.AppendFormat(
                "<b>Thanks to:</b> <br />Jamal, Mike Santifort, Martinmine, Rockster, The old Azure Team, Bruna F., and to all people that uses Yupi.<br /> <br /> ");
            info.AppendFormat(
                "<b>Warning:</b><br />1. This emulator was planned to be used for ManiaHotel (maniahotel.com.br)  <br />2. Please don't sell this emulator or earn money with it<br />3. All rights reserved to Sulake Corporation Oy<br />4. All Emulator rights for Mania Dev<br />");
            message.AppendString(info.ToString());
            message.AppendString("linkUrl");
            message.AppendString("event:");
            message.AppendString("linkTitle");
            message.AppendString("ok");
            client.SendMessage(message);

            return true;
        }
    }
}