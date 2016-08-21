using System;
using Headspring;
using System.Collections.Generic;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Model;
using System.Linq;
using System.Linq.Expressions;

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
		public static readonly HotelView Hotel = new HotelView ();

		/// <summary>
		/// User's Rooms
		/// </summary>
		public static readonly MyWorldView MyWorld = new MyWorldView ();

		/// <summary>
		/// Event Rooms
		/// </summary>
		public static readonly RoomAdsView RoomAds = new RoomAdsView ();

		public NavigatorView (string value) : base (value, value)
		{
			
		}

		public abstract IDictionary<NavigatorCategory, IList<RoomData>> GetCategories (string query, UserInfo user);
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

		protected virtual Expression<Func<RoomData, bool>> GetRoomPredicate (string query, UserInfo user)
		{
			return x => x.Name.StartsWith(query) && x.Category is T;
		}

		public override IDictionary<NavigatorCategory, IList<RoomData>> GetCategories (string query, UserInfo user)
		{
			return RoomRepository
				.FilterBy (GetRoomPredicate (query, user))
				.GroupBy (x => x.Category)
				.ToDictionary (x => x.Key, x => (IList<RoomData>)x.ToList ());
		}
	}
}

