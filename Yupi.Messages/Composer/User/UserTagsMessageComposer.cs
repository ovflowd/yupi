namespace Yupi.Messages.User
{
    using System;
    using System.Collections.Generic;

    using Yupi.Messages.Encoders;
    using Yupi.Model.Domain;
    using Yupi.Net;
    using Yupi.Protocol.Buffers;

    public class UserTagsMessageComposer : Yupi.Messages.Contracts.UserTagsMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, UserInfo info)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(info.Id);
                message.Append(info.Tags);
                session.Send(message);
            }
        }

        #endregion Methods
    }
}