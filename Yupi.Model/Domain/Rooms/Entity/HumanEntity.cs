using System;

namespace Yupi.Model.Domain
{
    [Ignore]
    public abstract class HumanEntity : RoomEntity
    {
        public HumanStatus HumanStatus { get; private set; }

        public Dance Dance { get; private set; }

        [Ignore]
        public delegate void OnDanceChangeType(HumanEntity entity);

        public override EntityStatus Status
        {
            get { return HumanStatus; }
        }

        public OnDanceChangeType OnDanceChange;

        public HumanEntity(Room room, int id) : base(room, id)
        {
            HumanStatus = new HumanStatus(this);
            this.Dance = Dance.None;
        }

        public virtual void SetDance(Dance dance)
        {
            this.Dance = dance;
            OnDanceChange(this);
        }

        public void StopDance()
        {
            this.SetDance(Dance.None);
        }
    }
}