// ---------------------------------------------------------------------------------
// <copyright file="NavigatorMetaDataComposer.cs" company="https://github.com/sant0ro/Yupi">
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
namespace Yupi.Messages.Navigator
{
    using System;

    using Yupi.Controller;
    using Yupi.Protocol.Buffers;

    public class NavigatorMetaDataComposer : Yupi.Messages.Contracts.NavigatorMetaDataComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                NavigatorView[] views = NavigatorView.GetSorted();

                message.AppendInteger(views.Length);

                foreach (NavigatorView view in views)
                {
                    message.AppendString(view.DisplayName);
                    // TODO Could not find out where this is being used in client?!
                    message.AppendInteger(1); // Count Saved Searches
                    message.AppendInteger(1); // Saved Search Id
                    message.AppendString("query");
                    message.AppendString("filter");
                    message.AppendString("localization");
                }
                session.Send(message);
            }
        }

        #endregion Methods
    }
}