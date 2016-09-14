using System;

namespace Yupi.Model.Domain.Components
{
    public class UserRespectComponent
    {
        public virtual int DailyRespectPoints { get; set; }
        public virtual int DailyPetRespectPoints { get; set; }
        public virtual int DailyCompetitionVotes { get; set; }
        public virtual int Respect { get; set; }
    }
}