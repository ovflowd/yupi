namespace Yupi.Protocol
{
    using System;

    using Yupi.Protocol.Buffers;

    public interface ISender
    {
        #region Methods

        void Send(ServerMessage message);

        #endregion Methods
    }
}