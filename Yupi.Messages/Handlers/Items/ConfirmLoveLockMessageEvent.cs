using System;

namespace Yupi.Messages.Items
{
	public class ConfirmLoveLockMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Router router)
		{
			uint pId = Request.GetUInt32();
			bool confirmLoveLock = Request.GetBool();

			Room room = Session.GetHabbo().CurrentRoom;

			RoomItem item = room?.GetRoomItemHandler().GetItem(pId);
			if (item == null || item.GetBaseItem().InteractionType != Interaction.LoveShuffler)
				return;

			uint userIdOne = item.InteractingUser;
			uint userIdTwo = item.InteractingUser2;
			RoomUser userOne = room.GetRoomUserManager().GetRoomUserByHabbo(userIdOne);
			RoomUser userTwo = room.GetRoomUserManager().GetRoomUserByHabbo(userIdTwo);

			if (userOne == null && userTwo == null)
			{
				item.InteractingUser = 0;
				item.InteractingUser2 = 0;
				return;
			}

			if (userOne == null)
			{
				userTwo.CanWalk = true;
				userTwo.GetClient().SendNotif("Your partner has left the room or has cancelled the love lock.");
				userTwo.LoveLockPartner = 0;
				item.InteractingUser = 0;
				item.InteractingUser2 = 0;
				return;
			}

			if (userTwo == null)
			{
				userOne.CanWalk = true;
				userOne.GetClient().SendNotif("Your partner has left the room or has cancelled the love lock.");
				userOne.LoveLockPartner = 0;
				item.InteractingUser = 0;
				item.InteractingUser2 = 0;
				return;
			}

			if (!confirmLoveLock)
			{
				item.InteractingUser = 0;
				item.InteractingUser2 = 0;

				userOne.LoveLockPartner = 0;
				userOne.CanWalk = true;
				userTwo.LoveLockPartner = 0;
				userTwo.CanWalk = true;
				return;
			}

			SimpleServerMessageBuffer loock = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("LoveLockDialogueSetLockedMessageComposer"));
			loock.AppendInteger(item.Id);

			if (userIdOne == Session.GetHabbo().Id)
			{
				userOne.GetClient().SendMessage(loock);
				userOne.LoveLockPartner = userIdTwo;
			}
			else if (userIdTwo == Session.GetHabbo().Id)
			{
				userTwo.GetClient().SendMessage(loock);
				userTwo.LoveLockPartner = userIdOne;
			}

			// Now check if both of the users have confirmed.
			if (userOne.LoveLockPartner == 0 || userTwo.LoveLockPartner == 0)
				return;

			item.ExtraData = $"1{'\u0005'}{userOne.GetUserName()}{'\u0005'}{userTwo.GetUserName()}{'\u0005'}{userOne.GetClient().GetHabbo().Look}{'\u0005'}{userTwo.GetClient().GetHabbo().Look}{'\u0005'}{DateTime.Now.ToString("dd/MM/yyyy")}";

			userOne.LoveLockPartner = 0;
			userTwo.LoveLockPartner = 0;
			item.InteractingUser = 0;
			item.InteractingUser2 = 0;

			item.UpdateState(true, false);

			using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
			{
				queryReactor.SetQuery("UPDATE items_rooms SET extra_data = @extraData WHERE id = @id");
				queryReactor.AddParameter("extraData", item.ExtraData);
				queryReactor.AddParameter("id", item.Id);
				queryReactor.RunQuery();
			}

			SimpleServerMessageBuffer messageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("UpdateRoomItemMessageComposer"));
			item.Serialize(messageBuffer);
			room.SendMessage(messageBuffer);

			loock = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("LoveLockDialogueCloseMessageComposer"));
			loock.AppendInteger(item.Id);
			userOne.GetClient().SendMessage(loock);
			userTwo.GetClient().SendMessage(loock);
			userOne.CanWalk = true;
			userTwo.CanWalk = true;
		}
	}
}

