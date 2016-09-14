using Yupi.Protocol;

namespace Yupi.Messages.User
{
    public class LoadCatalogClubGiftsMessageComposer : Contracts.LoadCatalogClubGiftsMessageComposer
    {
        public override void Compose(ISender session)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(0); // i
                message.AppendInteger(0); // i2
                message.AppendInteger(1); // TODO Magic constants
                session.Send(message);
            }
        }
    }
}