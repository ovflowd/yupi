using Yupi.Protocol;

namespace Yupi.Messages.Catalog
{
    public class CatalogPurchaseNotAllowedMessageComposer : Contracts.CatalogPurchaseNotAllowedMessageComposer
    {
        public override void Compose(ISender session, bool isForbidden)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(isForbidden);
                session.Send(message);
            }
        }
    }
}