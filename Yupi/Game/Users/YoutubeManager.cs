using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using Yupi.Data.Base.Sessions.Interfaces;
using Yupi.Game.GameClients.Interfaces;
using Yupi.Messages;

namespace Yupi.Game.Users
{
    /// <summary>
    ///     Class YoutubeManager.
    /// </summary>
    internal class YoutubeManager
    {
        internal static readonly Regex YoutubeVideoRegex = new Regex(@"youtu(?:\.be|be\.com)/(?:.*v(?:/|=)|(?:.*/)?)([a-zA-Z0-9-_]+)", RegexOptions.IgnoreCase);

        internal uint UserId;
        internal Dictionary<string, YoutubeVideo> Videos;

        internal YoutubeManager(uint id)
        {
            UserId = id;
            Videos = new Dictionary<string, YoutubeVideo>();
            RefreshVideos();
        }

        public void RefreshVideos()
        {
            Videos.Clear();

            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                queryReactor.SetQuery("SELECT * FROM users_videos_youtube WHERE user_id = @user_id");
                queryReactor.AddParameter("user_id", UserId);

                DataTable table = queryReactor.GetTable();

                if (table == null)
                    return;

                foreach (DataRow row in table.Rows)
                    Videos.Add((string) row["video_id"], new YoutubeVideo((string) row["video_id"], (string) row["name"], (string) row["description"]));
            }
        }

        public string GetTitle(string url)
        {
            string id = GetArgs(url, "v", '?');

            WebClient client = new WebClient();

            return GetArgs(client.DownloadString("http://youtube.com/get_video_info?video_id=" + id), "title", '&');
        }

        public string GetTitleById(string videoId)
        {
            WebClient client = new WebClient();

            return GetArgs(client.DownloadString("http://youtube.com/get_video_info?video_id=" + videoId), "title", '&');
        }

        private string GetArgs(string args, string key, char query)
        {
            int iqs = args.IndexOf(query);

            if (iqs != -1)
            {
                string querystring = (iqs < args.Length - 1) ? args.Substring(iqs + 1) : string.Empty;
                NameValueCollection nvcArgs = HttpUtility.ParseQueryString(querystring);
                return nvcArgs[key];
            }
            return string.Empty;
        }

        public void AddUserVideo(GameClient client, string video)
        {
            if (client != null)
            {
                Match youtubeMatch = YoutubeVideoRegex.Match(video);

                string id;
                string videoName;

                if (youtubeMatch.Success)
                {
                    id = youtubeMatch.Groups[1].Value;
                    videoName = GetTitleById(id);

                    if (string.IsNullOrEmpty(videoName))
                    {
                        client.SendWhisper("This Youtube Video doesn't Exists");
                        return;
                    }
                }
                else
                {
                    client.SendWhisper("This Youtube Url is Not Valid");
                    return;
                }

                UserId = client.GetHabbo().Id;

                using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                {
                    queryReactor.SetQuery(
                        "INSERT INTO users_videos_youtube (user_id, video_id, name, description) VALUES (@user_id, @video_id, @name, @name)");
                    queryReactor.AddParameter("user_id", UserId);
                    queryReactor.AddParameter("video_id", id);
                    queryReactor.AddParameter("name", videoName);
                    queryReactor.RunQuery();
                }

                RefreshVideos();

                client.SendNotif("Youtube Video Added Sucessfully!");
            }
        }
    }

    /// <summary>
    ///     Class YoutubeVideo.
    /// </summary>
    internal class YoutubeVideo
    {
        internal string Description;
        internal string Name;
        internal string VideoId;

        internal YoutubeVideo(string videoId, string name, string description)
        {
            VideoId = videoId;
            Name = name;
            Description = description;
        }

        internal void Serialize(ServerMessage message)
        {
            message.AppendString(VideoId);
            message.AppendString(Name);
            message.AppendString(Description);
        }
    }
}