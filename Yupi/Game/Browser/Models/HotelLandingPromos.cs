/**
     Because i love chocolat...                                      
                                    88 88  
                                    "" 88  
                                       88  
8b       d8 88       88 8b,dPPYba,  88 88  
`8b     d8' 88       88 88P'    "8a 88 88  
 `8b   d8'  88       88 88       d8 88 ""  
  `8b,d8'   "8a,   ,a88 88b,   ,a8" 88 aa  
    Y88'     `"YbbdP'Y8 88`YbbdP"'  88 88  
    d8'                 88                 
   d8'                  88     
   
   Private Habbo Hotel Emulating System
   @author Claudio A. Santoro W.
   @author Kessiler R.
   @version dev-beta
   @license MIT
   @copyright Sulake Corporation Oy
   @observation All Rights of Habbo, Habbo Hotel, and all Habbo contents and it's names, is copyright from Sulake
   Corporation Oy. Yupi! has nothing linked with Sulake. 
   This Emulator is Only for DEVELOPMENT uses. If you're selling this you're violating Sulakes Copyright.
*/

using Yupi.Messages;

namespace Yupi.Game.Browser.Models
{
    /// <summary>
    ///     Class SmallPromo.
    /// </summary>
    public class HotelLandingPromos
    {
        /// <summary>
        ///     The body
        /// </summary>
        private readonly string _body;

        /// <summary>
        ///     The button
        /// </summary>
        private readonly string _button;

        /// <summary>
        ///     The header
        /// </summary>
        private readonly string _header;

        /// <summary>
        ///     The image
        /// </summary>
        private readonly string _image;

        /// <summary>
        ///     The index
        /// </summary>
        private readonly int _index;

        /// <summary>
        ///     The in game promo
        /// </summary>
        private readonly int _inGamePromo;

        /// <summary>
        ///     The special action
        /// </summary>
        private readonly string _specialAction;

        /// <summary>
        ///     Initializes a new instance of the <see cref="HotelLandingPromos" /> class.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="header">The header.</param>
        /// <param name="body">The body.</param>
        /// <param name="button">The button.</param>
        /// <param name="inGame">The in game.</param>
        /// <param name="specialAction">The special action.</param>
        /// <param name="image">The image.</param>
        public HotelLandingPromos(int index, string header, string body, string button, int inGame, string specialAction,
            string image)
        {
            _index = index;
            _header = header;
            _body = body;
            _button = button;
            _inGamePromo = inGame;
            _specialAction = specialAction;
            _image = image;
        }

        /// <summary>
        ///     Serializes the specified composer.
        /// </summary>
        /// <param name="composer">The composer.</param>
        /// <returns>ServerMessage.</returns>
        internal ServerMessage Serialize(ServerMessage composer)
        {
            composer.AppendInteger(_index);
            composer.AppendString(_header);
            composer.AppendString(_body);
            composer.AppendString(_button);
            composer.AppendInteger(_inGamePromo);
            composer.AppendString(_specialAction);
            composer.AppendString(_image);

            return composer;
        }
    }
}