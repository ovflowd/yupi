namespace Yupi.Messages.Groups
{
    using System;

    using Yupi.Protocol.Buffers;

    public class GrouprequestReloadMessageComposer : Yupi.Messages.Contracts.GrouprequestReloadMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, int groupId)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(groupId);
                session.Send(message);
            }
        }

        #endregion Methods
    }
}