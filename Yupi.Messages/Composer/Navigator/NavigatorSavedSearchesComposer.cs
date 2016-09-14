using System.Collections.Generic;
using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Navigator
{
    public class NavigatorSavedSearchesComposer : Contracts.NavigatorSavedSearchesComposer
    {
        public override void Compose(ISender session, IList<UserSearchLog> searchLog)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(searchLog.Count);

                foreach (var entry in searchLog)
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