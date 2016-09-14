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

        public override void Compose(Yupi.Protocol.ISender session, IList<CatalogPage> sortedPages, string type,
            int rank)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                // TODO Refactor pages to TREE
                message.AppendBool(true);
                message.AppendInteger(0);
                message.AppendInteger(-1);
                message.AppendString("root");
                message.AppendString(string.Empty);
                message.AppendInteger(0);
                message.AppendInteger(sortedPages.Count());

                foreach (CatalogPage cat in sortedPages)
                {
                    message.AppendBool(cat.Visible);
                    message.AppendInteger(cat.IconImage);
                    message.AppendInteger(cat.Id);
                    message.AppendString(cat.CodeName);
                    message.AppendString(cat.Caption);
                    message.AppendInteger(cat.FlatOffers.Count);

                    foreach (uint i in cat.FlatOffers.Keys)
                        message.AppendInteger(i);

                    IOrderedEnumerable<CatalogPage> sortedSubPages =
                        cat.Children.Where(x => x.MinRank <= rank).OrderBy(x => x.OrderNum);

                    message.AppendInteger(sortedSubPages.Count());

                    foreach (CatalogPage subCat in sortedSubPages)
                    {
                        message.AppendBool(subCat.Visible);
                        message.AppendInteger(subCat.IconImage);
                        message.AppendInteger(subCat.Id);
                        message.AppendString(subCat.CodeName);
                        message.AppendString(subCat.Caption);
                        message.AppendInteger(subCat.FlatOffers.Count);

                        foreach (uint i2 in subCat.FlatOffers.Keys)
                            message.AppendInteger(i2);

                        message.AppendInteger(0);
                    }
                }

                message.AppendBool(false);
                message.AppendString(type);

                session.Send(message);
            }
        }

        #endregion Methods
    }
}