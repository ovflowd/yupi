namespace Yupi.Controller
{
    using System;
    using System.Linq.Expressions;

    using Yupi.Model.Domain;
    using Yupi.Util;

    public class MyWorldView : NavigatorView<FlatNavigatorCategory>
    {
        #region Constructors

        public MyWorldView()
            : base("myworld_view")
        {
        }

        #endregion Constructors

        #region Methods

        protected override Expression<Func<RoomData, bool>> GetRoomPredicate(string query, UserInfo user)
        {
            return base.GetRoomPredicate(query, user).AndAlso(x => x.Owner == user);
        }

        #endregion Methods
    }
}