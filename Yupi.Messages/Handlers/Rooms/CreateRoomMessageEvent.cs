using System;
using Yupi.Messages.Contracts;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Model;


namespace Yupi.Messages.Rooms
{
	public class CreateRoomMessageEvent : AbstractHandler
	{
		private IRepository<NavigatorCategory> NavigatorRepository;
		private IRepository<RoomData> RoomRepository;

		public CreateRoomMessageEvent ()
		{
			NavigatorRepository = DependencyFactory.Resolve<IRepository<NavigatorCategory>> ();
			RoomRepository = DependencyFactory.Resolve<IRepository<RoomData>> ();
		}
		
		public override void HandleMessage ( Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			string name = request.GetString();
			string description = request.GetString();
			string roomModel = request.GetString();
			int categoryId = request.GetInteger();
			int maxVisitors = request.GetInteger();
			int tradeStateId = request.GetInteger();

			RoomModel model;
			TradingState tradeState;

			if (!RoomModel.TryParse (roomModel, out model) 
				|| !TradingState.TryFromInt32(tradeStateId, out tradeState)) {
				return;
			}

			NavigatorCategory category = NavigatorRepository.FindBy (categoryId);

			if (category.MinRank > session.Info.Rank) {
				return;
			}

			// TODO Filter Name, Description, max visitors
			RoomData data = new RoomData() {
				Name = name,
				Description = description,
				Model = model,
				Category = category,
				UsersMax = maxVisitors,
				TradeState = tradeState,
				Owner = session.Info
			};

			RoomRepository.Save (data);

			router.GetComposer<OnCreateRoomInfoMessageComposer> ().Compose (session, data);
		}
	}
}

