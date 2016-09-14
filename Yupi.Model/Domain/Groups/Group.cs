// ---------------------------------------------------------------------------------
// <copyright file="Group.cs" company="https://github.com/sant0ro/Yupi">
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
    using System.Data;
    using System.Globalization;

    public class Group
    {
        #region Properties

        public virtual uint AdminOnlyDeco
        {
            get; set;
        }

        [ManyToMany]
        public virtual IList<UserInfo> Admins
        {
            get; protected set;
        }

        public virtual string Badge
        {
            get; set;
        }

        // TODO Rename
        public virtual GroupSymbolColours Colour1
        {
            get; set;
        }

        public virtual GroupBackGroundColours Colour2
        {
            get; set;
        }

        public virtual int CreateTime
        {
            get; set;
        }

        public virtual UserInfo Creator
        {
            get; set;
        }

        public virtual string Description
        {
            get; set;
        }

        public virtual GroupForum Forum
        {
            get; set;
        }

        public virtual int Id
        {
            get; protected set;
        }

        [ManyToMany]
        public virtual IList<UserInfo> Members
        {
            get; protected set;
        }

        public virtual string Name
        {
            get; set;
        }

        [ManyToMany]
        public virtual IList<UserInfo> Requests
        {
            get; protected set;
        }

        public virtual RoomData Room
        {
            get; set;
        }

        // TODO ???
        public virtual uint State
        {
            get; set;
        }

        #endregion Properties

        #region Constructors

        public Group(UserInfo creator)
            : this()
        {
            Creator = creator;
            Admins.Add(creator);
        }

        protected Group()
        {
            Admins = new List<UserInfo>();
            Members = new List<UserInfo>();
            Requests = new List<UserInfo>();
        }

        #endregion Constructors
    }
}