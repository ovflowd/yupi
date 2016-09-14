using System;
using Yupi.Protocol.Buffers;
using Yupi.Net;
using System.Collections.Generic;
using Yupi.Model.Domain;
using Yupi.Messages.Encoders;


namespace Yupi.Messages.User
{
    public class UserTagsMessageComposer : Yupi.Messages.Contracts.UserTagsMessageComposer
    {
        public override void Compose(Yupi.Protocol.ISender session, UserInfo info)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(info.Id);
                message.Append(info.Tags);
                session.Send(message);
            }
        }
    }
}