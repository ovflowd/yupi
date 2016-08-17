namespace Yupi.Emulator.Game.Users.Figures
{
    /// <summary>
    ///     Struct AvatarFigureParts
    /// </summary>
     public struct AvatarFigureParts
    {
        /// <summary>
        ///     The part
        /// </summary>
     public string Part;

        /// <summary>
        ///     The part identifier
        /// </summary>
     public string PartId;

        /// <summary>
        ///     The gender
        /// </summary>
     public string Gender;

        /// <summary>
        ///     The colorable
        /// </summary>
     public string Colorable;

        /// <summary>
        ///     Initializes a new instance of the <see cref="AvatarFigureParts" /> struct.
        /// </summary>
        /// <param name="part"></param>
        /// <param name="partId">The part.</param>
        /// <param name="gender">The part identifier.</param>
        /// <param name="colorable"></param>
        public AvatarFigureParts(string part, string partId, string gender, string colorable)
        {
            Part = part;
            PartId = partId;
            Gender = gender;
            Colorable = colorable;
        }
    }
}