using System;

using System.Data;


namespace Yupi.Messages.Other
{
	public class HabboCameraMessageEvent : AbstractHandler
	{
		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
			{
				queryReactor.SetQuery($"SELECT * FROM cms_stories_photos_preview WHERE user_id = {session.GetHabbo().Id} AND type = 'PHOTO' ORDER BY id DESC LIMIT 1");

				DataTable table = queryReactor.GetTable();

				foreach (DataRow dataRow in table.Rows)
				{
					object date = dataRow["date"];
					object room = dataRow["room_id"];
					object photo = dataRow["id"];
					object image = dataRow["image_url"];

					using (IQueryAdapter queryReactor2 = Yupi.GetDatabaseManager().GetQueryReactor())
					{
						queryReactor2.SetQuery(
							"INSERT INTO cms_stories_photos (user_id,user_name,room_id,image_preview_url,image_url,type,date,tags) VALUES (@user_id,@user_name,@room_id,@image_url,@image_url,@type,@date,@tags)");
						queryReactor2.AddParameter("user_id", session.GetHabbo().Id);
						queryReactor2.AddParameter("user_name", session.GetHabbo().UserName);
						queryReactor2.AddParameter("room_id", room);
						queryReactor2.AddParameter("image_url", image);
						queryReactor2.AddParameter("type", "PHOTO");
						queryReactor2.AddParameter("date", date);
						queryReactor2.AddParameter("tags", "");
						queryReactor2.RunQuery();

						string newPhotoData = "{\"t\":" + date + ",\"u\":\"" + photo + "\",\"m\":\"\",\"s\":" + room +
							",\"w\":\"" + image + "\"}";

						UserItem item = session.GetHabbo()
							.GetInventoryComponent()
							.AddNewItem(0, "external_image_wallitem_poster", newPhotoData, 0, true, false, 0, 0);

						session.GetHabbo().GetInventoryComponent().UpdateItems(false);
						session.GetHabbo().Credits -= 2;
						session.GetHabbo().UpdateCreditsBalance();
						session.GetHabbo().GetInventoryComponent().SendNewItems(item.Id);
					}
				}
			}

			router.GetComposer<CameraPurchaseOk> ().Compose (session);
		}
	}
}

