namespace Yupi.Model.Domain
{
    using System.Collections.Generic;

    public class Poll
    {
        #region Properties

        public virtual int Id
        {
            get; protected set;
        }

        public virtual string Invitation
        {
            get; set;
        }

        public virtual string PollName
        {
            get; set;
        }

        public virtual string Prize
        {
            get; set;
        }

        public virtual IList<PollQuestion> Questions
        {
            get; protected set;
        }

        public virtual RoomData Room
        {
            get; set;
        }

        public virtual string Thanks
        {
            get; set;
        }

        #endregion Properties

        #region Constructors

        //public PollType Type{ get; set; }
        public Poll()
        {
            Questions = new List<PollQuestion>();
        }

        #endregion Constructors
    }
}