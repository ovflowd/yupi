using System;

namespace Yupi.Protocol
{
	public interface ISession<T> : Yupi.Net.ISession<T>, ISender
	{
		IRouter Router { get; set; }
	}
}

