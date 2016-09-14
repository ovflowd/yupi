namespace Yupi.Model.Domain
{
    using System;

    public class FootballGateItem : FloorItem<FootballGateBaseItem>
    {
        #region Properties

        public virtual string LookFemale
        {
            get; set;
        }

        public virtual string LookMale
        {
            get; set;
        }

        #endregion Properties

        #region Constructors

        public FootballGateItem()
        {
            LookMale = "lg-270-82.ch-210-66";
            LookFemale = "lg-270-82.ch-210-66";
        }

        #endregion Constructors

        #region Methods

        public override string GetExtraData()
        {
            return String.Join(";", LookMale, LookFemale);
        }

        #endregion Methods
    }
}