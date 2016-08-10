using System;

namespace Yupi.Model.Domain
{
	public class PostItItem : WallItem<WallBaseItem>
	{
		public virtual string Text { get; set; }

		// TODO Validate RGB-HTML string
		public virtual string Color { get; set; }

		public PostItItem ()
		{
			Color = "FFFF33";
		}

		public override string GetExtraData ()
		{
			return Color;
		}
	}
}

