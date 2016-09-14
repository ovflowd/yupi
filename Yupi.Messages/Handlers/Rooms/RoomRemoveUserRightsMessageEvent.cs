namespace Yupi.Messages.Rooms
{
    using System;
    using System.Collections.Generic;

    public class RoomRemoveUserRightsMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            /*
            Room room = Yupi.GetGame().GetRoomManager().GetRoom(session.GetHabbo().CurrentRoomId);

            if (room == null || !room.CheckRights(session, true))
                return;

            List<uint> users = new List<uint> ();

            int count = request.GetInteger();

            {
                for (int i = 0; i < count; i++)
                {
                    uint userId = request.GetUInt32();

                    if (room.UsersWithRights.Contains(userId))
                        room.UsersWithRights.Remove(userId);

                    RoomUser roomUserByHabbo = room.GetRoomUserManager().GetRoomUserByHabbo(userId);

                    if (roomUserByHabbo == null || roomUserByHabbo.IsBot) {
                        continue;
                    }

                    users.Add (userId);

                    router.GetComposer<RoomRightsLevelMessageComposer> ().Compose (roomUserByHabbo.GetClient(), 0);
                    roomUserByHabbo.RemoveStatus("flatctrl 1");
                    roomUserByHabbo.UpdateNeeded = true;

                    router.GetComposer<RemoveRightsMessageComposer> ().Compose (session, room.RoomId, userId);
                }

                UsersWithRights();

                using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor()) {
                    queryReactor.SetQuery("DELETE FROM rooms_rights WHERE room_id = @room AND user_id IN (" + String.Join(",", users) +")");
                    queryReactor.AddParameter ("room", room.RoomId);
                    queryReactor.RunQuery ();
                }
            }
            */
            throw new NotImplementedException();
        }

        #endregion Methods
    }
}