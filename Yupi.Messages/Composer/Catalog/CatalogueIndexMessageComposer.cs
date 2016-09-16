// ---------------------------------------------------------------------------------
// <copyright file="CatalogueIndexMessageComposer.cs" company="https://github.com/sant0ro/Yupi">
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
namespace Yupi.Messages.Catalog
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public class CatalogueIndexMessageComposer : Yupi.Messages.Contracts.CatalogueIndexMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, CatalogPage root, string type,
            int rank)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                AppendPage(root, message, rank);
                message.AppendBool(false); // TODO CATALOG_NEW_ITEMS_SHOW

                /* TODO
                 * Type:
                 * public static const NORMAL:String = "NORMAL";
                 * public static const BUILDER:String = "BUILDERS_CLUB";
                 */
                message.AppendString(type);

                session.Send(message);
            }
        }

        private void AppendPage(CatalogPage page, ServerMessage message, int rank) {
            message.AppendBool(page.Visible);
            message.AppendInteger(page.Icon);
            message.AppendInteger(page.Id);
            message.AppendString(page.CodeName);
            message.AppendString(page.Caption);
            message.AppendInteger(page.Items.Count);

            foreach (CatalogOffer item in page.Items)
            {
                message.AppendInteger(item.Id);
            }

            IOrderedEnumerable<CatalogPage> sortedSubPages =
                page.Children.Where(x => x.MinRank <= rank).OrderBy(x => x.OrderNum);

            message.AppendInteger(sortedSubPages.Count());

            foreach(CatalogPage child in sortedSubPages) {
                AppendPage(child, message, rank);
            }
        }

        #endregion Methods
    }
}