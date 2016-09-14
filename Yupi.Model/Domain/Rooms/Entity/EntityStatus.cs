// ---------------------------------------------------------------------------------
// <copyright file="EntityStatus.cs" company="https://github.com/sant0ro/Yupi">
//   Copyright (c) 2016 Claudio Santoro, TheDoctor
// </copyright>
// <license>
//   Permission is hereby granted, free of charge, to any person obtaining a copy
//   of this software and associated documentation files (the "Software"), to deal
//   in the Software without restriction, including without limitation the rights
//   to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//   copies of the Software, and to permit persons to whom the Software is
//   furnished to do so, subject to the following conditions:
//
//   The above copyright notice and this permission notice shall be included in
//   all copies or substantial portions of the Software.
//
//   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//   IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//   FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//   AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//   LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//   OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//   THE SOFTWARE.
// </license>
// ---------------------------------------------------------------------------------
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

        #region Properties

        public EntityPosture Posture
        {
            get; private set;
        }

        #endregion Properties

        #region Constructors

        public EntityStatus(RoomEntity entity)
        {
            Contract.Requires(entity != null);
            this.Entity = entity;
            SetPosture(StandPosture.Default);
            TemporaryStates = new List<IStatusString>();
        }

        #endregion Constructors

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