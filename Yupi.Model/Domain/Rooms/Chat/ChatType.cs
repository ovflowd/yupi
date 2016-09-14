using System;
using Headspring;

namespace Yupi.Model.Domain
{
    public class ChatType : Enumeration<ChatType>
    {
        public static readonly ChatType FreeFlowMode = new ChatType(0, "FreeFlowMode");
        public static readonly ChatType LineByLine = new ChatType(1, "LineByLine");

        private ChatType(int value, string displayName) : base(value, displayName)
        {
        }
    }
}