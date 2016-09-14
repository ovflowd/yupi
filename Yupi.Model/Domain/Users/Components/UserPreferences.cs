namespace Yupi.Model.Domain.Components
{
    using System;
    using System.Data;

    public class UserPreferences
    {
        #region Properties

        public virtual ChatBubbleStyle ChatBubbleStyle
        {
            get; set;
        }

        /// <summary>
        ///     Disable Room Camera
        /// </summary>
        public virtual bool DisableCameraFollow
        {
            get; set;
        }

        /// <summary>
        ///     Ignore Room Invitations
        /// </summary>
        public virtual bool IgnoreRoomInvite
        {
            get; set;
        }

        /// <summary>
        ///     Navigator Height
        /// </summary>
        public virtual int NavigatorHeight
        {
            get; set;
        }

        /// <summary>
        ///     Navigator Width
        /// </summary>
        public virtual int NavigatorWidth
        {
            get; set;
        }

        /// <summary>
        ///     Navigator Position X
        /// </summary>
        public virtual int NewnaviX
        {
            get; set;
        }

        /// <summary>
        ///     Navigator Position Y
        /// </summary>
        public virtual int NewnaviY
        {
            get; set;
        }

        /// <summary>
        ///     User Prefers Old Chat
        /// </summary>
        public virtual bool PreferOldChat
        {
            get; set;
        }

        // TODO What do the single values mean?
        public virtual int Volume1
        {
            get; set;
        }

        public virtual int Volume2
        {
            get; set;
        }

        public virtual int Volume3
        {
            get; set;
        }

        #endregion Properties

        #region Constructors

        public UserPreferences()
        {
            NavigatorHeight = 600;
            NavigatorWidth = 580;
            Volume1 = 100;
            Volume2 = 100;
            Volume3 = 100;
            ChatBubbleStyle = ChatBubbleStyle.Normal;
        }

        #endregion Constructors
    }
}