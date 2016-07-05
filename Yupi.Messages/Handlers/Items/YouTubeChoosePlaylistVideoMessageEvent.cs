using System;

namespace Yupi.Messages.Items
{
	public class YouTubeChoosePlaylistVideoMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			throw new NotImplementedException ();
		}
	}
}

