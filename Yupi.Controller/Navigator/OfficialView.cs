namespace Yupi.Controller
{
    using System;

    using Yupi.Model.Domain;

    public class OfficialView : NavigatorView<OfficialNavigatorCategory>
    {
        #region Constructors

        public OfficialView()
            : base("official_view")
        {
        }

        #endregion Constructors
    }
}