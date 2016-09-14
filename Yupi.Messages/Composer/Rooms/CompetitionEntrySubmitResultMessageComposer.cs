// ---------------------------------------------------------------------------------
// <copyright file="CompetitionEntrySubmitResultMessageComposer.cs" company="https://github.com/sant0ro/Yupi">
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
namespace Yupi.Messages.Rooms
{
    using System;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public class CompetitionEntrySubmitResultMessageComposer : Yupi.Messages.Contracts.CompetitionEntrySubmitResultMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, RoomCompetition competition, int status,
            RoomData room = null)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(competition.Id);
                message.AppendString(competition.Name);
                message.AppendInteger(status);

                if (status != 3)
                {
                    message.AppendInteger(0);
                    message.AppendInteger(0);
                }
                else
                {
                    message.AppendInteger(competition.RequiredItems.Count);

                    foreach (BaseItem furni in competition.RequiredItems)
                    {
                        message.AppendString(furni.Name);
                    }

                    if (room == null)
                        message.AppendInteger(0);
                    else
                    {
                        message.StartArray();

                        foreach (BaseItem furni in competition.RequiredItems)
                        {
                            /*
                            if (!room.GetRoomItemHandler().HasFurniByItemName(furni))
                            {
                                message.AppendString(furni);
                                message.SaveArray();
                            }
                            */
                            throw new NotImplementedException();
                        }

                        message.EndArray();
                    }
                }
                session.Send(message);
            }
        }

        #endregion Methods
    }
}