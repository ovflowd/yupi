using System;
using Yupi.Protocol.Buffers;

using System.Collections.Generic;
using System.Linq;

namespace Yupi.Messages.Other
{
	public class FigureSetIdsMessageComposer : AbstractComposerVoid
	{
		// TODO Refactor
		public override void Compose ( Yupi.Protocol.ISender session)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				List<string> clothing = session.GetHabbo().ClothesManagerManager.Clothing;

				message.StartArray();

				foreach (
					ClothingItem item1 in
					clothing.Select(clothing1 => Yupi.GetGame().GetClothingManager().GetClothesInFurni(clothing1)))
				{
					foreach (int clothe in item1.Clothes)
						message.AppendInteger(clothe);

					message.SaveArray();
				}

				message.EndArray();
				message.StartArray();

				foreach (
					ClothingItem item2 in
					clothing.Select(clothing2 => Yupi.GetGame().GetClothingManager().GetClothesInFurni(clothing2)))
				{
					foreach (int clothe in item2.Clothes)
						message.AppendString(item2.ItemName);

					message.SaveArray();
				}

				message.EndArray();

				session.Send (message);
			}
		}
	}
}

