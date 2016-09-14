namespace Yupi.Messages.Rooms
{
    using System;
    using System.Collections.Generic;
    using System.Numerics;

    using Yupi.Model.Domain;

    public class UserWalkMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            int targetX = request.GetInteger();
            int targetY = request.GetInteger();

            RoomEntity entity = session.RoomEntity;

            var target = new Vector2(targetX, targetY);

            if (entity == null || !entity.CanWalk() || entity.Position.Equals(target))
            {
                return;
            }

            entity.Wake();

            entity.Walk(target);

            /* TODO Implement Horse
            if (entity.RidingHorse) {
                RoomUser roomUserByVirtualId = currentRoom.GetRoomUserManager ().GetRoomUserByVirtualId ((int)roomUserByHabbo.HorseId);

                roomUserByVirtualId.MoveTo (targetX, targetY);
            }*/
        }

        #endregion Methods
    }
}