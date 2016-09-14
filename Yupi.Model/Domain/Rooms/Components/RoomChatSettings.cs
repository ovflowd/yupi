namespace Yupi.Model.Domain.Components
{
    using System;

    using Headspring;

    public class RoomChatSettings
    {
        #region Constructors

        public RoomChatSettings()
        {
            Balloon = ChatBalloon.Normal;
            Speed = ChatSpeed.Normal;
            MaxDistance = 14;
            FloodProtection = FloodProtection.Standard;
            Type = ChatType.FreeFlowMode;
        }

        #endregion Constructors

        #region Properties

        public virtual ChatBalloon Balloon
        {
            get; set;
        }

        public virtual FloodProtection FloodProtection
        {
            get; set;
        }

        public virtual int MaxDistance
        {
            get; protected set;
        }

        public virtual ChatSpeed Speed
        {
            get; set;
        }

        public virtual ChatType Type
        {
            get; set;
        }

        #endregion Properties

        #region Methods

        public bool isValidDistance(int distance)
        {
            return (distance >= 3 && distance <= 90);
        }

        public void SetMaxDistance(int distance)
        {
            if (!isValidDistance(distance))
            {
                throw new ArgumentOutOfRangeException("distance");
            }

            this.MaxDistance = distance;
        }

        #endregion Methods
    }
}