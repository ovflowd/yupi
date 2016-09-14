using System;
using Headspring;

namespace Yupi.Model.Domain
{
    public class ChatBubbleStyle : Enumeration<ChatBubbleStyle>
    {
        public static readonly ChatBubbleStyle Normal = new ChatBubbleStyle(0, "Normal");
        public static readonly ChatBubbleStyle Generic = new ChatBubbleStyle(1, "Generic", x => false);
        public static readonly ChatBubbleStyle Bot = new ChatBubbleStyle(2, "Bot", x => false);
        public static readonly ChatBubbleStyle FiringMyLazer = new ChatBubbleStyle(3, "FiringMyLazer");
        public static readonly ChatBubbleStyle GothicRose = new ChatBubbleStyle(4, "GothicRose");
        public static readonly ChatBubbleStyle Piglet = new ChatBubbleStyle(5, "Piglet");
        public static readonly ChatBubbleStyle Sausagedog = new ChatBubbleStyle(6, "Sausagedog");
        public static readonly ChatBubbleStyle Dragon = new ChatBubbleStyle(7, "Dragon");
        public static readonly ChatBubbleStyle Hearts = new ChatBubbleStyle(8, "Hearts");
        public static readonly ChatBubbleStyle NormalDarkTurquoise = new ChatBubbleStyle(9, "NormalDarkTurquoise");
        public static readonly ChatBubbleStyle NormalDarkYellow = new ChatBubbleStyle(10, "NormalDarkYellow");
        public static readonly ChatBubbleStyle NormalGreen = new ChatBubbleStyle(11, "NormalGreen");
        public static readonly ChatBubbleStyle NormalPink = new ChatBubbleStyle(12, "NormalPink");
        public static readonly ChatBubbleStyle NormalPurple = new ChatBubbleStyle(13, "NormalPurple");
        public static readonly ChatBubbleStyle NormalSkyBlue = new ChatBubbleStyle(14, "NormalSkyBlue");
        public static readonly ChatBubbleStyle NormalYellow = new ChatBubbleStyle(15, "NormalYellow");
        public static readonly ChatBubbleStyle StickingPlaster = new ChatBubbleStyle(16, "StickingPlaster");
        public static readonly ChatBubbleStyle NormalRed = new ChatBubbleStyle(17, "NormalRed");
        public static readonly ChatBubbleStyle NormalBlue = new ChatBubbleStyle(18, "NormalBlue");
        public static readonly ChatBubbleStyle NormalGrey = new ChatBubbleStyle(19, "NormalGrey");
        public static readonly ChatBubbleStyle FortuneTeller = new ChatBubbleStyle(20, "FortuneTeller");
        public static readonly ChatBubbleStyle ZombieHand = new ChatBubbleStyle(21, "ZombieHand");
        public static readonly ChatBubbleStyle Skeleton = new ChatBubbleStyle(22, "Skeleton");

        public static readonly ChatBubbleStyle Staff = new ChatBubbleStyle(23, "Staff",
            x => x.HasPermission("staff_chat_bubble"));

        public static readonly ChatBubbleStyle Parrot = new ChatBubbleStyle(24, "Parrot");
        public static readonly ChatBubbleStyle Pirate = new ChatBubbleStyle(25, "Pirate");

        private readonly Func<UserInfo, bool> CanUsePredicatement;

        protected ChatBubbleStyle(int value, string displayName, Func<UserInfo, bool> canUse = null)
            : base(value, displayName)
        {
            CanUsePredicatement = canUse;
        }

        public bool CanUse(UserInfo info)
        {
            return (CanUsePredicatement == null) || CanUsePredicatement(info);
        }
    }
}