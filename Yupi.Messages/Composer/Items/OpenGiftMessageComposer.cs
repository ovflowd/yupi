using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Items
{
    public class OpenGiftMessageComposer : Contracts.OpenGiftMessageComposer
    {
        public override void Compose(ISender session, BaseItem item, string text)
        {
            using (var message = Pool.GetMessageBuffer(Id))
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
    }
}