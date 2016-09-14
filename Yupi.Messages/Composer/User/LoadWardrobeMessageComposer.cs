namespace Yupi.Messages.User
{
    using System;
    using System.Collections.Generic;
    using System.Data;

    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public class LoadWardrobeMessageComposer : Yupi.Messages.Contracts.LoadWardrobeMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, IList<WardrobeItem> wardrobe)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(wardrobe.Count);
                foreach (WardrobeItem item in wardrobe)
                {
                    message.AppendInteger(item.Slot);
                    message.AppendString(item.Look);
                    message.AppendString(item.Gender.ToUpper());
                }

                session.Send(message);
            }
        }

        #endregion Methods
    }
}