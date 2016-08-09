using System;


using System.Collections.Generic;
using Yupi.Protocol.Buffers;


using System.Globalization;
using Yupi.Model.Domain;
using Yupi.Model.Domain.Components;

namespace Yupi.Messages.Items
{
	public class LoadInventoryMessageComposer : Yupi.Messages.Contracts.LoadInventoryMessageComposer
	{
		public override void Compose ( Yupi.Protocol.ISender session, Inventory inventory)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(1);
				message.AppendInteger(0);
				message.AppendInteger(floor.Count + wall.Count + songDisks.Count);

				foreach (UserItem userItem in floor)
				{
					SerializeFloor(message, userItem);
				}

				foreach (UserItem userItem in wall)
				{
					message.AppendInteger(item.Id);
					message.AppendString(userItem.BaseItem.Type.ToString().ToUpper());
					message.AppendInteger(item.Id);
					message.AppendInteger(userItem.BaseItem.SpriteId);

					if (userItem.BaseItem.Name.Contains("a2") || userItem.BaseItem.Name == "floor")
						message.AppendInteger(3);
					else if (userItem.BaseItem.Name.Contains("wallpaper") && userItem.BaseItem.Name != "wildwest_wallpaper")
						message.AppendInteger(2);
					else if (userItem.BaseItem.Name.Contains("landscape"))
						message.AppendInteger(4);
					else
						message.AppendInteger(1);

					message.AppendInteger(0);
					message.AppendString(userItem.ExtraData);
					message.AppendBool(userItem.BaseItem.AllowRecycle);
					message.AppendBool(userItem.BaseItem.AllowTrade);
					message.AppendBool(userItem.BaseItem.AllowInventoryStack);
					message.AppendBool(false); //SELLABLE_ICON
					message.AppendInteger(-1); //secondsToExpiration
					message.AppendBool(true); //hasRentPeriodStarted
					message.AppendInteger(-1); //flatId
				}

				foreach (UserItem userItem in songDisks)
				{
					SerializeFloor(message, userItem);
				}
				session.Send (message);
			}
		}

		// TODO Refactor
		private void SerializeFloor(ServerMessage message, UserItem item)
		{
			message.AppendInteger(item.Id);
			message.AppendString(item.BaseItem.Type.ToString(CultureInfo.InvariantCulture).ToUpper());
			message.AppendInteger(item.Id);
			message.AppendInteger(item.BaseItem.SpriteId);
			int extraParam = 0;

			try
			{
				if (item.BaseItem.InteractionType == Interaction.Gift)
				{
					string[] split = item.ExtraData.Split((char) 9);
					int ribbon, color;
					int.TryParse(split[2], out ribbon);
					int.TryParse(split[3], out color);
					extraParam = ribbon*1000 + color;
				}
			}
			catch
			{
				extraParam = 1001;
			}

			message.AppendInteger(extraParam);

			if (item.BaseItem.IsGroupItem)
			{
				Group group = Yupi.GetGame().GetGroupManager().GetGroup(item.GroupId);

				if (group != null)
				{
					message.AppendInteger(2);
					message.AppendInteger(5);
					message.AppendString(item.ExtraData);
					message.AppendString(group.Id.ToString(CultureInfo.InvariantCulture));
					message.AppendString(group.Badge);
					message.AppendString(Yupi.GetGame().GetGroupManager().GetGroupColour(group.Colour1, true));
					message.AppendString(Yupi.GetGame().GetGroupManager().GetGroupColour(group.Colour2, false));
				}
				else
				{
					message.AppendInteger(0);
					message.AppendString(string.Empty);
				}
			}
			else if (item.LimitedStack > 0)
			{
				message.AppendString(string.Empty);
				message.AppendBool(true);
				message.AppendBool(false);
				message.AppendString(item.ExtraData);
			}
			else if ((item.BaseItem.InteractionType == Interaction.Moplaseed) &&
				(item.BaseItem.InteractionType == Interaction.RareMoplaSeed))
			{
				message.AppendInteger(1);
				message.AppendInteger(1);
				message.AppendString("rarity");
				message.AppendString(ExtraData);
			}
			else
			{
				switch (item.BaseItem.InteractionType)
				{
				case Interaction.BadgeDisplay:
					string[] extra = item.ExtraData.Split('|');
					message.AppendInteger(2);
					message.AppendInteger(4);
					message.AppendString("0");
					message.AppendString(extra[0]);
					message.AppendString(extra.Length > 1 ? extra[1] : "");
					message.AppendString(extra.Length > 1 ? extra[2] : "");
					break;

				case Interaction.YoutubeTv:
					message.AppendInteger(1);
					message.AppendInteger(1);
					message.AppendString("THUMBNAIL_URL");
					message.AppendString(item.ExtraData);
					break;

				case Interaction.Mannequin:
					message.AppendInteger(1);

					if (!item.ExtraData.Contains('\u0005'.ToString()))
					{
						message.AppendInteger(3); // Count Of Values
						message.AppendString("GENDER");
						message.AppendString("M");
						message.AppendString("FIGURE");
						message.AppendString(string.Empty);
						message.AppendString("OUTFIT_NAME");
						message.AppendString(string.Empty);
					}
					else
					{
						string[] extradatas = item.ExtraData.Split('\u0005');

						message.AppendInteger(3); // Count Of Values
						message.AppendString("GENDER");
						message.AppendString(extradatas[0]);
						message.AppendString("FIGURE");
						message.AppendString(extradatas[1]);
						message.AppendString("OUTFIT_NAME");
						message.AppendString(extradatas[2]);
					}
					break;

				default:
					message.AppendInteger(0);
					if (!item.BaseItem.IsGroupItem)
						message.AppendString(item.ExtraData);
					break;
				}
			}

			if (item.LimitedSellId > 0)
			{
				message.AppendInteger(item.LimitedSellId);
				message.AppendInteger(item.LimitedStack);
			}

			/* messageBuffer.AppendInteger((BaseItem.InteractionType == InteractionType.gift) ? 9 : 0);
                messageBuffer.AppendInteger(0);
                messageBuffer.AppendString((BaseItem.InteractionType == InteractionType.gift)
                    ? string.Empty
                    : ExtraData);*/

			message.AppendBool(item.BaseItem.AllowRecycle);
			message.AppendBool(item.BaseItem.AllowTrade);
			message.AppendBool(item.LimitedSellId <= 0 && item.BaseItem.AllowInventoryStack);
			message.AppendBool(false); // sellable
			message.AppendInteger(-1); // expireTime
			message.AppendBool(true); // hasRentPeriodStarted
			message.AppendInteger(-1); // flatId

			if (item.BaseItem.Type == 's') {
				message.AppendString (string.Empty); //slotId
				message.AppendInteger (0);
			}
		}
	}
}

