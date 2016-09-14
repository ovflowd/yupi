namespace Yupi.Model.Domain
{
    using System;
    using System.Collections.Generic;

    public class EcotronLevel
    {
        #region Properties

        public virtual int Id
        {
            get; protected set;
        }

        public virtual IList<EcotronReward> Rewards
        {
            get; protected set;
        }

        #endregion Properties

        #region Constructors

        public EcotronLevel()
        {
            Rewards = new List<EcotronReward>();
        }

        #endregion Constructors
    }
}