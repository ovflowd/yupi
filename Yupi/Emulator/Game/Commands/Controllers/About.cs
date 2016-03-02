using System.Text;
using Yupi.Emulator.Game.Commands.Interfaces;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Messages;
using Yupi.Emulator.Messages.Buffers;

namespace Yupi.Emulator.Game.Commands.Controllers
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
            SimpleServerMessageBuffer messageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.SendRequest("SuperNotificationMessageComposer"));

            messageBuffer.AppendString("Yupi");
            messageBuffer.AppendInteger(4);
            messageBuffer.AppendString("title");
            messageBuffer.AppendString("Yupi");
            messageBuffer.AppendString("message");

            StringBuilder info = new StringBuilder();

            info.Append("<h5><b>Yupi - Based on Azure Emulator</b><h5></br></br>");
            info.Append("<br />");
            info.AppendFormat("<b><br />Developed by:</b> <br />Kessiler Rodrigues (Kessiler)<br />Claudio Santoro (sant0ro/bi0s) <br />Rafael Oliveira (iPlezier) <br /><br /> ");
            info.AppendFormat("<b>Thanks to:</b> <br />Jamal, Mike Santifort, Martinmine, Rockster, The old Azure Team, Bruna F., and to all people that uses Yupi.<br /> <br /> ");

            messageBuffer.AppendString(info.ToString());
            messageBuffer.AppendString("linkUrl");
            messageBuffer.AppendString("event:");
            messageBuffer.AppendString("linkTitle");
            messageBuffer.AppendString("ok");

            client.SendMessage(messageBuffer);

            return true;
        }
    }
}