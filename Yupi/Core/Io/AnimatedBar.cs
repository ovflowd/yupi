using System.Collections.Generic;

namespace Yupi.Core.Io
{
    public class AnimatedBar : AbstractBar
    {
        private readonly List<string> _animation;
        private int _counter;

        public AnimatedBar()
        {
            _animation = new List<string> { "/", "-", @"\", "|" };
            _counter = 0;
        }

        /// <summary>
        /// prints the character found in the animation according to the current index
        /// </summary>
        public override void Step()
        {
            System.Console.Write("{0}\b", _animation[_counter]);
            _counter++;

            if (_counter == _animation.Count)
                _counter = 0;
        }
    }
}