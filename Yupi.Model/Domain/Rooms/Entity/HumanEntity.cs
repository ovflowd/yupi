// ---------------------------------------------------------------------------------
// <copyright file="HumanEntity.cs" company="https://github.com/sant0ro/Yupi">
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