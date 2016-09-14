using System;
using Yupi.Protocol.Buffers;
using Yupi.Net;


namespace Yupi.Messages
{
	public abstract class AbstractHandler
	{
		/// <summary>
		/// Gets a value indicating whether this <see cref="Yupi.Messages.AbstractHandler"/> requires a user being attached to the session
		/// </summary>
		/// <value><c>true</c> if requires user; otherwise, <c>false</c>.</value>
		public virtual bool RequireUser {
			get { 
				return true;  // TODO should be validated by router (session.GetHabbo() != null)
			}
		}

		public abstract void HandleMessage( Yupi.Model.Domain.Habbo session, ClientMessage request, Yupi.Protocol.IRouter router);
	}
}

