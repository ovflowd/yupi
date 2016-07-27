namespace Yupi.Model.Domain
{
	public class TargetedOffer
    {
		public virtual int Id { get; protected set; }
		public virtual string Identifier { get; protected set; }
		public virtual int ExpirationTime { get; protected set; }
		public virtual int PurchaseLimit { get; protected set; }
		public virtual int CostCredits { get; protected set; }
		public virtual int CostDiamonds { get; protected set; }
		public virtual int CostDuckets { get; protected set; }
		public virtual string Title { get; protected set; }
		public virtual string Description { get; protected set; }
		public virtual string Image { get; protected set; }

		public virtual BaseItem[] Products { get; protected set; }
    }
}