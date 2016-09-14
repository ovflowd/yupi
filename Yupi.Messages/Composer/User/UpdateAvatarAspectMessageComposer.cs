namespace Yupi.Messages.User
{
    using System;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public class UpdateAvatarAspectMessageComposer : Yupi.Messages.Contracts.UpdateAvatarAspectMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, UserInfo habbo)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendString(habbo.Look);
                message.AppendString(habbo.Gender.ToUpper()); // TODO Make sure gender is stored UPPER
                session.Send(message);
            }
        }

        #endregion Methods
    }
}