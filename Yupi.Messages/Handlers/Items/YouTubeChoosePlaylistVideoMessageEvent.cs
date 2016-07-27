using System;

namespace Yupi.Messages.Items
{
	public class YouTubeChoosePlaylistVideoMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Net.ISession<IGameClient> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			throw new NotImplementedException ();
		}
	}
}

