using Yupi.Core.Security.BlackWords.Enums;

namespace Yupi.Core.Security.BlackWords.Structs
{
    /// <summary>
    /// Struct BlackWord
    /// </summary>
    internal struct BlackWord
    {
        /// <summary>
        /// The word
        /// </summary>
        public string Word;

        /// <summary>
        /// The type
        /// </summary>
        public BlackWordType Type;

        public BlackWordTypeSettings TypeSettings => BlackWordsManager.GetSettings(Type);

        /// <summary>
        /// Initializes a new instance of the <see cref="BlackWord"/> struct.
        /// </summary>
        /// <param name="word">The word.</param>
        /// <param name="type">The type.</param>
        public BlackWord(string word, BlackWordType type)
        {
            Word = word;
            Type = type;
        }
    }
}