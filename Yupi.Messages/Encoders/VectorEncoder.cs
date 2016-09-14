namespace Yupi.Messages.Encoders
{
    using System;
    using System.Globalization;
    using System.Numerics;

    using Yupi.Protocol.Buffers;

    public static class VectorEncoder
    {
        #region Methods

        public static void Append(this ServerMessage message, Vector3 vector)
        {
            message.AppendInteger((int) vector.X);
            message.AppendInteger((int) vector.Y);
            message.AppendString(vector.Z.ToString(CultureInfo.InvariantCulture));
        }

        #endregion Methods
    }
}