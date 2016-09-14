using System.Collections.Generic;

namespace Yupi.Model.Domain
{
	public class Poll
	{
		public virtual int Id { get; protected set; }

		public virtual string Invitation { get; set; }

		public virtual string PollName { get; set; }

		public virtual string Prize { get; set; }

		public virtual IList<PollQuestion> Questions { get; protected set; }

		public virtual RoomData Room { get; set; }

		public virtual string Thanks { get; set; }

		//public PollType Type{ get; set; }

		public Poll ()
		{
			Questions = new List<PollQuestion> ();
		}
	}
}