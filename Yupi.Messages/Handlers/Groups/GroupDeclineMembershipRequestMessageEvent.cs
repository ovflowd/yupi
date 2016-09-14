using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Groups
{
    public class GroupDeclineMembershipRequestMessageEvent : AbstractHandler
    {
        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            var groupId = request.GetInteger();
            var userId = request.GetInteger();
            /*
            Group group = Yupi.GetGame().GetGroupManager().GetGroup(groupId);

            if (session.GetHabbo().Id != group.CreatorId && !group.Admins.ContainsKey(session.GetHabbo().Id) &&
                !group.Requests.ContainsKey(userId))
                return;

            group.Requests.Remove(userId);

            router.GetComposer<GroupMembersMessageComposer> ().Compose (session, group, 2u, session);

            Yupi.Messages.Rooms room = Yupi.GetGame().GetRoomManager().GetRoom(group.RoomId);

            RoomUser roomUserByHabbo = room?.GetRoomUserManager().GetRoomUserByHabbo(Yupi.GetHabboById(userId).Name);

            if (roomUserByHabbo != null)
            {
                if (roomUserByHabbo.Statusses.ContainsKey("flatctrl 1"))
                    roomUserByHabbo.RemoveStatus("flatctrl 1");

                roomUserByHabbo.UpdateNeeded = true;
            }

            router.GetComposer<GroupDataMessageComposer> ().Compose (session, group, session.GetHabbo());
        
            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager ().GetQueryReactor ()) {
                queryReactor.SetQuery ("DELETE FROM group_requests WHERE group_id = @group_id AND user_id = @user_id");
                queryReactor.AddParameter("group_id", groupId);
                queryReactor.AddParameter("user_id", userId);
                queryReactor.RunQuery ();
            }
            */
        }
    }
}