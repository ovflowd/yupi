namespace Yupi.Messages.Rooms
{
    using System;

    using Yupi.Messages.User;
    using Yupi.Model.Domain;

    public class RoomUserActionMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            if (session.RoomEntity == null)
                return;

            int actionId = request.GetInteger();

            UserAction action;

            if (!UserAction.TryParse(actionId, out action))
            {
                return;
            }

            if (action == UserAction.Idle)
            {
                session.RoomEntity.Sleep();
            }
            else
            {
                session.RoomEntity.Wake();

                session.RoomEntity.Room.EachUser(
                    roomSession =>
                    {
                        roomSession.Router.GetComposer<RoomUserActionMessageComposer>()
                            .Compose(roomSession, session.RoomEntity.Id, action);
                    });
            }
        }

        #endregion Methods
    }
}