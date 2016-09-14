using System;
using Yupi.Model.Domain;
using System.Linq.Expressions;
using Yupi.Util;

namespace Yupi.Controller
{
    public class MyWorldView : NavigatorView<FlatNavigatorCategory>
    {
        public MyWorldView() : base("myworld_view")
        {
        }

        protected override Expression<Func<RoomData, bool>> GetRoomPredicate(string query, UserInfo user)
        {
            return base.GetRoomPredicate(query, user).AndAlso(x => x.Owner == user);
        }
    }
}