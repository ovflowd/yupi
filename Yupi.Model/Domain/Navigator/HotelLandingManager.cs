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

using System.Collections.Generic;

namespace Yupi.Model.Domain
{
    /// <summary>
    ///     Class HotelView.
    /// </summary>
    public class HotelLandingManager
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="HotelLandingManager" /> class.
        /// </summary>
        public HotelLandingManager()
        {
            HotelViewPromosIndexers = new List<HotelLandingPromos>();
            HotelViewBadges = new Dictionary<string, string>();
        }

        public virtual int Id { get; protected set; }

        /// <summary>
        ///     The furni reward identifier
        /// </summary>
        public virtual BaseItem FurniReward { get; set; }

        // TODO What does this contain?
        public virtual Dictionary<string, string> HotelViewBadges { get; set; }

        /// <summary>
        ///     The hotel view promos indexers
        /// </summary>
        public virtual IList<HotelLandingPromos> HotelViewPromosIndexers { get; set; }
    }
}