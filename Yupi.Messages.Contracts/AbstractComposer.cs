// ---------------------------------------------------------------------------------
// <copyright file="AbstractComposer.cs" company="https://github.com/sant0ro/Yupi">
//   Copyright (c) 2016 Claudio Santoro, TheDoctor
// </copyright>
// <license>
//   Permission is hereby granted, free of charge, to any person obtaining a copy
//   of this software and associated documentation files (the "Software"), to deal
//   in the Software without restriction, including without limitation the rights
//   to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//   copies of the Software, and to permit persons to whom the Software is
//   furnished to do so, subject to the following conditions:
//
//   The above copyright notice and this permission notice shall be included in
//   all copies or substantial portions of the Software.
//
//   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//   IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//   FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//   AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//   LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//   OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//   THE SOFTWARE.
// </license>
// ---------------------------------------------------------------------------------
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