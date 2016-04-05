using System;
using System.Data;
using Yupi.Emulator.Data.Base.Adapters.Interfaces;

namespace Yupi.Emulator.Game.Users
{
    /// <summary>
    ///     Class UserPreferences.
    /// </summary>
     class UserPreferences
    {
        /// <summary>
        ///     User Id
        /// </summary>
        private readonly uint _userId;

        /// <summary>
        ///     User Chat Color
        /// </summary>
         int ChatColor;

        /// <summary>
        ///     Disable Room Camera
        /// </summary>
         bool DisableCameraFollow;

        /// <summary>
        ///     Ignore Room Invitations
        /// </summary>
         bool IgnoreRoomInvite;

        /// <summary>
        ///     Navigator Height
        /// </summary>
         int NavigatorHeight = 600;

        /// <summary>
        ///     Navigator Width
        /// </summary>
         int NavigatorWidth = 580;

        /// <summary>
        ///     Navigator Position X
        /// </summary>
         int NewnaviX;

        /// <summary>
        ///     Navigator Position Y
        /// </summary>
         int NewnaviY;

        /// <summary>
        ///     User Prefers Old Chat
        /// </summary>
         bool PreferOldChat;

        /// <summary>
        ///     User Volume Settings
        /// </summary>
         string Volume = "0,0,0";

        /// <summary>
        ///     Initializes a new instance of the <see cref="UserPreferences" /> class.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
         UserPreferences(uint userId)
        {
            _userId = userId;

            DataRow userPreferences;

            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                queryReactor.RunFastQuery($"SELECT COUNT(*) FROM users_preferences WHERE user_id = {_userId}");

                int existsPreference = queryReactor.GetInteger();

                if (existsPreference == 0)
                {
                    queryReactor.RunFastQuery($"REPLACE INTO users_preferences (user_id, volume) VALUES ({_userId}, '100,100,100')");

                    return;
                }

                queryReactor.RunFastQuery($"SELECT * FROM users_preferences WHERE user_id = {_userId}");

                userPreferences = queryReactor.GetRow();

                if (userPreferences == null)
                    return;
            }

            PreferOldChat = Yupi.EnumToBool((string) userPreferences["prefer_old_chat"]);

            IgnoreRoomInvite = Yupi.EnumToBool((string) userPreferences["ignore_room_invite"]);

            DisableCameraFollow = Yupi.EnumToBool((string) userPreferences["disable_camera_follow"]);

            Volume = (string) userPreferences["volume"];

            NewnaviX = Convert.ToInt32(userPreferences["newnavi_x"]);

            NewnaviY = Convert.ToInt32(userPreferences["newnavi_y"]);

            NavigatorWidth = Convert.ToInt32(userPreferences["newnavi_width"]);

            NavigatorHeight = Convert.ToInt32(userPreferences["newnavi_height"]);

            ChatColor = Convert.ToInt32(userPreferences["chat_color"]);
        }

         void Save()
        {
            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                queryReactor.SetQuery("UPDATE users_preferences SET volume = @volume, prefer_old_chat = @prefer_old_chat, ignore_room_invite = @ignore_room_invite, newnavi_x = @newnavi_x, newnavi_y = @newnavi_y, newnavi_width = @newnavi_width, newnavi_height = @newnavi_height, disable_camera_follow = @disable_camera_follow, chat_color = @chat_color WHERE user_id = @userid");
                queryReactor.AddParameter("userid", _userId);
                queryReactor.AddParameter("prefer_old_chat", Yupi.BoolToEnum(PreferOldChat));
                queryReactor.AddParameter("ignore_room_invite", Yupi.BoolToEnum(IgnoreRoomInvite));
                queryReactor.AddParameter("volume", Volume);
                queryReactor.AddParameter("newnavi_x", NewnaviX);
                queryReactor.AddParameter("newnavi_y", NewnaviY);
                queryReactor.AddParameter("newnavi_width", NavigatorWidth);
                queryReactor.AddParameter("newnavi_height", NavigatorHeight);
                queryReactor.AddParameter("disable_camera_follow", Yupi.BoolToEnum(DisableCameraFollow));
                queryReactor.AddParameter("chat_color", ChatColor);
                queryReactor.RunQuery();
            }
        }
    }
}