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
using FluentNHibernate.Data;
using System.Collections.Generic;

namespace Yupi.Model.Groups
{
	public class GroupForum : Entity
	{
		public virtual string ForumDescription { get; set; }

		public virtual string ForumName { get; set; }

		public virtual double ForumScore { get; set; }

		public virtual uint WhoCanMod { get; set; }

		public virtual uint WhoCanPost { get; set; }

		public virtual uint WhoCanRead { get; set; }

		public virtual uint WhoCanThread { get; set; }

		public virtual IList<GroupForumPost> Posts { get; protected set; }

		public GroupForum ()
		{
			Posts = new List<GroupForumPost> ();
		}
	}
}