using System;
using System.Collections.Specialized;
using Yupi.Protocol.Buffers;
using Yupi.Emulator.Game.RoomBots;

namespace Yupi.Messages.Bots
{
	public class BotInventoryMessageComposer : AbstractComposer<HybridDictionary>
	{
		public override void Compose (Yupi.Protocol.ISender session, HybridDictionary items)
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

