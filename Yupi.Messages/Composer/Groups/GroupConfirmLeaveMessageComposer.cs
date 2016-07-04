using System;
using Yupi.Emulator.Game.Groups.Structs;
using Yupi.Emulator.Game.Users;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Groups
{
	public class GroupConfirmLeaveMessageComposer : AbstractComposer
	{
		public override void Compose (Yupi.Protocol.ISender session, Habbo user, Group group, int type)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(group.Id);
				message.AppendInteger(type);
				message.AppendInteger(user.Id);
				message.AppendString(user.UserName);
				message.AppendString(user.Look);
				message.AppendString(string.Empty);
				session.Send (message);
			}
		}
	}
}

