using System;
using Yupi.Protocol.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Yupi.Model.Domain;
using Yupi.Controller;
using Yupi.Model.Repository;
using Yupi.Model;
using Yupi.Messages.Encoders;


namespace Yupi.Messages.Navigator
{
	// TODO Refactor
	public class SearchResultSetComposer : Yupi.Messages.Contracts.SearchResultSetComposer
	{
		public override void Compose (Yupi.Protocol.ISender session, string staticId, string query, IList<SearchResultEntry> results)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendString (staticId);
				message.AppendString (query);

				message.AppendInteger (results.Count);

				foreach (var result in results) {
					message.AppendString (staticId);
					message.AppendString (result.Category.Caption);
					message.AppendInteger (1); // TODO actionAllowed ( 1 = Show More, 2 = Back)
					message.AppendBool (!result.Category.IsOpened);
					message.AppendInteger (result.Category.IsImage); // TODO ViewMode (Possible Values?)

					// Room Count
					message.AppendInteger (result.Rooms.Count);

					foreach (RoomData roomData in result.Rooms) {
						message.Append (roomData);
					}
				}

				session.Send (message);
			}
		}
	}
}

