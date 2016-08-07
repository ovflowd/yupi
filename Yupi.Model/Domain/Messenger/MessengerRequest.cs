
namespace Yupi.Model.Domain
{
	/// <summary>
	///     Class MessengerRequest.
	/// </summary>
	public class MessengerRequest
	{
		public virtual int Id { get; protected set; }

		public virtual UserInfo To { get; set; }

		public virtual UserInfo From { get; set; }
	}
}