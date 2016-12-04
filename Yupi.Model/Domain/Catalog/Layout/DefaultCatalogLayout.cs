#region Header

// ---------------------------------------------------------------------------------
// <copyright file="DefaultCatalogLayout.cs" company="https://github.com/sant0ro/Yupi">
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

    
    public abstract class DefaultCatalogLayout : DefaultImageLayout
    {
        #region Properties

        [Required]
        public virtual TString Description
        {
            get; set;
        } = string.Empty;

        [Required]
        public virtual TString HeaderDescription
        {
            get; set;
        } = string.Empty;

        [Ignore]
        public override string[] Images
        {
            get {
                return new string[] { HeaderImage, TeaserImage1, SpecialImage, TeaserImage2, TeaserImage3 };
            }
        }

        [Required]
        public virtual TString SpecialText
        {
            get; set;
        } = string.Empty;

        [Required]
        public virtual TString Text1
        {
            get; set;
        } = string.Empty;

        [Required]
        public virtual TString Text2 {
            get; set;
        } = string.Empty;

        [Ignore]
        public override TString[] Texts
        {
            get {
                return new TString[] { HeaderDescription, Description, SpecialText, Text1, Text2 };
            }
        }

        #endregion Properties
    }
}