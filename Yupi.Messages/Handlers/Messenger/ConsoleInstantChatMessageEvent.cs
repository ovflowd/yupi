using System;

namespace Yupi.Messages.Messenger
{
	// TODO Rename?
	public class ConsoleInstantChatMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Net.ISession<IGameClient> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			uint toId = request.GetUInt32();
			string text = request.GetString();

			if (session.GetHabbo().GetMessenger() == null || string.IsNullOrWhiteSpace (text))
				return;
			

		    session.GetHabbo ().GetMessenger ().SendInstantMessage (toId, text);
		}
	}
}

