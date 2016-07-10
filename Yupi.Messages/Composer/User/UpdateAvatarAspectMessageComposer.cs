using System;

using Yupi.Protocol.Buffers;
using Yupi.Model.Users;


namespace Yupi.Messages.User
{
	public class UpdateAvatarAspectMessageComposer : AbstractComposer<Habbo>
	{
		public override void Compose (Yupi.Protocol.ISender session, Habbo habbo)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendString (habbo.Look);
				message.AppendString (habbo.Gender.ToUpper()); // TODO Make sure gender is stored UPPER
				session.Send (message);
			}
		}
	}
}

