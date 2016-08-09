using System;
using Headspring;

namespace Yupi.Model.Domain
{
	public class TalentType : Enumeration<TalentType>
	{
		public static readonly TalentType Citizenship = new TalentType(1, "citizenship");
		public static readonly TalentType Status = new TalentType(2, "status");

		private TalentType (int value, string displayName) : base(value, displayName)
		{
			
		}
	}
}

