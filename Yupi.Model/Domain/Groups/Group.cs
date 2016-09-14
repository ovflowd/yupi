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