using System;

namespace Yupi.Messages.Messenger
{
	public class DeclineFriendMessageEvent : AbstractHandler
	{
		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			if (session.GetHabbo().GetMessenger() == null) return;

			// TODO variable name
			bool flag = request.GetBool();

			request.GetInteger(); // TODO Unused

			if (flag) {
				session.GetHabbo ().GetMessenger ().HandleAllRequests ();
			} else {
				uint sender = request.GetUInt32();
				session.GetHabbo().GetMessenger().HandleRequest(sender);
			}
		}
	}
}

