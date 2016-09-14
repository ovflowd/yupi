using System.Collections.Generic;
using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Navigator
{
    public class FlatCategoriesMessageComposer : Contracts.FlatCategoriesMessageComposer
    {
        public override void Compose(ISender session, IList<FlatNavigatorCategory> categories, int userRank)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(categories.Count);

                foreach (var flatCat in categories)
                {
                    message.AppendInteger(flatCat.Id);
                    message.AppendString(flatCat.Caption);
                    message.AppendBool(flatCat.MinRank <= userRank); // Visible
                    message.AppendBool(false); // Disabled?
                    message.AppendString(string.Empty); // Not used by client
                    message.AppendString(string.Empty); // TODO navigator.flatcategory.global.{VALUE} (External Texts)
                    message.AppendBool(flatCat.MinRank == 7); // Requires Rank 7
                }

                session.Send(message);
            }
        }
    }
}