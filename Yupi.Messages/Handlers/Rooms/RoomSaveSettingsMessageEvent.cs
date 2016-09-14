using System;
using System.Collections.Generic;
using Yupi.Model.Repository;
using Yupi.Model.Domain;
using Yupi.Model;
using Yupi.Controller;
using Yupi.Model.Domain.Components;


namespace Yupi.Messages.Rooms
{
    public class RoomSaveSettingsMessageEvent : AbstractHandler
    {
        private RoomManager RoomManager;
        private Repository<RoomData> RoomRepository;
        private Repository<FlatNavigatorCategory> NavigatorCategoryRepository;

        public RoomSaveSettingsMessageEvent()
        {
            RoomManager = DependencyFactory.Resolve<RoomManager>();
            RoomRepository = DependencyFactory.Resolve<Repository<RoomData>>();
            NavigatorCategoryRepository = DependencyFactory.Resolve<Repository<FlatNavigatorCategory>>();
        }

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
                roomData.Description = request.GetString();
            }

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

            if (session.Info.Subscription.IsValid())
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
    }
}