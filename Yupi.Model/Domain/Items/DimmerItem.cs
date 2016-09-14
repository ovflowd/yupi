namespace Yupi.Model.Domain
{
    using System;

    public class DimmerItem : WallItem<DimmerBaseItem>
    {
        #region Constructors

        public DimmerItem()
        {
            Data = new MoodlightData();
        }

        #endregion Constructors

        #region Properties

        public virtual MoodlightData Data
        {
            get; set;
        }

        #endregion Properties
    }
}