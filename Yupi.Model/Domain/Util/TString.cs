#region Header

// ---------------------------------------------------------------------------------
// <copyright file="TString.cs" company="https://github.com/sant0ro/Yupi">
//   Copyright (c) 2016 Claudio Santoro, TheDoctor
// </copyright>
// <license>
//   Permission is hereby granted, free of charge, to any person obtaining a copy
//   of this software and associated documentation files (the "Software"), to deal
//   in the Software without restriction, including without limitation the rights
//   to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//   copies of the Software, and to permit persons to whom the Software is
//   furnished to do so, subject to the following conditions:
//
//   The above copyright notice and this permission notice shall be included in
//   all copies or substantial portions of the Software.
//
//   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//   IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//   FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//   AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//   LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//   OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//   THE SOFTWARE.
// </license>
// ---------------------------------------------------------------------------------

#endregion Header

namespace Yupi.Model.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.Serialization;

    
    public class TString : ICloneable
    {
        #region Properties

        public virtual int Id
        {
            get; protected set;
        }

        /// <summary>
        /// The dictionary mapping languages to localized strings.
        /// </summary>
        /// <value>The localized strings.</value>
        public virtual ISet<Translation> Translations
        {
            get; protected set;
        }

        [Required]
        public virtual string Value
        {
            get; protected set;
        }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Creates a new TString Instance.
        /// </summary>
        /// <param name="value">The english value</param>
        public TString(string value)
            : this()
        {
            this.Value = value;
        }

        /// <summary>
        /// Creates an empty string.
        /// </summary>
        protected TString()
        {
            this.Translations = new HashSet<Translation>(new TranslationComparer());
        }

        #endregion Constructors

        #region Methods

        public static implicit operator TString(string value)
        {
            return new TString(value);
        }

        public virtual object Clone()
        {
            TString copy = new TString();
            copy.Translations = new HashSet<Translation>(this.Translations, new TranslationComparer ());
            return copy;
        }

        /// <summary>
        /// Get the english value
        /// </summary>
        public virtual string Get()
        {
            return this.Value;
        }

        /// <summary>
        /// Get the string of the specified language
        /// </summary>
        /// <param name="culture">The language to get the string for.</param>
        public virtual string Get(CultureInfo culture)
        {
            Translation translation = this.Translations.FirstOrDefault (x => x.Language == culture);

            if (translation == null) {
                return this.Value;
            } else {
                return translation.Value;
            }
        }

        public override int GetHashCode()
        {
            return this.Translations.GetHashCode();
        }

        /// <summary>
        /// Remove the string of the specified culture.
        /// </summary>
        /// <remarks>English can't be deleted!</remarks>
        /// <param name="culture">The culture to remove.</param>
        public virtual TString Remove(CultureInfo culture)
        {
            Translation translation = this.Translations.FirstOrDefault (x => x.Language == culture);

            if (translation != null)
            {
                this.Translations.Remove(translation);
            }

            return this;
        }

        /// <summary>
        /// Set the value for the specified language
        /// </summary>
        /// <param name="culture">The language</param>
        /// <param name="value">The localized string</param>
        public virtual TString Set(CultureInfo culture, string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            this.Translations.Add (new Translation (culture, value));
            return this;
        }

        public override string ToString()
        {
            return this.Get();
        }

        #endregion Methods

        #region Nested Types

        private class TranslationComparer : IEqualityComparer<Translation>
        {
            #region Methods

            public bool Equals(Translation x, Translation y)
            {
                return x.Language == y.Language;
            }

            public int GetHashCode(Translation obj)
            {
                return obj.Language.GetHashCode ();
            }

            #endregion Methods
        }

        #endregion Nested Types
    }
}