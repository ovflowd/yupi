using System;
using Yupi.Messages.Rooms;
using Yupi.Model.Repository;
using Yupi.Model.Domain;
using Yupi.Model;


namespace Yupi.Messages.Groups
{
    public class GroupMakeAdministratorMessageEvent : AbstractHandler
    {
        private IRepository<Group> GroupRepository;

        public GroupMakeAdministratorMessageEvent()
        {
            GroupRepository = DependencyFactory.Resolve<IRepository<Group>>();
        }

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            int groupId = request.GetInteger();
            int memberId = request.GetInteger();
            // TODO Rename variables
            Group group = GroupRepository.FindBy(groupId);
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