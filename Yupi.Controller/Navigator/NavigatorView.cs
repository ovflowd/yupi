// ---------------------------------------------------------------------------------
// <copyright file="NavigatorView.cs" company="https://github.com/sant0ro/Yupi">
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
namespace Yupi.Controller
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using Headspring;

    using NHibernate.Criterion;

    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Model.Repository;
    using Yupi.Util;

    // TODO Not sure wether this should be here or in Model
    public abstract class NavigatorView : Enumeration<NavigatorView>
    {
        #region Fields

        /// <summary>
        /// Public rooms
        /// </summary>
        public static readonly HotelView Hotel = new HotelView(2);

        /// <summary>
        /// User's Rooms
        /// </summary>
        public static readonly MyWorldView MyWorld = new MyWorldView(3);

        /// <summary>
        /// Official Rooms
        /// </summary>
        public static readonly OfficialView Official = new OfficialView(1);

        /// <summary>
        /// Event Rooms
        /// </summary>
        public static readonly RoomAdsView RoomAds = new RoomAdsView(4);

        private static NavigatorView[] Sorted;

        #endregion Fields

        #region Constructors

        public NavigatorView(int value, string displayName)
            : base(value, displayName)
        {
        }

        #endregion Constructors

        #region Methods

        public static NavigatorView[] GetSorted()
        {
            if (NavigatorView.Sorted == null)
            {
                NavigatorView.Sorted = GetAll().OrderBy(x => x.Value).ToArray();
            }
            return NavigatorView.Sorted;
        }

        public abstract IDictionary<NavigatorCategory, IList<RoomData>> GetCategories(string query, UserInfo user);

        #endregion Methods
    }

    public abstract class NavigatorView<T> : NavigatorView
        where T : NavigatorCategory
    {
        #region Fields

        protected IRepository<T> NavigatorRepository;
        protected IRepository<RoomData> RoomRepository;

        #endregion Fields

        #region Constructors

        protected NavigatorView(int value, string displayName)
            : base(value, displayName)
        {
            NavigatorRepository = DependencyFactory.Resolve<IRepository<T>>();
            RoomRepository = DependencyFactory.Resolve<IRepository<RoomData>>();
        }

        #endregion Constructors

        #region Methods

        public override IDictionary<NavigatorCategory, IList<RoomData>> GetCategories(string query, UserInfo user)
        {
            return RoomRepository
                .FilterBy(GetRoomPredicate(query, user))
                .GroupBy(x => x.Category)
                .ToDictionary(x => x.Key, x => (IList<RoomData>) x.ToList());
        }

        protected virtual Expression<Func<RoomData, bool>> GetRoomPredicate(string query, UserInfo user)
        {
            Expression<Func<RoomData, bool>> predicate = (x => x.Category is T);

            if (string.IsNullOrEmpty(query))
            {
                return predicate;
            }

            string filter = "";
            string[] values = query.Split(' ');

            if (query.Contains(":"))
            {
                string[] args = query.Split(new char[] {':'}, 2);

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
                return predicate.AndAlso(x => x.Tags.Any(y => y.Value.IsIn(values)));
                case "roomname":
                default:
                    return predicate.AndAlso(x => x.Name.IsIn(values));
                case "group":
                    return predicate.AndAlso(x => x.Group.Name.IsIn(values));
            }
        }

        #endregion Methods
    }
}