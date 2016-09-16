// ---------------------------------------------------------------------------------
// <copyright file="RoomAlterFilterMessageEvent.cs" company="https://github.com/sant0ro/Yupi">
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
using Yupi.Model.Repository;
using Yupi.Model.Domain;
using Yupi.Model;


namespace Yupi.Messages.Rooms
{
    using System;

    public class RoomAlterFilterMessageEvent : AbstractHandler
    {
        private IRepository<RoomData> RoomRepository;

        public RoomAlterFilterMessageEvent()
        {
            RoomRepository = DependencyFactory.Resolve<IRepository<RoomData>>();
        }

        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            int roomId = request.GetInteger();
            bool shouldAdd = request.GetBool();
            string word = request.GetString();

            RoomData room = RoomRepository.FindBy(roomId);

            if (room != null && room.HasOwnerRights(session.Info))
            {
                bool hasChanged = false;
                if (shouldAdd)
                {
                    hasChanged = room.WordFilter.Add (word);
                }
                else
                {
                    hasChanged = room.WordFilter.Remove (word);
                }

                if (hasChanged)
                {
                    RoomRepository.Save(room);
                }
            }
        }

        #endregion Methods
    }
}