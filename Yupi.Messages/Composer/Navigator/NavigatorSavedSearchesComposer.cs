using System;
using System.Collections.Generic;
using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Navigator
{
    public class NavigatorSavedSearchesComposer : Yupi.Messages.Contracts.NavigatorSavedSearchesComposer
    {
        public override void Compose(Yupi.Protocol.ISender session, IList<UserSearchLog> searchLog)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(searchLog.Count);

                foreach (UserSearchLog entry in searchLog)
                {
                    message.AppendInteger(entry.Id);
                    message.AppendString(entry.Value1);
                    message.AppendString(entry.Value2);
                    message.AppendString(string.Empty);
                }
                session.Send(message);
            }
        }
    }
}