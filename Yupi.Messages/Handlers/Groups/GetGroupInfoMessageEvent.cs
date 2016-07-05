using System;


namespace Yupi.Messages.Groups
{
	public class GetGroupInfoMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Router router)
		{
			uint groupId = request.GetUInt32();
			bool newWindow = request.GetBool();

			Group group = Yupi.GetGame().GetGroupManager().GetGroup(groupId);

			if (group == null)
				return;

			router.GetComposer<GroupDataMessageComposer> ().Compose (session, group, session.GetHabbo(), newWindow);
		}
	}
}

