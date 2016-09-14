namespace Yupi.Model.Domain
{
    using System.Text.RegularExpressions;

    public class MoodlightPreset
    {
        #region Properties

        public virtual bool BackgroundOnly
        {
            get; set;
        }

        public virtual string ColorCode
        {
            get; set;
        }

        public virtual int ColorIntensity
        {
            get; set;
        }

        public virtual int Id
        {
            get; protected set;
        }

        #endregion Properties
    }
}