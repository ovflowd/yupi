namespace Yupi.Messages.Rooms
{
    using System;

    public class GiveRightsMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            /*
            uint userId = request.GetUInt32();
            Room room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);

            if (room == null)
                return;

            RoomUser roomUserByHabbo = room.GetRoomUserManager().GetRoomUserByHabbo(userId);

            if (roomUserByHabbo == null || roomUserByHabbo.IsBot || !room.CheckRights(session, true)) {
                return;
            }

            if (room.UsersWithRights.Contains(userId))
            {
                session.SendNotif(Yupi.GetLanguage().GetVar("no_room_rights_error"));
                return;
            }

            room.UsersWithRights.Add(userId);

            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager ().GetQueryReactor ()) {
                queryReactor.SetQuery ("INSERT INTO rooms_rights (room_id,user_id) VALUES (@room, @user)");
                queryReactor.AddParameter ("room", room.RoomId);
                queryReactor.AddParameter ("user", userId);
                queryReactor.RunQuery ();
            }

                router.GetComposer<GiveRoomRightsMessageComposer> ().Compose (session, room.RoomId, roomUserByHabbo.GetClient ().GetHabbo ());
                roomUserByHabbo.UpdateNeeded = true;

                roomUserByHabbo.AddStatus("flatctrl 1", string.Empty);

            router.GetComposer<RoomRightsLevelMessageComposer> ().Compose (roomUserByHabbo.GetClient(), 1);
            UsersWithRights();
            */
            throw new NotImplementedException();
        }

        #endregion Methods
    }
}