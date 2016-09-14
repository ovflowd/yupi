using System;
using Headspring;

namespace Yupi.Model.Domain
{
    public class Dance : Enumeration<Dance>
    {
        public static readonly Dance None = new Dance(0, false);
        public static readonly Dance Default = new Dance(1, false);
        public static readonly Dance PogoMogo = new Dance(2, true);
        public static readonly Dance DuckFunk = new Dance(3, true);
        public static readonly Dance TheRollie = new Dance(4, true);

        public readonly bool ClubOnly;

        private Dance(int value, bool clubOnly) : base(value, value.ToString())
        {
            this.ClubOnly = clubOnly;
        }
    }
}