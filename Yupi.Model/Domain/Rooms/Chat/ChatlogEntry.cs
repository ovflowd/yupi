using System;
using System.Collections.Generic;
using System.Linq;

namespace Yupi.Model.Domain
{
	public class ChatlogEntry
	{
		public virtual int Id { get; set; }
		public virtual UserInfo User { get; set; }
		public virtual DateTime Timestamp { get; protected set; }
		public virtual string Message { get; protected set; }
		public virtual ChatBubbleStyle Bubble { get; set; }
		public virtual IList<Link> Links { get; protected set; }
		public virtual bool Whisper { get; set; }

		private static Dictionary<string, ChatEmotion> Emotions;

		static ChatlogEntry() {
			Emotions = new Dictionary<string, ChatEmotion>
			{
				// Smile
				{":)", ChatEmotion.Smile},
				{";)", ChatEmotion.Smile},
				{":d", ChatEmotion.Smile},
				{";d", ChatEmotion.Smile},
				{":]", ChatEmotion.Smile},
				{";]", ChatEmotion.Smile},
				{"=)", ChatEmotion.Smile},
				{"=]", ChatEmotion.Smile},
				{":-)", ChatEmotion.Smile},

				// Angry
				{">:(", ChatEmotion.Angry},
				{">:[", ChatEmotion.Angry},
				{">;[", ChatEmotion.Angry},
				{">;(", ChatEmotion.Angry},
				{">=(", ChatEmotion.Angry},
				{":@", ChatEmotion.Angry},

				// Shocked
				{":o", ChatEmotion.Shocked},
				{";o", ChatEmotion.Shocked},
				{">;o", ChatEmotion.Shocked},
				{">:o", ChatEmotion.Shocked},
				{"=o", ChatEmotion.Shocked},
				{">=o", ChatEmotion.Shocked},

				// Sad
				{";'(", ChatEmotion.Sad},
				{";[", ChatEmotion.Sad},
				{":[", ChatEmotion.Sad},
				{";(", ChatEmotion.Sad},
				{"=(", ChatEmotion.Sad},
				{"='(", ChatEmotion.Sad},
				{"=[", ChatEmotion.Sad},
				{"='[", ChatEmotion.Sad},
				{":(", ChatEmotion.Sad},
				{":-(", ChatEmotion.Sad}
			};
		}

		public ChatlogEntry (string message) : this()
		{
			Message = message;
		}

		protected ChatlogEntry ()
		{
			Links = new List<Link> ();
			Bubble = ChatBubbleStyle.Normal;
			Timestamp = DateTime.Now;
		}

		public virtual ChatEmotion GetEmotion()
		{
			// TODO Cache
			// Default is ChatEmotion.None (because it has the value 0)
			return Emotions.FirstOrDefault (x => Message.Contains(x.Key)).Value;
		}

		public virtual string FilteredMessage() {
			// TODO Filter
			return Message.Trim ();
		}
	}
}

