using System;
using System.Collections.Generic;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Catalog
{
	public class GroupFurniturePageMessageComposer : AbstractComposer<HashSet<GroupMember>>
	{
		public override void Compose (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, HashSet<GroupMember> userGroups)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger (userGroups.Count);

				// TODO Refactor groups
				foreach (GroupMember member in userGroups) {
					Group habboGroup = Yupi.GetGame ().GetGroupManager ().GetGroup (member.GroupId);

					message.AppendInteger(habboGroup.Id);
					message.AppendString(habboGroup.Name);
					message.AppendString(habboGroup.Badge);
					message.AppendString(Yupi.GetGame().GetGroupManager().SymbolColours.Contains(habboGroup.Colour1)
						? ((GroupSymbolColours)
							Yupi.GetGame().GetGroupManager().SymbolColours[habboGroup.Colour1]).Colour
						: "4f8a00");
					message.AppendString(
						Yupi.GetGame().GetGroupManager().BackGroundColours.Contains(habboGroup.Colour2)
						? ((GroupBackGroundColours)
							Yupi.GetGame().GetGroupManager().BackGroundColours[habboGroup.Colour2]).Colour
						: "4f8a00");
					message.AppendBool(habboGroup.CreatorId == Session.GetHabbo().Id);
					message.AppendInteger(habboGroup.CreatorId);
					message.AppendBool(habboGroup.Forum.Id != 0);
				}

				session.Send (message);
			}
		}
	}
}

