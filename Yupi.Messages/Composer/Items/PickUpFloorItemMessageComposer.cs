namespace Yupi.Messages.Items
{
    using System;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public class PickUpFloorItemMessageComposer : Yupi.Messages.Contracts.PickUpFloorItemMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, FloorItem item, int pickerId)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendString(item.Id.ToString());
                message.AppendBool(false); //expired
                message.AppendInteger(pickerId);
                message.AppendInteger(0); // delay
                session.Send(message);
            }
        }

        #endregion Methods
    }
}