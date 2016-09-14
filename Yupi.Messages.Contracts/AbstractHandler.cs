namespace Yupi.Messages
{
    using System;

    using Yupi.Net;
    using Yupi.Protocol.Buffers;

    public abstract class AbstractHandler
    {
        #region Properties

        /// <summary>
        /// Gets a value indicating whether this <see cref="Yupi.Messages.AbstractHandler"/> requires a user being attached to the session
        /// </summary>
        /// <value><c>true</c> if requires user; otherwise, <c>false</c>.</value>
        public virtual bool RequireUser
        {
            get
            {
                return true; // TODO should be validated by router (session.GetHabbo() != null)
            }
        }

        #endregion Properties

        #region Methods

        public abstract void HandleMessage(Yupi.Model.Domain.Habbo session, ClientMessage request,
            Yupi.Protocol.IRouter router);

        #endregion Methods
    }
}