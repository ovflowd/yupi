// ---------------------------------------------------------------------------------
// <copyright file="GoToRoomByNameMessageEvent.cs" company="https://github.com/sant0ro/Yupi">
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
namespace Yupi.Messages.Other
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Yupi.Controller;
    using Yupi.Messages.User;
    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Util;

    public class GoToRoomByNameMessageEvent : AbstractHandler
    {
        #region Fields

        private RoomManager RoomManager;

        #endregion Fields

        #region Constructors

        public GoToRoomByNameMessageEvent()
        {
            RoomManager = DependencyFactory.Resolve<RoomManager>();
        }

        #endregion Constructors

        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            string name = request.GetString();

            // TODO Refactor
            switch (name)
            {
                case "predefined_noob_lobby":
                {
                    /*
                    int roomId = Convert.ToUInt32(Yupi.GetDbConfig().DbData["noob.lobby.roomid"]);
                    router.GetComposer<RoomForwardMessageComposer> ().Compose (session, roomId);*/
                    throw new NotImplementedException();
                    break;
                }
                case "random_friending_room":
                {
                    Room room = RoomManager.GetActive().ToList().Random();
                    router.GetComposer<RoomForwardMessageComposer>().Compose(session, room.Data.Id);
                    break;
                }
            }
        }

        #endregion Methods
    }
}