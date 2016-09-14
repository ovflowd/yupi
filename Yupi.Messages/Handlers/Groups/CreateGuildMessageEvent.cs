// ---------------------------------------------------------------------------------
// <copyright file="CreateGuildMessageEvent.cs" company="https://github.com/sant0ro/Yupi">
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
namespace Yupi.Messages.Groups
{
    using System;
    using System.Collections.Generic;

    using Yupi.Messages.Catalog;

    public class CreateGuildMessageEvent : AbstractHandler
    {
        #region Methods

        // TODO Refactor
        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            List<int> gStates = new List<int>();
            string name = request.GetString();
            string description = request.GetString();
            int roomid = request.GetInteger();
            int color = request.GetInteger();
            int num3 = request.GetInteger();

            request.GetInteger(); // TODO Unused

            int guildBase = request.GetInteger();
            int guildBaseColor = request.GetInteger();
            int num6 = request.GetInteger();
            throw new NotImplementedException();
            /*
            RoomData roomData = Yupi.GetGame().GetRoomManager().GenerateRoomData(roomid);

            if (roomData.Owner != session.GetHabbo().Name)
                return;

            for (int i = 0; i < num6*3; i++)
                gStates.Add(request.GetInteger());

            string image = Yupi.GetGame().GetGroupManager().GenerateGuildImage(guildBase, guildBaseColor, gStates);

            Group theGroup;

            Yupi.GetGame()
                .GetGroupManager()
                .CreateGroup(name, description, roomid, image, session,
                    !Yupi.GetGame().GetGroupManager().SymbolColours.Contains(color) ? 1 : color,
                    !Yupi.GetGame().GetGroupManager().BackGroundColours.Contains(num3) ? 1 : num3, out theGroup);

            session.Router.GetComposer<PurchaseOKMessageComposer> ().Compose (session, 0u, "CREATE_GUILD", 10);

            router.GetComposer<GroupRoomMessageComposer> ().Compose (session, roomid, theGroup.Id);

            roomData.Group = theGroup;
            roomData.GroupId = theGroup.Id;
            roomData.SerializeRoomData(Response, session, true);

            if (!session.GetHabbo().InRoom || session.GetHabbo().CurrentRoom.RoomId != roomData.Id)
            {
                session.PrepareRoomForUser(roomData.Id, roomData.PassWord);
                session.GetHabbo().CurrentRoomId = roomData.Id;
            }

            if (session.GetHabbo().CurrentRoom != null &&
                !session.GetHabbo().CurrentRoom.LoadedGroups.ContainsKey(theGroup.Id))
                session.GetHabbo().CurrentRoom.LoadedGroups.Add(theGroup.Id, theGroup.Badge);

            if (CurrentLoadingRoom != null && !CurrentLoadingRoom.LoadedGroups.ContainsKey(theGroup.Id))
                CurrentLoadingRoom.LoadedGroups.Add(theGroup.Id, theGroup.Badge);

            if (CurrentLoadingRoom != null)
            {
                router.GetComposer<RoomGroupMessageComposer> ().Compose (CurrentLoadingRoom);

                if (session.GetHabbo ().FavouriteGroup != theGroup.Id) {
                    router.GetComposer<ChangeFavouriteGroupMessageComposer> ().Compose (session, theGroup, CurrentLoadingRoom.GetRoomUserManager ().GetRoomUserByHabbo (session.GetHabbo ().Id).VirtualId);
                }
            }

            */
        }

        #endregion Methods
    }
}