using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Camera
{
	public class CameraStorageUrlMessageComposer : Yupi.Messages.Contracts.CameraStorageUrlMessageComposer
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

