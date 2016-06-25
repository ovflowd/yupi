using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Navigator
{
	public class SearchResultSetComposer : AbstractComposer
	{
		// TODO Refactor
		public void Compose (Yupi.Protocol.ISender session, string value1, string value2)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendString(value1);
				message.AppendString(value2);
				message.AppendInteger(value2.Length > 0 ? 1 : Yupi.GetGame().GetNavigator().GetNewNavigatorLength(value1));

				if (value2.Length > 0)
					SearchResultList.SerializeSearches(value2, message, session);
				else
					SearchResultList.SerializeSearchResultListStatics(value1, true, message, session);

				session.Send (message);
			}
		}
	}
}

