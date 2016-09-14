namespace Yupi.Messages.Camera
{
    using System;

    using Yupi.Protocol.Buffers;

    public class CameraStorageUrlMessageComposer : Yupi.Messages.Contracts.CameraStorageUrlMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, string url)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendString(url);
                session.Send(message);
            }
        }

        #endregion Methods
    }
}