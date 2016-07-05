using System;



using Yupi.Messages.Items;



namespace Yupi.Messages.Rooms
{
	public class RoomGetHeightmapMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Net.ISession<GameClient> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			if (session.GetHabbo ().LoadingRoom <= 0u || CurrentLoadingRoom == null)
				return;

			RoomData roomData = CurrentLoadingRoom.RoomData;

			if (roomData == null)
				return;

			if (roomData.Model == null || CurrentLoadingRoom.GetGameMap () == null) {
				router.GetComposer<OutOfRoomMessageComposer> ().Compose (session);
				session.ClearRoomLoading ();
			} else {
				router.GetComposer<HeightMapMessageComposer> ().Compose (session, CurrentLoadingRoom.GetGameMap ());
				router.GetComposer<FloorMapMessageComposer> ().Compose (session, CurrentLoadingRoom.GetGameMap ().Model.GetHeightmap (), 
					CurrentLoadingRoom.RoomData.WallHeight);
				GetRoomData3 ();
			}
		}

		private void GetRoomData3(Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.IRouter router)
		{
			if (session.GetHabbo().LoadingRoom <= 0u || !session.GetHabbo().LoadingChecksPassed ||  CurrentLoadingRoom == null || session == null)
				return;

			if (CurrentLoadingRoom.RoomData.UsersNow >= CurrentLoadingRoom.RoomData.UsersMax &&
				!session.GetHabbo().HasFuse("fuse_enter_full_rooms"))
			{
				
				router.GetComposer<RoomEnterErrorMessageComposer> ().Compose (session, RoomEnterErrorMessageComposer.Error.ROOM_FULL);
				return;
			}

			router.GetComposer<RoomFloorItemsMessageComposer> ().Compose (session, CurrentLoadingRoom.RoomData, CurrentLoadingRoom.GetRoomItemHandler ().FloorItems);
			router.GetComposer<RoomWallItemsMessageComposer> ().Compose (session, CurrentLoadingRoom.RoomData, CurrentLoadingRoom.GetRoomItemHandler ().WallItems);


			CurrentLoadingRoom.GetRoomUserManager().AddUserToRoom(session, session.GetHabbo().SpectatorMode);

			// TODO Why is the spectator mode disabled here?
			session.GetHabbo().SpectatorMode = false;

			RoomCompetition competition = Yupi.GetGame().GetRoomManager().GetCompetitionManager().Competition;

			if (competition != null)
			{
				if (CurrentLoadingRoom.CheckRights(session, true))
				{
					if (!competition.Entries.ContainsKey (CurrentLoadingRoom.RoomData.Id))
						router.GetComposer<CompetitionEntrySubmitResultMessageComposer> ().Compose (session, competition, 
							CurrentLoadingRoom.RoomData.State != 0 ? 4 : 1);
					else
					{
						switch (competition.Entries[CurrentLoadingRoom.RoomData.Id].CompetitionStatus)
						{
						case 3:
							break;
						default:
							if (competition.HasAllRequiredFurnis(CurrentLoadingRoom))
								router.GetComposer<CompetitionEntrySubmitResultMessageComposer> ().Compose (session, competition, 2);
							else
								router.GetComposer<CompetitionEntrySubmitResultMessageComposer> ().Compose (session, competition, 3, CurrentLoadingRoom);
							break;
						}
					}
				}
				else if (!CurrentLoadingRoom.CheckRights(session, true) &&
					competition.Entries.ContainsKey(CurrentLoadingRoom.RoomData.Id))
				{
					if (session.GetHabbo ().DailyCompetitionVotes > 0)
						router.GetComposer<CompetitionVotingInfoMessageComposer> ().Compose (session, competition, session.GetHabbo().DailyCompetitionVotes);
				}
			}

			if (Yupi.GetUnixTimeStamp() < session.GetHabbo().FloodTime && session.GetHabbo().FloodTime != 0)
			{
				router.GetComposer<FloodFilterMessageComposer> ().Compose (session, session.GetHabbo ().FloodTime - Yupi.GetUnixTimeStamp ());
			}

			session.ClearRoomLoading();

			Poll poll;

			if (!Yupi.GetGame().GetPollManager().TryGetPoll(CurrentLoadingRoom.RoomId, out poll) ||
				session.GetHabbo().GotPollData(poll.Id))
				return;

			router.GetComposer<SuggestPollMessageComposer> ().Compose (session, poll);
		}

	}
}

