using FluentNHibernate.Data;
using Yupi.Model.Users;

namespace Yupi.Model.Messenger
{
    /// <summary>
    ///     Class MessengerRequest.
    /// </summary>
     public class MessengerRequest : Entity
    {
		public virtual Habbo To { get; set; }
		public virtual Habbo From { get; set; }
    }
}