namespace Yupi.Model.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    public class Translation
    {
        #region Properties

        public virtual int Id
        {
            get; set;
        }

        [Ignore]
        public virtual CultureInfo Language
        {
            get; set;
        }

        [Required]
        public virtual int LanguageId
        {
            get {
                return Language.LCID;
            }
            set {
                Language = CultureInfo.GetCultureInfo (value);
            }
        }

        [Required]
        public virtual string Value
        {
            get; set;
        }

        #endregion Properties

        #region Constructors

        public Translation(CultureInfo language, string value)
        {
            Language = language;
            Value = value;
        }

        protected Translation()
        {
        }

        #endregion Constructors
    }
}