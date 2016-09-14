using System.Collections.Generic;
using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Navigator
{
    // TODO Renamed from NavigatorCategorys
    public class NavigatorCategoriesComposer : Contracts.NavigatorCategoriesComposer
    {
        public override void Compose(ISender session, IList<NavigatorCategory> categories)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(categories.Count);

                foreach (var category in categories)
                    message.AppendString(category.Caption);
                session.Send(message);
            }
        }
    }
}