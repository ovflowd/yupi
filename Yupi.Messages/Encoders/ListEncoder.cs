using System;
using Yupi.Protocol.Buffers;
using System.Collections.Generic;

namespace Yupi.Messages.Encoders
{
	public static class ListEncoder
	{
		public static void Append(this ServerMessage message, IList<string> value) {
			message.AppendInteger (value.Count);
			foreach(string entry in value) {
				message.AppendString(entry);
			}
		}
	}
}

