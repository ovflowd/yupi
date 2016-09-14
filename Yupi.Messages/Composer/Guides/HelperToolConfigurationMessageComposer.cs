using System;

using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Guides
{
	public class HelperToolConfigurationMessageComposer : Yupi.Messages.Contracts.HelperToolConfigurationMessageComposer
	{
		public override void Compose( Yupi.Protocol.ISender session, bool onDuty, int guideCount, int helperCount, int guardianCount) {
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendBool(onDuty);
				message.AppendInteger(guideCount);
				message.AppendInteger(helperCount);
				message.AppendInteger(guardianCount);
				session.Send (message);
			}
		}
	}
}

