using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.GameCenter
{
	public class GameCenterLoadGameUrlMessageComposer : AbstractComposerVoid
	{
		public override void Compose ( Yupi.Protocol.ISender session)
		{
			// TODO  hardcoded message
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(18);
				message.AppendString(Convert.ToString(Yupi.GetUnixTimeStamp()));
				message.AppendString(ServerExtraSettings.GameCenterStoriesUrl);
				session.Send (message);
			}
		}
	}
}

