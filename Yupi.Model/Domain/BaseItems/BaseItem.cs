using System.Collections.Generic;

namespace Yupi.Model.Domain
{
	[IsDiscriminated]
	public abstract class BaseItem
	{
		public virtual int Id { get; protected set; }

		/// <summary>
		///     The allow gift
		/// </summary>
		public virtual bool AllowGift { get; set; }

		/// <summary>
		///     The allow inventory stack
		/// </summary>
		public virtual bool AllowInventoryStack { get; set; }

		/// <summary>
		///     The allow marketplace sell
		/// </summary>
		public virtual bool AllowMarketplaceSell { get; set; }

		/// <summary>
		///     The allow recycle
		/// </summary>
		public virtual bool AllowRecycle { get; set; }

		/// <summary>
		///     The allow trade
		/// </summary>
		public virtual bool AllowTrade { get; set; }

		/// <summary>
		///     The height
		/// </summary>
		public virtual double Height { get; set; }
	
		/// <summary>
		///     The length
		/// </summary>
		public virtual int Length { get; set; }

		/// <summary>
		///     The name
		/// </summary>
		public virtual string Name { get; set; }

		/// <summary>
		///     The public name
		/// </summary>
		public virtual string PublicName { get; set; }

		/// <summary>
		///     The sprite identifier
		/// </summary>
		public virtual int SpriteId { get; set; }

		/// <summary>
		///     The stackable
		/// </summary>
		public virtual bool Stackable { get; set; }

		/// <summary>
		///     The stack multipler
		/// </summary>
		public virtual bool StackMultipler { get; set; }

		/// <summary>
		///     The subscriber only
		/// </summary>
		public virtual bool SubscriberOnly { get; set; }

		/// <summary>
		///     The toggle height
		/// </summary>
		public virtual double[] ToggleHeight { get; set; }

		/// <summary>
		///     The walkable
		/// </summary>
		public virtual bool Walkable { get; set; }

		/// <summary>
		///     The width
		/// </summary>
		public virtual int Width { get; set; }
	}
}