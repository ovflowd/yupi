using System;
using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;


namespace Yupi.Messages.User
{
    public class UpdateUserDataMessageComposer : Yupi.Messages.Contracts.UpdateUserDataMessageComposer
    {
        // TODO Does -1 mean self???
        public override void Compose(Yupi.Protocol.ISender room, UserInfo habbo, int roomUserId = -1)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
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