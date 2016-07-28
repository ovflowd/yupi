using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Landing
{
	public class LandingWidgetMessageComposer : AbstractComposer<string>
	{
		public override void Compose ( Yupi.Protocol.ISender session, string text)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				if (!string.IsNullOrEmpty(text))
				{
					// TODO Refactor
					string[] array = text.Split(',');

					message.AppendString(text);
					message.AppendString(array[1]);
				}
				else
				{
					message.AppendString(string.Empty);
					message.AppendString(string.Empty);
				}
				session.Send (message);
			}
		}
	}
}

