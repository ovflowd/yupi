using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Rooms
{
    public class CreateRoomMessageEvent : AbstractHandler
    {
        private readonly IRepository<NavigatorCategory> NavigatorRepository;
        private readonly IRepository<RoomData> RoomRepository;

        public CreateRoomMessageEvent()
        {
            NavigatorRepository = DependencyFactory.Resolve<IRepository<NavigatorCategory>>();
            RoomRepository = DependencyFactory.Resolve<IRepository<RoomData>>();
        }

        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            var name = request.GetString();
            var description = request.GetString();
            var roomModel = request.GetString();
            var categoryId = request.GetInteger();
            var maxVisitors = request.GetInteger();
            var tradeStateId = request.GetInteger();

            RoomModel model;
            TradingState tradeState;

            if (!RoomModel.TryParse(roomModel, out model)
                || !TradingState.TryFromInt32(tradeStateId, out tradeState)) return;

            var category = NavigatorRepository.FindBy(categoryId);

            if (category.MinRank > session.Info.Rank) return;

            // TODO Filter Name, Description, max visitors
            var data = new RoomData
            {
                Name = name,
                Description = description,
                Model = model,
                Category = category,
                UsersMax = maxVisitors,
                TradeState = tradeState,
                Owner = session.Info
            };

            RoomRepository.Save(data);

            router.GetComposer<OnCreateRoomInfoMessageComposer>().Compose(session, data);
        }
    }
}