using System;

using System.Data;
using Yupi.Protocol.Buffers;
using Yupi.Model;
using System.Collections.Generic;

namespace Yupi.Messages.User
{
	public class LoadWardrobeMessageComposer : Yupi.Messages.Contracts.LoadWardrobeMessageComposer
	{
		public override void Compose (Yupi.Protocol.ISender session, IList<WardrobeItem> wardrobe)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger (wardrobe.Count);
				foreach (WardrobeItem item in wardrobe) {
					message.AppendInteger (item.Slot);
					message.AppendString (item.Look);
					message.AppendString (item.Gender.ToUpper());
				}

				session.Send (message);
			}

		}
	}
}

