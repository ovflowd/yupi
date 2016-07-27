namespace Yupi.Model.Domain
{
	/// <summary>
	///     Class EcotronReward.
	/// </summary>
	public class EcotronReward
	{
		public virtual int Id { get; protected set; }
		public virtual int RewardLevel { get; protected set; }
		public virtual BaseItem BaseItem { get; protected set; }
	}
}