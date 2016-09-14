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
            int userRank)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(categories.Count);

                foreach (FlatNavigatorCategory flatCat in categories)
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

        #endregion Methods
    }
}