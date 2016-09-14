namespace Yupi.Model.Domain
{
    [Ignore]
    public abstract class HumanEntity : RoomEntity
    {
        [Ignore]
        public delegate void OnDanceChangeType(HumanEntity entity);

        public OnDanceChangeType OnDanceChange;

        public HumanEntity(Room room, int id) : base(room, id)
        {
            HumanStatus = new HumanStatus(this);
            Dance = Dance.None;
        }

        public HumanStatus HumanStatus { get; }

        public Dance Dance { get; private set; }

        public override EntityStatus Status
        {
            get { return HumanStatus; }
        }

        public virtual void SetDance(Dance dance)
        {
            Dance = dance;
            OnDanceChange(this);
        }

        public void StopDance()
        {
            SetDance(Dance.None);
        }
    }
}