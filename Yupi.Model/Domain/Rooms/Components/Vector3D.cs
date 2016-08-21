using System;

namespace Yupi.Model.Domain.Components
{
	public class Vector2D {
		public virtual int X { get; set; }
		public virtual int Y { get; set; }

		public Vector2D() {
			this.X = 0;
			this.Y = 0;
		}

		public Vector2D(int x, int y) {
			this.X = x;
			this.Y = y;
		}

		public virtual bool Equals(Vector2D other)
		{
			// If parameter is null return false:
			if ((object)other == null)
			{
				return false;
			}

			// Return true if the fields match:
			return (X == other.X) && (Y == other.Y);
		}

		public override bool Equals(System.Object other)
		{
			return Equals (other as Vector2D);
		}

		public virtual bool Equals(int otherX, int otherY) {
			return X == otherX && Y == otherY;
		}

		public override int GetHashCode()
		{
			return X ^ Y;
		}
	}

	public class Vector3D : Vector2D {
		
		public virtual double Z { get; set; } // TODO Why double?

		public Vector3D() : base() {
			this.Z = 0;
		}

		public Vector3D(int x, int y, double z) : base(x, y) {
			this.Z = z;
		}

		public virtual bool Equals(Vector3D other)
		{
			return base.Equals (other) && this.Z == other.Z;
		}

		public override bool Equals(System.Object other)
		{
			return Equals (other as Vector3D);
		}

		public virtual bool Equals(int otherX, int otherY, double otherZ) {
			return base.Equals(otherX, otherY) && this.Z == otherZ;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode() ^ (int)Z;
		}
	}
}

