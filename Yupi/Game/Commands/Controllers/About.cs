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
            var message =
                new ServerMessage(LibraryParser.OutgoingRequest("SuperNotificationMessageComposer"));

            message.AppendString("Yupi");
            message.AppendInteger(4);
            message.AppendString("title");
            message.AppendString("Yupi");
            message.AppendString("message");
            var info = new StringBuilder();
            info.Append("<h5><b>Yupi Alpha - Based on Azure Emulator</b><h5></br></br>");
            info.Append("<br />");
            info.AppendFormat(
                "<b><br />Developed by:</b> <br />Claudio Santoro (sant0ro/bi0s) <br />Kessiler Rodrigues (Kessiler)<br />Rafael Oliveira (iPlezier) <br /><br /> ");
            info.AppendFormat(
                "<b>Thanks to:</b> <br />Jamal, and the old Azure Team, Lucca Fierri (Droppy), Bruna Freitas, and to all people that uses Yupi.<br /> <br /> ");
            info.AppendFormat("<b>Estatisticas:</b> <br />");
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