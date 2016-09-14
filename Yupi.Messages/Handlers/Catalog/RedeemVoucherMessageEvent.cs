using System;
using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Catalog
{
    public class RedeemVoucherMessageEvent : AbstractHandler
    {
        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            var voucher = request.GetString();
            var productName = string.Empty;
            var productDescription = string.Empty;
            var isValid = false;
            /*
            DataRow row;

            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                queryReactor.SetQuery("SELECT * FROM items_vouchers WHERE voucher = @vo LIMIT 1");
                queryReactor.AddParameter("vo", voucher);
                row = queryReactor.GetRow();
            }

            if (row != null)
            {
                isValid = true;

                using (IQueryAdapter queryreactor2 = Yupi.GetDatabaseManager().GetQueryReactor())
                {
                    queryreactor2.SetQuery("DELETE FROM items_vouchers WHERE voucher = @vou LIMIT 1");
                    queryreactor2.AddParameter("vou", voucher);
                    queryreactor2.RunQuery();
                }

                session.GetHabbo().Credits += (uint) row["value"];
                session.GetHabbo().UpdateCreditsBalance();
                session.GetHabbo().NotifyNewPixels((uint) row["extra_duckets"]);
            }

            session.GetHabbo().NotifyVoucher(isValid, productName, productDescription);
            */
            throw new NotImplementedException();
        }
    }
}