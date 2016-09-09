using System;
using Yupi.Protocol;
using Yupi.Model.Domain;
using Yupi.Net;

namespace Yupi.Model.Domain
{
	[Ignore]
	public class Habbo : ISender
	{
		public UserInfo Info { get; set; }

		public UserEntity RoomEntity { get; set; }

		public string ReleaseName;

		public DateTime TimePingReceived;

		// TODO Is this at the right place?
		public string MachineId;

		// TODO Refactor?
		public bool IsRidingHorse { get; set; }

		// TODO Remove?
		public Room Room { 
			get { 
				return RoomEntity?.Room; 
			} 
		}

		public ISession<Habbo> Session { get; set; }

		public IRouter Router { get; set; }

		// TODO Can this be solved in a better way?
		public Habbo GuideOtherUser;

		public RoomData TeleportingTo;

		public Habbo (ISession<Habbo> session, IRouter router)
		{
			this.Session = session;
			this.Router = router;
			TimePingReceived = DateTime.Now;
		}

		public void Send (Yupi.Protocol.Buffers.ServerMessage message)
		{
			Session.Send (message.GetReversedBytes ());
		}
	}
}

