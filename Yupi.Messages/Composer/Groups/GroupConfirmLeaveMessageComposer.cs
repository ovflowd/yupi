using System;


using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Groups
{
	public class GroupConfirmLeaveMessageComposer : AbstractComposer<Habbo, Group, int>
	{
		public override void Compose (Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Habbo user, Group group, int type)
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

