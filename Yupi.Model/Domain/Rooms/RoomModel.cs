using System;
using System.Globalization;
using Yupi.Model.Domain.Components;

namespace Yupi.Model.Domain
{
	/// <summary>
	///     Class RoomModel.
	/// </summary>
	public class RoomModel
	{
		public virtual int Id { get; protected set; }

		/// <summary>
		///     The club only
		/// </summary>
		public virtual bool ClubOnly { get; set; }

		/// <summary>
		///     The door orientation
		/// </summary>
		public virtual int DoorOrientation { get; set; }

		/// <summary>
		///     The door position
		/// </summary>
		public virtual Vector Door { get; protected set; }

		/// <summary>
		///     The got public pool
		/// </summary>
		public virtual bool GotPublicPool { get; set; }

		/// <summary>
		///     The heightmap
		/// </summary>
		public virtual string Heightmap { get; set; }

		/// <summary>
		///     The static furni map
		/// </summary>
		public virtual string StaticFurniMap { get; set; }
	}
}