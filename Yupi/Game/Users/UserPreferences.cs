using System;
using System.Data;
using Yupi.Data.Base.Adapters.Interfaces;

namespace Yupi.Game.Users
{
    /// <summary>
    ///     Class UserPreferences.
    /// </summary>
    internal class UserPreferences
    {
        /// <summary>
        ///     The _user identifier
        /// </summary>
        private readonly uint _userId;

        internal int ChatColor;
        internal bool DisableCameraFollow;
        internal bool IgnoreRoomInvite;
        internal int NewnaviHeight = 600;
        internal int NewnaviWidth = 580;
        internal int NewnaviX;
        internal int NewnaviY;

        internal bool PreferOldChat;
        internal string Volume = "0,0,0";

        /// <summary>
        ///     Initializes a new instance of the <see cref="UserPreferences" /> class.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        internal UserPreferences(uint userId)
        {
            _userId = userId;

            DataRow row;

            using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                commitableQueryReactor.SetQuery("SELECT * FROM users_preferences WHERE userid = " + _userId);
                commitableQueryReactor.AddParameter("userid", _userId);
                row = commitableQueryReactor.GetRow();

                if (row == null)
                {
                    commitableQueryReactor.RunFastQuery("REPLACE INTO users_preferences (userid, volume) VALUES (" +
                                                        _userId +
                                                        ", '100,100,100')");
                    return;
                }
            }

            PreferOldChat = Yupi.EnumToBool((string) row["prefer_old_chat"]);
            IgnoreRoomInvite = Yupi.EnumToBool((string) row["ignore_room_invite"]);
            DisableCameraFollow = Yupi.EnumToBool((string) row["disable_camera_follow"]);
            Volume = (string) row["volume"];
            NewnaviX = Convert.ToInt32(row["newnavi_x"]);
            NewnaviY = Convert.ToInt32(row["newnavi_y"]);
            NewnaviWidth = Convert.ToInt32(row["newnavi_width"]);
            NewnaviHeight = Convert.ToInt32(row["newnavi_height"]);
            ChatColor = Convert.ToInt32(row["chat_color"]);
        }

        internal void Save()
        {
            using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                commitableQueryReactor.SetQuery(
                    "UPDATE users_preferences SET volume = @volume, prefer_old_chat = @prefer_old_chat, ignore_room_invite = @ignore_room_invite, newnavi_x = @newnavi_x, newnavi_y = @newnavi_y, newnavi_width = @newnavi_width, newnavi_height = @newnavi_height, disable_camera_follow = @disable_camera_follow, chat_color = @chat_color WHERE userid = @userid");
                commitableQueryReactor.AddParameter("userid", _userId);
                commitableQueryReactor.AddParameter("prefer_old_chat", Yupi.BoolToEnum(PreferOldChat));
                commitableQueryReactor.AddParameter("ignore_room_invite", Yupi.BoolToEnum(IgnoreRoomInvite));
                commitableQueryReactor.AddParameter("volume", Volume);
                commitableQueryReactor.AddParameter("newnavi_x", NewnaviX);
                commitableQueryReactor.AddParameter("newnavi_y", NewnaviY);
                commitableQueryReactor.AddParameter("newnavi_width", NewnaviWidth);
                commitableQueryReactor.AddParameter("newnavi_height", NewnaviHeight);
                commitableQueryReactor.AddParameter("disable_camera_follow", Yupi.BoolToEnum(DisableCameraFollow));
                commitableQueryReactor.AddParameter("chat_color", ChatColor);
                commitableQueryReactor.RunQuery();
            }
        }
    }
}