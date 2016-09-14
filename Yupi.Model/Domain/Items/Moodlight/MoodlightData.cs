// ---------------------------------------------------------------------------------
// <copyright file="MoodlightData.cs" company="https://github.com/sant0ro/Yupi">
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
namespace Yupi.Model.Domain
{
    using System.Collections.Generic;
    using System.Data;
    using System.Text;

    public class MoodlightData
    {
        #region Properties

        public virtual MoodlightPreset CurrentPreset
        {
            get; set;
        }

        public virtual bool Enabled
        {
            get; set;
        }

        public virtual int Id
        {
            get; protected set;
        }

        public virtual IList<MoodlightPreset> Presets
        {
            get; protected set;
        }

        #endregion Properties

        #region Constructors

        public MoodlightData()
        {
            Presets = new List<MoodlightPreset>();
            Enabled = false;
            CurrentPreset = new MoodlightPreset();
        }

        #endregion Constructors

        #region Methods

        public virtual string GenerateExtraData()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(Enabled ? 2 : 1); // TODO Enum?
            stringBuilder.Append(",");
            stringBuilder.Append(CurrentPreset);
            stringBuilder.Append(",");
            stringBuilder.Append(CurrentPreset.BackgroundOnly ? 2 : 1);
            stringBuilder.Append(",");
            stringBuilder.Append(CurrentPreset.ColorCode);
            stringBuilder.Append(",");
            stringBuilder.Append(CurrentPreset.ColorIntensity);
            return stringBuilder.ToString();
        }

        #endregion Methods
    }
}