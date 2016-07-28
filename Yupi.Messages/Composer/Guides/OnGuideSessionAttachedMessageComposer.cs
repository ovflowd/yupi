using System;
using Yupi.Protocol.Buffers;


namespace Yupi.Messages.Guides
{
	public class OnGuideSessionAttachedMessageComposer : AbstractComposer
	{
		// TODO Find the meaning of val1 & val2
		public void Compose( Yupi.Protocol.ISender session, bool val1, int userId, string message, int val2) {
			using (ServerMessage response = Pool.GetMessageBuffer (Id)) {
				response.AppendBool(false);
				response.AppendInteger(userId);
				response.AppendString(message);
				response.AppendInteger(30);
				session.Send (response);
			}
		}
	}
}

