using System;

using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;


namespace Yupi.Messages.Groups
{
	public class ChangeFavouriteGroupMessageComposer : AbstractComposer<Group, int>
	{
		// TODO Refactor
		public override void Compose (Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Group group, int virtualId)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(virtualId);

				if (group == null) {
					message.AppendInteger (-1);
					message.AppendInteger (-1);
					message.AppendString (string.Empty);
				} else {
					message.AppendInteger (group.Id);
					message.AppendInteger (3); // TODO Hardcoded
					message.AppendString (group.Name);

				}
				session.Send (message);
			}
		}
	}
}

