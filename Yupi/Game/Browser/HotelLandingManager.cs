/**
     Because i love chocolat...                                      
                                    88 88  
                                    "" 88  
                                       88  
8b       d8 88       88 8b,dPPYba,  88 88  
`8b     d8' 88       88 88P'    "8a 88 88  
 `8b   d8'  88       88 88       d8 88 ""  
  `8b,d8'   "8a,   ,a88 88b,   ,a8" 88 aa  
    Y88'     `"YbbdP'Y8 88`YbbdP"'  88 88  
    d8'                 88                 
   d8'                  88     
   
   Private Habbo Hotel Emulating System
   @author Claudio A. Santoro W.
   @author Kessiler R.
   @version dev-beta
   @license MIT
   @copyright Sulake Corporation Oy
   @observation All Rights of Habbo, Habbo Hotel, and all Habbo contents and it's names, is copyright from Sulake
   Corporation Oy. Yupi! has nothing linked with Sulake. 
   This Emulator is Only for DEVELOPMENT uses. If you're selling this you're violating Sulakes Copyright.
*/

using System;
using System.Collections.Generic;
using System.Data;
using Yupi.Data.Base.Adapters.Interfaces;
using Yupi.Game.Browser.Models;
using Yupi.Messages;

namespace Yupi.Game.Browser
{
    /// <summary>
    ///     Class HotelView.
    /// </summary>
    public class HotelLandingManager
    {
        /// <summary>
        ///     The furni reward identifier
        /// </summary>
        internal int FurniRewardId;

        /// <summary>
        ///     The furni reward name
        /// </summary>
        internal string FurniRewardName;

        internal Dictionary<string, string> HotelViewBadges;

        /// <summary>
        ///     The hotel view promos indexers
        /// </summary>
        internal List<HotelLandingPromos> HotelViewPromosIndexers;

        /// <summary>
        ///     Initializes a new instance of the <see cref="HotelLandingManager" /> class.
        /// </summary>
        public HotelLandingManager()
        {
            HotelViewPromosIndexers = new List<HotelLandingPromos>();
            HotelViewBadges = new Dictionary<string, string>();

            List();
            LoadReward();
            LoadHvBadges();
        }

        /// <summary>
        ///     Loads the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>SmallPromo.</returns>
        public static HotelLandingPromos Load(int index)
        {
            using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                commitableQueryReactor.SetQuery(
                    "SELECT hotelview_promos.`index`,hotelview_promos.header,hotelview_promos.body,hotelview_promos.button,hotelview_promos.in_game_promo,hotelview_promos.special_action,hotelview_promos.image,hotelview_promos.enabled FROM hotelview_promos WHERE hotelview_promos.`index` = @x LIMIT 1");
                commitableQueryReactor.AddParameter("x", index);

                DataRow row = commitableQueryReactor.GetRow();

                return new HotelLandingPromos(index, (string) row[1], (string) row[2], (string) row[3],
                    Convert.ToInt32(row[4]), (string) row[5], (string) row[6]);
            }
        }

        /// <summary>
        ///     Refreshes the promo list.
        /// </summary>
        public void RefreshPromoList()
        {
            HotelViewPromosIndexers.Clear();
            List();
            LoadReward();
            HotelViewBadges.Clear();
            LoadHvBadges();
        }

        /// <summary>
        ///     Smalls the promo composer.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>ServerMessage.</returns>
        internal ServerMessage SmallPromoComposer(ServerMessage message)
        {
            message.AppendInteger(HotelViewPromosIndexers.Count);

            foreach (HotelLandingPromos current in HotelViewPromosIndexers)
                current.Serialize(message);

            return message;
        }

        /// <summary>
        ///     Loads the reward.
        /// </summary>
        private void LoadReward()
        {
            using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                commitableQueryReactor.SetQuery(
                    "SELECT hotelview_rewards_promos.furni_id, hotelview_rewards_promos.furni_name FROM hotelview_rewards_promos WHERE hotelview_rewards_promos.enabled = 1 LIMIT 1");

                DataRow row = commitableQueryReactor.GetRow();

                if (row == null)
                    return;

                FurniRewardId = Convert.ToInt32(row[0]);
                FurniRewardName = Convert.ToString(row[1]);
            }
        }

        /// <summary>
        ///     Lists this instance.
        /// </summary>
        private void List()
        {
            using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                commitableQueryReactor.SetQuery(
                    "SELECT * from hotelview_promos WHERE hotelview_promos.enabled = '1' ORDER BY hotelview_promos.`index` DESC");
                DataTable table = commitableQueryReactor.GetTable();

                foreach (DataRow dataRow in table.Rows)
                    HotelViewPromosIndexers.Add(new HotelLandingPromos(Convert.ToInt32(dataRow[0]), (string) dataRow[1],
                        (string) dataRow[2], (string) dataRow[3], Convert.ToInt32(dataRow[4]), (string) dataRow[5],
                        (string) dataRow[6]));
            }
        }

        private void LoadHvBadges()
        {
            using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                commitableQueryReactor.SetQuery("SELECT * FROM hotelview_badges WHERE enabled = '1'");

                DataTable table = commitableQueryReactor.GetTable();

                foreach (DataRow dataRow in table.Rows)
                    HotelViewBadges.Add((string) dataRow[0], (string) dataRow[1]);
            }
        }
    }
}