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
    ///     Class GroupForum.
    /// </summary>
    internal class GroupForum
    {
        /// <summary>
        ///     The forum description
        /// </summary>
        internal string ForumDescription;

        /// <summary>
        ///     The forum last poster identifier
        /// </summary>
        internal uint ForumLastPosterId;

        /// <summary>
        ///     The forum last poster name
        /// </summary>
        internal string ForumLastPosterName;

        /// <summary>
        ///     The forum last poster timestamp
        /// </summary>
        internal uint ForumLastPosterTimestamp;

        /// <summary>
        ///     The forum messages count
        /// </summary>
        internal uint ForumMessagesCount;

        /// <summary>
        ///     The forum name
        /// </summary>
        internal string ForumName;

        /// <summary>
        ///     The forum score
        /// </summary>
        internal double ForumScore;

        /// <summary>
        ///     The identifier
        /// </summary>
        internal uint Id;

        internal uint WhoCanMod;

        internal uint WhoCanPost;

        internal uint WhoCanRead;

        internal uint WhoCanThread;

        /// <summary>
        ///     Initializes a new instance of the <see cref="GroupForum" /> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="forumName">Name of the forum.</param>
        /// <param name="forumDescription">The forum description.</param>
        /// <param name="forumMessagesCount">The forum messages count.</param>
        /// <param name="forumScore">The forum score.</param>
        /// <param name="forumLastPosterId">The forum last poster identifier.</param>
        /// <param name="forumLastPosterName">Name of the forum last poster.</param>
        /// <param name="forumLastPosterTimestamp">The forum last poster timestamp.</param>
        /// <param name="whoCanRead"></param>
        /// <param name="whoCanPost"></param>
        /// <param name="whoCanThread"></param>
        /// <param name="whoCanMod"></param>
        internal GroupForum(uint id, string forumName,
            string forumDescription, uint forumMessagesCount, double forumScore, uint forumLastPosterId,
            string forumLastPosterName, uint forumLastPosterTimestamp,
            uint whoCanRead, uint whoCanPost, uint whoCanThread, uint whoCanMod)
        {
            Id = id;
            ForumName = forumName;
            ForumDescription = forumDescription;
            ForumMessagesCount = forumMessagesCount;
            ForumScore = forumScore;
            ForumLastPosterId = forumLastPosterId;
            ForumLastPosterName = forumLastPosterName;
            ForumLastPosterTimestamp = forumLastPosterTimestamp;
            WhoCanRead = whoCanRead;
            WhoCanPost = whoCanPost;
            WhoCanThread = whoCanThread;
            WhoCanMod = whoCanMod;
        }

        /// <summary>
        ///     Gets the forum last post time.
        /// </summary>
        /// <value>The forum last post time.</value>
        internal uint ForumLastPostTime => (uint) Yupi.GetUnixTimeStamp() - ForumLastPosterTimestamp;
    }
}