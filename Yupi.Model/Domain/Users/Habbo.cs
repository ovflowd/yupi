namespace Yupi.Model.Domain
{
    using System;

    using Yupi.Model.Domain;
    using Yupi.Net;
    using Yupi.Protocol;

    [Ignore]
    public class Habbo : ISender
    {
        #region Fields

        // TODO Can this be solved in a better way?
        public Habbo GuideOtherUser;

        // TODO Is this at the right place?
        public string MachineId;
        public string ReleaseName;
        public RoomData TeleportingTo;
        public DateTime TimePingReceived;

        #endregion Fields

        #region Constructors

        public Habbo(ISession<Habbo> session, IRouter router)
        {
            this.Session = session;
            this.Router = router;
            TimePingReceived = DateTime.Now;
        }

        #endregion Constructors

        #region Properties

        public UserInfo Info
        {
            get; set;
        }

        // TODO Refactor?
        public bool IsRidingHorse
        {
            get; set;
        }

        // TODO Remove?
        public Room Room
        {
            get { return RoomEntity?.Room; }
        }

        public UserEntity RoomEntity
        {
            get; set;
        }

        public IRouter Router
        {
            get; set;
        }

        public ISession<Habbo> Session
        {
            get; set;
        }

        #endregion Properties

        #region Methods

        public void Send(Yupi.Protocol.Buffers.ServerMessage message)
        {
            Session.Send(message.GetReversedBytes());
        }

        #endregion Methods
    }
}