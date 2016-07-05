using System;
using System.Collections.Generic;

using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Navigator
{
	public class NavigatorSavedSearchesComposer : AbstractComposer<Dictionary<int, UserSearchLog>>
	{
		public override void Compose (Yupi.Protocol.ISender session, Dictionary<int, UserSearchLog> searchLog)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(searchLog.Count);

				foreach (UserSearchLog entry in searchLog.Values)
				{
					message.AppendInteger(entry.Id);
					message.AppendString(entry.Value1);
					message.AppendString(entry.Value2);
					message.AppendString(string.Empty);
				}
				session.Send (message);
			}
		}
	}
}

