using System;
using System.Collections.Specialized;
using Yupi.Protocol.Buffers;


namespace Yupi.Messages.Bots
{
	public class BotInventoryMessageComposer : AbstractComposer<HybridDictionary>
	{
		public override void Compose (Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, HybridDictionary items)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(items.Count);
				foreach (RoomBot current in items.Values)
				{
					message.AppendInteger(current.BotId);
					message.AppendString(current.Name);
					message.AppendString(current.Motto);
					message.AppendString(current.Gender.ToLower()); 
					message.AppendString(current.Look);
				}
				session.Send (message);
			}
		}
	}
}

