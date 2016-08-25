using System;
using Nancy;
using Nancy.TinyIoc;

namespace Yupi.Rest
{
	public class Bootstrapper : DefaultNancyBootstrapper
	{
		protected override void ConfigureApplicationContainer(TinyIoCContainer container)
		{
			// Register our app dependency as a normal singleton           
			// @see https://github.com/NancyFx/Nancy/wiki/Bootstrapping-nancy
			// TODO Unify Microsoft.Practices.Unity and NancyFx TinyIoC
		} 
	}
}

