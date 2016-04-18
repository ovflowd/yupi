using System;
using Yupi.Protocol.Buffers;
using Yupi.Emulator.Game.Groups.Structs;

namespace Yupi.Messages.Groups
{
	public class GroupForumThreadUpdateMessageComposer : AbstractComposer
	{
		public override void Compose (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, int groupId, GroupForumPost thread, bool pin, bool Lock)
		{
			// TODO Hardcoded message
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(groupId);
				message.AppendInteger(thread.Id);
				message.AppendInteger(thread.PosterId);
				message.AppendString(thread.PosterName);
				message.AppendString(thread.Subject);
				message.AppendBool(pin);
				message.AppendBool(Lock);
				message.AppendInteger(Yupi.GetUnixTimeStamp() - thread.Timestamp);
				message.AppendInteger(thread.MessageCount + 1);
				message.AppendInteger(0);
				message.AppendInteger(0);
				message.AppendInteger(1);
				message.AppendString(string.Empty);
				message.AppendInteger(Yupi.GetUnixTimeStamp() - thread.Timestamp);
				message.AppendByte(thread.Hidden ? 10 : 1);
				message.AppendInteger(1);
				message.AppendString(thread.Hider);
				message.AppendInteger(0);
				session.Send (message);
			}
		}
	}
}

