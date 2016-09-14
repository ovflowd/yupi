namespace Yupi.Messages.Items
{
    using System;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public class LoadPostItMessageComposer : Yupi.Messages.Contracts.LoadPostItMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, PostItItem item)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendString(item.Id.ToString());
                message.AppendString(item.Text);
                session.Send(message);
            }
        }

        #endregion Methods
    }
}