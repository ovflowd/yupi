namespace Yupi.Model.Domain
{
    using System;

    [Ignore]
    public abstract class HumanEntity : RoomEntity
    {
        #region Fields

        public OnDanceChangeType OnDanceChange;

        #endregion Fields

        #region Delegates

        [Ignore]
        public delegate void OnDanceChangeType(HumanEntity entity);

        #endregion Delegates

        #region Properties

        public Dance Dance
        {
            get; private set;
        }

        public HumanStatus HumanStatus
        {
            get; private set;
        }

        public override EntityStatus Status
        {
            get { return HumanStatus; }
        }

        #endregion Properties

        #region Constructors

        public HumanEntity(Room room, int id)
            : base(room, id)
        {
            HumanStatus = new HumanStatus(this);
            this.Dance = Dance.None;
        }

        #endregion Constructors

        #region Methods

        public virtual void SetDance(Dance dance)
        {
            this.Dance = dance;
            OnDanceChange(this);
        }

        public void StopDance()
        {
            this.SetDance(Dance.None);
        }

        #endregion Methods
    }
}