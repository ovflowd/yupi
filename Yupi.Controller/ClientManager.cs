using System;
using System.Collections.Generic;
using Yupi.Protocol;
using Yupi.Model.Domain;
using System.Linq;

namespace Yupi.Controller
{
	public class ClientManager
	{
		public IList<ISession<Habbo>> Connections { get; private set; }

		public ClientManager ()
		{
			Connections = new List<ISession<Habbo>> ();
		}

		public bool IsOnline(UserInfo info) {
			return Connections.Any (x => x.UserData.Info == info);
		}

		public ISession<Habbo> GetByInfo(UserInfo info) {
			return Connections.Single (x => x.UserData.Info == info);
		}
	}
}

