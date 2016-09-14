namespace Yupi.Model.Domain
{
    using System;

    using Headspring;

    public class ChatType : Enumeration<ChatType>
    {
        #region Fields

        public static readonly ChatType FreeFlowMode = new ChatType(0, "FreeFlowMode");
        public static readonly ChatType LineByLine = new ChatType(1, "LineByLine");

        #endregion Fields

        #region Constructors

        private ChatType(int value, string displayName)
            : base(value, displayName)
        {
        }

        #endregion Constructors
    }
}