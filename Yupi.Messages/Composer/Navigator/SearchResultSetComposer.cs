using System;
using Yupi.Protocol.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Yupi.Model.Domain;
using Yupi.Controller;
using Yupi.Model.Repository;
using Yupi.Model;


namespace Yupi.Messages.Navigator
{
	// TODO Refactor
	public class SearchResultSetComposer : Yupi.Messages.Contracts.SearchResultSetComposer
	{
		private RoomManager RoomManager;
		private ClientManager ClientManager;
		private IRepository<RoomData> RoomRepository;
		private IRepository<NavigatorCategory> NavigatorRepository;

		public SearchResultSetComposer ()
		{
			NavigatorRepository = DependencyFactory.Resolve<IRepository<NavigatorCategory>> ();
		}

		public override void Compose (Yupi.Protocol.ISender session, string staticId, string query)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendString (staticId);
				message.AppendString (query);

				NavigatorView view = NavigatorView.FromValue (staticId);

				IDictionary<NavigatorCategory, IList<RoomData>> categories = view.GetCategories (query);
				message.AppendInteger (categories.Count);

				foreach (var category in categories) {
					message.AppendString (staticId);
					message.AppendString (category.Key.Caption);
					message.AppendInteger (1); // TODO actionAllowed ( 1 = Show More, 2 = Back)
					message.AppendBool (!category.Key.IsOpened);
					message.AppendInteger (category.Key.IsImage); // TODO ViewMode (Possible Values?)

					// Room Count
					message.AppendInteger (category.Value.Count);

					foreach (RoomData roomData in category.Value) {
						Serialize (roomData, message);
					}
				}

				session.Send (message);
			}
		}


		private void Serialize (RoomData data, ServerMessage messageBuffer)
		{
			Room room = RoomManager.GetIfLoaded (data);

			messageBuffer.AppendInteger (data.Id);
			messageBuffer.AppendString (data.Name);
			messageBuffer.AppendInteger (data.Owner.Id);
			messageBuffer.AppendString (data.Owner.UserName);
			messageBuffer.AppendInteger ((int)data.State);
			messageBuffer.AppendInteger (room == null ? 0 : room.GetUserCount ());
			messageBuffer.AppendInteger (data.UsersMax);
			messageBuffer.AppendString (data.Description);
			messageBuffer.AppendInteger (data.TradeState);
			messageBuffer.AppendInteger (data.Score); // Score
			messageBuffer.AppendInteger (data.Score); // Ranking Difference?
			messageBuffer.AppendInteger (data.Category.Id);
			messageBuffer.AppendInteger (data.Tags.Count);

			foreach (string tag in data.Tags) {
				messageBuffer.AppendString (tag);
			}
				
			RoomFlags flags = data.GetFlags ();

			messageBuffer.AppendInteger ((int)flags);

			if ((flags & RoomFlags.Image) > 0) {
				messageBuffer.AppendString (data.NavigatorImage);
			}

			if ((flags & RoomFlags.Group) > 0) {
				messageBuffer.AppendInteger (data.Group.Id);
				messageBuffer.AppendString (data.Group.Name);
				messageBuffer.AppendString (data.Group.Badge);
			}

			if ((flags & RoomFlags.Event) > 0) {
				messageBuffer.AppendString (data.Event.Name);
				messageBuffer.AppendString (data.Event.Description);
				messageBuffer.AppendInteger ((int)(data.Event.ExpiresAt - DateTime.Now).TotalMinutes);
			}
		}
			
		/*
		private void SerializeSearchResultListFlatcats (NavigatorCategory flatCat, bool direct, ServerMessage messageBuffer)
		{
			messageBuffer.AppendString ($"category__" + flatCat.Caption);
			messageBuffer.AppendString (flatCat.Caption);
			messageBuffer.AppendInteger (0);  // TODO Hardcoded values!
			messageBuffer.AppendBool (true);
			messageBuffer.AppendInteger (-1);


			SerializeNavigatorPromotedRooms (messageBuffer, RoomManager.GetActive().Select(x => x.Data), flatCat, direct);
		}

		private void SerializeNavigatorPromotedRooms (ServerMessage reply, IEnumerable<RoomData> rooms, NavigatorCategory category, bool direct)
		{
			IList<RoomData> roomsCategory = rooms.Where(x => x.Category == category).ToList();
			reply.AppendInteger (roomsCategory.Count);

			foreach (RoomData data in roomsCategory) {
				Serialize (data, reply);
			}
		}
			
		private void SerializePromotionsResultListFlatcats (NavigatorCategory category, bool direct, ServerMessage messageBuffer)
		{
			messageBuffer.AppendString ("new_ads");
			messageBuffer.AppendString (category.Caption);
			messageBuffer.AppendInteger (0);
			messageBuffer.AppendBool (true);
			messageBuffer.AppendInteger (-1);

			SerializeNavigatorPromotedRooms (messageBuffer, RoomManager.GetEventRooms(), category, direct);
		}
*/
		private void SerializeSearchResultListStatics (string staticId, bool direct, ServerMessage messageBuffer, UserInfo user, bool opened = false, bool showImage = false)
		{
			
			return;
			//throw new NotImplementedException ();
		
			/*
			if (string.IsNullOrEmpty (staticId) || staticId == "official")
				staticId = "official_view";

			if (staticId != "hotel_view" && staticId != "roomads_view" && staticId != "myworld_view" && !staticId.StartsWith ("category__") && staticId != "official_view") {
				messageBuffer.AppendString (staticId);
				messageBuffer.AppendString (string.Empty);
				messageBuffer.AppendInteger (1);
				messageBuffer.AppendBool (!opened);
				messageBuffer.AppendInteger (showImage ? 1 : 0);
			}

			switch (staticId) {
			case "hotel_view":
			case "official_view":
			case "myworld_view":
				{
					NavigatorCategory navCategory =  NavigatorRepository.FindBy (x => x.Caption == staticId);

					if (navCategory != null) {

						foreach (NavigatorCategory subCategory in navCategory.SubCategories)
							SerializeSearchResultListStatics (subCategory.Caption, false, messageBuffer, user, subCategory.IsOpened, subCategory.IsImage);
					}
					break;
				}
			
				{
					NavigatorCategory navCategory =  NavigatorRepository.FindBy (x => x.Caption == staticId);
					if (navCategory != null) {
						foreach (NavigatorCategory subCategory in navCategory.SubCategories)
							SerializeSearchResultListStatics (subCategory.Caption, false, messageBuffer, user, subCategory.IsOpened, subCategory.IsImage);
					}
					break;
				}
			case "roomads_view":
				{
					foreach (PublicCategory flat in Yupi.GetGame().GetNavigator().PrivateCategories.Values)
						SerializePromotionsResultListFlatcats (flat.Id, false, messageBuffer);

					NavigatorCategory navCategory =  NavigatorRepository.FindBy (x => x.Caption == staticId);

					foreach (NavigatorCategory subCategory in navCategory.SubCategories)
						SerializeSearchResultListStatics (subCategory.Caption, false, messageBuffer, user, subCategory.IsOpened, subCategory.IsImage);

					break;
				}
			case "official-root":
				{
					SerializePublicRooms (messageBuffer);

					break;
				}
			case "staffpicks":
				{
					SerializeStaffPicks (messageBuffer);

					break;
				}
			case "my":
				{
					messageBuffer.AppendInteger(user.UsersRooms.Count);

					foreach (RoomData data in user.UsersRooms) {
						Serialize (data, messageBuffer);
					}
					break;
				}
			case "favorites":
				{
					messageBuffer.AppendInteger (user.FavoriteRooms.Count);

					foreach (RoomData data in user.FavoriteRooms) {
						Serialize (data, messageBuffer);
					}

					break;
				}
			case "friends_rooms":
				{
					// Well, that escalated quickly ! :D
					List<Room> roomsFriends = user.Relationships.Relationships
						.Select (x => ClientManager.GetByInfo (x.Friend))
						.Where (x => x != null && x.UserData.Room != null)
						.Select (x => x.UserData.Room)
						.OrderByDescending (x => x.GetUserCount ())
						.Take (50)
						.ToList();

					messageBuffer.AppendInteger (roomsFriends.Count);

					foreach (Room data in roomsFriends) {
						Serialize (data.Data, messageBuffer);
					}

					break;
				}
			case "recommended":
				{
					break;
				}
			case "popular":
				{
					List<Room> activeRooms = RoomManager
						.GetActive()
						.ToList ();

					messageBuffer.AppendInteger (activeRooms.Count);

					foreach (Room room in activeRooms) {
						// TODO Create a serialize method taking Room as an argument
						Serialize (room.Data, messageBuffer);
					}

					break;
				}
			case "top_promotions":
				{
					List<RoomData> rooms = RoomManager.GetEventRooms().ToList();

					messageBuffer.AppendInteger (rooms.Count);

					foreach (RoomData room in rooms)
						Serialize (room, messageBuffer);

					break;
				}
			case "my_groups":
				{
					messageBuffer.AppendInteger (user.UserGroups.Count);

					foreach (Group group in user.UserGroups) {
						Serialize (group.Room, messageBuffer);
					}

					break;
				}
			case "history":
				{
					messageBuffer.AppendInteger (user.RecentlyVisitedRooms.Count);

					foreach (RoomData room in user.RecentlyVisitedRooms) {
						Serialize (room, messageBuffer);
					}
					break;
				}
			default:
				{
					if (staticId.StartsWith ("category__"))
						SerializeSearchResultListFlatcats (Yupi.GetGame ().GetNavigator ().GetFlatCatIdByName (staticId.Replace ("category__", string.Empty)), true, messageBuffer);
					else
						messageBuffer.AppendInteger (0);

					break;
				}
			}
			*/
		}

		/*
		private void SerializeStaffPicks (ServerMessage messageBuffer)
		{
			messageBuffer.StartArray ();

			foreach (PublicItem item in Yupi.GetGame().GetNavigator().PublicRooms.Values.Where(t => t.ParentId == -2)) {
				messageBuffer.Clear ();

				if (item.GetPublicRoomData == null)
					continue;

				item.GetPublicRoomData.Serialize (messageBuffer);

				messageBuffer.SaveArray ();
			}

			messageBuffer.EndArray ();
		}

		private void SerializePublicRooms (ServerMessage messageBuffer)
		{
			messageBuffer.StartArray ();

			foreach (PublicItem item in Yupi.GetGame().GetNavigator().PublicRooms.Values) {
				if (item.ParentId == -1) {
					messageBuffer.Clear ();

					if (item.GetPublicRoomData == null)
						continue;

					item.GetPublicRoomData.Serialize (messageBuffer);

					messageBuffer.SaveArray ();
				}
			}

			messageBuffer.EndArray ();
		}

		private void SerializeSearches (string searchQuery, ServerMessage messageBuffer)
		{
			messageBuffer.AppendString (string.Empty);
			messageBuffer.AppendString (searchQuery);
			messageBuffer.AppendInteger (2);
			messageBuffer.AppendBool (false);
			messageBuffer.AppendInteger (0);

			IList<RoomData> rooms = RoomManager.Search (searchQuery);

			messageBuffer.AppendInteger (rooms.Count);

			foreach (RoomData data in rooms) {
				Serialize (data, messageBuffer);
			}
		}

		
		*/
	}
}

