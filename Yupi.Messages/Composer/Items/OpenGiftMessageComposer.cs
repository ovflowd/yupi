namespace Yupi.Messages.Items
{
    using System;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public class OpenGiftMessageComposer : Yupi.Messages.Contracts.OpenGiftMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, BaseItem item, string text)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendString(item.Type.ToString());
                message.AppendInteger(item.SpriteId);
                message.AppendString(item.Name);
                message.AppendInteger(item.Id);
                message.AppendString(item.Type.ToString());
                message.AppendBool(true);
                message.AppendString(text);
                session.Send(message);
            }
        }

        #endregion Methods
    }
}