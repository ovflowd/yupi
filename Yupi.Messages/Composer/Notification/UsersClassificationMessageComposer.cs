namespace Yupi.Messages.Notification
{
    using System;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public class UsersClassificationMessageComposer : Yupi.Messages.Contracts.UsersClassificationMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, UserInfo habbo, string word)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(1);
                message.AppendInteger(habbo.Id);
                message.AppendString(habbo.Name);
                message.AppendString("BadWord: " + word);
                session.Send(message);
            }
        }

        #endregion Methods
    }
}