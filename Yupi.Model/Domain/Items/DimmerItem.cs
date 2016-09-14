using System;

namespace Yupi.Model.Domain
{
	public class DimmerItem : WallItem<DimmerBaseItem>
	{
		public virtual MoodlightData Data { get; set; }

		public DimmerItem ()
		{
			Data = new MoodlightData ();
		}
	}
}

