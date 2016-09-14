namespace Yupi.Model.Domain
{
    using System;

    public class PostItBaseItem : WallBaseItem
    {
        #region Methods

        public override Item CreateNew()
        {
            return new PostItItem()
            {
                BaseItem = this
            };
        }

        #endregion Methods
    }
}