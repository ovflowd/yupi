using System;

using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;


namespace Yupi.Messages.Messenger
{
	public class FriendUpdateMessageComposer : AbstractComposer<Relationship>
	{
		public override void Compose ( Yupi.Protocol.ISender session, Relationship relationship)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(0);
				message.AppendInteger(1);
				message.AppendInteger(0);
				message.AppendInteger(relationship.Friend.Id);
				message.AppendString(relationship.Friend.UserName);
				message.AppendInteger(relationship.Friend.IsOnline);
				message.AppendBool(!relationship.Friend.AppearOffline && relationship.Friend.IsOnline);
				message.AppendBool(!relationship.Friend.HideInRoom && relationship.Friend.InRoom);
				message.AppendString(relationship.Friend.Look);
				message.AppendInteger(0);
				message.AppendString(relationship.Friend.Motto);
				message.AppendString(string.Empty);
				message.AppendString(string.Empty);
				message.AppendBool(true);
				message.AppendBool(false);
				message.AppendBool(false);
				message.AppendShort(relationship.Type);
				message.AppendBool(false);
				session.Send (message);
			}
		}
	}
}

