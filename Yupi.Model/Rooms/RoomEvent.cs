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
using FluentNHibernate.Data;

namespace Yupi.Model.Rooms
{
	/// <summary>
	///     Class RoomEvent.
	/// </summary>
	public class RoomEvent : Entity
	{
		/// <summary>
		///     The category
		/// </summary>
		public virtual int Category { get; set; }
		// TODO What is the category? Foreing key!
		/// <summary>
		///     The description
		/// </summary>
		public virtual string Description { get; set; }

		/// <summary>
		///     The name
		/// </summary>
		public virtual string Name { get; set; }

		/// <summary>
		///     The time
		/// </summary>
		public virtual DateTime ExpiresAt { get; set; }

		public RoomEvent ()
		{
			ExpiresAt = new DateTime ().AddHours (2);
		}

		/// <summary>
		///     Gets a value indicating whether this instance has expired.
		/// </summary>
		/// <value><c>true</c> if this instance has expired; otherwise, <c>false</c>.</value>
		public bool HasExpired ()
		{
			return ExpiresAt < DateTime.Now;
		}
	}
}