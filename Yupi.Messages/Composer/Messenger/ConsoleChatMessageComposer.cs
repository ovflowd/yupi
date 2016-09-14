namespace Yupi.Messages.Messenger
{
    using System;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public class ConsoleChatMessageComposer : Yupi.Messages.Contracts.ConsoleChatMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, MessengerMessage msg)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(msg.From.Id);
                message.AppendString(msg.Text);
                message.AppendInteger((int) msg.Diff().TotalSeconds);
                session.Send(message);
            }
        }

        #endregion Methods
    }
}