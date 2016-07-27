using System;
using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;


namespace Yupi.Messages.Youtube
{
	public class YouTubeLoadVideoMessageComposer : AbstractComposer<YoutubeTVItem>
	{
		public override void Compose (Yupi.Protocol.ISender session, YoutubeTVItem tv)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(tv.Id);
				message.AppendString(tv.PlayingVideo.Id);
				message.AppendInteger(0); // TODO Probably strings (desc?)
				message.AppendInteger(0);
				message.AppendInteger(0);
				session.Send (message);
			}
		}
	}
}

