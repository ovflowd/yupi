namespace Yupi.Model.Domain
{
    using System;

    public class AvatarEffect
    {
        #region Properties

        // TODO Redundant information (UserEffectComponent.ActiveEffect)
        public virtual bool Activated
        {
            get; set;
        }

        public virtual DateTime ActivatedAt
        {
            get; set;
        }

        public virtual int EffectId
        {
            get; set;
        }

        public virtual int Id
        {
            get; set;
        }

        public virtual int TotalDuration
        {
            get; set;
        }

        // TODO What is this good for?
        public virtual short Type
        {
            get; set;
        }

        #endregion Properties

        #region Methods

        public virtual void Activate()
        {
            Activated = true;
            ActivatedAt = DateTime.Now;
        }

        public virtual bool HasExpired()
        {
            return TimeLeft() != -1 && TimeLeft() <= 0;
        }

        public virtual int TimeLeft()
        {
            if (!Activated || TotalDuration == -1)
                return -1;

            double remaining = (DateTime.Now - ActivatedAt).TotalSeconds;

            if (remaining >= TotalDuration)
                return 0;

            return (int) (TotalDuration - remaining);
        }

        #endregion Methods
    }
}