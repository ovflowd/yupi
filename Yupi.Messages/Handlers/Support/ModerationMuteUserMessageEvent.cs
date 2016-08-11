using System;


namespace Yupi.Messages.Support
{
	public class ModerationMuteUserMessageEvent : AbstractHandler
	{
		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			if (!session.UserData.Info.HasPermission("fuse_mute"))
				return;

			uint userId = request.GetUInt32();
			string message = request.GetString();
			GameClient clientByUserId = Yupi.GetGame().GetClientManager().GetClientByUserId(userId);

			clientByUserId.GetHabbo().Mute();
			clientByUserId.SendNotif(message);
		}
	}
}

