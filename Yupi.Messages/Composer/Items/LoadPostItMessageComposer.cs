using System;


using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Items
{
	public class LoadPostItMessageComposer : AbstractComposer<PostItItem>
	{
		public override void Compose (Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, PostItItem item)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendString(item.Id.ToString());
				message.AppendString(item.Text);
				session.Send (message);
			}
		}
	}
}

