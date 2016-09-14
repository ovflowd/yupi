// ---------------------------------------------------------------------------------
// <copyright file="SuperNotificationMessageComposer.cs" company="https://github.com/sant0ro/Yupi">
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
namespace Yupi.Messages.Notification
{
    using System;

    using Yupi.Protocol.Buffers;

    public class SuperNotificationMessageComposer : Yupi.Messages.Contracts.SuperNotificationMessageComposer
    {
        #region Methods

        // TODO Title default: ${generic.alert.title}
        // TODO might be that url default is "event:"
        // unknown might be icon id!
        public override void Compose(Yupi.Protocol.ISender session, string title, string content, string url = "",
            string urlName = "", string unknown = "", int unknown2 = 4)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendString(unknown);
                message.AppendInteger(unknown2);
                // TODO Refactor
                if (unknown2 == 0)
                {
                    return;
                }

                switch (unknown2)
                {
                    case 0:
                        // nothing more to do
                        break;
                    case 1:
                        message.AppendString("errors");
                        message.AppendString(content);
                        break;
                    case 2:
                        message.AppendString("link");
                        message.AppendString("event:");
                        message.AppendString("linkTitle");
                        message.AppendString("ok");
                        break;
                    case 4:
                        message.AppendString("title");
                        message.AppendString(title);
                        break;
                }

                if (unknown2 == 3 || unknown2 == 4)
                {
                    message.AppendString("message");
                    message.AppendString(content);
                    message.AppendString("linkUrl");
                    message.AppendString(url);
                    message.AppendString("linkTitle");
                    message.AppendString(urlName);
                }

                session.Send(message);
            }
        }

        #endregion Methods
    }
}