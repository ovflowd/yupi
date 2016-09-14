namespace Yupi.Messages.Rooms
{
    using System;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public class DanceStatusMessageComposer : Yupi.Messages.Contracts.DanceStatusMessageComposer
    {
        #region Methods

        // TODO Create enum for Dances
        // TODO Replace entityId with RoomEntity EVERYWHERE!
        public override void Compose(Yupi.Protocol.ISender room, int entityId, Dance dance)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(entityId);
                message.AppendInteger(dance.Value);
                room.Send(message);
            }
        }

        #endregion Methods
    }
}