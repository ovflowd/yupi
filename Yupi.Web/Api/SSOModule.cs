// ---------------------------------------------------------------------------------
// <copyright file="SSOModule.cs" company="https://github.com/sant0ro/Yupi">
//   Copyright (c) 2016 Claudio Santoro, TheDoctor
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
namespace Yupi.Web
{
    using System;

    using Nancy;

    using Yupi.Controller;
    using Yupi.Model;

    public class SSOModule : NancyModule
    {
        #region Fields

        private SSOManager SSOManager;

        #endregion Fields

        #region Constructors

        public SSOModule() : base("/api")
        {
            SSOManager = DependencyFactory.Resolve<SSOManager>();
            Post["/sso/{id:int}"] = parameters =>
            {
                string ticket = SSOManager.GenerateTicket(parameters.id);
                return Response.AsJson(new SSOTicket(ticket));
            };
        }

        #endregion Constructors

        #region Nested Types

        private class SSOTicket
        {
            #region Properties

            public string Ticket
            {
                get; set;
            }

            #endregion Properties

            #region Constructors

            public SSOTicket(string ticket)
            {
                this.Ticket = ticket;
            }

            #endregion Constructors
        }

        #endregion Nested Types
    }
}