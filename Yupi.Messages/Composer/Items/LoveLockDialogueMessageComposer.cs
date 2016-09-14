using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Items
{
    public class LoveLockDialogueMessageComposer : Contracts.LoveLockDialogueMessageComposer
    {
        public override void Compose(ISender user1, ISender user2, LovelockItem loveLock)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(loveLock.Id);
                message.AppendBool(true);

                // TODO use loveLock.InteractingUser
                user1.Send(message);
                user2.Send(message);
            }
        }
    }
}