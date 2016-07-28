using System;

namespace Yupi.Messages.Messenger
{
	public class DeleteFriendMessageEvent : AbstractHandler
	{
		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			if (session.GetHabbo().GetMessenger() == null) return;

			int count = request.GetInteger();
			for (int i = 0; i < count; i++)
			{
				uint friendId = request.GetUInt32();
				if (session.GetHabbo().Relationships.ContainsKey(Convert.ToInt32(friendId)))
				{
					session.SendNotif(Yupi.GetLanguage().GetVar("buddy_error_1"));
					return;
				}
				session.GetHabbo().GetMessenger().DestroyFriendship(friendId);
			}
		}
	}
}

