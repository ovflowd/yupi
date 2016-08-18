using System;
using Yupi.Protocol.Buffers;

using System.Collections.Generic;
using System.Linq;
using Yupi.Model.Domain;

namespace Yupi.Messages.Other
{
	public class FigureSetIdsMessageComposer : Yupi.Messages.Contracts.FigureSetIdsMessageComposer
	{
		// TODO Refactor
		public override void Compose ( Yupi.Protocol.ISender session)
		{
			throw new NotImplementedException ();
			/*
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				List<string> clothing = session.GetHabbo().ClothesManagerManager.Clothing;

				message.AppendInteger (clothing.Count);

				foreach (
					ClothingItem item1 in
					clothing.Select(clothing1 => Yupi.GetGame().GetClothingManager().GetClothesInFurni(clothing1)))
				{
					foreach (int clothe in item1.Clothes)
						message.AppendInteger(clothe);
				}

				message.AppendInteger (clothing.Count);

				foreach (
					ClothingItem item2 in
					clothing.Select(clothing2 => Yupi.GetGame().GetClothingManager().GetClothesInFurni(clothing2)))
				{
					foreach (int clothe in item2.Clothes)
						message.AppendString(item2.ItemName);
				}

				session.Send (message);
			}
			*/
		}
	}
}

