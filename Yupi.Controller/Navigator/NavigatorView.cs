using System;
using Headspring;
using System.Collections.Generic;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Model;
using System.Linq;

namespace Yupi.Controller
{
	// TODO Not sure wether this should be here or in Model
	public abstract class NavigatorView : Enumeration<NavigatorView, string>
	{
		/// <summary>
		/// Official Rooms
		/// </summary>
		public static readonly OfficialView Official = new OfficialView ();

		/// <summary>
		/// Public rooms
		/// </summary>
		public static readonly HotelView Hotel = new HotelView();

		/// <summary>
		/// User's Rooms
		/// </summary>
		public static readonly MyWorldView MyWorld = new MyWorldView();

		/// <summary>
		/// Event Rooms
		/// </summary>
		public static readonly RoomAdsView RoomAds = new RoomAdsView();

		public NavigatorView (string value) : base(value, value)
		{
			
		}

		public abstract IDictionary<NavigatorCategory, IList<RoomData>> GetCategories (string query);
	}

	public abstract class NavigatorView<T> : NavigatorView where T : NavigatorCategory
	{
		protected IRepository<T> NavigatorRepository;
		protected IRepository<RoomData> RoomRepository;

		protected NavigatorView (string value) : base (value)
		{
			NavigatorRepository = DependencyFactory.Resolve<IRepository<T>> ();
			RoomRepository = DependencyFactory.Resolve<IRepository<RoomData>> ();
		}

		public override IDictionary<NavigatorCategory, IList<RoomData>> GetCategories (string query)
		{
			var result = new Dictionary<NavigatorCategory, IList<RoomData>> ();
			var categories = NavigatorRepository.All ();

			foreach (NavigatorCategory category in categories) {
				if (!result.ContainsKey (category)) {
					IList<RoomData> rooms = RoomRepository.FilterBy (x => x.Category == category).ToList ();

					// TODO Filter query

					if(rooms.Count > 0) {
						result.Add (category, rooms);
					}
				}
			}

			return result;
		}
	}
}

