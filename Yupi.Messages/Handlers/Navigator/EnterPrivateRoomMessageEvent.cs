// ---------------------------------------------------------------------------------
// <copyright file="EnterPrivateRoomMessageEvent.cs" company="https://github.com/sant0ro/Yupi">
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
namespace Yupi.Messages.Navigator
{
    using System;

    using Yupi.Controller;
    using Yupi.Messages.Contracts;
    using Yupi.Model;
    using Yupi.Model.Domain;

    public class EnterPrivateRoomMessageEvent : AbstractHandler
    {
        #region Fields

        private RoomManager RoomManager;

        #endregion Fields

        #region Constructors

        // TODO Rename? This look like GetRoomInfoEvent or something like that
        public EnterPrivateRoomMessageEvent()
        {
            RoomManager = DependencyFactory.Resolve<RoomManager>();
        }

        #endregion Constructors

        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            int roomId = request.GetInteger();

            string pWd = request.GetString();

            if (session.Room != null)
            {
                RoomManager.RemoveUser(session);
            }

            Room room = RoomManager.LoadOrGet(roomId);

            if (room == null)
            {
                return;
            }

            if (room.GetUserCount() >= room.Data.UsersMax
                && !session.Info.HasPermission("fuse_enter_full_rooms")
                && room.Data.Owner != session.Info)
            {
                /// TODO Remove on other room load / disconnect
                room.Queue.Add(session.Info);

                router.GetComposer<RoomQueueComposer>().Compose(session, room.Queue.Count);
            }
            else if (!session.Info.HasPermission("fuse_enter_any_room")
                     && room.Data.BannedUsers.Contains(session.Info))
            {
                router.GetComposer<RoomEnterErrorMessageComposer>()
                    .Compose(session, RoomEnterErrorMessageComposer.Error.BANNED);
                router.GetComposer<OutOfRoomMessageComposer>().Compose(session);
            }
            else
            {
                router.GetComposer<PrepareRoomMessageComposer>().Compose(session);

                bool isReload = false;
                // TODO Extract to controller!

                if (!isReload
                    && !session.Info.HasPermission("fuse_enter_any_room")
                    && !room.Data.HasOwnerRights(session.Info)
                    && session.TeleportingTo != room.Data)
                {
                    if (room.Data.State == RoomState.Bell)
                    {
                        if (room.GetUserCount() == 0)
                        {
                            router.GetComposer<DoorbellNoOneMessageComposer>().Compose(session);
                        }
                        else
                        {
                            // TODO String.Empty == 'I am ringing' ?
                            router.GetComposer<DoorbellMessageComposer>().Compose(session, string.Empty);

                            room.EachUser((user) =>
                            {
                                if (room.Data.HasRights(user.Info))
                                {
                                    user.Router.GetComposer<DoorbellMessageComposer>().Compose(user, session.Info.Name);
                                }
                            });
                        }

                        return;
                    }
                    else if (room.Data.State == RoomState.Locked)
                    {
                        if (pWd != room.Data.Password)
                        {
                            router.GetComposer<RoomErrorMessageComposer>().Compose(session, -100002);
                            router.GetComposer<OutOfRoomMessageComposer>().Compose(session);
                            return;
                        }
                    }
                }

                if (session.Info.FavouriteGroup != null)
                {
                    room.GroupsInRoom.Add(session.Info.FavouriteGroup);
                }

                router.GetComposer<RoomGroupMessageComposer>().Compose(session, room.GroupsInRoom);
                router.GetComposer<InitialRoomInfoMessageComposer>().Compose(session, room.Data);

                if (session.Info.SpectatorMode)
                {
                    router.GetComposer<SpectatorModeMessageComposer>().Compose(session);
                }

                if (room.Data.WallPaper > 0)
                {
                    router.GetComposer<RoomSpacesMessageComposer>()
                        .Compose(session, RoomSpacesMessageComposer.RoomSpacesType.Wallpaper, room.Data);
                }

                if (room.Data.Floor > 0)
                {
                    router.GetComposer<RoomSpacesMessageComposer>()
                        .Compose(session, RoomSpacesMessageComposer.RoomSpacesType.Floor, room.Data);
                }

                if (room.Data.LandScape > 0)
                {
                    router.GetComposer<RoomSpacesMessageComposer>()
                        .Compose(session, RoomSpacesMessageComposer.RoomSpacesType.Landscape, room.Data);
                }
                // TODO Magic numbers!
                int rightsLevel = 0;

                if (room.Data.HasOwnerRights(session.Info))
                {
                    rightsLevel = 4;
                    router.GetComposer<HasOwnerRightsMessageComposer>().Compose(session);
                }
                else if (room.Data.HasRights(session.Info))
                {
                    rightsLevel = 1;
                }

                router.GetComposer<RoomRightsLevelMessageComposer>().Compose(session, rightsLevel);
                router.GetComposer<RoomRatingMessageComposer>()
                    .Compose(session, room.Data.Score, room.CanVote(session.Info));
                router.GetComposer<RoomUpdateMessageComposer>().Compose(session, room.Data.Id);

                session.Info.RecentlyVisitedRooms.Add(room.Data);
                room.AddUser(session);
            }
        }

        #endregion Methods
    }
}