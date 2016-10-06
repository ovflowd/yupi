// ---------------------------------------------------------------------------------
// <copyright file="PurchaseFromCatalogAsGiftMessageEvent.cs" company="https://github.com/sant0ro/Yupi">
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

    using Yupi.Controller;
    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Model.Repository;

    public class PurchaseFromCatalogAsGiftMessageEvent : AbstractHandler
    {
        #region Fields

        private CatalogController CatalogController;
        private IRepository<UserInfo> UserRepository;

        #endregion Fields

        #region Constructors

        public PurchaseFromCatalogAsGiftMessageEvent()
        {
            UserRepository = DependencyFactory.Resolve<IRepository<UserInfo>>();
            CatalogController = DependencyFactory.Resolve<CatalogController>();
        }

        #endregion Constructors

        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message,
            Yupi.Protocol.IRouter router)
        {
            int pageId = message.GetInteger();
            int itemId = message.GetInteger();
            string extraData = message.GetString();
            string giftUser = message.GetString();
            string giftMessage = message.GetString();
            int giftSpriteId = message.GetInteger();
            int giftLazo = message.GetInteger();
            int giftColor = message.GetInteger();
            bool showSender = message.GetBool();

            UserInfo info = UserRepository.Find(x => x.Name == giftUser);

            if (info == null)
            {
                router.GetComposer<GiftErrorMessageComposer>().Compose(session, giftUser);
            }

            CatalogOffer item = CatalogController.GetById(pageId, itemId);
            CatalogController.PurchaseGift(session, item, extraData, info);
        }

        #endregion Methods
    }
}