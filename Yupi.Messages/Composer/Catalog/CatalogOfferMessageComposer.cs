// ---------------------------------------------------------------------------------
// <copyright file="CatalogOfferMessageComposer.cs" company="https://github.com/sant0ro/Yupi">
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

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public class CatalogOfferMessageComposer : Yupi.Messages.Contracts.CatalogOfferMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, CatalogOffer item)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(item.Id);
                message.AppendString(item.Name);
                message.AppendBool(false);
                message.AppendInteger(item.CreditsCost);

                if (item.DiamondsCost > 0)
                {
                    message.AppendInteger(item.DiamondsCost);
                    message.AppendInteger(105);
                }
                else
                {
                    message.AppendInteger(item.DucketsCost);
                    message.AppendInteger(0);
                }

                message.AppendBool(item.AllowGift);
                // TODO Refactor
                switch (item.Name)
                {
                    case "g0 group_product":
                        message.AppendInteger(0);
                        break;

                    case "room_ad_plus_badge":
                        message.AppendInteger(1);
                        message.AppendString("b");
                        message.AppendString("RADZZ");
                        break;

                    default:
                        if (item.Name.StartsWith("builders_club_addon_") || item.Name.StartsWith("builders_club_time_"))
                            message.AppendInteger(0);
                        else if (item.Badge == "")
                            message.AppendInteger(item.BaseItems.Count);
                        else
                        {
                            message.AppendInteger(item.BaseItems.Count + 1);
                            message.AppendString("b");
                            message.AppendString(item.Badge);
                        }
                        throw new NotImplementedException();
                        break;
                }
                foreach (BaseItem baseItem in item.BaseItems.Keys)
                {
                    if (item.Name == "g0 group_product" || item.Name.StartsWith("builders_club_addon_") ||
                        item.Name.StartsWith("builders_club_time_"))
                        break;
                    if (item.Name == "room_ad_plus_badge")
                    {
                        message.AppendString("");
                        message.AppendInteger(0);
                    }
                    else
                    {
                        message.AppendString(baseItem.Type.ToString());
                        message.AppendInteger(baseItem.SpriteId);
                        /*
                        if (item.Name.Contains ("wallpaper_single") || item.Name.Contains ("floor_single") ||
                           item.Name.Contains ("landscape_single")) {
                            string[] array = item.Name.Split ('_');
                            message.AppendString (array [2]);
                        } else if (item.Name.StartsWith ("bot_") || baseItem.InteractionType == Interaction.MusicDisc ||
                                item.BaseItem.Name == "poster")
                            message.AppendString (item.ExtraData);
                        else if (item.Name.StartsWith ("poster_")) {
                            string[] array2 = item.Name.Split ('_');
                            message.AppendString (array2 [1]);
                        } else if (item.Name.StartsWith ("poster ")) {
                            string[] array3 = item.Name.Split (' ');
                            message.AppendString (array3 [1]);
                        } else if (item.SongId > 0u && baseItem.InteractionType == Interaction.MusicDisc)
                            message.AppendString (item.ExtraData);
                        else
                            message.AppendString (string.Empty);
            */
                        throw new NotImplementedException();
                        message.AppendInteger(item.BaseItems[baseItem]);
                        message.AppendBool(item is LimitedCatalogItem);
                        if (!(item is LimitedCatalogItem))
                            continue;
                        message.AppendInteger(((LimitedCatalogItem) item).LimitedStack);
                        message.AppendInteger(((LimitedCatalogItem) item).LimitedStack -
                                              ((LimitedCatalogItem) item).LimitedSold);
                    }
                }
                message.AppendInteger(item.ClubOnly ? 1 : 0);

                message.AppendBool(item.HaveOffer && !(item is LimitedCatalogItem));

                session.Send(message);
            }
        }

        #endregion Methods
    }
}