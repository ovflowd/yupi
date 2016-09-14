namespace Yupi.Messages.Catalog
{
    using System;

    using Yupi.Protocol.Buffers;

    public class CheckPetNameMessageComposer : Yupi.Messages.Contracts.CheckPetNameMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, int status, string name)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(status);
                message.AppendString(name);
                session.Send(message);
            }
        }

        #endregion Methods
    }
}