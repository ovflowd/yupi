using System;
using Yupi.Protocol;
using Yupi.Net;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages
{
	class AbstractComposer : IComposer
	{
		protected short Id;
		protected ServerMessagePool Pool;

		public void Init (short id, ServerMessagePool pool)
		{
			this.Id = id;
			this.Pool = pool;
		}
	}

	public class AbstractComposerVoid : AbstractComposer {
		public abstract void Compose(ISender session);
	}

	public abstract class AbstractComposer<T> : AbstractComposer {
		public abstract void Compose(ISender session, T value);
	}

	public abstract class AbstractComposer<T, U> : AbstractComposer {
		public abstract void Compose(ISender session, T first, U second);
	}

	public abstract class AbstractComposer<T, U, V> : AbstractComposer {
		public abstract void Compose(ISender session, T first, U second, V third);
	}

	public abstract class AbstactComposerEmpty : AbstractComposerVoid {
		public override void Compose (ISender session)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				session.Send (message);
			}
		}
	}
}

