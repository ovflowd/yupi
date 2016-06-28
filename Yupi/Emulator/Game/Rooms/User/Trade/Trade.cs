using System;
using System.Collections.Generic;
using System.Linq;
using Yupi.Emulator.Core.Io.Logger;
using Yupi.Emulator.Game.Items.Interfaces;
using Yupi.Emulator.Messages;


namespace Yupi.Emulator.Game.Rooms.User.Trade
{
    /// <summary>
    ///     Class Trade.
    /// </summary>
     public class Trade
    {
        /// <summary>
        ///     The _one identifier
        /// </summary>
        private readonly uint _oneId;

        /// <summary>
        ///     The _room identifier
        /// </summary>
        private readonly uint _roomId;

        /// <summary>
        ///     The _two identifier
        /// </summary>
        private readonly uint _twoId;

        /// <summary>
        ///     The _users
        /// </summary>
        private readonly TradeUser[] _users;

        /// <summary>
        ///     The _trade stage
        /// </summary>
        private int _tradeStage;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Trade" /> class.
        /// </summary>
        /// <param name="userOneId">The user one identifier.</param>
        /// <param name="userTwoId">The user two identifier.</param>
        /// <param name="roomId">The room identifier.</param>
     public Trade(uint userOneId, uint userTwoId, uint roomId)
        {
            _oneId = userOneId;
            _twoId = userTwoId;
            _users = new TradeUser[2];
            _users[0] = new TradeUser(userOneId, roomId);
            _users[1] = new TradeUser(userTwoId, roomId);
            _tradeStage = 1;
            _roomId = roomId;
            TradeUser[] users = _users;
            foreach (TradeUser tradeUser in users.Where(tradeUser => !tradeUser.GetRoomUser().Statusses.ContainsKey("trd")))
            {
                tradeUser.GetRoomUser().AddStatus("trd", "");
                tradeUser.GetRoomUser().UpdateNeeded = true;
            }
            SimpleServerMessageBuffer simpleServerMessageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("TradeStartMessageComposer"));
            simpleServerMessageBuffer.AppendInteger(userOneId);
            simpleServerMessageBuffer.AppendInteger(1);
            simpleServerMessageBuffer.AppendInteger(userTwoId);
            simpleServerMessageBuffer.AppendInteger(1);
            SendMessageToUsers(simpleServerMessageBuffer);
        }

        /// <summary>
        ///     Gets a value indicating whether [all users accepted].
        /// </summary>
        /// <value><c>true</c> if [all users accepted]; otherwise, <c>false</c>.</value>
     public bool AllUsersAccepted
        {
            get
            {
                {
                    return _users.All(t => t == null || t.HasAccepted);
                }
            }
        }

        /// <summary>
        ///     Determines whether the specified identifier contains user.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns><c>true</c> if the specified identifier contains user; otherwise, <c>false</c>.</returns>
     public bool ContainsUser(uint id)
        {
            {
                return _users.Any(t => t != null && t.UserId == id);
            }
        }

        /// <summary>
        ///     Gets the trade user.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>TradeUser.</returns>
     public TradeUser GetTradeUser(uint id)
        {
            {
                return _users.FirstOrDefault(t => t != null && t.UserId == id);
            }
        }

        /// <summary>
        ///     Offers the item.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="item">The item.</param>
     public void OfferItem(uint userId, UserItem item)
        {
            TradeUser tradeUser = GetTradeUser(userId);

            if (tradeUser == null || item == null || !item.BaseItem.AllowTrade || tradeUser.HasAccepted ||
                _tradeStage != 1)
                return;

            ClearAccepted();

            if (!tradeUser.OfferedItems.Contains(item))
                tradeUser.OfferedItems.Add(item);

            UpdateTradeWindow();
        }

        /// <summary>
        ///     Takes the back item.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="item">The item.</param>
     public void TakeBackItem(uint userId, UserItem item)
        {
            TradeUser tradeUser = GetTradeUser(userId);
            if (tradeUser == null || item == null || tradeUser.HasAccepted || _tradeStage != 1)
            {
                return;
            }
            ClearAccepted();
            tradeUser.OfferedItems.Remove(item);
            UpdateTradeWindow();
        }

        /// <summary>
        ///     Accepts the specified user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
     public void Accept(uint userId)
        {
            TradeUser tradeUser = GetTradeUser(userId);

            if (tradeUser == null || _tradeStage != 1)
                return;

            tradeUser.HasAccepted = true;
            SimpleServerMessageBuffer simpleServerMessageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("TradeAcceptMessageComposer"));
            simpleServerMessageBuffer.AppendInteger(userId);
            simpleServerMessageBuffer.AppendInteger(1);
            SendMessageToUsers(simpleServerMessageBuffer);

            if (!AllUsersAccepted)
                return;

            SendMessageToUsers(new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("TradeConfirmationMessageComposer")));

            _tradeStage++;

            ClearAccepted();
        }

        /// <summary>
        ///     Unaccepts the specified user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
     public void Unaccept(uint userId)
        {
            TradeUser tradeUser = GetTradeUser(userId);

            if (tradeUser == null || _tradeStage != 1 || AllUsersAccepted)
                return;

            tradeUser.HasAccepted = false;
            SimpleServerMessageBuffer simpleServerMessageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("TradeAcceptMessageComposer"));
            simpleServerMessageBuffer.AppendInteger(userId);
            simpleServerMessageBuffer.AppendInteger(0);
            SendMessageToUsers(simpleServerMessageBuffer);
        }

        /// <summary>
        ///     Completes the trade.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
     public void CompleteTrade(uint userId)
        {
            TradeUser tradeUser = GetTradeUser(userId);
            if (tradeUser == null || _tradeStage != 2)
            {
                return;
            }
            tradeUser.HasAccepted = true;
            SimpleServerMessageBuffer simpleServerMessageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("TradeAcceptMessageComposer"));
            simpleServerMessageBuffer.AppendInteger(userId);
            simpleServerMessageBuffer.AppendInteger(1);
            SendMessageToUsers(simpleServerMessageBuffer);
            if (!AllUsersAccepted)
            {
                return;
            }
            _tradeStage = 999;
            Finnito();
        }

        /// <summary>
        ///     Clears the accepted.
        /// </summary>
     public void ClearAccepted()
        {
            TradeUser[] users = _users;
            foreach (TradeUser tradeUser in users)
            {
                tradeUser.HasAccepted = false;
            }
        }

        /// <summary>
        ///     Updates the trade window.
        /// </summary>
     public void UpdateTradeWindow()
        {
            SimpleServerMessageBuffer simpleServerMessageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("TradeUpdateMessageComposer"));

            {
                foreach (TradeUser tradeUser in _users.Where(tradeUser => tradeUser != null))
                {
                    simpleServerMessageBuffer.AppendInteger(tradeUser.UserId);
                    simpleServerMessageBuffer.AppendInteger(tradeUser.OfferedItems.Count);

                    foreach (UserItem current in tradeUser.OfferedItems)
                    {
                        simpleServerMessageBuffer.AppendInteger(current.Id);
                        simpleServerMessageBuffer.AppendString(current.BaseItem.Type.ToString().ToLower());
                        simpleServerMessageBuffer.AppendInteger(current.Id);
                        simpleServerMessageBuffer.AppendInteger(current.BaseItem.SpriteId);
                        simpleServerMessageBuffer.AppendInteger(0);
                        simpleServerMessageBuffer.AppendBool(true);
                        simpleServerMessageBuffer.AppendInteger(0);
                        simpleServerMessageBuffer.AppendString(string.Empty);
                        simpleServerMessageBuffer.AppendInteger(0);
                        simpleServerMessageBuffer.AppendInteger(0);
                        simpleServerMessageBuffer.AppendInteger(0);

                        if (current.BaseItem.Type == 's')
                            simpleServerMessageBuffer.AppendInteger(0);
                    }
                }

                SendMessageToUsers(simpleServerMessageBuffer);
            }
        }

        /// <summary>
        ///     Delivers the items.
        /// </summary>
     public void DeliverItems()
        {
            List<UserItem> offeredItems = GetTradeUser(_oneId).OfferedItems;
            List<UserItem> offeredItems2 = GetTradeUser(_twoId).OfferedItems;
            if (
                offeredItems.Any(
                    current =>
                        GetTradeUser(_oneId).GetClient().GetHabbo().GetInventoryComponent().GetItem(current.Id) == null))
            {
                GetTradeUser(_oneId).GetClient().SendNotif("El tradeo ha fallado.");
                GetTradeUser(_twoId).GetClient().SendNotif("El tradeo ha fallado.");
                return;
            }
            if (
                offeredItems2.Any(
                    current2 =>
                        GetTradeUser(_twoId).GetClient().GetHabbo().GetInventoryComponent().GetItem(current2.Id) == null))
            {
                GetTradeUser(_oneId).GetClient().SendNotif("El tradeo ha fallado.");
                GetTradeUser(_twoId).GetClient().SendNotif("El tradeo ha fallado.");
                return;
            }
            GetTradeUser(_twoId).GetClient().GetHabbo().GetInventoryComponent().RunDbUpdate();
            GetTradeUser(_oneId).GetClient().GetHabbo().GetInventoryComponent().RunDbUpdate();
            foreach (UserItem current3 in offeredItems)
            {
                GetTradeUser(_oneId).GetClient().GetHabbo().GetInventoryComponent().RemoveItem(current3.Id, false);
                GetTradeUser(_twoId)
                    .GetClient()
                    .GetHabbo()
                    .GetInventoryComponent()
                    .AddNewItem(current3.Id, current3.BaseItem.Name, current3.ExtraData, current3.GroupId, false, false,
                        0,
                        0,
                        current3.SongCode);
                GetTradeUser(_oneId).GetClient().GetHabbo().GetInventoryComponent().RunDbUpdate();
                GetTradeUser(_twoId).GetClient().GetHabbo().GetInventoryComponent().RunDbUpdate();
            }
            foreach (UserItem current4 in offeredItems2)
            {
                GetTradeUser(_twoId).GetClient().GetHabbo().GetInventoryComponent().RemoveItem(current4.Id, false);
                GetTradeUser(_oneId)
                    .GetClient()
                    .GetHabbo()
                    .GetInventoryComponent()
                    .AddNewItem(current4.Id, current4.BaseItem.Name, current4.ExtraData, current4.GroupId, false, false,
                        0,
                        0,
                        current4.SongCode);
                GetTradeUser(_twoId).GetClient().GetHabbo().GetInventoryComponent().RunDbUpdate();
                GetTradeUser(_oneId).GetClient().GetHabbo().GetInventoryComponent().RunDbUpdate();
            }
            SimpleServerMessageBuffer simpleServerMessageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("NewInventoryObjectMessageComposer"));
            simpleServerMessageBuffer.AppendInteger(1);
            int i = 1;
            if (offeredItems.Any(current5 => current5.BaseItem.Type.ToString().ToLower() != "s"))
            {
                i = 2;
            }
            simpleServerMessageBuffer.AppendInteger(i);
            simpleServerMessageBuffer.AppendInteger(offeredItems.Count);
            foreach (UserItem current6 in offeredItems)
            {
                simpleServerMessageBuffer.AppendInteger(current6.Id);
            }
            GetTradeUser(_twoId).GetClient().SendMessage(simpleServerMessageBuffer);
            SimpleServerMessageBuffer simpleServerMessage2 = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("NewInventoryObjectMessageComposer"));
            simpleServerMessage2.AppendInteger(1);
            i = 1;
            if (offeredItems2.Any(current7 => current7.BaseItem.Type.ToString().ToLower() != "s"))
            {
                i = 2;
            }
            simpleServerMessage2.AppendInteger(i);
            simpleServerMessage2.AppendInteger(offeredItems2.Count);
            foreach (UserItem current8 in offeredItems2)
            {
                simpleServerMessage2.AppendInteger(current8.Id);
            }
            GetTradeUser(_oneId).GetClient().SendMessage(simpleServerMessage2);
            GetTradeUser(_oneId).GetClient().GetHabbo().GetInventoryComponent().UpdateItems(false);
            GetTradeUser(_twoId).GetClient().GetHabbo().GetInventoryComponent().UpdateItems(false);
        }

        /// <summary>
        ///     Closes the trade clean.
        /// </summary>
     public void CloseTradeClean()
        {
            {
                foreach (
                    TradeUser tradeUser in _users.Where(tradeUser => tradeUser != null && tradeUser.GetRoomUser() != null))
                {
                    tradeUser.GetRoomUser().RemoveStatus("trd");
                    tradeUser.GetRoomUser().UpdateNeeded = true;
                }
                SendMessageToUsers(new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("TradeCompletedMessageComposer")));
                GetRoom().ActiveTrades.Remove(this);
            }
        }

        /// <summary>
        ///     Closes the trade.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
     public void CloseTrade(uint userId)
        {
            {
                foreach (
                    TradeUser tradeUser in _users.Where(tradeUser => tradeUser != null && tradeUser.GetRoomUser() != null))
                {
                    tradeUser.GetRoomUser().RemoveStatus("trd");
                    tradeUser.GetRoomUser().UpdateNeeded = true;
                }
                SimpleServerMessageBuffer simpleServerMessageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("TradeCloseMessageComposer"));
                simpleServerMessageBuffer.AppendInteger(userId);
                simpleServerMessageBuffer.AppendInteger(0);
                SendMessageToUsers(simpleServerMessageBuffer);
            }
        }

        /// <summary>
        ///     Sends the messageBuffer to users.
        /// </summary>
        /// <param name="message">The messageBuffer.</param>
     public void SendMessageToUsers(SimpleServerMessageBuffer messageBuffer)
        {
            if (_users == null)
            {
                return;
            }

            {
                foreach (TradeUser tradeUser in _users.Where(tradeUser => tradeUser != null && tradeUser.GetClient() != null))
                {
                    tradeUser.GetClient().SendMessage(messageBuffer);
                }
            }
        }

        /// <summary>
        ///     Finnitoes this instance.
        /// </summary>
        private void Finnito()
        {
            try
            {
                DeliverItems();
                CloseTradeClean();
            }
            catch (Exception ex)
            {
                YupiLogManager.LogException(ex, "Failed Trading with User.");
            }
        }

        /// <summary>
        ///     Gets the room.
        /// </summary>
        /// <returns>Room.</returns>
        private Room GetRoom()
        {
            return Yupi.GetGame().GetRoomManager().GetRoom(_roomId);
        }
    }
}