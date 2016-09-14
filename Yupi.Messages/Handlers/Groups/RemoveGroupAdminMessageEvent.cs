using System;
using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Groups
{
    public class RemoveGroupAdminMessageEvent : AbstractHandler
    {
        // TODO Refactor
        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            var num = request.GetUInt32();
            var num2 = request.GetUInt32();
            throw new NotImplementedException();
            /*
            Group group = Yupi.GetGame().GetGroupManager().GetGroup(num);

            if (session.GetHabbo().Id != group.CreatorId || !group.Members.ContainsKey(num2) ||
                !group.Admins.ContainsKey(num2))
                return;

            group.Members[num2].Rank = 0;
            group.Admins.Remove(num2);

            router.GetComposer<GroupMembersMessageComposer> ().Compose (session, group, 0u, session);

            Yupi.Messages.Rooms room = Yupi.GetGame().GetRoomManager().GetRoom(group.RoomId);
            RoomUser roomUserByHabbo = room?.GetRoomUserManager().GetRoomUserByHabbo(Yupi.GetHabboById(num2).Name);

            if (roomUserByHabbo != null)
            {
                if (roomUserByHabbo.Statusses.ContainsKey("flatctrl 1"))
                    roomUserByHabbo.RemoveStatus("flatctrl 1");

                router.GetComposer<RoomRightsLevelMessageComposer> ().Compose (session, 0);
                roomUserByHabbo.UpdateNeeded = true;
            }

            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager ().GetQueryReactor ()) {
                // TODO Magic number !!! (rank)
                queryReactor.SetQuery ("UPDATE groups_members SET rank = 0 WHERE group_id = @group_id AND user_id = @user_id");
                queryReactor.AddParameter("group_id", num);
                queryReactor.AddParameter("user_id", num2);
                queryReactor.RunQuery ();
            }
            */
        }
    }
}