using System.Collections.Generic;
using System.Linq;
using Yupi.Util;

namespace Yupi.Model.Domain.Components
{
    public class UserEffectComponent
    {
        public UserEffectComponent()
        {
            Effects = new List<AvatarEffect>();
        }

        public virtual IList<AvatarEffect> Effects { get; protected set; }
        public virtual AvatarEffect ActiveEffect { get; set; }

        // TODO Call this somewhere :D
        public virtual void Cleanup()
        {
            Effects.RemoveAll(x => x.HasExpired());
        }

        // TODO Magic constant
        public virtual bool HasEffect(int effectId)
        {
            return (effectId < 1) || Effects.Any(x => x.EffectId == effectId);
        }
    }
}