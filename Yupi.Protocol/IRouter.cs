using System;

namespace Yupi.Protocol
{
	public interface IRouter
	{
		T GetComposer<T> ();
	}
}

