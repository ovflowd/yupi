using System;
using System.Collections.Generic;
using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Groups
{
    public class GroupUpdateBadgeMessageEvent : AbstractHandler
    {
        private readonly IRepository<Group> GroupRepository;

        public GroupUpdateBadgeMessageEvent()
        {
            GroupRepository = DependencyFactory.Resolve<IRepository<Group>>();
        }

        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            var groupId = request.GetInteger();
            var group = GroupRepository.FindBy(groupId);

            if (group != null)
            {
                // TODO Unused
                request.GetInteger();

                var Base = request.GetInteger();
                var baseColor = request.GetInteger();

                // TODO Unused value!
                request.GetInteger();

                var guildStates = new List<int>();

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
    }
}