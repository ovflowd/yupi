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

using System;
using System.Collections.Generic;
using Yupi.Core.Io.Interfaces;

namespace Yupi.Core.Io
{
    public class AnimatedBar : AbstractBar
    {
        private readonly List<string> _animation;
        private int _counter;

        public AnimatedBar()
        {
            _animation = new List<string> {"/", "-", @"\", "|"};
            _counter = 0;
        }

        /// <summary>
        ///     prints the character found in the animation according to the current index
        /// </summary>
        public override void Step()
        {
            Console.Write("{0}\b", _animation[_counter%_animation.Count]);
            _counter++;
        }
    }
}