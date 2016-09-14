namespace Yupi.Model.Domain
{
    using System;
    using System.Collections.Generic;

    [Ignore]
    public class HumanStatus : EntityStatus
    {
        #region Constructors

        public HumanStatus(HumanEntity entity)
            : base(entity)
        {
            SetRights();
        }

        #endregion Constructors

        #region Properties

        public RoomRightLevel Rights
        {
            get; private set;
        }

        #endregion Properties

        #region Methods

        protected override void GetStates(List<IStatusString> states)
        {
            base.GetStates(states);
            states.Add(this.Rights);
        }

        protected virtual void SetRights()
        {
            Rights = RoomRightLevel.None;
            // TODO Implement
        }

        #endregion Methods
    }
}