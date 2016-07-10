
namespace Yupi.Model.Domain
{
	/// <summary>
	///     Class MessengerRequest.
	/// </summary>
	public class MessengerRequest
	{
		public virtual int Id { get; protected set; }

		public virtual Habbo To { get; set; }

		public virtual Habbo From { get; set; }
	}
}