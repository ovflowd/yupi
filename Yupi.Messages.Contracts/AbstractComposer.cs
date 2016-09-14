using System;
using Yupi.Protocol;
using Yupi.Net;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Contracts
{
    public class AbstractComposer : IComposer
    {
        protected short Id;
        protected ServerMessagePool Pool;

        public void Init(short id, ServerMessagePool pool)
        {
            this.Id = id;
            this.Pool = pool;
        }
    }

    public abstract class AbstractComposerVoid : AbstractComposer
    {
        public abstract void Compose(Yupi.Protocol.ISender session);
    }

    public abstract class AbstractComposer<T> : AbstractComposer
    {
        public abstract void Compose(Yupi.Protocol.ISender session, T value);
    }

    public abstract class AbstractComposer<T, U> : AbstractComposer
    {
        public abstract void Compose(Yupi.Protocol.ISender session, T first, U second);
    }

    public abstract class AbstractComposer<T, U, V> : AbstractComposer
    {
        public abstract void Compose(Yupi.Protocol.ISender session, T first, U second, V third);
    }

    // TODO Should be removed if possible
    public abstract class AbstractComposer<T, U, V, W> : AbstractComposer
    {
        public abstract void Compose(Yupi.Protocol.ISender session, T first, U second, V third, W fourth);
    }

    public abstract class AbstractComposerEmpty : AbstractComposerVoid
    {
        public override void Compose(Yupi.Protocol.ISender session)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                session.Send(message);
            }
        }
    }
}