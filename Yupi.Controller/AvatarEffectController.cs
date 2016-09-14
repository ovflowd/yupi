// ---------------------------------------------------------------------------------
// <copyright file="AvatarEffectController.cs" company="https://github.com/sant0ro/Yupi">
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
namespace Yupi.Controller
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Yupi.Messages.Contracts;
    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Model.Repository;

    public class AvatarEffectController
    {
        #region Fields

        private IRepository<UserInfo> UserRepository;

        #endregion Fields

        #region Constructors

        public AvatarEffectController()
        {
            UserRepository = DependencyFactory.Resolve<IRepository<UserInfo>>();
        }

        #endregion Constructors

        #region Methods

        // TODO Remove
        public void ActivateCustomEffect(int effectId, bool setAsCurrentEffect = true)
        {
            throw new NotImplementedException();
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

        #endregion Methods
    }
}