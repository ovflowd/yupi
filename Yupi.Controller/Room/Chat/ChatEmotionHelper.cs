using System;
using Yupi.Model;
using System.Collections.Generic;
using System.Linq;

namespace Yupi.Controller
{
	public class ChatEmotionHelper
	{
		private Dictionary<string, ChatEmotion> Emotions;

		public ChatEmotionHelper ()
		{
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
	
		public ChatEmotion GetEmotionForText(string text)
		{
			// Default is ChatEmotion.None (because it has the value 0)
			return Emotions.FirstOrDefault (x => text.Contains(x.Key)).Value;
		}
	}
}

