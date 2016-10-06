// ---------------------------------------------------------------------------------
// <copyright file="GroupForum.cs" company="https://github.com/sant0ro/Yupi">
//   Copyright (c) 2016 Claudio Santoro, TheDoctor
// </copyright>
// <license>
//   Permission is hereby granted, free of charge, to any person obtaining a copy
//   of this software and associated documentation files (the "Software"), to deal
//   in the Software without restriction, including without limitation the rights
//   to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//   copies of the Software, and to permit persons to whom the Software is
//   furnished to do so, subject to the following conditions:
//
//   The above copyright notice and this permission notice shall be included in
//   all copies or substantial portions of the Software.
//
//   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//   IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//   FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//   AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//   LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//   OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//   THE SOFTWARE.
// </license>
// ---------------------------------------------------------------------------------
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
        public virtual int WhoCanMod
        {
            get; set;
        }

        public virtual int WhoCanPost
        {
            get; set;
        }

        public virtual int WhoCanRead
        {
            get; set;
        }

        public virtual int WhoCanThread
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