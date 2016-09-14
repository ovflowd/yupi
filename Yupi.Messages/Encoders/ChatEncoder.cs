using System;
using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Encoders
{
    public static class ChatEncoder
    {
        public static void Append(this ServerMessage message, ChatMessage entry, int count)
        {
            message.AppendInteger(entry.Entity.Id);
            message.AppendString(entry.FilteredMessage());
            message.AppendInteger(entry.GetEmotion().Value);
            message.AppendInteger(entry.Bubble.Value);

            // Replaces placeholders the way String.Format does: {0}
            message.AppendInteger(entry.Links.Count);

            foreach (Link link in entry.Links)
            {
                message.AppendString(link.URL);
                message.AppendString(link.Text);
                message.AppendBool(link.IsInternal);
            }

            // Count is used to detect lag (client side)
            message.AppendInteger(count);
        }
    }
}