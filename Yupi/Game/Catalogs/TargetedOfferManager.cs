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

using System.Data;
using Yupi.Data.Base.Adapters.Interfaces;
using Yupi.Game.Catalogs.Composers;
using Yupi.Game.Catalogs.Interfaces;
using Yupi.Messages;

namespace Yupi.Game.Catalogs
{
    internal class TargetedOfferManager
    {
        internal TargetedOffer CurrentOffer;

        public TargetedOfferManager()
        {
            LoadOffer();
        }

        public void LoadOffer()
        {
            CurrentOffer = null;

            using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                commitableQueryReactor.SetQuery("SELECT * FROM catalog_targeted_offers WHERE enabled = '1' LIMIT 1");

                DataRow row = commitableQueryReactor.GetRow();

                if (row == null)
                    return;

                CurrentOffer = new TargetedOffer((int) row["id"], (string) row["identifier"], (uint) row["cost_credits"],
                    (uint) row["cost_duckets"], (uint) row["cost_diamonds"], (int) row["purchase_limit"],
                    (int) row["expiration_time"], (string) row["title"], (string) row["description"],
                    (string) row["image"], (string) row["products"]);
            }
        }

        public void GenerateMessage(ServerMessage message)
            => TargetedOfferComposer.GenerateMessage(message, CurrentOffer);
    }
}