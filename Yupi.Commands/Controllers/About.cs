// ---------------------------------------------------------------------------------
// <copyright file="About.cs" company="https://github.com/sant0ro/Yupi">
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
using System.Text;
using Yupi.Emulator.Game.Commands.Interfaces;
using Yupi.Emulator.Game.GameClients.Interfaces;



namespace Yupi.Emulator.Game.Commands.Controllers
{
    /// <summary>
    ///     Class About. This class cannot be inherited.
    /// </summary>
     public sealed class About : Command
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
            SimpleServerMessageBuffer messageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("SuperNotificationMessageComposer"));

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