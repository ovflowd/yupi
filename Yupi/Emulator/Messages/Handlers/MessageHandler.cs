using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Game.Rooms;
using Yupi.Emulator.Messages.Buffers;

namespace Yupi.Emulator.Messages.Handlers
{
    internal partial class MessageHandler
    {
        /// <summary>
        ///     The current loading room
        /// </summary>
        internal Room CurrentLoadingRoom;

        /// <summary>
        ///     The request
        /// </summary>
        protected SimpleClientMessageBuffer Request;

        /// <summary>
        ///     The response
        /// </summary>
        protected SimpleServerMessageBuffer Response;

        /// <summary>
        ///     The session
        /// </summary>
        protected GameClient Session;

        /// <summary>
        ///     Initializes a new instance of the <see cref="MessageHandler" /> class.
        /// </summary>
        /// <param name="session">The session.</param>
        internal MessageHandler(GameClient session)
        {
            Session = session;

            Response = new SimpleServerMessageBuffer();
        }

        /// <summary>
        ///     Gets the session.
        /// </summary>
        /// <returns>GameClient.</returns>
        internal GameClient GetSession() => Session;

        /// <summary>
        ///     Gets the response.
        /// </summary>
        /// <returns>SimpleServerMessageBuffer.</returns>
        internal SimpleServerMessageBuffer GetResponse() => Response;

        /// <summary>
        ///     Destroys this instance.
        /// </summary>
        internal void Destroy() => Session = null;

        /// <summary>
        ///     Handles the request.
        /// </summary>
        /// <param name="request">The request.</param>
        internal void HandleRequest(SimpleClientMessageBuffer request)
        {
            Request = request;

            PacketLibraryManager.ReceiveRequest(this, request);
        }

        /// <summary>
        ///     Sends the response.
        /// </summary>
        internal void SendResponse()
        {
            if (Response != null && Response.Id > 0 && Session?.GetConnection() != null)
                Session.GetConnection().Send(Response.GetReversedBytes());
        }
    }
}
