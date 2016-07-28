using System;


namespace Yupi.Messages.Navigator
{
	public class NewNavigatorAddSavedSearchEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			if (session.GetHabbo().NavigatorLogs.Count > 50)
			{
				session.SendNotif(Yupi.GetLanguage().GetVar("navigator_max"));

				return;
			}

			// TODO Refactor
			string value1 = request.GetString();

			string value2 = request.GetString();

			UserSearchLog naviLogs = new UserSearchLog(session.GetHabbo().NavigatorLogs.Count, value1, value2);

			if (!session.GetHabbo().NavigatorLogs.ContainsKey(naviLogs.Id))
				session.GetHabbo().NavigatorLogs.Add(naviLogs.Id, naviLogs);

			session.Send(NavigatorSavedSearchesComposer.Compose(session));
		}
	}
}

