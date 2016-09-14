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
using System.Linq;


namespace Yupi.Model.Domain
{
	/// <summary>
	///     Class Achievement.
	/// </summary>
	public class Achievement
	{
		public virtual int Id { get; protected set; }
		
		public virtual string Category { get; set; }

		public virtual string GroupName { get; set; }

		[OneToMany]
		public virtual IList<AchievementLevel> Levels { get; protected set; }

		public Achievement ()
		{
			Levels = new List<AchievementLevel> ();
		}

		public virtual int GetMaxLevel() {
			return Levels.Count - 1;
		}

		public virtual AchievementLevel DefaultLevel() {
			if (Levels.Any ()) {
				return Levels [0];
			} else {
				return null;
			}
		}

		public virtual AchievementLevel NextLevel(AchievementLevel current) {
			if (current.Level == GetMaxLevel ()) {
				return current;
			}
			return Levels.Single (x => x.Level == current.Level + 1);
		}
	}
}