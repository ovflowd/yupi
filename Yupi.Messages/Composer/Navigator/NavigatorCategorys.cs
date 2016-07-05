using System;

using Yupi.Protocol.Buffers;


namespace Yupi.Messages.Navigator
{
	public class NavigatorCategorys : AbstractComposer<HotelBrowserManager>
	{
		public override void Compose (Yupi.Protocol.ISender session, HotelBrowserManager navigator)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(navigator.PrivateCategories.Count + 4);

				foreach (PublicCategory flat in navigator.PrivateCategories.Values)
					message.AppendString($"category__{flat.Caption}");

				message.AppendString("recommended");
				message.AppendString("new_ads");
				message.AppendString("staffpicks");
				message.AppendString("official");
				session.Send (message);
			}
		}
	}
}

