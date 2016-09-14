namespace Yupi.Messages.Groups
{
    using System;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public class ChangeFavouriteGroupMessageComposer : Yupi.Messages.Contracts.ChangeFavouriteGroupMessageComposer
    {
        #region Methods

        // TODO Refactor
        public override void Compose(Yupi.Protocol.ISender session, Group group, int virtualId)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(virtualId);

                if (group == null)
                {
                    message.AppendInteger(-1);
                    message.AppendInteger(-1);
                    message.AppendString(string.Empty);
                }
                else
                {
                    message.AppendInteger(group.Id);
                    message.AppendInteger(3); // TODO Hardcoded
                    message.AppendString(group.Name);
                }
                session.Send(message);
            }
        }

        #endregion Methods
    }
}