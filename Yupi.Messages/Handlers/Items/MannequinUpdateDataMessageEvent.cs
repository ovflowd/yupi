using System;



namespace Yupi.Messages.Items
{
	public class MannequinUpdateDataMessageEvent : AbstractHandler
	{
		public override void HandleMessage ( Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			uint pId = request.GetUInt32();

			/*
			RoomItem item = session.GetHabbo().CurrentRoom.GetRoomItemHandler().GetItem(pId);

			if (item == null)
				return;

			if (!item.ExtraData.Contains(Convert.ToChar(5)))
				return;

			if (!session.GetHabbo().CurrentRoom.CheckRights(session, true))
				return;

			string[] array = item.ExtraData.Split(Convert.ToChar(5));

			array[0] = session.GetHabbo().Gender.ToLower();
			array[1] = string.Empty;

			string[] array2 = session.GetHabbo().Look.Split('.');
			// TODO Use String.Join??? (need more knowlege about figure strings
			foreach (
				string text in
				array2.Where(
					text =>
					!text.Contains("hr") && !text.Contains("hd") && !text.Contains("he") && !text.Contains("ea") &&
					!text.Contains("ha")))
			{
				array[1] += text + ".";
			}

			array[1] = array[1].TrimEnd('.');
			item.ExtraData = String.Join(Convert.ToChar(5).ToString(), array);
			item.UpdateNeeded = true;
			item.UpdateState(true, true);

			using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
			{
				queryReactor.SetQuery("UPDATE items_rooms SET extra_data = @extraData WHERE id = @id");
				queryReactor.AddParameter("extraData", item.ExtraData);
				queryReactor.AddParameter("id", item.Id);
				queryReactor.RunQuery();
			}
			*/
			throw new NotImplementedException ();
		}
	}
}

