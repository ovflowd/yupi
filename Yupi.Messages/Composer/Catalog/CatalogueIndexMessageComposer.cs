using System.Collections.Generic;
using System.Linq;
using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Catalog
{
    public class CatalogueIndexMessageComposer : Contracts.CatalogueIndexMessageComposer
    {
        public override void Compose(ISender session, IList<CatalogPage> sortedPages, string type, int rank)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                // TODO Refactor pages to TREE
                message.AppendBool(true);
                message.AppendInteger(0);
                message.AppendInteger(-1);
                message.AppendString("root");
                message.AppendString(string.Empty);
                message.AppendInteger(0);
                message.AppendInteger(sortedPages.Count());

                foreach (var cat in sortedPages)
                {
                    message.AppendBool(cat.Visible);
                    message.AppendInteger(cat.IconImage);
                    message.AppendInteger(cat.Id);
                    message.AppendString(cat.CodeName);
                    message.AppendString(cat.Caption);
                    message.AppendInteger(cat.FlatOffers.Count);

                    foreach (var i in cat.FlatOffers.Keys)
                        message.AppendInteger(i);

                    var sortedSubPages =
                        cat.Children.Where(x => x.MinRank <= rank).OrderBy(x => x.OrderNum);

                    message.AppendInteger(sortedSubPages.Count());

                    foreach (var subCat in sortedSubPages)
                    {
                        message.AppendBool(subCat.Visible);
                        message.AppendInteger(subCat.IconImage);
                        message.AppendInteger(subCat.Id);
                        message.AppendString(subCat.CodeName);
                        message.AppendString(subCat.Caption);
                        message.AppendInteger(subCat.FlatOffers.Count);

                        foreach (var i2 in subCat.FlatOffers.Keys)
                            message.AppendInteger(i2);

                        message.AppendInteger(0);
                    }
                }

                message.AppendBool(false);
                message.AppendString(type);

                session.Send(message);
            }
        }
    }
}