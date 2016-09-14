using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.User
{
    public class UpdateUserDataMessageComposer : Contracts.UpdateUserDataMessageComposer
    {
// TODO Does -1 mean self???
        public override void Compose(ISender room, UserInfo habbo, int roomUserId = -1)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(roomUserId);
                message.AppendString(habbo.Look);
                message.AppendString(habbo.Gender.ToLower());
                    // TODO ToLower here whereas ToUpper in UpdateAvatarAspectMessageComposer ?!
                message.AppendString(habbo.Motto);
                message.AppendInteger(habbo.Wallet.AchievementPoints);

                room.Send(message);
            }
        }
    }
}