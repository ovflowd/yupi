namespace Yupi.Model.Domain
{
    using System;

    public class PostItItem : WallItem<WallBaseItem>
    {
        #region Constructors

        public PostItItem()
        {
            Color = "FFFF33";
        }

        #endregion Constructors

        #region Properties

        // TODO Validate RGB-HTML string
        public virtual string Color
        {
            get; set;
        }

        public virtual string Text
        {
            get; set;
        }

        #endregion Properties

        #region Methods

        public override string GetExtraData()
        {
            return Color;
        }

        #endregion Methods
    }
}