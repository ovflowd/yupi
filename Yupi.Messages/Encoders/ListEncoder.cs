using System.Collections.Generic;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Encoders
{
    public static class ListEncoder
    {
        public static void Append(this ServerMessage message, IList<string> value)
        {
            message.AppendInteger(value.Count);
            foreach (var entry in value) message.AppendString(entry);
        }
    }
}