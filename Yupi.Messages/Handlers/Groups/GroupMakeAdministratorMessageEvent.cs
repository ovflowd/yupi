using System;
using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Groups
{
    public class GroupMakeAdministratorMessageEvent : AbstractHandler
    {
        private readonly IRepository<Group> GroupRepository;

        public GroupMakeAdministratorMessageEvent()
        {
            GroupRepository = DependencyFactory.Resolve<IRepository<Group>>();
        }

        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            var groupId = request.GetInteger();
            var memberId = request.GetInteger();
            // TODO Rename variables
            var group = GroupRepository.FindBy(groupId);
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
    }
}