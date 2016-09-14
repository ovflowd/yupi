using Yupi.Protocol;

namespace Yupi.Messages.Groups
{
    public class FavouriteGroupMessageComposer : Contracts.FavouriteGroupMessageComposer
    {
        // TODO userId vs groupId ??? TEST !!!
        public override void Compose(ISender session, int userId)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(userId);
                session.Send(message);
            }
        }
    }
}