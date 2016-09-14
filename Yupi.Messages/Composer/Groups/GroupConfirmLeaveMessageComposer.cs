namespace Yupi.Messages.Groups
{
    using System;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public class GroupConfirmLeaveMessageComposer : Yupi.Messages.Contracts.GroupConfirmLeaveMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, UserInfo user, Group group, int type)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(group.Id);
                message.AppendInteger(type);
                message.AppendInteger(user.Id);
                message.AppendString(user.Name);
                message.AppendString(user.Look);
                message.AppendString(string.Empty);
                session.Send(message);
            }
        }

        #endregion Methods
    }
}