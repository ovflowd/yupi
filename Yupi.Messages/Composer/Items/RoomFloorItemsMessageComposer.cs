// ---------------------------------------------------------------------------------
// <copyright file="RoomFloorItemsMessageComposer.cs" company="https://github.com/sant0ro/Yupi">
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
namespace Yupi.Messages.Items
{
    using System;
    using System.Collections.Generic;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public class RoomFloorItemsMessageComposer : Yupi.Messages.Contracts.RoomFloorItemsMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, RoomData data,
            IReadOnlyDictionary<uint, FloorItem> items)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                if (data.Group != null)
                {
                    // TODO Refactor
                    if (data.Group.AdminOnlyDeco == 1u)
                    {
                        message.AppendInteger(data.Group.Admins.Count + 1);

                        foreach (UserInfo member in data.Group.Admins)
                        {
                            if (member != null)
                            {
                                message.AppendInteger(member.Id);
                                message.AppendString(member.Name);
                            }
                        }

                        message.AppendInteger(data.Owner.Id);
                        message.AppendString(data.Owner.Name);
                    }
                    else
                    {
                        message.AppendInteger(data.Group.Members.Count + 1);

                        foreach (UserInfo member in data.Group.Members)
                        {
                            message.AppendInteger(member.Id);
                            message.AppendString(member.Name);
                        }
                    }
                }
                else
                {
                    message.AppendInteger(1);
                    message.AppendInteger(data.Owner.Id);
                    message.AppendString(data.Owner.Name);
                }

                message.AppendInteger(items.Count);

                foreach (KeyValuePair<uint, FloorItem> roomItem in items)
                {
                    // RoomItem::Serialize
                    throw new NotImplementedException();
                    //roomItem.Value.Serialize (message);
                }

                session.Send(message);
            }
        }

        #endregion Methods
    }
}