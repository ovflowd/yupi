// ---------------------------------------------------------------------------------
// <copyright file="RoomEntity.cs" company="https://github.com/sant0ro/Yupi">
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
    using System.Numerics;

    using Yupi.Model.Domain.Components;
    using Yupi.Protocol;

    [Ignore]
    public abstract class RoomEntity
    {
        #region Fields

        public int Id;

        private Queue<Vector2> Steps;

        #endregion Fields

        #region Delegates

        [Ignore]
        public delegate void OnSleepChange(RoomEntity entity);

        #endregion Delegates

        #region Properties

        public abstract BaseInfo BaseInfo
        {
            get;
        }

        public bool IsAsleep
        {
            get; private set;
        }

        public bool NeedsUpdate
        {
            get; private set;
        }

        public OnSleepChange OnSleepChangeCB
        {
            get; set;
        }

        public Vector3 Position
        {
            get; private set;
        }

        public Room Room
        {
            get; private set;
        }

        public int RotBody
        {
            get; private set;
        }

        // TODO Use enum
        public int RotHead
        {
            get; private set;
        }

        public abstract EntityStatus Status
        {
            get;
        }

        public abstract EntityType Type
        {
            get;
        }

        #endregion Properties

        #region Constructors

        public RoomEntity(Room room, int id)
        {
            this.Id = id;
            this.Room = room;
            Steps = new Queue<Vector2>();
        }

        #endregion Constructors

        #region Methods

        public bool CanWalk()
        {
            // TODO Implement
            return true;
        }

        public virtual void HandleChatMessage(UserEntity user, Action<Habbo> sendTo)
        {
            // TODO Implement Tent
            // TODO Implement Distance?

            int rotation = Position.CalculateRotation(user.Position);
            // TODO Should only be temporary
            // TODO Add distance calculation!
            SetHeadRotation(rotation);
        }

        public bool HasSteps()
        {
            return Steps.Count > 0;
        }

        public void NextStep()
        {
            if (!HasSteps())
            {
                throw new InvalidOperationException();
            }

            Vector2 nextStep = Steps.Dequeue();

            // TODO Be consequent about Vector2 vs Vector3!
            Vector3 nextPos = new Vector3(nextStep.X, nextStep.Y,
                Room.HeightMap.GetTileHeight((int) nextStep.X, (int) nextStep.Y));

            if (HasSteps())
            {
                Vector2 move = Steps.Peek();
                SetRotation(nextPos.ToVector2().CalculateRotation(move));
                Status.SetPosture(
                    new WalkPosture(new Vector3(move.X, move.Y, Room.HeightMap.GetTileHeight((int) move.X, (int) move.Y))));
            }
            else
            {
                Status.SetPosture(StandPosture.Default);
            }

            SetPosition(nextPos);
        }

        public virtual void OnRoomExit()
        {
            // Do nothing
        }

        public void SetHeadRotation(int rotation)
        {
            if (rotation < 0 || rotation > 7)
            {
                throw new ArgumentOutOfRangeException("rotation");
            }

            int delta = this.RotBody - rotation;
            this.RotHead = (this.RotBody - Math.Sign(delta))%8;
            ScheduleUpdate();
        }

        public void SetPosition(Vector3 newPosition)
        {
            this.Position = newPosition;
            ScheduleUpdate();
        }

        public void SetRotation(int rotation)
        {
            if (rotation < 0 || rotation > 7)
            {
                throw new ArgumentOutOfRangeException("rotation");
            }

            this.RotBody = rotation;
            this.RotHead = rotation;
            ScheduleUpdate();
        }

        public void Sleep()
        {
            if (!this.IsAsleep)
            {
                this.IsAsleep = true;
                OnSleepChangeCB(this);
            }
        }

        public void Wake()
        {
            if (this.IsAsleep)
            {
                this.IsAsleep = false;
                OnSleepChangeCB(this);
            }
        }

        public void Walk(Vector2 target)
        {
            this.Steps = new Queue<Vector2>(Room.Pathfinder.Find(this.Position.ToVector2(), target));
        }

        internal void ScheduleUpdate()
        {
            NeedsUpdate = true;
        }

        internal void UpdateComplete()
        {
            NeedsUpdate = false;
            Status.OnUpdateComplete();
        }

        #endregion Methods
    }
}