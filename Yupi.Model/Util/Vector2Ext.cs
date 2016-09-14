using System;
using System.Numerics;

namespace Yupi.Model
{
	public static class Vector2Ext
	{
		public static Vector2 ToVector2(this Vector3 source) {
			return new Vector2 (source.X, source.Y);
		}

		public static int CalculateRotation (this Vector3 source, Vector3 target)
		{
			return CalculateRotation (source.ToVector2(), target.ToVector2());
		}

		public static int CalculateRotation (this Vector2 source, Vector2 target)
		{
			double theta = Math.Atan2 (target.Y - source.Y, target.X - source.X);

			double degree = theta * 180 / Math.PI;

			if (degree < 0) {
				degree += 360;
			}

			return (((int)degree + 90) / 45) % 8;
		}

		public static bool Equals (this Vector3 a, Vector2 b)
		{
			return a.X == b.X && a.Y == b.Y;
		}
	}
}

