// ---------------------------------------------------------------------------------
// <copyright file="Global.asax.cs" company="https://github.com/sant0ro/Yupi">
//   Copyright (c) 2016 ${CopyrightHolder}
// </copyright>
// <license>
//   Permission is hereby granted, free of charge, to any person obtaining a copy
//   of this software and associated documentation files (the "Software"), to deal
//   in the Software without restriction, including without limitation the rights
//   to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//   copies of the Software, and to permit persons to whom the Software is
//   furnished to do so, subject to the following conditions:
//
//   The above copyright notice and this permission notice shall be included in
//   all copies or substantial portions of the Software.
//
//   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//   IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//   FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//   AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//   LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//   OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//   THE SOFTWARE.
// </license>
// ---------------------------------------------------------------------------------
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Http;

namespace Yupi.Web
{
    public class Global : HttpApplication
    {
        protected void Application_Start ()
        {
            AreaRegistration.RegisterAllAreas ();
            GlobalConfiguration.Configure (WebApiConfig.Register);
            RouteConfig.RegisterRoutes (RouteTable.Routes);
            GlobalFilters.Filters.Add (new ConfigActionFilter (), 0);
        }
    }
}
