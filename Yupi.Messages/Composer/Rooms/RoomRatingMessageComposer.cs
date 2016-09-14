namespace Yupi.Messages.Rooms
{
    using System;

    using Yupi.Protocol.Buffers;

    public class RoomRatingMessageComposer : Yupi.Messages.Contracts.RoomRatingMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, int rating, bool canVote)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(rating);
                message.AppendBool(canVote);
                session.Send(message);
            }
        }

        #endregion Methods
    }
}