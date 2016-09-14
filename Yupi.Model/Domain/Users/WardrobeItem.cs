using System;

namespace Yupi.Model.Domain
{
    public class WardrobeItem
    {
        public virtual int Id { get; protected set; }
        public virtual int Slot { get; set; }
        public virtual string Look { get; set; }
        public virtual string Gender { get; set; }
    }
}