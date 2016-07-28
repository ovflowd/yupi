using System;
using System.Collections.Generic;
using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Items
{
	public class EffectsInventoryMessageComposer : AbstractComposer<List<AvatarEffect>>
	{
		public override void Compose ( Yupi.Protocol.ISender session, List<AvatarEffect> effects)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(effects.Count);

				foreach (AvatarEffect current in effects)
				{
					message.AppendInteger(current.EffectId);
					message.AppendInteger(current.Type);
					message.AppendInteger(current.TotalDuration);
					message.AppendInteger(0);
					message.AppendInteger(current.TimeLeft);
					message.AppendBool(current.TotalDuration == -1);
				}
				session.Send (message);
			}
		}
	}
}

