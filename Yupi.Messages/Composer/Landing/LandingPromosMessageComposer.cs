using System.Collections.Generic;
using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Landing
{
    public class LandingPromosMessageComposer : Contracts.LandingPromosMessageComposer
    {
        public override void Compose(ISender session, List<HotelLandingPromos> promos)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(promos.Count);

                foreach (var promo in promos)
                {
                    message.AppendInteger(promo.Id);
                    message.AppendString(promo.Header);
                    message.AppendString(promo.Body);
                    message.AppendString(promo.Button);
                    message.AppendInteger(promo.InGamePromo);
                    message.AppendString(promo.SpecialAction);
                    message.AppendString(promo.Image);
                }

                session.Send(message);
            }
        }
    }
}