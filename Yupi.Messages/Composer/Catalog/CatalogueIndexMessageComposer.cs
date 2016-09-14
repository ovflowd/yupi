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