#region Header

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

#endregion Header

namespace Yupi.Model.Domain
{
    using System.Collections.Generic;
    using System.Linq;

    public class GroupForum
    {
        #region Properties

        public virtual string ForumDescription
        {
            get; set;
        }

        public virtual string ForumName
        {
            get; set;
        }

        public virtual double ForumScore
        {
            get; set;
        }

        public virtual int Id
        {
            get; protected set;
        }

        public virtual IList<GroupForumThread> Threads
        {
            get; protected set;
        }

        // TODO Enum
        public virtual uint WhoCanMod
        {
            get; set;
        }

        public virtual uint WhoCanPost
        {
            get; set;
        }

        public virtual uint WhoCanRead
        {
            get; set;
        }

        public virtual uint WhoCanThread
        {
            get; set;
        }

        #endregion Properties

        #region Constructors

        public GroupForum()
        {
            Threads = new List<GroupForumThread>();
        }

        #endregion Constructors

        #region Methods

        public virtual GroupForumPost GetLastPost()
        {
            GroupForumPost newest = null;

            foreach (GroupForumThread thread in Threads)
            {
                GroupForumPost current = thread.Posts.Last();

                if (newest == null || current.Timestamp > newest.Timestamp)
                {
                    newest = current;
                }
            }

            return newest;
        }

        public virtual int GetMessageCount()
        {
            int count = 0;
            foreach (GroupForumThread thread in Threads)
            {
                count += thread.Posts.Count;
            }

            return count;
        }

        public virtual GroupForumThread GetThread(int id)
        {
            return Threads.SingleOrDefault(x => x.Id == id);
        }

        #endregion Methods
    }
}