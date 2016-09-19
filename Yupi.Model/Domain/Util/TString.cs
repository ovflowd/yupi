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
    using System.Runtime.Serialization;

    [DataContract]
    public class TString : ICloneable
    {
        #region Fields

        private static readonly CultureInfo DefaultLang = CultureInfo.GetCultureInfo("en");

        #endregion Fields

        #region Properties

        public virtual int Id
        {
            get; protected set;
        }

        /// <summary>
        /// The dictionary mapping languages to localized strings.
        /// </summary>
        /// <value>The localized strings.</value>
        [DataMember]
        public virtual IDictionary<string, string> Translations
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
            this.Set(value);
        }

        /// <summary>
        /// Creates an empty string.
        /// </summary>
        public TString()
        {
            this.Translations = new Dictionary<string, string>();
            this.Set(string.Empty);
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
            copy.Translations = new Dictionary<string, string>(this.Translations);
            return copy;
        }

        /// <summary>
        /// Get the english value
        /// </summary>
        public virtual string Get()
        {
            return this.Get(DefaultLang);
        }

        /// <summary>
        /// Get the string of the specified language
        /// </summary>
        /// <param name="culture">The language to get the string for.</param>
        public virtual string Get(CultureInfo culture)
        {
            string localized;

            if (this.Translations.TryGetValue(culture.TwoLetterISOLanguageName, out localized))
            {
                return localized;
            }
            else
            {
                // English is forced to exist!
                return this.Translations[DefaultLang.TwoLetterISOLanguageName];
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
            if (culture.Equals(DefaultLang))
            {
                throw new InvalidOperationException("Deleting the english value is forbidden.");
            }

            if (this.Translations.ContainsKey(culture.TwoLetterISOLanguageName))
            {
                this.Translations.Remove(culture.TwoLetterISOLanguageName);
            }

            return this;
        }

        /// <summary>
        /// Set the english value
        /// </summary>
        /// <param name="value">English value</param>
        public virtual TString Set(string value)
        {
            this.Set(DefaultLang, value);
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
                throw new ArgumentNullException("value");
            }

            if (this.Translations.ContainsKey(culture.TwoLetterISOLanguageName))
            {
                this.Translations[culture.TwoLetterISOLanguageName] = value;
            }
            else
            {
                this.Translations.Add(culture.TwoLetterISOLanguageName, value);
            }

            return this;
        }

        public override string ToString()
        {
            return this.Get();
        }

        #endregion Methods
    }
}