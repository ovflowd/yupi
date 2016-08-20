using System;
using Yupi.Messages.Items;
using Yupi.Model.Domain;
using Yupi.Messages.User;



namespace Yupi.Messages.Rooms
{
	public class RoomGetHeightmapMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			if (session.Room == null)
				return;

			router.GetComposer<HeightMapMessageComposer> ().Compose (session, session.Room.HeightMap);

			router.GetComposer<FloorMapMessageComposer> ().Compose (session,session.Room.Data.Model.Heightmap, 
				session.Room.Data.WallHeight);


			if (session.Room.GetUserCount () >= session.Room.Data.UsersMax &&
			    !session.Info.HasPermission ("fuse_enter_full_rooms")) {
				router.GetComposer<RoomEnterErrorMessageComposer> ().Compose (session, RoomEnterErrorMessageComposer.Error.ROOM_FULL);
			} else {
				//router.GetComposer<RoomFloorItemsMessageComposer> ().Compose (session, session.Room.Data, session.Room.GetRoomItemHandler ().FloorItems);
				//router.GetComposer<RoomWallItemsMessageComposer> ().Compose (session, session.Room.Data, session.Room.GetRoomItemHandler ().WallItems);
			
				// TODO Implement
				router.GetComposer<SetRoomUserMessageComposer>().Compose(session, session.Room.Users);
				router.GetComposer<RoomFloorWallLevelsMessageComposer> ().Compose (session, session.Room.Data);
				router.GetComposer<RoomOwnershipMessageComposer> ().Compose (session, session.Room.Data, session.Info);

				foreach(UserInfo userWithRights in session.Room.Data.Rights) {
					router.GetComposer<GiveRoomRightsMessageComposer> ()
						.Compose (session, session.Room.Data.Id, userWithRights);
				}

				router.GetComposer<UpdateUserStatusMessageComposer> ().Compose (session, session.Room.Users);
				//Yupi.GetGame().GetRoomEvents().SerializeEventInfo(CurrentLoadingRoom.RoomId);

				foreach(RoomEntity entity in session.Room.Users) {
				//DanceStatusMessageComposer
				//RoomUserIdleMessageComposer
				//ApplyHanditemMessageComposer
				//ApplyEffectMessageComposer
					if (entity is UserEntity) {
						router.GetComposer<UpdateUserDataMessageComposer> ().Compose (session, ((UserEntity)entity).UserInfo);
					}
				}
				//CurrentLoadingRoom.GetRoomUserManager().AddUserToRoom(session, session.SpectatorMode);
				//GetRoomData3 ();
			}
		}

		private void GetRoomData3 (Yupi.Protocol.ISender session, Yupi.Protocol.IRouter router)
		{
			/*
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

			Poll poll;

			if (!Yupi.GetGame().GetPollManager().TryGetPoll(CurrentLoadingRoom.RoomId, out poll) ||
				session.GetHabbo().GotPollData(poll.Id))
				return;

			router.GetComposer<SuggestPollMessageComposer> ().Compose (session, poll);
			*/
			throw new NotImplementedException ();
		}

	}
}

