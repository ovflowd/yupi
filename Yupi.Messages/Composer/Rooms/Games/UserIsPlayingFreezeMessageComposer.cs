using Yupi.Protocol;

namespace Yupi.Messages.Games
{
    public class UserIsPlayingFreezeMessageComposer : Contracts.UserIsPlayingFreezeMessageComposer
    {
        public override void Compose(ISender session, bool isPlaying)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendBool(isPlaying);
                session.Send(message);
            }
        }
    }
}