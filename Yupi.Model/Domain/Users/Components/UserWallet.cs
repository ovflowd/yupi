namespace Yupi.Model.Domain.Components
{
    using System;

    public class UserWallet
    {
        #region Properties

        public virtual int AchievementPoints
        {
            get; set;
        }

        public virtual int Credits
        {
            get; set;
        }

        public virtual int Diamonds
        {
            get; set;
        }

        public virtual int Duckets
        {
            get; set;
        }

        #endregion Properties
    }
}