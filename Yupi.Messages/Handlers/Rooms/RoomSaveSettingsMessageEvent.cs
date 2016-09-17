// ---------------------------------------------------------------------------------
// <copyright file="RoomSaveSettingsMessageEvent.cs" company="https://github.com/sant0ro/Yupi">
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

    using Yupi.Controller;
    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Model.Domain.Components;
    using Yupi.Model.Repository;

    public class RoomSaveSettingsMessageEvent : AbstractHandler
    {
        #region Fields

        private Repository<FlatNavigatorCategory> NavigatorCategoryRepository;
        private RoomManager RoomManager;
        private Repository<RoomData> RoomRepository;

        #endregion Fields

        #region Constructors

        public RoomSaveSettingsMessageEvent()
        {
            RoomManager = DependencyFactory.Resolve<RoomManager>();
            RoomRepository = DependencyFactory.Resolve<Repository<RoomData>>();
            NavigatorCategoryRepository = DependencyFactory.Resolve<Repository<FlatNavigatorCategory>>();
        }

        #endregion Constructors

        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            int roomId = request.GetInteger();

            RoomData roomData = RoomRepository.FindBy(roomId);

            if (roomData == null || !roomData.HasOwnerRights(session.Info))
            {
                return;
            }

            // TODO Filter
            string newName = request.GetString();

            // TODO Magic constant
            if (newName.Length > 2)
            {
                roomData.Name = newName;
            }

            // TODO Filter
            roomData.Description = request.GetString();

            int stateId = request.GetInteger();

            RoomState state;

            if (RoomState.TryFromInt32(stateId, out state))
            {
                roomData.State = state;
            }

            roomData.Password = request.GetString();
            roomData.UsersMax = request.GetInteger();

            int categoryId = request.GetInteger();

            FlatNavigatorCategory category = NavigatorCategoryRepository.FindBy(categoryId);

            if (category != null && category.MinRank <= session.Info.Rank)
            {
                roomData.Category = category;
            }

            int tagCount = request.GetInteger();

            if (tagCount <= 2)
            {
                roomData.Tags.Clear();

                for (int i = 0; i < tagCount; i++)
                    roomData.Tags.Add(request.GetString().ToLower());
            }

            TradingState tradeState;

            if (TradingState.TryFromInt32(request.GetInteger(), out tradeState))
            {
                roomData.TradeState = tradeState;
            }

            roomData.AllowPets = request.GetBool();
            roomData.AllowPetsEating = request.GetBool();
            roomData.AllowWalkThrough = request.GetBool();

            bool hideWall = request.GetBool();
            int wallThickness = request.GetInteger();
            int floorThickness = request.GetInteger();

            if (session.Info.Subscription.HasLevel(ClubLevel.HC))
            {
                roomData.HideWall = hideWall;
                roomData.WallThickness = wallThickness;
                roomData.FloorThickness = floorThickness;
            }
            else
            {
                roomData.HideWall = false;
                roomData.WallThickness = 0;
                roomData.FloorThickness = 0;
            }

            RoomModerationRight right;

            if (RoomModerationRight.TryFromInt32(request.GetInteger(), out right))
            {
                roomData.ModerationSettings.WhoCanMute = right;
            }

            if (RoomModerationRight.TryFromInt32(request.GetInteger(), out right))
            {
                roomData.ModerationSettings.WhoCanKick = right;
            }

            if (RoomModerationRight.TryFromInt32(request.GetInteger(), out right))
            {
                roomData.ModerationSettings.WhoCanBan = right;
            }

            ChatType chatType;

            if (ChatType.TryFromInt32(request.GetInteger(), out chatType))
            {
                roomData.Chat.Type = chatType;
            }

            ChatBalloon chatBalloon;

            if (ChatBalloon.TryFromInt32(request.GetInteger(), out chatBalloon))
            {
                roomData.Chat.Balloon = chatBalloon;
            }

            ChatSpeed chatSpeed;

            if (ChatSpeed.TryFromInt32(request.GetInteger(), out chatSpeed))
            {
                roomData.Chat.Speed = chatSpeed;
            }

            int maxDistance = request.GetInteger();

            if (roomData.Chat.isValidDistance(maxDistance))
            {
                roomData.Chat.SetMaxDistance(maxDistance);
            }

            FloodProtection floodProtection;

            if (FloodProtection.TryFromInt32(request.GetInteger(), out floodProtection))
            {
                roomData.Chat.FloodProtection = floodProtection;
            }

            request.GetBool(); //TODO allow_dyncats_checkbox

            router.GetComposer<RoomSettingsSavedMessageComposer>().Compose(session, roomData.Id);

            Room room = RoomManager.GetIfLoaded(roomData);

            if (room != null)
            {
                room.EachUser(x =>
                {
                    x.Router.GetComposer<RoomUpdateMessageComposer>().Compose(x, roomData.Id);
                    x.Router.GetComposer<RoomFloorWallLevelsMessageComposer>().Compose(x, roomData);
                    x.Router.GetComposer<RoomChatOptionsMessageComposer>().Compose(x, roomData);
                    x.Router.GetComposer<RoomDataMessageComposer>().Compose(x, roomData, x.Info, true, true);
                });
            }
        }

        #endregion Methods
    }
}