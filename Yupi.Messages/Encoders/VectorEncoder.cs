using System.Globalization;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Encoders
{
    public static class VectorEncoder
    {
        public static void Append(this ServerMessage message, Vector3 vector)
        {
            message.AppendInteger((int) vector.X);
            message.AppendInteger((int) vector.Y);
            message.AppendString(vector.Z.ToString(CultureInfo.InvariantCulture));
        }
    }
}