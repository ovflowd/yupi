namespace Yupi.Messages.Rooms
{
    using System;
    using System.Numerics;

    using Yupi.Model;

    public class LookAtUserMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            int x = request.GetInteger();
            int y = request.GetInteger();

            if (session.RoomEntity == null)
            {
                return;
            }

            Vector3 target = new Vector3(x, y, 0);

            if (session.RoomEntity.Position == target)
            {
                return;
            }

            int rotation = session.RoomEntity.Position.CalculateRotation(target);

            session.RoomEntity.SetRotation(rotation);

            session.RoomEntity.Wake();

            // TODO Implement Horse
            /*
            if (!roomUserByHabbo.RidingHorse)
                return;

            RoomUser roomUserByVirtualId = session.GetHabbo().CurrentRoom.GetRoomUserManager().GetRoomUserByVirtualId(Convert.ToInt32(roomUserByHabbo.HorseId));

            roomUserByVirtualId.SetRot(rotation, false);
            roomUserByVirtualId.UpdateNeeded = true;
            */
        }

        #endregion Methods
    }
}