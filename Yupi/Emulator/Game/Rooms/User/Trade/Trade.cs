using System;
using System.Collections.Generic;
using System.Linq;
using Yupi.Emulator.Core.Io.Logger;
using Yupi.Emulator.Game.Items.Interfaces;

using Yupi.Protocol;
using Yupi.Protocol.Buffers;


namespace Yupi.Emulator.Game.Rooms.User.Trade
{
    /// <summary>
    ///     Class Trade.
    /// </summary>
     public class Trade : ISender
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

        private readonly TradeUser _userOne;
		private readonly TradeUser _userTwo;

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
			_userOne = new TradeUser(userOneId, roomId);
			_userTwo = new TradeUser(userTwoId, roomId);
            _tradeStage = 1;
            _roomId = roomId;
            
			_userOne.GetRoomUser().AddStatus("trd");
			_userOne.GetRoomUser().UpdateNeeded = true;

			_userTwo.GetRoomUser().AddStatus("trd");
			_userTwo.GetRoomUser().UpdateNeeded = true;

			Router.GetComposer<TradeStartMessageComposer> ().Compose (_userOne, userOneId, userTwoId);
			Router.GetComposer<TradeStartMessageComposer> ().Compose (_userTwo, userOneId, userTwoId);      
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
					return _userOne.HasAccepted && _userTwo.HasAccepted;
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
						return _userOne.UserId == id || _userTwo.UserId == id;
            }
        }

        /// <summary>
        ///     Gets the trade user.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>TradeUser.</returns>
				// TODO Remove this shit
     private TradeUser GetTradeUser(uint id)
        {
            {

				if (id == _userOne.UserId) {
					return _userOne;
				} else {
							return _userTwo;
				}
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

			Router.GetComposer<TradeAcceptMessageComposer> ().Compose (_userOne, userId, true);      
			Router.GetComposer<TradeAcceptMessageComposer> ().Compose (_userTwo, userId, true);   

            if (!AllUsersAccepted)
                return;

			Router.GetComposer<TradeConfirmationMessageComposer> ().Compose (_userOne);      
			Router.GetComposer<TradeConfirmationMessageComposer> ().Compose (_userTwo);   

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

			Router.GetComposer<TradeAcceptMessageComposer> ().Compose (_userOne, userId, false);      
			Router.GetComposer<TradeAcceptMessageComposer> ().Compose (_userTwo, userId, false);      
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

			Router.GetComposer<TradeAcceptMessageComposer> ().Compose (_userOne, userId, true);      
			Router.GetComposer<TradeAcceptMessageComposer> ().Compose (_userTwo, userId, true);    

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

			_userOne.HasAccepted = false;
			_userTwo.HasAccepted = false;
        }

        /// <summary>
        ///     Updates the trade window.
        /// </summary>
     public void UpdateTradeWindow()
        {
			Router.GetComposer<TradeUpdateMessageComposer> ().Compose (_userOne.GetClient(), _userOne, _userTwo);
			Router.GetComposer<TradeUpdateMessageComposer> ().Compose (_userTwo.GetClient(), _userOne, _userTwo);
        }

        /// <summary>
        ///     Delivers the items.
        /// </summary>
     public void DeliverItems()
        {
            List<UserItem> offeredItems = _userOne.OfferedItems;
            List<UserItem> offeredItems2 = __userTwo.OfferedItems;
            if (
                offeredItems.Any(
                    current =>
                        _userOne.GetClient().GetHabbo().GetInventoryComponent().GetItem(current.Id) == null))
            {
                _userOne.GetClient().SendNotif("El tradeo ha fallado.");
                __userTwo.GetClient().SendNotif("El tradeo ha fallado.");
                return;
            }
            if (
                offeredItems2.Any(
                    current2 =>
                        __userTwo.GetClient().GetHabbo().GetInventoryComponent().GetItem(current2.Id) == null))
            {
                _userOne.GetClient().SendNotif("El tradeo ha fallado.");
                __userTwo.GetClient().SendNotif("El tradeo ha fallado.");
                return;
            }
            __userTwo.GetClient().GetHabbo().GetInventoryComponent().RunDbUpdate();
            _userOne.GetClient().GetHabbo().GetInventoryComponent().RunDbUpdate();
            foreach (UserItem current3 in offeredItems)
            {
                _userOne.GetClient().GetHabbo().GetInventoryComponent().RemoveItem(current3.Id, false);
                __userTwo
                    .GetClient()
                    .GetHabbo()
                    .GetInventoryComponent()
                    .AddNewItem(current3.Id, current3.BaseItem.Name, current3.ExtraData, current3.GroupId, false, false,
                        0,
                        0,
                        current3.SongCode);
                _userOne.GetClient().GetHabbo().GetInventoryComponent().RunDbUpdate();
                __userTwo.GetClient().GetHabbo().GetInventoryComponent().RunDbUpdate();
            }
            foreach (UserItem current4 in offeredItems2)
            {
                __userTwo.GetClient().GetHabbo().GetInventoryComponent().RemoveItem(current4.Id, false);
                _userOne
                    .GetClient()
                    .GetHabbo()
                    .GetInventoryComponent()
                    .AddNewItem(current4.Id, current4.BaseItem.Name, current4.ExtraData, current4.GroupId, false, false,
                        0,
                        0,
                        current4.SongCode);
                __userTwo.GetClient().GetHabbo().GetInventoryComponent().RunDbUpdate();
                _userOne.GetClient().GetHabbo().GetInventoryComponent().RunDbUpdate();
            }

			Router.GetComposer<NewInventoryObjectMessageComposer> ().Compose (__userTwo.GetClient (), null, offeredItems);
			Router.GetComposer<NewInventoryObjectMessageComposer> ().Compose (_userOne.GetClient (), null, offeredItems2);
            _userOne.GetClient().GetHabbo().GetInventoryComponent().UpdateItems(false);
            __userTwo.GetClient().GetHabbo().GetInventoryComponent().UpdateItems(false);
        }

        /// <summary>
        ///     Closes the trade clean.
        /// </summary>
     public void CloseTradeClean()
        {
            {
						
						_userOne.GetRoomUser().RemoveStatus("trd");
						_userOne.GetRoomUser().UpdateNeeded = true;

						_userTwo.GetRoomUser().RemoveStatus("trd");
						_userTwo.GetRoomUser().UpdateNeeded = true;

				Router.GetComposer<TradeCompletedMessageComposer> ().Compose (_userOne);      
				Router.GetComposer<TradeCompletedMessageComposer> ().Compose (_userTwo);     
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
				_userOne.GetRoomUser().RemoveStatus("trd");
				_userOne.GetRoomUser().UpdateNeeded = true;
                
				_userTwo.GetRoomUser().RemoveStatus("trd");
				_userTwo.GetRoomUser().UpdateNeeded = true;

				Router.GetComposer<TradeCompletedMessageComposer> ().Compose (_userOne, userId);      
				Router.GetComposer<TradeCompletedMessageComposer> ().Compose (_userTwo, userId); 
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