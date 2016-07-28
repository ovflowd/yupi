using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Camera
{
	public class CameraStorageUrlMessageComposer : AbstractComposer<string>
	{
		public override void Compose ( Yupi.Protocol.ISender session, string url)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendString (url);
				session.Send (message);
			}
		}
	}
}

