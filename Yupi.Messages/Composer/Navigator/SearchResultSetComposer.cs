namespace Yupi.Messages.Navigator
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    using Yupi.Controller;
    using Yupi.Messages.Encoders;
    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Model.Repository;
    using Yupi.Protocol.Buffers;

    // TODO Refactor
    public class SearchResultSetComposer : Yupi.Messages.Contracts.SearchResultSetComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, string staticId, string query,
            IDictionary<NavigatorCategory, IList<RoomData>> results)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendString(staticId);
                message.AppendString(query);

                message.AppendInteger(results.Count);

                foreach (var result in results)
                {
                    message.AppendString(staticId);
                    message.AppendString(result.Key.Caption);
                    message.AppendInteger(1); // TODO actionAllowed ( 1 = Show More, 2 = Back)
                    message.AppendBool(!result.Key.IsOpened);
                    message.AppendInteger(result.Key.IsImage); // TODO ViewMode (Possible Values?)

                    // Room Count
                    message.AppendInteger(result.Value.Count);

                    foreach (RoomData roomData in result.Value)
                    {
                        message.Append(roomData);
                    }
                }

                session.Send(message);
            }
        }

        #endregion Methods
    }
}