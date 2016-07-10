using System;
using System.Data;
using Yupi.Emulator.Data.Base.Adapters.Interfaces;
using Yupi.Data;

namespace Yupi.Model.Domain.Components
{
    public class UserPreferences
    {
        /// <summary>
        ///     User Chat Color
        /// </summary>
		public virtual int ChatColor { get; set; }

        /// <summary>
        ///     Disable Room Camera
        /// </summary>
		public virtual bool DisableCameraFollow { get; set; }

        /// <summary>
        ///     Ignore Room Invitations
        /// </summary>
		public virtual bool IgnoreRoomInvite { get; set; }

        /// <summary>
        ///     Navigator Height
        /// </summary>
		public virtual int NavigatorHeight  { get; set; }

        /// <summary>
        ///     Navigator Width
        /// </summary>
		public virtual int NavigatorWidth  { get; set; }

        /// <summary>
        ///     Navigator Position X
        /// </summary>
		public virtual int NewnaviX { get; set; }

        /// <summary>
        ///     Navigator Position Y
        /// </summary>
		public virtual int NewnaviY { get; set; }

        /// <summary>
        ///     User Prefers Old Chat
        /// </summary>
		public virtual bool PreferOldChat { get; set; }

        /// <summary>
        ///     User Volume Settings
        /// </summary>
		public virtual string Volume { get; set; }

     	public UserPreferences()
        {
			NavigatorHeight = 600;
			NavigatorWidth = 580;
			Volume = "100,100,100";
        }
    }
}