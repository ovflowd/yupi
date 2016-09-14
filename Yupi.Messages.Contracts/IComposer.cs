namespace Yupi.Messages.Contracts
{
    using System;

    using Yupi.Net;
    using Yupi.Protocol;
    using Yupi.Protocol.Buffers;

    public interface IComposer
    {
        #region Methods

        void Init(short id, ServerMessagePool pool);

        #endregion Methods
    }
}