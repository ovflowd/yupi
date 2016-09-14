// ---------------------------------------------------------------------------------
// <copyright file="LoadVolumeMessageComposer.cs" company="https://github.com/sant0ro/Yupi">
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

    using Yupi.Model.Domain.Components;
    using Yupi.Protocol.Buffers;

    public class LoadVolumeMessageComposer : Yupi.Messages.Contracts.LoadVolumeMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, UserPreferences preferences)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(preferences.Volume1);
                message.AppendInteger(preferences.Volume2);
                message.AppendInteger(preferences.Volume3);
                message.AppendBool(preferences.PreferOldChat);
                message.AppendBool(preferences.IgnoreRoomInvite);
                message.AppendBool(preferences.DisableCameraFollow);
                // TODO Add to preferences
                message.AppendInteger(3); // collapse friends (3 = no)
                message.AppendInteger(preferences.ChatBubbleStyle.Value); //bubble

                session.Send(message);
            }
        }

        #endregion Methods
    }
}