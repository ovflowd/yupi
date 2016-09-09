using System;




using Yupi.Messages.Rooms;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Model;
using Yupi.Controller;

namespace Yupi.Messages.Navigator
{
	public class ToggleStaffPickMessageEvent : AbstractHandler
	{
		private IRepository<RoomData> RoomRepository;
		private AchievementManager AchievementManager;
		private RoomManager RoomManager;

		public ToggleStaffPickMessageEvent ()
		{
			RoomRepository = DependencyFactory.Resolve<IRepository<RoomData>> ();
			AchievementManager = DependencyFactory.Resolve<AchievementManager> ();
			RoomManager = DependencyFactory.Resolve<RoomManager> ();
		}

		public override void HandleMessage (Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			int roomId = request.GetInteger ();

			request.GetBool (); // TODO Unused

			RoomData roomData = RoomRepository.FindBy (roomId);

			if (roomData == null)
				return;

			AchievementManager.ProgressUserAchievement (roomData.Owner, "ACH_Spr", 1);

			// TODO Add room to Staff Pick category!
			throw new NotImplementedException();

			Room room = RoomManager.GetIfLoaded (roomData);

			if (room != null) {
				room.Each ((entitySession) => {
					entitySession.Router.GetComposer<RoomDataMessageComposer> ().Compose (entitySession, roomData, entitySession.Info, true, true);
				});
			}
		}
	}
}

