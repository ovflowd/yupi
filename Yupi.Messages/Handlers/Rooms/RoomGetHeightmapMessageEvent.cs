// ---------------------------------------------------------------------------------
// <copyright file="RoomGetHeightmapMessageEvent.cs" company="https://github.com/sant0ro/Yupi">
//   Copyright (c) 2016 Claudio Santoro, TheDoctor
// </copyright>
// <license>
//   Permission is hereby granted, free of charge, to any person obtaining a copy
//   of this software and associated documentation files (the "Software"), to deal
//   in the Software without restriction, including without limitation the rights
//   to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//   copies of the Software, and to permit persons to whom the Software is
//   furnished to do so, subject to the following conditions:
//
//   The above copyright notice and this permission notice shall be included in
//   all copies or substantial portions of the Software.
//
//   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//   IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//   FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//   AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//   LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//   OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//   THE SOFTWARE.
// </license>
// ---------------------------------------------------------------------------------
namespace Yupi.Messages.Rooms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Yupi.Messages.Items;
    using Yupi.Messages.User;
    using Yupi.Model.Domain;

    public class RoomGetHeightmapMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            if (session.Room == null || session.RoomEntity == null)
                return;

            router.GetComposer<HeightMapMessageComposer>().Compose(session, session.Room.HeightMap);

            router.GetComposer<FloorMapMessageComposer>().Compose(session, session.Room.Data.Model.Heightmap,
                session.Room.Data.WallHeight);

            if (session.Room.GetUserCount() >= session.Room.Data.UsersMax &&
                !session.Info.HasPermission("fuse_enter_full_rooms"))
            {
                router.GetComposer<RoomEnterErrorMessageComposer>()
                    .Compose(session, RoomEnterErrorMessageComposer.Error.ROOM_FULL);
            }
            else
            {
                // TODO Implement
                router.GetComposer<RoomFloorItemsMessageComposer>()
                    .Compose(session, session.Room.Data, new Dictionary<uint, FloorItem>());
                router.GetComposer<RoomWallItemsMessageComposer>()
                    .Compose(session, session.Room.Data, new Dictionary<uint, FloorItem>());
                router.GetComposer<SetRoomUserMessageComposer>().Compose(session, session.Room.Users);
                router.GetComposer<RoomFloorWallLevelsMessageComposer>().Compose(session, session.Room.Data);
                router.GetComposer<RoomOwnershipMessageComposer>().Compose(session, session.Room.Data, session.Info);

                foreach (UserInfo userWithRights in session.Room.Data.Rights)
                {
                    router.GetComposer<GiveRoomRightsMessageComposer>()
                        .Compose(session, session.Room.Data.Id, userWithRights);
                }

                router.GetComposer<UpdateUserStatusMessageComposer>().Compose(session, session.Room.Users);

                // TODO Implement
                //Yupi.GetGame().GetRoomEvents().SerializeEventInfo(CurrentLoadingRoom.RoomId);

                foreach (RoomEntity entity in session.Room.Users)
                {
                    if (entity is HumanEntity)
                    {
                        router.GetComposer<DanceStatusMessageComposer>()
                            .Compose(session, entity.Id, ((HumanEntity) entity).Dance);
                    }
                    router.GetComposer<RoomUserIdleMessageComposer>().Compose(session, entity.Id, entity.IsAsleep);

                    // TODO Implement
                    //ApplyHanditemMessageComposer
                    //ApplyEffectMessageComposer
                }

                session.Room.EachUser(entity =>
                {
                    entity.Router.GetComposer<SetRoomUserMessageComposer>()
                        .Compose(entity, session.RoomEntity);
                });

                // TODO Implement
                //GetRoomData3()
            }
        }

        private void GetRoomData3(Yupi.Protocol.ISender session, Yupi.Protocol.IRouter router)
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
            throw new NotImplementedException();
        }

        #endregion Methods
    }
}