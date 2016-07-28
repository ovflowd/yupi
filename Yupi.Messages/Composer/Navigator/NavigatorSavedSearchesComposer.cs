using System;
using System.Collections.Generic;

using Yupi.Protocol.Buffers;
using Yupi.Emulator.Game.Browser.Models;

namespace Yupi.Messages.Navigator
{
	public class NavigatorSavedSearchesComposer : AbstractComposer<IList<UserSearchLog>>
	{
		public override void Compose (Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, IList<UserSearchLog> searchLog)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(searchLog.Count);

				foreach (UserSearchLog entry in searchLog)
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

