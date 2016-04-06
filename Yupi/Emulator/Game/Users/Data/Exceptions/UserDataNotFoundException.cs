using System;
using System.Runtime.Serialization;

namespace Yupi.Emulator.Game.Users.Data.Exceptions
{
    /// <summary>
    ///     Class UserDataNotFoundException.
    /// </summary>
	[Serializable]
     public class UserDataNotFoundException : Exception
    {
		/// <summary>
		/// Initializes a new instance of the <see cref="Yupi.Emulator.Game.Users.Data.Exceptions.UserDataNotFoundException"/> class.
		/// </summary>
		public UserDataNotFoundException() : base()
		{
		}

        /// <summary>
        ///     Initializes a new instance of the <see cref="UserDataNotFoundException" /> class.
        /// </summary>
        /// <param name="reason">The reason.</param>
        public UserDataNotFoundException(string reason) : base(reason)
        {
        }

		/// <summary>
		/// Initializes a new instance of the <see cref="Yupi.Emulator.Game.Users.Data.Exceptions.UserDataNotFoundException"/> class.
		/// </summary>
		/// <param name="reason">Reason.</param>
		/// <param name="innerException">Inner exception.</param>
		public UserDataNotFoundException(string reason, Exception innerException) : base(reason, innerException)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Yupi.Emulator.Game.Users.Data.Exceptions.UserDataNotFoundException"/> class.
		/// </summary>
		/// <param name="info">Info.</param>
		/// <param name="context">Context.</param>
		protected UserDataNotFoundException(SerializationInfo info, StreamingContext context) 
			: base(info, context)
		{
		}
    }
}