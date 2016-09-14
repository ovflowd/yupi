using System.Collections.Generic;
using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.User
{
    public class LoadWardrobeMessageComposer : Contracts.LoadWardrobeMessageComposer
    {
        public override void Compose(ISender session, IList<WardrobeItem> wardrobe)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(wardrobe.Count);
                foreach (var item in wardrobe)
                {
                    message.AppendInteger(item.Slot);
                    message.AppendString(item.Look);
                    message.AppendString(item.Gender.ToUpper());
                }

                session.Send(message);
            }
        }
    }
}