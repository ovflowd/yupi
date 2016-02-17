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

namespace Yupi.Game.Groups.Structs
{
    /// <summary>
    ///     Class GroupUser.
    /// </summary>
    internal class GroupMember
    {
        /// <summary>
        ///     The date of join on group
        /// </summary>
        internal int DateJoin;

        /// <summary>
        ///     The group identifier
        /// </summary>
        internal uint GroupId;

        /// <summary>
        ///     The identifier
        /// </summary>
        internal uint Id;

        /// <summary>
        ///     The look
        /// </summary>
        internal string Look;

        /// <summary>
        ///     The name
        /// </summary>
        internal string Name;

        /// <summary>
        ///     The rank
        /// </summary>
        internal int Rank;

        /// <summary>
        ///     Initializes a new instance of the <see cref="GroupMember" /> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="look"></param>
        /// <param name="groupId">The group identifier.</param>
        /// <param name="rank">The rank.</param>
        /// <param name="name"></param>
        /// <param name="dateJoin"></param>
        internal GroupMember(uint id, string name, string look, uint groupId, int rank, int dateJoin)
        {
            Id = id;
            Name = name;
            Look = look;
            GroupId = groupId;
            Rank = rank;
            DateJoin = dateJoin;
        }
    }
}