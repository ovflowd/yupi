using System.Collections.Generic;
using System.Linq;
using Yupi.Data.Base.Adapters.Interfaces;
using Yupi.Game.GameClients.Interfaces;
using Yupi.Game.Rooms;
using Yupi.Game.Rooms.User;
using Yupi.Game.Users.Data.Models;
using Yupi.Messages;
using Yupi.Messages.Parsers;

namespace Yupi.Game.Users.Inventory.Components
{
    /// <summary>
    ///     Class AvatarEffectComponent.
    /// </summary>
    internal class AvatarEffectComponent
    {
        /// <summary>
        ///     The _user identifier
        /// </summary>
        private readonly uint _userId;

        /// <summary>
        ///     The _effects
        /// </summary>
        private List<AvatarEffect> _effects;

        /// <summary>
        ///     The _session
        /// </summary>
        private GameClient _session;

        /// <summary>
        ///     The current effect
        /// </summary>
        internal int CurrentEffect;

        /// <summary>
        ///     Initializes a new instance of the <see cref="AvatarEffectComponent" /> class.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="client">The client.</param>
        /// <param name="data">The data.</param>
        internal AvatarEffectComponent(uint userId, GameClient client, UserData data)
        {
            _userId = userId;
            _session = client;
            _effects = new List<AvatarEffect>();

            foreach (AvatarEffect current in data.Effects)
            {
                if (!current.HasExpired)
                    _effects.Add(current);

                if (!current.HasExpired)
                    return;

                using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                    commitableQueryReactor.RunFastQuery("DELETE FROM users_effects WHERE user_id = " + userId +
                                                        " AND effect_id = " + current.EffectId + "; ");
            }
        }

        /// <summary>
        ///     Gets the packet.
        /// </summary>
        /// <returns>ServerMessage.</returns>
        internal ServerMessage GetPacket()
        {
            ServerMessage serverMessage = new ServerMessage(LibraryParser.OutgoingRequest("EffectsInventoryMessageComposer"));

            serverMessage.AppendInteger(_effects.Count);

            foreach (AvatarEffect current in _effects)
            {
                serverMessage.AppendInteger(current.EffectId);
                serverMessage.AppendInteger(current.Type);
                serverMessage.AppendInteger(current.TotalDuration);
                serverMessage.AppendInteger(0);
                serverMessage.AppendInteger(current.TimeLeft);
                serverMessage.AppendBool(current.TotalDuration == -1);
            }

            return serverMessage;
        }

        /// <summary>
        ///     Adds the new effect.
        /// </summary>
        /// <param name="effectId">The effect identifier.</param>
        /// <param name="duration">The duration.</param>
        /// <param name="type">The type.</param>
        internal void AddNewEffect(int effectId, int duration, short type)
        {
            using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                commitableQueryReactor.RunFastQuery(
                    string.Concat(
                        "INSERT INTO users_effects (user_id,effect_id,total_duration,is_activated,activated_stamp) VALUES (",
                        _userId, ",", effectId, ",", duration, ",'0',0)"));

            _effects.Add(new AvatarEffect(effectId, duration, false, 0.0, type));

            GetClient()
                .GetMessageHandler()
                .GetResponse()
                .Init(LibraryParser.OutgoingRequest("AddEffectToInventoryMessageComposer"));

            GetClient().GetMessageHandler().GetResponse().AppendInteger(effectId);
            GetClient().GetMessageHandler().GetResponse().AppendInteger(type);
            GetClient().GetMessageHandler().GetResponse().AppendInteger(duration);
            GetClient().GetMessageHandler().GetResponse().AppendBool(duration == -1);

            GetClient().GetMessageHandler().SendResponse();
        }

        /// <summary>
        ///     Determines whether the specified effect identifier has effect.
        /// </summary>
        /// <param name="effectId">The effect identifier.</param>
        /// <returns><c>true</c> if the specified effect identifier has effect; otherwise, <c>false</c>.</returns>
        internal bool HasEffect(int effectId) => effectId < 1 || _effects.Any(x => x.EffectId == effectId);

        /// <summary>
        ///     Activates the effect.
        /// </summary>
        /// <param name="effectId">The effect identifier.</param>
        internal void ActivateEffect(int effectId)
        {
            if (!_session.GetHabbo().InRoom)
                return;

            if (!HasEffect(effectId))
                return;

            if (effectId < 1)
                ActivateCustomEffect(effectId);

            if (effectId < 1)
                return;

            AvatarEffect avatarEffect = _effects.Last(x => x.EffectId == effectId);

            avatarEffect.Activate();

            using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                commitableQueryReactor.RunFastQuery(
                    string.Concat("UPDATE users_effects SET is_activated = '1', activated_stamp = ",
                        Yupi.GetUnixTimeStamp(), " WHERE user_id = ", _userId, " AND effect_id = ", effectId));

            EnableInRoom(effectId);
        }

        /// <summary>
        ///     Activates the custom effect.
        /// </summary>
        /// <param name="effectId">The effect identifier.</param>
        /// <param name="setAsCurrentEffect">if set to <c>true</c> [set as current effect].</param>
        internal void ActivateCustomEffect(int effectId, bool setAsCurrentEffect = true)
        {
            EnableInRoom(effectId, setAsCurrentEffect);
        }

        /// <summary>
        ///     Called when [room exit].
        /// </summary>
        internal void OnRoomExit()
        {
            CurrentEffect = 0;
        }

        /// <summary>
        ///     Checks the expired.
        /// </summary>
        internal void CheckExpired()
        {
            if (!_effects.Any())
                return;

            List<AvatarEffect> list = _effects.Where(current => current.HasExpired).ToList();

            foreach (AvatarEffect current2 in list)
                StopEffect(current2.EffectId);

            list.Clear();
        }

        /// <summary>
        ///     Stops the effect.
        /// </summary>
        /// <param name="effectId">The effect identifier.</param>
        internal void StopEffect(int effectId)
        {
            List<AvatarEffect> avatarEffect = _effects.Where(x => x.EffectId == effectId).ToList();

            if (!avatarEffect.Any())
                return;

            AvatarEffect effect = avatarEffect.Last();

            if (effect == null || !effect.HasExpired)
                return;

            using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                commitableQueryReactor.RunFastQuery(string.Concat("DELETE FROM users_effects WHERE user_id = ", _userId,
                    " AND effect_id = ", effectId, " AND is_activated = 1"));

            _effects.Remove(effect);

            GetClient()
                .GetMessageHandler()
                .GetResponse()
                .Init(LibraryParser.OutgoingRequest("StopAvatarEffectMessageComposer"));

            GetClient().GetMessageHandler().GetResponse().AppendInteger(effectId);
            GetClient().GetMessageHandler().SendResponse();

            if (CurrentEffect >= 0)
                ActivateCustomEffect(-1);
        }

        /// <summary>
        ///     Disposes this instance.
        /// </summary>
        internal void Dispose()
        {
            _effects.Clear();
            _effects = null;
            _session = null;
        }

        /// <summary>
        ///     Gets the client.
        /// </summary>
        /// <returns>GameClient.</returns>
        internal GameClient GetClient() => _session;

        /// <summary>
        ///     Enables the in room.
        /// </summary>
        /// <param name="effectId">The effect identifier.</param>
        /// <param name="setAsCurrentEffect">if set to <c>true</c> [set as current effect].</param>
        private void EnableInRoom(int effectId, bool setAsCurrentEffect = true)
        {
            Room userRoom = GetUserRoom();

            RoomUser roomUserByHabbo = userRoom?.GetRoomUserManager().GetRoomUserByHabbo(GetClient().GetHabbo().Id);

            if (roomUserByHabbo == null)
                return;

            if (setAsCurrentEffect)
                CurrentEffect = effectId;

            ServerMessage serverMessage = new ServerMessage(LibraryParser.OutgoingRequest("ApplyEffectMessageComposer"));

            serverMessage.AppendInteger(roomUserByHabbo.VirtualId);
            serverMessage.AppendInteger(effectId);
            serverMessage.AppendInteger(0);

            userRoom.SendMessage(serverMessage);
        }

        /// <summary>
        ///     Gets the user room.
        /// </summary>
        /// <returns>Room.</returns>
        private Room GetUserRoom() => _session.GetHabbo().CurrentRoom;
    }
}