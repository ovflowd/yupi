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
using System.Data;
using System.Linq;
using Yupi.Emulator.Data.Base.Adapters.Interfaces;

namespace Yupi.Model.Domain
{
	public class RoomCompetition
	{
		/// <summary>
		///     Competition Entries
		/// </summary>
		public IList<RoomCompetitionEntry> Entries;

		/// <summary>
		///     Competition Id
		/// </summary>
		public int Id;

		/// <summary>
		///     Competition Name
		/// </summary>
		public string Name;

		/// <summary>
		///     Required Items
		/// </summary>
		public IList<BaseItem> RequiredItems;

		public RoomCompetition ()
		{
			RequiredItems = new List<BaseItem> ();
		}
	}

}
