namespace Yupi.Messages.Items
{
    using System;

    using Yupi.Model.Domain;
    using Yupi.Protocol;
    using Yupi.Protocol.Buffers;

    public class LoveLockDialogueMessageComposer : Yupi.Messages.Contracts.LoveLockDialogueMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender user1, Yupi.Protocol.ISender user2, LovelockItem loveLock)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(loveLock.Id);
                message.AppendBool(true);

                // TODO use loveLock.InteractingUser
                user1.Send(message);
                user2.Send(message);
            }
        }

        #endregion Methods
    }
}