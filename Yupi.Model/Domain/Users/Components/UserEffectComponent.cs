// ---------------------------------------------------------------------------------
// <copyright file="UserEffectComponent.cs" company="https://github.com/sant0ro/Yupi">
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
namespace Yupi.Model.Domain.Components
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Yupi.Util;

    public class UserEffectComponent
    {
        #region Properties

        public virtual AvatarEffect ActiveEffect
        {
            get; set;
        }

        public virtual IList<AvatarEffect> Effects
        {
            get; protected set;
        }

        #endregion Properties

        #region Constructors

        public UserEffectComponent()
        {
            Effects = new List<AvatarEffect>();
        }

        #endregion Constructors

        #region Methods

        // TODO Call this somewhere :D
        public virtual void Cleanup()
        {
            Effects.RemoveAll((x) => x.HasExpired());
        }

        public virtual bool HasEffect(int effectId)
        {
            return Effects.Any(x => x.EffectId == effectId);
        }

        #endregion Methods
    }
}