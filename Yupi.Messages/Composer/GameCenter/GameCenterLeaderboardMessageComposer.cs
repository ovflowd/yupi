using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.GameCenter
{
    public class GameCenterLeaderboardMessageComposer : Contracts.GameCenterLeaderboardMessageComposer
    {
        public override void Compose(ISender session, UserInfo user)
        {
            // TODO hardcoded message
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(2014);
                message.AppendInteger(49);
                message.AppendInteger(0);
                message.AppendInteger(0);
                message.AppendInteger(6526);
                message.AppendInteger(1);
                message.AppendInteger(user.Id);
                message.AppendInteger(0);
                message.AppendInteger(1);
                message.AppendString(user.Name);
                message.AppendString(user.Look);
                message.AppendString(user.Gender);
                message.AppendInteger(1);
                message.AppendInteger(18);

                session.Send(message);
            }
        }
    }
}