using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Headspring;
using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Util;

namespace Yupi.Controller
{
    // TODO Not sure wether this should be here or in Model
    public abstract class NavigatorView : Enumeration<NavigatorView, string>
    {
        /// <summary>
        ///     Official Rooms
        /// </summary>
        public static readonly OfficialView Official = new OfficialView();

        /// <summary>
        ///     Public rooms
        /// </summary>
        public static readonly HotelView Hotel = new HotelView();

        /// <summary>
        ///     User's Rooms
        /// </summary>
        public static readonly MyWorldView MyWorld = new MyWorldView();

        /// <summary>
        ///     Event Rooms
        /// </summary>
        public static readonly RoomAdsView RoomAds = new RoomAdsView();

        public NavigatorView(string value) : base(value, value)
        {
        }

        public abstract IDictionary<NavigatorCategory, IList<RoomData>> GetCategories(string query, UserInfo user);
    }

    public abstract class NavigatorView<T> : NavigatorView where T : NavigatorCategory
    {
        protected IRepository<T> NavigatorRepository;
        protected IRepository<RoomData> RoomRepository;

        protected NavigatorView(string value) : base(value)
        {
            NavigatorRepository = DependencyFactory.Resolve<IRepository<T>>();
            RoomRepository = DependencyFactory.Resolve<IRepository<RoomData>>();
        }

        protected virtual Expression<Func<RoomData, bool>> GetRoomPredicate(string query, UserInfo user)
        {
            Expression<Func<RoomData, bool>> predicate = x => x.Category is T;

            if (string.IsNullOrEmpty(query)) return predicate;

            var filter = "";
            var values = query.Split(' ');

            if (query.Contains(":"))
            {
                var args = query.Split(new[] {':'}, 2);

                if (args.Length >= 2)
                {
                    filter = args[0];
                    values = args[1].Split(' ');
                }
            }

            // TODO Refactor (I don't really like this...)
            switch (filter)
            {
                case "owner":
                    return predicate.AndAlso(x => x.Owner.Name == values[0]);
                case "tag":
                    return predicate.AndAlso(x => x.Tags.Any(tag => values.Contains(tag)));
                case "roomname":
                default:
                    return predicate.AndAlso(x => x.Name.IsIn(values));
                case "group":
                    return predicate.AndAlso(x => x.Group.Name.IsIn(values));
            }
        }

        public override IDictionary<NavigatorCategory, IList<RoomData>> GetCategories(string query, UserInfo user)
        {
            return RoomRepository
                .FilterBy(GetRoomPredicate(query, user))
                .GroupBy(x => x.Category)
                .ToDictionary(x => x.Key, x => (IList<RoomData>) x.ToList());
        }
    }
}