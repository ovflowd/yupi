using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Other
{
	public class CameraPurchaseOk : AbstractComposerVoid
	{
		public override void Compose (Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				session.Send (message);
			}
		}
	}
}

