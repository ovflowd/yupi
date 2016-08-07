using System;

using Yupi.Protocol.Buffers;
using System.Globalization;
using Yupi.Model.Domain;

namespace Yupi.Messages.User
{
	public class UserObjectMessageComposer : Yupi.Messages.Contracts.UserObjectMessageComposer
	{
		public override void Compose ( Yupi.Protocol.ISender session, UserInfo habbo)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(habbo.Id);
				message.AppendString(habbo.UserName);
				message.AppendString(habbo.Look);
				message.AppendString(habbo.Gender.ToUpper());
				message.AppendString(habbo.Motto);
				message.AppendString(string.Empty);
				message.AppendBool(false);
				message.AppendInteger(habbo.Respect);
				message.AppendInteger(habbo.DailyRespectPoints);
				message.AppendInteger(habbo.DailyPetRespectPoints);
				message.AppendBool(true);
				message.AppendString(habbo.LastOnline.ToString(CultureInfo.InvariantCulture));
				message.AppendBool(habbo.CanChangeName);
				message.AppendBool(false);

				session.Send (message);
			}
		}
	}
}

