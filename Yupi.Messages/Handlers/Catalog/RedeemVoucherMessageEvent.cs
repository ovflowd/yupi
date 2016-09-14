// ---------------------------------------------------------------------------------
// <copyright file="RedeemVoucherMessageEvent.cs" company="https://github.com/sant0ro/Yupi">
//   Copyright (c) 2016 Claudio Santoro, TheDoctor
// </copyright>
// <license>
//   Permission is hereby granted, free of charge, to any person obtaining a copy
//   of this software and associated documentation files (the "Software"), to deal
//   in the Software without restriction, including without limitation the rights
//   to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//   copies of the Software, and to permit persons to whom the Software is
//   furnished to do so, subject to the following conditions:
//
//   The above copyright notice and this permission notice shall be included in
//   all copies or substantial portions of the Software.
//
//   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//   IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//   FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//   AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//   LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//   OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//   THE SOFTWARE.
// </license>
// ---------------------------------------------------------------------------------
namespace Yupi.Messages.Catalog
{
    using System;
    using System.Data;

    public class RedeemVoucherMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            string voucher = request.GetString();
            string productName = string.Empty;
            string productDescription = string.Empty;
            bool isValid = false;
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

        #endregion Methods
    }
}