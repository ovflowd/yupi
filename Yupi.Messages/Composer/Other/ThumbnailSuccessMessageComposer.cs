namespace Yupi.Messages.Other
{
    using System;

    using Yupi.Protocol.Buffers;

    public class ThumbnailSuccessMessageComposer : Yupi.Messages.Contracts.ThumbnailSuccessMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendBool(true);
                message.AppendBool(false); // TODO Hardcoded
                session.Send(message);
            }
        }

        #endregion Methods
    }
}