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
using Yupi.Emulator.Data.Base.Adapters.Interfaces;
using Yupi.Emulator.Game.Browser.Models;
using Yupi.Emulator.Messages.Buffers;

namespace Yupi.Emulator.Game.Browser
{
    /// <summary>
    ///     Class HotelView.
    /// </summary>
    public class HotelLandingManager
    {
        /// <summary>
        ///     The furni reward identifier
        /// </summary>
     public int FurniRewardId;

        /// <summary>
        ///     The furni reward name
        /// </summary>
     public string FurniRewardName;

        /// <summary>
        ///     Hotel View Badges
        /// </summary>
     public Dictionary<string, string> HotelViewBadges;

        /// <summary>
        ///     The hotel view promos indexers
        /// </summary>
     public List<HotelLandingPromos> HotelViewPromosIndexers;

        /// <summary>
        ///     Initializes a new instance of the <see cref="HotelLandingManager" /> class.
        /// </summary>
        public HotelLandingManager()
        {
            HotelViewPromosIndexers = new List<HotelLandingPromos>();
            HotelViewBadges = new Dictionary<string, string>();

            LoadPromos();
            LoadReward();
            LoadBadges();
        }

        /// <summary>
        ///     Refreshes the promo list.
        /// </summary>
        public void RefreshPromoList()
        {
            HotelViewPromosIndexers.Clear();
            HotelViewBadges.Clear();

            LoadPromos();
            LoadReward();       
            LoadBadges();
        }

        /// <summary>
        ///     Loads the reward.
        /// </summary>
        private void LoadReward()
        {
            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                queryReactor.SetQuery("SELECT * FROM hotelview_rewards_promos WHERE enabled = '1' LIMIT 1");

                DataRow row = queryReactor.GetRow();

                if (row == null)
                    return;

                FurniRewardId = Convert.ToInt32(row["furni_id"]);

                FurniRewardName = Convert.ToString(row["furni_name"]);
            }
        }

        /// <summary>
        ///     Lists this instance.
        /// </summary>
        private void LoadPromos()
        {
            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                queryReactor.SetQuery("SELECT * FROM hotelview_promos WHERE enabled = '1' ORDER BY id DESC");

                DataTable table = queryReactor.GetTable();

                if (table == null)
                    return;

                foreach (DataRow dataRow in table.Rows)
                    HotelViewPromosIndexers.Add(new HotelLandingPromos(Convert.ToInt32(dataRow["id"]), (string) dataRow["header"], (string) dataRow["body"], (string) dataRow["button"], Convert.ToInt32(dataRow["in_game_promo"]), (string) dataRow["special_action"], (string) dataRow["image"]));
            }
        }

        private void LoadBadges()
        {
            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                queryReactor.SetQuery("SELECT * FROM hotelview_badges WHERE enabled = '1'");

                DataTable table = queryReactor.GetTable();

                if (table == null)
                    return;

                foreach (DataRow dataRow in table.Rows)
                    HotelViewBadges.Add((string) dataRow["name"], (string) dataRow["badge"]);
            }
        }
    }
}