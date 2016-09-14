namespace Yupi.Messages.Items
{
    using System;

    using Yupi.Protocol.Buffers;

    public class BuildersClubUpdateFurniCountMessageComposer : Yupi.Messages.Contracts.BuildersClubUpdateFurniCountMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, int itemsUsed)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(itemsUsed);
                session.Send(message);
            }
        }

        #endregion Methods
    }
}