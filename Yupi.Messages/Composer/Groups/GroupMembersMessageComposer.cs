using System;
using Yupi.Emulator.Game.Groups.Structs;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Groups
{
	public class GroupMembersMessageComposer : AbstractComposer<Group>
	{
		public override void Compose (Yupi.Protocol.ISender session, Group group)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				Yupi.GetGame().GetGroupManager().SerializeGroupMembers(message, group, 0u, session);
				session.Send (message);
			}
		}
	}
}

