using System;
using Yupi.Model.Domain;

namespace Yupi.Controller
{
    public class OfficialView : NavigatorView<OfficialNavigatorCategory>
    {
        public OfficialView() : base("official_view")
        {
        }
    }
}