namespace Yupi.Messages.Contracts
{
    using System;

    using Yupi.Net;
    using Yupi.Protocol;
    using Yupi.Protocol.Buffers;

    public class AbstractComposer : IComposer
    {
        #region Fields

        protected short Id;
        protected ServerMessagePool Pool;

        #endregion Fields

        #region Methods

        public void Init(short id, ServerMessagePool pool)
        {
            this.Id = id;
            this.Pool = pool;
        }

        #endregion Methods
    }

    public abstract class AbstractComposer<T> : AbstractComposer
    {
        #region Methods

        public abstract void Compose(Yupi.Protocol.ISender session, T value);

        #endregion Methods
    }

    public abstract class AbstractComposer<T, U> : AbstractComposer
    {
        #region Methods

        public abstract void Compose(Yupi.Protocol.ISender session, T first, U second);

        #endregion Methods
    }

    public abstract class AbstractComposer<T, U, V> : AbstractComposer
    {
        #region Methods

        public abstract void Compose(Yupi.Protocol.ISender session, T first, U second, V third);

        #endregion Methods
    }

    // TODO Should be removed if possible
    public abstract class AbstractComposer<T, U, V, W> : AbstractComposer
    {
        #region Methods

        public abstract void Compose(Yupi.Protocol.ISender session, T first, U second, V third, W fourth);

        #endregion Methods
    }

    public abstract class AbstractComposerEmpty : AbstractComposerVoid
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                session.Send(message);
            }
        }

        #endregion Methods
    }

    public abstract class AbstractComposerVoid : AbstractComposer
    {
        #region Methods

        public abstract void Compose(Yupi.Protocol.ISender session);

        #endregion Methods
    }
}