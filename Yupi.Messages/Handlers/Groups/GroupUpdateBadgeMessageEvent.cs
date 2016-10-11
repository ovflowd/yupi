// ---------------------------------------------------------------------------------
// <copyright file="GroupUpdateBadgeMessageEvent.cs" company="https://github.com/sant0ro/Yupi">
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

    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Model.Repository;

    public class GroupUpdateBadgeMessageEvent : AbstractHandler
    {
        #region Fields

        private IRepository<Group> GroupRepository;

        #endregion Fields

        #region Constructors

        public GroupUpdateBadgeMessageEvent()
        {
            GroupRepository = DependencyFactory.Resolve<IRepository<Group>>();
        }

        #endregion Constructors

        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            int groupId = request.GetInteger();
            Group group = GroupRepository.Find(groupId);

            if (group != null)
            {
                // TODO Unused
                request.GetInteger();

                int Base = request.GetInteger();
                int baseColor = request.GetInteger();

                // TODO Unused value!
                request.GetInteger();

                List<int> guildStates = new List<int>();

                throw new NotImplementedException();

                /*
                for (int i = 0; i < 12; i++)
                    guildStates.Add (request.GetInteger ());

                string badge = Yupi.GetGame ().GetGroupManager ().GenerateGuildImage (Base, baseColor, guildStates);

                guild.Badge = badge;

                router.GetComposer<RoomGroupMessageComposer> ().Compose (room);

                router.GetComposer<GroupDataMessageComposer> ().Compose (room, guild, session.GetHabbo ());

                using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager ().GetQueryReactor ()) {
                    queryReactor.SetQuery (
                            $"UPDATE groups_data SET group_badge = @badge WHERE id = {guildId}");

                    queryReactor.AddParameter ("badge", badge);
                    queryReactor.RunQuery ();
                }

                if (session.GetHabbo ().CurrentRoom != null) {
                    // TODO Isn't this a duplicate of the above?
                    session.GetHabbo ().CurrentRoom.LoadedGroups [guildId] = guild.Badge;

                    router.GetComposer<RoomGroupMessageComposer> ().Compose (session.GetHabbo ().CurrentRoom);

                    router.GetComposer<GroupDataMessageComposer> ().Compose (session.GetHabbo ().CurrentRoom, guild, session.GetHabbo ());
                }
                */
            }
        }

        #endregion Methods
    }
}