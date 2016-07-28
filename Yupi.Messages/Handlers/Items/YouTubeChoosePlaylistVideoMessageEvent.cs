using System;

namespace Yupi.Messages.Items
{
	public class YouTubeChoosePlaylistVideoMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			throw new NotImplementedException ();
		}
	}
}

