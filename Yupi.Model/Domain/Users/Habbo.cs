using System;
using Yupi.Net;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Model.Domain
{
    [Ignore]
    public class Habbo : ISender
    {
        // TODO Can this be solved in a better way?
        public Habbo GuideOtherUser;

        // TODO Is this at the right place?
        public string MachineId;

        public string ReleaseName;

        public RoomData TeleportingTo;

        public DateTime TimePingReceived;

        public Habbo(ISession<Habbo> session, IRouter router)
        {
            Session = session;
            Router = router;
            TimePingReceived = DateTime.Now;
        }

        public UserInfo Info { get; set; }

        public UserEntity RoomEntity { get; set; }

        // TODO Refactor?
        public bool IsRidingHorse { get; set; }

        // TODO Remove?
        public Room Room
        {
            get { return RoomEntity?.Room; }
        }

        public ISession<Habbo> Session { get; set; }

        public IRouter Router { get; set; }

        public void Send(ServerMessage message)
        {
            Session.Send(message.GetReversedBytes());
        }
    }
}