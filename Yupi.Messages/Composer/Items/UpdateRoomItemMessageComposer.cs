namespace Yupi.Messages.Items
{
    using System;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public class UpdateRoomItemMessageComposer : Yupi.Messages.Contracts.UpdateRoomItemMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, FloorItem item)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                throw new NotImplementedException();
                //item.Serialize(message);
                session.Send(message);
            }
        }

        #endregion Methods
    }
}