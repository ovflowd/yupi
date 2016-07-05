using System;
using Yupi.Emulator.Data.Base.Managers.Interfaces;

namespace Yupi.Data
{
	public class DatabaseSingleton
	{
		// HACK Will be replaced by ORM later on...
		public static IDatabaseManager DatabaseManager;
	}
}

