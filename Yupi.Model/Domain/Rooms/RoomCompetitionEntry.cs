using System;

namespace Yupi.Model.Domain
{
    public class RoomCompetitionEntry
    {
        public virtual int Id { get; set; }
        public virtual RoomData Room { get; set; }
        public virtual int Votes { get; set; }
    }
}