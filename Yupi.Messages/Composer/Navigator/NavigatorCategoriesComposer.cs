namespace Yupi.Messages.Navigator
{
    using System;
    using System.Collections.Generic;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    // TODO Renamed from NavigatorCategorys
    public class NavigatorCategoriesComposer : Yupi.Messages.Contracts.NavigatorCategoriesComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, IList<NavigatorCategory> categories)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(categories.Count);

                foreach (NavigatorCategory category in categories)
                    message.AppendString(category.Caption);
                session.Send(message);
            }
        }

        #endregion Methods
    }
}