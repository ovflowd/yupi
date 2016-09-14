using Yupi.Protocol;

namespace Yupi.Messages.Catalog
{
    public class CatalogueOfferConfigMessageComposer : Contracts.CatalogueOfferConfigMessageComposer
    {
        public override void Compose(ISender session)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                // TODO Hardcoded message
                message.AppendInteger(100);
                message.AppendInteger(6);
                message.AppendInteger(2);
                message.AppendInteger(1);
                message.AppendInteger(2);
                message.AppendInteger(40);
                message.AppendInteger(99);
                session.Send(message);
            }
        }
    }
}