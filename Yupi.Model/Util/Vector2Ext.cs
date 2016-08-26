using System;
using System.Numerics;

namespace Yupi.Model
{
	public static class Vector2Ext
	{
		public static int CalculateRotation (this Vector2 source, Vector2 target)
		{
			Vector2 direction = target - source;
			double theta = Math.Atan2 (direction.Y, direction.X);

			double degree = theta * 180 / Math.PI;

			return ((int)degree + 90) / 45;
		}

		public static bool Equals (this Vector3 a, Vector2 b)
		{
			return a.X == b.X && a.Y == b.Y;
		}
	}
}

