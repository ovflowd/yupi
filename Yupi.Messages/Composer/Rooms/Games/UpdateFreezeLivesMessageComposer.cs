using Yupi.Protocol;

namespace Yupi.Messages.Games
{
    public class UpdateFreezeLivesMessageComposer : Contracts.UpdateFreezeLivesMessageComposer
    {
        public override void Compose(ISender session, int roomId, int lives)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(roomId);
                message.AppendInteger(lives);
                session.Send(message);
            }
        }
    }
}