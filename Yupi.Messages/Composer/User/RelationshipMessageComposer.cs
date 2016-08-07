using System;

using System.Linq;
using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;


namespace Yupi.Messages.User
{
	public class RelationshipMessageComposer : Yupi.Messages.Contracts.RelationshipMessageComposer
	{
		public override void Compose ( Yupi.Protocol.ISender session, UserInfo habbo)
		{
			// TODO Refactor

			int num = habbo.Relationships.Count(x => x.Value.Type == 1);
			int num2 = habbo.Relationships.Count(x => x.Value.Type == 2);
			int num3 = habbo.Relationships.Count(x => x.Value.Type == 3);

			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(habbo.Id);
				message.AppendInteger(habbo.Relationships.Count);
				foreach (Relationship current in habbo.Relationships.Values)
				{
					// TODO There should be no need to convert!!!!
					UserInfo habboForId2 = Yupi.GetHabboById(Convert.ToUInt32(current.UserId));
					if (habboForId2 == null)
					{
						message.AppendInteger(0);
						message.AppendInteger(0);
						message.AppendInteger(0);
						message.AppendString("Placeholder");
						message.AppendString("hr-115-42.hd-190-1.ch-215-62.lg-285-91.sh-290-62");
					}
					else
					{
						message.AppendInteger(current.Type);
						message.AppendInteger(current.Type == 1 ? num : (current.Type == 2 ? num2 : num3));
						message.AppendInteger(current.UserId);
						message.AppendString(habboForId2.UserName);
						message.AppendString(habboForId2.Look);
					}
				}

				session.Send (message);
			}
		}
	}
}

