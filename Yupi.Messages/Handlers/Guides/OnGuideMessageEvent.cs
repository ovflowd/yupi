// ---------------------------------------------------------------------------------
// <copyright file="OnGuideMessageEvent.cs" company="https://github.com/sant0ro/Yupi">
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
namespace Yupi.Messages.Guides
{
    using System;

    using Yupi.Controller;
    using Yupi.Model;
    using Yupi.Model.Domain;

    public class OnGuideMessageEvent : AbstractHandler
    {
        #region Fields

        private GuideManager GuideManager;

        #endregion Fields

        #region Constructors

        public OnGuideMessageEvent()
        {
            GuideManager = DependencyFactory.Resolve<GuideManager>();
        }

        #endregion Constructors

        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            request.GetBool(); // TODO Unused

            string idAsString = request.GetString();

            int userId;
            int.TryParse(idAsString, out userId);

            if (userId == 0)
            {
                return;
            }

            string message = request.GetString();

            Habbo guide = GuideManager.GetRandomGuide();

            if (guide == null)
            {
                router.GetComposer<OnGuideSessionErrorComposer>().Compose(session);
                return;
            }

            router.GetComposer<OnGuideSessionAttachedMessageComposer>().Compose(session, false, userId, message, 30);
            router.GetComposer<OnGuideSessionAttachedMessageComposer>().Compose(guide, true, userId, message, 15);

            guide.GuideOtherUser = session;
            session.GuideOtherUser = guide;
        }

        #endregion Methods
    }
}