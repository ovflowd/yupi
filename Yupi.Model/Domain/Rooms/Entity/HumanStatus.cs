using System.Collections.Generic;

namespace Yupi.Model.Domain
{
    [Ignore]
    public class HumanStatus : EntityStatus
    {
        public HumanStatus(HumanEntity entity) : base(entity)
        {
            SetRights();
        }

        public RoomRightLevel Rights { get; private set; }

        protected override void GetStates(List<IStatusString> states)
        {
            base.GetStates(states);
            states.Add(Rights);
        }

        protected virtual void SetRights()
        {
            Rights = RoomRightLevel.None;
            // TODO Implement
        }
    }
}