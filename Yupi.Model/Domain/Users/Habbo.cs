using System;
using Yupi.Protocol;
using Yupi.Model.Domain;

namespace Yupi.Model.Domain
{
	[Ignore]
	public class Habbo : ISender
	{
		public UserInfo Info { get; protected set; }

		public UserEntity RoomEntity { get; protected set; }

		public DateTime TimePingReceived;

		// TODO Is this at the right place?
		public string MachineId;

		// TODO Refactor?
		public bool IsRidingHorse { get; set; }
		public Room Room { get; set; }

		public ISession<Habbo> Session { get; private set; }

		// TODO Can this be solved in a better way?
		public Habbo GuideOtherUser;

		public RoomData TeleportingTo;

		public Habbo (ISession<Habbo> session, UserInfo info)
		{
			Info = info;
			Session = session;
			TimePingReceived = DateTime.Now;
		}

		public void Send (Yupi.Protocol.Buffers.ServerMessage message)
		{
			// TODO Link session
			throw new NotImplementedException ();
		}
	}
}

