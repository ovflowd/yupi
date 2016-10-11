#region Header

// ---------------------------------------------------------------------------------
// <copyright file="FrontpageLayout.cs" company="https://github.com/sant0ro/Yupi">
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

    [Serializable]
    public class FrontpageLayout : CatalogPageLayout
    {
        #region Properties

        public virtual TString Content
        {
            get; set;
        }

        public virtual string HeaderImage
        {
            get; set;
        }

        [Ignore]
        public override string[] Images
        {
            get
            {
                return new string[] { HeaderImage, TeaserImage };
            }
        }

        [Ignore]
        public override string Name
        {
            get
            {
                return "frontpage4";
            }
        }

        public virtual string TeaserImage
        {
            get; set;
        }

        [Ignore]
        public override TString[] Texts
        {
            get
            {
                return new TString[] { Content, VoucherDescription };
            }
        }

        public virtual TString VoucherDescription
        {
            get; set;
        }

        #endregion Properties

        #region Constructors

        public FrontpageLayout()
        {
            this.Content = string.Empty;
            this.VoucherDescription = string.Empty;
            this.TeaserImage = string.Empty;
            this.HeaderImage = string.Empty;
        }

        public FrontpageLayout(TString content, TString voucherDescription)
            : this()
        {
            this.Content = content;
            this.VoucherDescription = voucherDescription;
        }

        #endregion Constructors
    }
}