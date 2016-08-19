using System;
using Yupi.Model.Domain;

namespace Yupi.Controller
{
	public class MyWorldView : NavigatorView<FlatNavigatorCategory>
	{
		public MyWorldView () : base("myworld_view")
		{

		}

		protected override Func<RoomData, bool> GetRoomPredicate (string query, UserInfo user)
		{
			return x => base.GetRoomPredicate (query, user)(x) && x.Owner == user;
		}
	}
}

