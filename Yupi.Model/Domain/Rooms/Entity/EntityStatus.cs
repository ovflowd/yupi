namespace Yupi.Model.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Numerics;
    using System.Text;

    [Ignore]
    public abstract class EntityStatus
    {
        #region Fields

        private RoomEntity Entity;
        private List<IStatusString> TemporaryStates;

        #endregion Fields

        #region Constructors

        public EntityStatus(RoomEntity entity)
        {
            Contract.Requires(entity != null);
            this.Entity = entity;
            SetPosture(StandPosture.Default);
            TemporaryStates = new List<IStatusString>();
        }

        #endregion Constructors

        #region Properties

        public EntityPosture Posture
        {
            get; private set;
        }

        #endregion Properties

        #region Methods

        public bool IsSitting()
        {
            return this.Posture is SitPosture;
        }

        public void SetPosture(EntityPosture posture)
        {
            Contract.Requires(posture != null);
            this.Posture = posture;
            OnChange();
        }

        public void Sign(Sign sign)
        {
            this.TemporaryStates.Add(sign);
            this.OnChange();
        }

        public void Sit()
        {
            SetPosture(SitPosture.Default);
        }

        public void Stand()
        {
            SetPosture(StandPosture.Default);
        }

        public override string ToString()
        {
            return ToStatusString();
        }

        internal void OnUpdateComplete()
        {
            this.TemporaryStates.Clear();
        }

        protected virtual void GetStates(List<IStatusString> states)
        {
            states.Add(this.Posture);
            states.AddRange(this.TemporaryStates);
        }

        protected void OnChange()
        {
            this.Entity.ScheduleUpdate();
        }

        private string ToStatusString()
        {
            List<IStatusString> states = new List<IStatusString>();
            GetStates(states);

            List<string> stateStrings = new List<string>(states.Count);

            foreach (IStatusString state in states)
            {
                string statusStr = state?.ToStatusString();

                if (!string.IsNullOrEmpty(statusStr))
                {
                    stateStrings.Add(statusStr);
                }
            }

            return string.Join("/", stateStrings);
        }

        #endregion Methods
    }
}