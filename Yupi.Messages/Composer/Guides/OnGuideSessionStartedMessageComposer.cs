namespace Yupi.Messages.Guides
{
    using System;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public class OnGuideSessionStartedMessageComposer : Yupi.Messages.Contracts.OnGuideSessionStartedMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, UserInfo habbo)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(habbo.Id);
                message.AppendString(habbo.Name);
                message.AppendString(habbo.Look);
                message.AppendInteger(habbo.Id);
                message.AppendString(habbo.Name);
                message.AppendString(habbo.Look);
                session.Send(message);
            }
        }

        #endregion Methods
    }
}