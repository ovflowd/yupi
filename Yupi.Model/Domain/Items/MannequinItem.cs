namespace Yupi.Model.Domain
{
    using System;

    public class MannequinItem : FloorItem<MannequinBaseItem>
    {
        #region Properties

        // TODO Enum for gender
        public virtual string Gender
        {
            get; set;
        }

        public virtual string Look
        {
            get; set;
        }

        #endregion Properties

        #region Constructors

        public MannequinItem()
        {
            Gender = "m";
            Look = "lg-270-82.ch-210-66";
        }

        #endregion Constructors

        #region Methods

        public override string GetExtraData()
        {
            // TODO What is the last string good for?
            return string.Join("\u0005", Gender, Look, "Mannequin");
        }

        #endregion Methods
    }
}