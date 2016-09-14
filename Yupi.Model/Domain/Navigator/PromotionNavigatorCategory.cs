namespace Yupi.Model.Domain
{
    using System;

    public class PromotionNavigatorCategory : NavigatorCategory
    {
        #region Properties

        // TODO Can this be moved up?
        public virtual bool Visible
        {
            get; set;
        }

        #endregion Properties
    }
}