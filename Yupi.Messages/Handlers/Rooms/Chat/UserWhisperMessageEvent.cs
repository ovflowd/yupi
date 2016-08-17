using System;


using System.Linq;
using System.Text.RegularExpressions;



using System.Collections.Generic;

namespace Yupi.Messages.Chat
{
	public class UserWhisperMessageEvent : AbstractHandler
	{
		public override void HandleMessage ( Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			/*
			if (!session.GetHabbo().InRoom)
				return;

			Room currentRoom = session.GetHabbo().CurrentRoom;
			string text = request.GetString();
			string targetUser = text.Split(' ')[0];
			string msg = text.Substring(targetUser.Length + 1);
			int colour = request.GetInteger();

			RoomUser roomUserByHabbo = currentRoom.GetRoomUserManager().GetRoomUserByHabbo(session.GetHabbo().Id);
			RoomUser roomUserByHabbo2 = currentRoom.GetRoomUserManager().GetRoomUserByHabbo(targetUser);

			// TODO Wordfilter should do replacement itself
			msg = currentRoom.WordFilter.Aggregate(msg,
				(current1, current) => Regex.Replace(current1, current, "bobba", RegexOptions.IgnoreCase));

			BlackWord word;
			// TODO Difference between Wordfilter and Blacklist? Is the Blacklist really required?
			if (BlackWordsManager.Check(msg, BlackWordType.Hotel, out word))
			{
				BlackWordTypeSettings settings = word.TypeSettings;

				if (settings.ShowMessage)
				{
					session.SendWhisper("The sent message contains the word: " + word.Word +
						" That is not allowed here, you could be banned!");
					return;
				}
			}

			TimeSpan span = DateTime.Now - _floodTime;

			if (span.Seconds > 4)
				_floodCount = 0;

			if ((span.Seconds < 4) && (_floodCount > 5) && (session.GetHabbo().Rank < 5))
				return;

			_floodTime = DateTime.Now;
			_floodCount++;

			if (roomUserByHabbo == null || roomUserByHabbo2 == null)
			{
				session.SendWhisper(msg);
				return;
			}

			if (session.GetHabbo().Rank < 4 && currentRoom.CheckMute(session))
				return;

			currentRoom.AddChatlog(session.GetHabbo().Id, $"<whispered to {text2}>: {msg}", false);

			if (!roomUserByHabbo.IsBot &&
				(colour == 2 || (colour == 23 && !session.Info.HasPermission("fuse_mod")) || colour < 0 ||
					colour > 29))
				colour = roomUserByHabbo.LastBubble; // or can also be just 0

			roomUserByHabbo.UnIdle();

			router.GetComposer<WhisperMessageComposer> ().Compose (roomUserByHabbo.GetClient (), roomUserByHabbo.VirtualId, msg, colour);

			if (!roomUserByHabbo2.IsBot && roomUserByHabbo2.UserId != roomUserByHabbo.UserId &&
				!roomUserByHabbo2.GetClient().GetHabbo().MutedUsers.Contains(session.GetHabbo().Id))
				router.GetComposer<WhisperMessageComposer> ().Compose (roomUserByHabbo2.GetClient(), roomUserByHabbo.VirtualId, msg, colour);

			List<RoomUser> roomUserByRank = currentRoom.GetRoomUserManager().GetRoomUserByRank(4);

			if (!roomUserByRank.Any())
				return;

			foreach (RoomUser current2 in roomUserByRank)
				if (current2 != null && current2.HabboId != roomUserByHabbo2.HabboId &&
					current2.HabboId != roomUserByHabbo.HabboId && current2.GetClient() != null)
				{
					router.GetComposer<WhisperMessageComposer> ().Compose (current2, roomUserByHabbo.VirtualId, $"Whisper to {text2}: {msg}", colour);
				}
				*/
			throw new NotImplementedException ();
		}
	}
}

