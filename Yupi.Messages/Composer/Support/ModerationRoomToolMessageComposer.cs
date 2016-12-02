// ---------------------------------------------------------------------------------
// <copyright file="ModerationRoomToolMessageComposer.cs" company="https://github.com/sant0ro/Yupi">
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
namespace Yupi.Messages.Support
{
    using System;

    using Yupi.Controller;
    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public class ModerationRoomToolMessageComposer : Yupi.Messages.Contracts.ModerationRoomToolMessageComposer
    {
        #region Fields

        private RoomManager RoomManager;

        #endregion Fields

        #region Constructors

        public ModerationRoomToolMessageComposer()
        {
            RoomManager = DependencyFactory.Resolve<RoomManager>();
        }

        #endregion Constructors

        #region Methods

        // TODO Refactor
        public override void Compose(Yupi.Protocol.ISender session, RoomData data, bool isLoaded)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(data.Id);
                message.AppendInteger(RoomManager.UsersNow(data));

                message.AppendBool(false); // TODO Meaning? (isOwnerInRoom?)

                message.AppendInteger(data.Owner.Id);
                message.AppendString(data.Owner.Name);
                message.AppendBool(isLoaded);
                message.AppendString(data.Name);
                message.AppendString(data.Description);
                message.AppendInteger(data.Tags.Count);

                foreach (Tag tag in data.Tags) {
                    message.AppendString (tag.Value);
                }

                message.AppendBool(false);

                session.Send(message);
            }
        }

        #endregion Methods
    }
}