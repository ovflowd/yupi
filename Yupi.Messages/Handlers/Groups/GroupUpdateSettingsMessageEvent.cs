// ---------------------------------------------------------------------------------
// <copyright file="GroupUpdateSettingsMessageEvent.cs" company="https://github.com/sant0ro/Yupi">
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

    using Yupi.Controller;
    using Yupi.Messages.Rooms;
    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Model.Repository;

    public class GroupUpdateSettingsMessageEvent : AbstractHandler
    {
        #region Fields

        private IRepository<Group> GroupRepository;
        private RoomManager RoomManager;

        #endregion Fields

        #region Constructors

        public GroupUpdateSettingsMessageEvent()
        {
            GroupRepository = DependencyFactory.Resolve<IRepository<Group>>();
            RoomManager = DependencyFactory.Resolve<RoomManager>();
        }

        #endregion Constructors

        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            int groupId = request.GetInteger();
            uint state = request.GetUInt32();
            uint admindeco = request.GetUInt32();

            Group group = GroupRepository.FindBy(groupId);

            if (group?.Creator != session.Info)
                return;

            group.State = state;
            group.AdminOnlyDeco = admindeco;

            GroupRepository.Save(group);

            Room room = RoomManager.GetIfLoaded(group.Room);
            /*
            if (room != null)
            {
                foreach (RoomUser current in room.GetRoomUserManager().GetRoomUsers())
                {
                    if (room.Data.Owner != current.UserId && !group.Admins.ContainsKey(current.UserId) &&
                        group.Members.ContainsKey(current.UserId))
                    {
                        // TODO USE F*KING ENUMS!
                        if (admindeco == 1u)
                        {
                            current.RemoveStatus("flatctrl 1");
                            router.GetComposer<RoomRightsLevelMessageComposer> ().Compose (current.GetClient(), 0);
                        }
                        else
                        {
                            if (admindeco == 0u && !current.Statusses.ContainsKey("flatctrl 1"))
                            {
                                current.AddStatus("flatctrl 1", string.Empty);
                                router.GetComposer<RoomRightsLevelMessageComposer> ().Compose (current.GetClient(), 1);
                            }
                        }

                        current.UpdateNeeded = true;
                    }
                }
            }
            router.GetComposer<GroupDataMessageComposer> ().Compose (session.GetHabbo().CurrentRoom, group, session.GetHabbo());
                */
            throw new NotImplementedException();
        }

        #endregion Methods
    }
}