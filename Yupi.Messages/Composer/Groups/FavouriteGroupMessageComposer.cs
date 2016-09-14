namespace Yupi.Messages.Groups
{
    using System;

    using Yupi.Protocol.Buffers;

    public class FavouriteGroupMessageComposer : Yupi.Messages.Contracts.FavouriteGroupMessageComposer
    {
        #region Methods

        // TODO userId vs groupId ??? TEST !!!
        public override void Compose(Yupi.Protocol.ISender session, int userId)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(userId);
                session.Send(message);
            }
        }

        #endregion Methods
    }
}