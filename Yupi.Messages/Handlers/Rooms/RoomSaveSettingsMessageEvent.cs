using Yupi.Controller;
using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Rooms
{
    public class RoomSaveSettingsMessageEvent : AbstractHandler
    {
        private readonly Repository<FlatNavigatorCategory> NavigatorCategoryRepository;
        private readonly RoomManager RoomManager;
        private readonly Repository<RoomData> RoomRepository;

        public RoomSaveSettingsMessageEvent()
        {
            RoomManager = DependencyFactory.Resolve<RoomManager>();
            RoomRepository = DependencyFactory.Resolve<Repository<RoomData>>();
            NavigatorCategoryRepository = DependencyFactory.Resolve<Repository<FlatNavigatorCategory>>();
        }

        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            var roomId = request.GetInteger();

            var roomData = RoomRepository.FindBy(roomId);

            if ((roomData == null) || !roomData.HasOwnerRights(session.Info)) return;

            // TODO Filter
            var newName = request.GetString();

            // TODO Magic constant
            if (newName.Length > 2) roomData.Description = request.GetString();

            var stateId = request.GetInteger();

            RoomState state;

            if (RoomState.TryFromInt32(stateId, out state)) roomData.State = state;

            roomData.Password = request.GetString();
            roomData.UsersMax = request.GetInteger();

            var categoryId = request.GetInteger();

            var category = NavigatorCategoryRepository.FindBy(categoryId);

            if ((category != null) && (category.MinRank <= session.Info.Rank)) roomData.Category = category;

            var tagCount = request.GetInteger();

            if (tagCount <= 2)
            {
                roomData.Tags.Clear();

                for (var i = 0; i < tagCount; i++)
                    roomData.Tags.Add(request.GetString().ToLower());
            }

            TradingState tradeState;

            if (TradingState.TryFromInt32(request.GetInteger(), out tradeState)) roomData.TradeState = tradeState;

            roomData.AllowPets = request.GetBool();
            roomData.AllowPetsEating = request.GetBool();
            roomData.AllowWalkThrough = request.GetBool();

            var hideWall = request.GetBool();
            var wallThickness = request.GetInteger();
            var floorThickness = request.GetInteger();

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
                roomData.ModerationSettings.WhoCanMute = right;

            if (RoomModerationRight.TryFromInt32(request.GetInteger(), out right))
                roomData.ModerationSettings.WhoCanKick = right;

            if (RoomModerationRight.TryFromInt32(request.GetInteger(), out right))
                roomData.ModerationSettings.WhoCanBan = right;

            ChatType chatType;

            if (ChatType.TryFromInt32(request.GetInteger(), out chatType)) roomData.Chat.Type = chatType;

            ChatBalloon chatBalloon;

            if (ChatBalloon.TryFromInt32(request.GetInteger(), out chatBalloon)) roomData.Chat.Balloon = chatBalloon;

            ChatSpeed chatSpeed;

            if (ChatSpeed.TryFromInt32(request.GetInteger(), out chatSpeed)) roomData.Chat.Speed = chatSpeed;

            var maxDistance = request.GetInteger();

            if (roomData.Chat.isValidDistance(maxDistance)) roomData.Chat.SetMaxDistance(maxDistance);

            FloodProtection floodProtection;

            if (FloodProtection.TryFromInt32(request.GetInteger(), out floodProtection))
                roomData.Chat.FloodProtection = floodProtection;

            request.GetBool(); //TODO allow_dyncats_checkbox

            router.GetComposer<RoomSettingsSavedMessageComposer>().Compose(session, roomData.Id);

            var room = RoomManager.GetIfLoaded(roomData);

            if (room != null)
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