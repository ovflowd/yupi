using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Yupi.Model.Domain
{
    [Ignore]
    public abstract class EntityStatus
    {
        private readonly RoomEntity Entity;

        private readonly List<IStatusString> TemporaryStates;

        public EntityStatus(RoomEntity entity)
        {
            Contract.Requires(entity != null);
            Entity = entity;
            SetPosture(StandPosture.Default);
            TemporaryStates = new List<IStatusString>();
        }

        public EntityPosture Posture { get; private set; }

        public void SetPosture(EntityPosture posture)
        {
            Contract.Requires(posture != null);
            Posture = posture;
            OnChange();
        }

        internal void OnUpdateComplete()
        {
            TemporaryStates.Clear();
        }

        public bool IsSitting()
        {
            return Posture is SitPosture;
        }

        public void Stand()
        {
            SetPosture(StandPosture.Default);
        }

        public void Sit()
        {
            SetPosture(SitPosture.Default);
        }

        public void Sign(Sign sign)
        {
            TemporaryStates.Add(sign);
            OnChange();
        }

        public override string ToString()
        {
            return ToStatusString();
        }

        protected void OnChange()
        {
            Entity.ScheduleUpdate();
        }

        protected virtual void GetStates(List<IStatusString> states)
        {
            states.Add(Posture);
            states.AddRange(TemporaryStates);
        }

        private string ToStatusString()
        {
            var states = new List<IStatusString>();
            GetStates(states);

            var stateStrings = new List<string>(states.Count);

            foreach (var state in states)
            {
                var statusStr = state?.ToStatusString();

                if (!string.IsNullOrEmpty(statusStr)) stateStrings.Add(statusStr);
            }

            return string.Join("/", stateStrings);
        }
    }
}