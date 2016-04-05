using System;
using Yupi.Protocol;
using Yupi.Net;
using Yupi.Emulator.Game.GameClients.Interfaces;

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
		public abstract void Compose(GameClient session);
	}

	public abstract class AbstractComposer<T> : AbstractComposer {
		public abstract void Compose(GameClient session, T value);
	}
}

