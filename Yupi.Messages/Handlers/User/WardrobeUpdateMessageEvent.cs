using System.Linq;
using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.User
{
    public class WardrobeUpdateMessageEvent : AbstractHandler
    {
        public override void HandleMessage(Habbo session, ClientMessage message, IRouter router)
        {
            var slot = message.GetInteger();
            var look = message.GetString();
            var gender = message.GetString();
            // TODO Filter look & gender

            var item = session.Info.Inventory.Wardrobe.FirstOrDefault(x => x.Slot == slot);

            if (item != default(WardrobeItem))
            {
                item.Look = look;
                item.Gender = gender;
            }
        }
    }
}