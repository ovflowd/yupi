using System;

namespace Yupi.Model.Domain
{
	public class PostItItem : WallItem
	{
		public virtual string Text { get; set; }

		public override string GetExtraData ()
		{
			return Text;
		}
	}
}

