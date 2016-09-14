namespace Yupi.Messages.Encoders
{
    using System;
    using System.Collections.Generic;

    using Yupi.Protocol.Buffers;

    public static class ListEncoder
    {
        #region Methods

        public static void Append(this ServerMessage message, IList<string> value)
        {
            message.AppendInteger(value.Count);
            foreach (string entry in value)
            {
                message.AppendString(entry);
            }
        }

        #endregion Methods
    }
}