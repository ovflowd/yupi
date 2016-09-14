// ---------------------------------------------------------------------------------
// <copyright file="CatalogueClubPageMessageComposer.cs" company="https://github.com/sant0ro/Yupi">
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
    using System.Collections.Generic;

    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Model.Repository;
    using Yupi.Protocol.Buffers;

    public class CatalogueClubPageMessageComposer : Yupi.Messages.Contracts.CatalogueClubPageMessageComposer
    {
        #region Fields

        private IRepository<CatalogPage> CatalogRepository;

        #endregion Fields

        #region Constructors

        public CatalogueClubPageMessageComposer()
        {
            CatalogRepository = DependencyFactory.Resolve<IRepository<CatalogPage>>();
        }

        #endregion Constructors

        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, int windowId)
        {
            // TODO Refactor
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                throw new NotImplementedException();
                /*
                List<CatalogItem> habboClubItems = Yupi.GetGame ().GetCatalogManager ().HabboClubItems;

                message.AppendInteger (habboClubItems.Count);

                foreach (CatalogItem item in habboClubItems) {
                    message.AppendInteger (item.Id);
                    message.AppendString (item.Name);
                    message.AppendBool (false);
                    message.AppendInteger (item.CreditsCost);

                    if (item.DiamondsCost > 0) {
                        message.AppendInteger (item.DiamondsCost);
                        message.AppendInteger (105);
                    } else {
                        message.AppendInteger (item.DucketsCost);
                        message.AppendInteger (0);
                    }

                    message.AppendBool (true);
                    string[] fuckingArray = item.Name.Split ('_');
                    double dayTime = 31;

                    if (item.Name.Contains ("DAY"))
                        dayTime = int.Parse (fuckingArray [3]);
                    else if (item.Name.Contains ("MONTH")) {
                        int monthTime = int.Parse (fuckingArray [3]);
                        dayTime = monthTime * 31;
                    } else if (item.Name.Contains ("YEAR")) {
                        int yearTimeOmg = int.Parse (fuckingArray [3]);
                        dayTime = yearTimeOmg * 31 * 12;
                    }

                    DateTime newExpiryDate = DateTime.Now.AddDays (dayTime);

                    if (session.GetHabbo ().GetSubscriptionManager ().HasSubscription)
                        newExpiryDate =
                        Yupi.UnixToDateTime (session.GetHabbo ().GetSubscriptionManager ().GetSubscription ().ExpireTime)
                            .AddDays (dayTime);

                    message.AppendInteger ((int)dayTime / 31);
                    message.AppendInteger ((int)dayTime);
                    message.AppendBool (false);
                    message.AppendInteger ((int)dayTime);
                    message.AppendInteger (newExpiryDate.Year);
                    message.AppendInteger (newExpiryDate.Month);
                    message.AppendInteger (newExpiryDate.Day);
                }

                message.AppendInteger (windowId);
                session.Send(message);
                */
            }
        }

        #endregion Methods
    }
}