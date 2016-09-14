// ---------------------------------------------------------------------------------
// <copyright file="GetCameraRequest.cs" company="https://github.com/sant0ro/Yupi">
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
namespace Yupi.Messages.Camera
{
    using System;
    using System.Web.Script.Serialization;

    public class GetCameraRequest : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            /*
            try
            {
                int count = Request.GetInteger();

                byte[] bytes = Request.GetBytes(count);
                string outData = Converter.Deflate(bytes);

                // TODO Don't use post. Might want to consider some binary DB storage
                string url = WebManager.HttpPostJson(ServerExtraSettings.StoriesApiServerUrl, outData);

                JavaScriptSerializer serializer = new JavaScriptSerializer();

                dynamic jsonArray = serializer.Deserialize<object>(outData);
                string encodedurl = ServerExtraSettings.StoriesApiHost + url;

                encodedurl = encodedurl.Replace("\n", string.Empty);

                int roomId = jsonArray["roomid"];
                long timeStamp = jsonArray["timestamp"];

                using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                {
                    queryReactor.SetQuery("INSERT INTO cms_stories_photos_preview (user_id,user_name,room_id,image_preview_url,image_url,type,date,tags) VALUES (@userid,@username,@roomid,@imagepreviewurl,@imageurl,@types,@dates,@tag)");
                    queryReactor.AddParameter("userid", session.GetHabbo().Id);
                    queryReactor.AddParameter("username", session.GetHabbo().Name);
                    queryReactor.AddParameter("roomid", roomId);
                    queryReactor.AddParameter("imagepreviewurl", encodedurl);
                    queryReactor.AddParameter("imageurl", encodedurl);
                    queryReactor.AddParameter("types", "PHOTO");
                    queryReactor.AddParameter("dates", timeStamp);
                    queryReactor.AddParameter("tag", "");
                    queryReactor.RunQuery();
                }
                // TODO When do we need encoded url and when not?
                router.GetComposer<CameraStorageUrlMessageComposer>().Compose(session, url);
            }
            catch (Exception)
            {
                // TODO Log exception!
                session.SendNotif("An error occured while processing the image!");
            }*/
            throw new NotImplementedException();
        }

        #endregion Methods
    }
}