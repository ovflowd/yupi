// ---------------------------------------------------------------------------------
// <copyright file="FlatCategoriesMessageComposer.cs" company="https://github.com/sant0ro/Yupi">
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
    using System.Collections.Generic;
    using System.Collections.Specialized;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public class FlatCategoriesMessageComposer : Yupi.Messages.Contracts.FlatCategoriesMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, IList<FlatNavigatorCategory> categories,
            Rank userRank)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(categories.Count);

                foreach (FlatNavigatorCategory flatCat in categories)
                {
                    message.AppendInteger(flatCat.Id);
                    message.AppendString(flatCat.Caption.Get(session.Language));
                    message.AppendBool(flatCat.MinRank <= userRank); // Visible
                    message.AppendBool(false); // Disabled?
                    message.AppendString(string.Empty); // Not used by client
                    message.AppendString(string.Empty); // TODO navigator.flatcategory.global.{VALUE} (External Texts)
                    message.AppendBool(flatCat.MinRank == Rank.Admin); // Requires Rank 7
                }

                session.Send(message);
            }
        }

        #endregion Methods
    }
}