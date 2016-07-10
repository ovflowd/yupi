using Yupi.Model.Domain.Users;

namespace Yupi.Model.Domain.Messenger
{
	/// <summary>
	///     Class MessengerRequest.
	/// </summary>
	public class MessengerRequest
	{
		public virtual int Id { get; set; }

		public virtual Habbo To { get; set; }

		public virtual Habbo From { get; set; }
	}
}