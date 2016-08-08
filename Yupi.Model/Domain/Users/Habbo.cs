using System;
using Yupi.Protocol;

namespace Yupi.Model.Domain
{
	[Ignore]
	public class Habbo : ISender
	{
		public UserInfo Info { get; protected set; }

		public UserEntity RoomEntity { get; protected set; }

		public bool IsOnline { get; set; }

		// TODO Refactor?
		public bool IsRidingHorse { get; set; }
		public Room Room { get; set; }

		public ISession<Habbo> Session { get; private set; }

		public Habbo ()
		{
			Info = new UserInfo ();
		}

		public void Send (Yupi.Protocol.Buffers.ServerMessage message)
		{
			// TODO Link session
			throw new NotImplementedException ();
		}
	}
}

