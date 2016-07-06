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
using System.Globalization;
using Yupi.Emulator.Data.Base.Adapters.Interfaces;
using FluentNHibernate.Data;
using Yupi.Model.Users;


namespace Yupi.Model.Groups
{
	public class Group : Entity
	{
		public virtual uint AdminOnlyDeco { get; set; }

		public virtual string Badge { get; set; }

		public virtual int Colour1 { get; set; }

		public virtual int Colour2 { get; set; }

		public virtual int CreateTime { get; set; }

		public virtual uint CreatorId { get; set; }

		public virtual string Description { get; set; }

		public virtual GroupForum Forum { get; set; }

		public virtual IList<Habbo> Admins { get; protected set; }

		public virtual IList<Habbo> Members { get; protected set; }

		public virtual IList<Habbo> Requests { get; protected set; }

		public virtual uint RoomId { get; set; }

		public virtual uint State { get; set; }
	}
}