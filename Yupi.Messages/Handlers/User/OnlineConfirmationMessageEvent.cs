// ---------------------------------------------------------------------------------
// <copyright file="OnlineConfirmationMessageEvent.cs" company="https://github.com/sant0ro/Yupi">
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
namespace Yupi.Messages.User
{
    using System;

    using Yupi.Messages.Contracts;
    using Yupi.Util;

    public class OnlineConfirmationMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            string username = request.GetString();

            // TODO Implement
            //session.Router.GetComposer<HotelClosedMessageComposer> ().Compose (session, 8, 0, false);

            if (username != session.Info.Name)
            {
                // TODO Should we check this and disconnect on mismatch?
            }

            // TODO Welcome message
            //router.GetComposer<SuperNotificationMessageComposer>().Compose(session, T._("Welcome"), T._("This is a development build"));
            /*
            YupiWriterManager.WriteLine(request.GetString() + " joined game. With IP " + session.RemoteAddress, "Yupi.User", ConsoleColor.DarkGreen);

            if (!ServerConfigurationSettings.Data.ContainsKey("welcome.message.enabled") ||
                ServerConfigurationSettings.Data["welcome.message.enabled"] != "true")
                return;

            if (!ServerConfigurationSettings.Data.ContainsKey("welcome.message.image") ||
                string.IsNullOrEmpty(ServerConfigurationSettings.Data["welcome.message.image"]))
                session.SendNotifWithScroll(ServerExtraSettings.WelcomeMessage.Replace("%username%",
                    session.GetHabbo().Name));
            else
                session.SendNotif(ServerExtraSettings.WelcomeMessage.Replace("%username%", session.GetHabbo().Name),
                    ServerConfigurationSettings.Data.ContainsKey("welcome.message.title")
                    ? ServerConfigurationSettings.Data["welcome.message.title"]
                    : string.Empty, ServerConfigurationSettings.Data["welcome.message.image"]);
                    */
        }

        #endregion Methods
    }
}