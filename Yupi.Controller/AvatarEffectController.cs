using System.Collections.Generic;
using System.Linq;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Model;
using Yupi.Messages.Contracts;
using System;


namespace Yupi.Controller
{
    public class AvatarEffectController
    {
        private IRepository<UserInfo> UserRepository;

        public AvatarEffectController()
        {
            UserRepository = DependencyFactory.Resolve<IRepository<UserInfo>>();
        }

        // TODO Validate effectIDs !!!
        public void AddNewEffect(Habbo user, int effectId, int duration, short type)
        {
            AvatarEffect effect = new AvatarEffect()
            {
                EffectId = effectId,
                TotalDuration = duration,
                Type = type
            };

            user.Info.EffectComponent.Effects.Add(effect);

            UserRepository.Save(user.Info);

            user.Router.GetComposer<AddEffectToInventoryMessageComposer>().Compose(user, effect);
        }

        public void ActivateEffect(UserEntity user, int effectId)
        {
            if (user.Room == null || !user.UserInfo.EffectComponent.HasEffect(effectId) || effectId < 1)
                return;

            if (user.UserInfo.EffectComponent.ActiveEffect != null)
            {
                StopEffect(user.User, user.UserInfo.EffectComponent.ActiveEffect);
            }

            AvatarEffect avatarEffect = user.UserInfo.EffectComponent.Effects.Last(x => x.EffectId == effectId);
            avatarEffect.Activate();
            user.UserInfo.EffectComponent.ActiveEffect = avatarEffect;

            UserRepository.Save(user.UserInfo);

            EnableInRoom(user, avatarEffect);
        }

        // TODO Remove
        public void ActivateCustomEffect(int effectId, bool setAsCurrentEffect = true)
        {
            throw new NotImplementedException();
        }

        public void CheckExpired(Habbo user)
        {
            var expiredEffects = user.Info.EffectComponent.Effects.Where(current => current.HasExpired());

            foreach (AvatarEffect effect in expiredEffects)
            {
                StopEffect(user, effect);
            }
        }

        public void StopEffect(Habbo user, AvatarEffect effect)
        {
            if (effect == null)
                return;

            if (effect.HasExpired())
            {
                user.Info.EffectComponent.Effects.Remove(effect);
                UserRepository.Save(user.Info);
            }

            user.Router.GetComposer<StopAvatarEffectMessageComposer>().Compose(user, effect);
        }

        private void EnableInRoom(UserEntity entity, AvatarEffect effect, bool setAsCurrentEffect = true)
        {
            Room userRoom = entity.Room;

            if (setAsCurrentEffect)
            {
                entity.UserInfo.EffectComponent.ActiveEffect = effect;
            }

            userRoom.EachUser(
                (session) =>
                {
                    session.Router.GetComposer<ApplyEffectMessageComposer>()
                        .Compose(session, entity, effect);
                }
            );
        }
    }
}