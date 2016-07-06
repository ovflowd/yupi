using System;

namespace Yupi.Models.Rooms
{
	public class Vector<T> {
		public T X = default(T);

		public Vector(T X) {
			this.X = X;
		}
	}

	public class Vector<T, U> : Vector<T> {
		public U Y = default(U);

		public Vector(T X, U Y) {
			this.X = X;
			this.Y = Y;
		}
	}

	public class Vector<T, U, V> : Vector<T, U>
	{
		public V Z = default(V);

		public Vector(T X, U Y, V Z) {
			this.X = X;
			this.Y = Y;
			this.Z = Z;
		}
	}
}

