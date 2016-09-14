using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Groups
{
    public class GrouprequestReloadMessageComposer : Yupi.Messages.Contracts.GrouprequestReloadMessageComposer
    {
        public override void Compose(Yupi.Protocol.ISender session, int groupId)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(groupId);
                session.Send(message);
            }
        }
    }
}