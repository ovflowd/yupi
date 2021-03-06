﻿// ---------------------------------------------------------------------------------
// <copyright file="IgnoreUserMessageEvent.cs" company="https://github.com/sant0ro/Yupi">
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

    public class IgnoreUserMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            string username = request.GetString();

            /*
            // TODO Really?! By username?! Who the hell thought that would be a good idea? S.u.l.a.k.e ...?
            Habbo habbo = Yupi.GetGame().GetClientManager().GetClientByUserName(username).GetHabbo();

            if (habbo == null)
                return;
            // TODO Rename mute to ignore!
            if (session.GetHabbo().MutedUsers.Contains(habbo.Id) || habbo.Rank > 4u)
                return;

            session.GetHabbo().MutedUsers.Add(habbo.Id);
            router.GetComposer<UpdateIgnoreStatusMessageComposer> ().Compose (session, UpdateIgnoreStatusMessageComposer.State.IGNORE, username);
            */
            throw new NotImplementedException();
        }

        #endregion Methods
    }
}