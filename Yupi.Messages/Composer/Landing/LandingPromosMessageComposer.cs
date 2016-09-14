namespace Yupi.Messages.Landing
{
    using System;
    using System.Collections.Generic;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public class LandingPromosMessageComposer : Yupi.Messages.Contracts.LandingPromosMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, List<HotelLandingPromos> promos)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(promos.Count);

                foreach (HotelLandingPromos promo in promos)
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

        #endregion Methods
    }
}