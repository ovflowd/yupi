namespace Yupi.Messages.Items
{
    using System;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public class PickUpWallItemMessageComposer : Yupi.Messages.Contracts.PickUpWallItemMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, WallItem item, int pickerId)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendString(item.Id.ToString());
                message.AppendInteger(pickerId);
                session.Send(message);
            }
        }

        #endregion Methods
    }
}