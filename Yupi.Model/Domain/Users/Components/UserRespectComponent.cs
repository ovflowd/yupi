namespace Yupi.Model.Domain.Components
{
    using System;

    public class UserRespectComponent
    {
        #region Properties

        public virtual int DailyCompetitionVotes
        {
            get; set;
        }

        public virtual int DailyPetRespectPoints
        {
            get; set;
        }

        public virtual int DailyRespectPoints
        {
            get; set;
        }

        public virtual int Respect
        {
            get; set;
        }

        #endregion Properties
    }
}