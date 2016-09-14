using System;

namespace Yupi.Model.Domain.Components
{
    public class RoomChatSettings
    {
        public RoomChatSettings()
        {
            Balloon = ChatBalloon.Normal;
            Speed = ChatSpeed.Normal;
            MaxDistance = 14;
            FloodProtection = FloodProtection.Standard;
            Type = ChatType.FreeFlowMode;
        }

        public virtual ChatBalloon Balloon { get; set; }
        public virtual ChatSpeed Speed { get; set; }
        public virtual int MaxDistance { get; protected set; }
        public virtual FloodProtection FloodProtection { get; set; }
        public virtual ChatType Type { get; set; }

        public bool isValidDistance(int distance)
        {
            return (distance >= 3) && (distance <= 90);
        }

        public void SetMaxDistance(int distance)
        {
            if (!isValidDistance(distance)) throw new ArgumentOutOfRangeException("distance");

            MaxDistance = distance;
        }
    }
}