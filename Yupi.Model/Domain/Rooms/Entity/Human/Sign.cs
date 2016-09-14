using System;
using Headspring;

namespace Yupi.Model.Domain
{
    public class Sign : Enumeration<Sign>, IStatusString
    {
        public static readonly Sign Zero = new Sign(0, "Zero");
        public static readonly Sign One = new Sign(1, "One");
        public static readonly Sign Two = new Sign(2, "Two");
        public static readonly Sign Three = new Sign(3, "Three");
        public static readonly Sign Four = new Sign(4, "Four");
        public static readonly Sign Five = new Sign(5, "Five");
        public static readonly Sign Six = new Sign(6, "Six");
        public static readonly Sign Seven = new Sign(7, "Seven");
        public static readonly Sign Eight = new Sign(8, "Eight");
        public static readonly Sign Nine = new Sign(9, "Nine");
        public static readonly Sign Ten = new Sign(10, "Ten");
        public static readonly Sign Heart = new Sign(11, "Heart");
        public static readonly Sign Skull = new Sign(12, "Skull");
        public static readonly Sign Exclamation_Mark = new Sign(13, "Exclamation_Mark");
        public static readonly Sign Soccer_Ball = new Sign(14, "Soccer_Ball");
        public static readonly Sign Smiley = new Sign(15, "Smiley");
        public static readonly Sign Soccer_Red_Card = new Sign(16, "Soccer_Red_Card");
        public static readonly Sign Soccer_Yellow_Card = new Sign(17, "Soccer_Yellow_Card");

        protected Sign(int value, string displayName) : base(value, displayName)
        {
        }

        public string ToStatusString()
        {
            return "sign " + this.Value;
        }
    }
}