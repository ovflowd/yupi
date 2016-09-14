namespace Yupi.Model.Domain.Components
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Yupi.Util;

    public class UserEffectComponent
    {
        #region Constructors

        public UserEffectComponent()
        {
            Effects = new List<AvatarEffect>();
        }

        #endregion Constructors

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

        #region Methods

        // TODO Call this somewhere :D
        public virtual void Cleanup()
        {
            Effects.RemoveAll((x) => x.HasExpired());
        }

        // TODO Magic constant
        public virtual bool HasEffect(int effectId)
        {
            return effectId < 1 || Effects.Any(x => x.EffectId == effectId);
        }

        #endregion Methods
    }
}