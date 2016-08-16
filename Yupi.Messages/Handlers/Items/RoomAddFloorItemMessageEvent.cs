using System;








using Yupi.Messages.Notification;

namespace Yupi.Messages.Items
{
	public class RoomAddFloorItemMessageEvent : AbstractHandler
	{
		// TODO Refactor
		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			/*
			try
			{
				Yupi.Messages.Rooms room = Yupi.GetGame().GetRoomManager().GetRoom(session.GetHabbo().CurrentRoomId);

				if (room == null || Yupi.GetDbConfig().DbData["placing_enabled"] != "1")
					return;

				if (!room.CheckRights(session, false, true))
				{
					router.GetComposer<SuperNotificationMessageComposer>().Compose(session, "", "${room.error.cant_set_not_owner}", "", "", "furni_placement_error", 1);
					return;
				}

				string placementData = request.GetString();
				string[] dataBits = placementData.Split(' ');

				uint itemId = uint.Parse(dataBits[0].Replace("-", string.Empty));

				UserItem item = session.GetHabbo().GetInventoryComponent().GetItem(itemId);

				if (item == null)
					return;

				// TODO Use enums!
				string type = dataBits[1].StartsWith(":") ? "wall" : "floor";
				int x, y, rot;
				double z;

				switch (type)
				{
				case "wall":
					{
						switch (item.BaseItem.InteractionType)
						{
						case Interaction.Dimmer:
							{
								if (room.MoodlightData != null &&
									room.GetRoomItemHandler().GetItem(room.MoodlightData.ItemId) != null)
									session.SendNotif(Yupi.GetLanguage().GetVar("room_moodlight_one_allowed"));
								goto PlaceWall;
							}
						default:
							{
								goto PlaceWall;
							}
						}
					}
				case "floor":
					{
						x = int.Parse(dataBits[1]);
						y = int.Parse(dataBits[2]);
						rot = int.Parse(dataBits[3]);
						z = room.GetGameMap().SqAbsoluteHeight(x, y);

						if (z >= 100)
							goto CannotSetItem;

						switch (item.BaseItem.InteractionType)
						{
						case Interaction.BreedingTerrier:
						case Interaction.BreedingBear:
							{
								RoomItem roomItemBreed = new RoomItem(item.Id, room.RoomId, item.BaseItem.Name,
									item.ExtraData,
									x, y, z, rot, room, session.GetHabbo().Id, 0, string.Empty, false);

								if (item.BaseItem.InteractionType == Interaction.BreedingTerrier)
								if (!room.GetRoomItemHandler().BreedingTerrier.ContainsKey(roomItemBreed.Id))
									room.GetRoomItemHandler().BreedingTerrier.Add(roomItemBreed.Id, roomItemBreed);
								else if (!room.GetRoomItemHandler().BreedingBear.ContainsKey(roomItemBreed.Id))
									room.GetRoomItemHandler().BreedingBear.Add(roomItemBreed.Id, roomItemBreed);
								goto PlaceFloor;
							}
						case Interaction.Alert:
						case Interaction.VendingMachine:
						case Interaction.ScoreBoard:
						case Interaction.Bed:
						case Interaction.PressurePadBed:
						case Interaction.Trophy:
						case Interaction.RoomEffect:
						case Interaction.PostIt:
						case Interaction.Gate:
						case Interaction.None:
						case Interaction.HcGate:
						case Interaction.Teleport:
						case Interaction.QuickTeleport:
						case Interaction.Guillotine:
							{
								goto PlaceFloor;
							}
						case Interaction.Hopper:
							{
								if (room.GetRoomItemHandler().HopperCount > 0)
									return;
								goto PlaceFloor;
							}
						case Interaction.FreezeTile:
							{
								if (!room.GetGameMap().SquareHasFurni(x, y, Interaction.FreezeTile))
									goto PlaceFloor;
								goto CannotSetItem;
							}
						case Interaction.FreezeTileBlock:
							{
								if (!room.GetGameMap().SquareHasFurni(x, y, Interaction.FreezeTileBlock))
									goto PlaceFloor;
								goto CannotSetItem;
							}
						case Interaction.Toner:
							{
								TonerData tonerData = room.TonerData;
								if (tonerData != null && room.GetRoomItemHandler().GetItem(tonerData.ItemId) != null)
								{
									session.SendNotif(Yupi.GetLanguage().GetVar("room_toner_one_allowed"));
									return;
								}
								// TODO GOTO HELL!
								goto PlaceFloor;
							}
						default:
							{
								goto PlaceFloor;
							}
						}
					}
				}

				PlaceWall:

				WallCoordinate coordinate = new WallCoordinate(":" + placementData.Split(':')[1]);

				RoomItem roomItemWall = new RoomItem(item.Id, room.RoomId, item.BaseItem.Name, item.ExtraData,
					coordinate, room, session.GetHabbo().Id, item.GroupId, false);

				if (room.GetRoomItemHandler().SetWallItem(session, roomItemWall))
					session.GetHabbo().GetInventoryComponent().RemoveItem(itemId, true);

				return;

				PlaceFloor:

				RoomItem roomItem = new RoomItem(item.Id, room.RoomId, item.BaseItem.Name, item.ExtraData, x, y, z, rot, room,
					session.GetHabbo().Id, item.GroupId, item.SongCode, false);

				if (room.GetRoomItemHandler().SetFloorItem(session, roomItem, x, y, rot, true, false, true))
				{
					session.GetHabbo().GetInventoryComponent().RemoveItem(itemId, true);

					if (roomItem.IsWired)
					{
						IWiredItem item5 = room.GetWiredHandler().GenerateNewItem(roomItem);

						room.GetWiredHandler().AddWired(item5);

						WiredHandler.SaveWired(item5);
					}
				}

				Yupi.GetGame()
					.GetAchievementManager()
					.ProgressUserAchievement(session, "ACH_RoomDecoFurniCount", 1, true);
				return;

				CannotSetItem:
				router.GetComposer<SuperNotificationMessageComposer>().Compose(session, "", "${room.error.cant_set_item}", "", "", "furni_placement_error", 1);
			}
			catch (Exception e)
			{
				router.GetComposer<SuperNotificationMessageComposer>().Compose(session, "", "${room.error.cant_set_item}", "", "", "furni_placement_error", 1);
				YupiLogManager.LogException(e, "Failed Handling Item.", "Yupi.Mobi");
			}*/
			throw new NotImplementedException ();
		}
	}
}

