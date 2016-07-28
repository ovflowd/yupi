using System;

using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Items
{
	public class LoveLockDialogueMessageComposer : AbstractComposer<ISender, LovelockItem>
	{
		public override void Compose (Yupi.Protocol.ISender user1, Yupi.Protocol.ISender user2, LovelockItem loveLock)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(loveLock.Id);
				message.AppendBool(true);

				// TODO use loveLock.InteractingUser
				user1.Send (message);
				user2.Send (message);
			}
		}
	}
}

