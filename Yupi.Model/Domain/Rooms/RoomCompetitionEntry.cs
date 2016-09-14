namespace Yupi.Model.Domain
{
    using System;

    public class RoomCompetitionEntry
    {
        #region Properties

        public virtual int Id
        {
            get; set;
        }

        public virtual RoomData Room
        {
            get; set;
        }

        public virtual int Votes
        {
            get; set;
        }

        #endregion Properties
    }
}