// ---------------------------------------------------------------------------------
// <copyright file="GroupMakeAdministratorMessageEvent.cs" company="https://github.com/sant0ro/Yupi">
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

    using Yupi.Messages.Rooms;
    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Model.Repository;

    public class GroupMakeAdministratorMessageEvent : AbstractHandler
    {
        #region Fields

        private IRepository<Group> GroupRepository;

        #endregion Fields

        #region Constructors

        public GroupMakeAdministratorMessageEvent()
        {
            GroupRepository = DependencyFactory.Resolve<IRepository<Group>>();
        }

        #endregion Constructors

        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            int groupId = request.GetInteger();
            int memberId = request.GetInteger();
            // TODO Rename variables
            Group group = GroupRepository.Find(groupId);
            throw new NotImplementedException();
            /*
            if (session.Info != group.Creator || !group.Members.ContainsKey(memberId) ||
                group.Admins.ContainsKey(memberId))
                return;

            group.Members[memberId].Rank = 1;

            group.Admins.Add(memberId, group.Members[memberId]);

            router.GetComposer<GroupMembersMessageComposer> ().Compose (session, group, 1u, session);

            Yupi.Messages.Rooms room = Yupi.GetGame().GetRoomManager().GetRoom(group.RoomId);

            RoomUser roomUserByHabbo = room?.GetRoomUserManager().GetRoomUserByHabbo(Yupi.GetHabboById(memberId).Name);

            if (roomUserByHabbo != null)
            {
                if (!roomUserByHabbo.Statusses.ContainsKey("flatctrl 1"))
                    roomUserByHabbo.AddStatus("flatctrl 1", "");

                router.GetComposer<RoomRightsLevelMessageComposer> ().Compose (roomUserByHabbo.GetClient (), 1);
                roomUserByHabbo.UpdateNeeded = true;
            }
            */
        }

        #endregion Methods
    }
}