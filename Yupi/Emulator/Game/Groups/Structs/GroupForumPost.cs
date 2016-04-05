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

namespace Yupi.Emulator.Game.Groups.Structs
{
    /// <summary>
    ///     Class GroupForumPost.
    /// </summary>
     class GroupForumPost
    {
        /// <summary>
        ///     The group identifier
        /// </summary>
         uint GroupId;

        /// <summary>
        ///     The hidden
        /// </summary>
         bool Hidden;

        /// <summary>
        ///     The hider
        /// </summary>
         string Hider;

        /// <summary>
        ///     The identifier
        /// </summary>
         uint Id;

        /// <summary>
        ///     The locked
        /// </summary>
         bool Locked;

        /// <summary>
        ///     The message count
        /// </summary>
         int MessageCount;

        /// <summary>
        ///     The parent identifier
        /// </summary>
         uint ParentId;

        /// <summary>
        ///     The pinned
        /// </summary>
         bool Pinned;

        /// <summary>
        ///     The post content
        /// </summary>
         string PostContent;

        /// <summary>
        ///     The poster identifier
        /// </summary>
         uint PosterId;

        /// <summary>
        ///     The poster look
        /// </summary>
         string PosterLook;

        /// <summary>
        ///     The poster name
        /// </summary>
         string PosterName;

        /// <summary>
        ///     The subject
        /// </summary>
         string Subject;

        /// <summary>
        ///     The timestamp
        /// </summary>
         int Timestamp;

        /// <summary>
        ///     Initializes a new instance of the <see cref="GroupForumPost" /> class.
        /// </summary>
        /// <param name="row">The row.</param>
         GroupForumPost(DataRow row)
        {
            Id = uint.Parse(row["id"].ToString());
            ParentId = uint.Parse(row["parent_id"].ToString());
            GroupId = uint.Parse(row["group_id"].ToString());
            Timestamp = int.Parse(row["timestamp"].ToString());
            Pinned = row["pinned"].ToString() == "1";
            Locked = row["locked"].ToString() == "1";
            Hidden = row["hidden"].ToString() == "1";
            PosterId = uint.Parse(row["poster_id"].ToString());
            PosterName = row["poster_name"].ToString();
            PosterLook = row["poster_look"].ToString();
            Subject = row["subject"].ToString();
            PostContent = row["post_content"].ToString();
            Hider = row["post_hider"].ToString();
            MessageCount = 0;

            if (ParentId == 0)
                MessageCount = Yupi.GetGame().GetGroupManager().GetMessageCountForThread(Id);
        }
    }
}