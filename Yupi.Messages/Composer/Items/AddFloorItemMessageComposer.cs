namespace Yupi.Messages.Items
{
    using System;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public class AddFloorItemMessageComposer : Yupi.Messages.Contracts.AddFloorItemMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender room, FloorItem item)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                /*
                item.Serialize(message);
                message.AppendString(room.Data.Owner.Name);
                */
                throw new NotImplementedException();
                room.Send(message);
            }
        }

        #endregion Methods
    }
}