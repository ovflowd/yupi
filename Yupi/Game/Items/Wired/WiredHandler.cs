using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Yupi.Data;
using Yupi.Data.Base.Adapters.Interfaces;
using Yupi.Game.Items.Interactions;
using Yupi.Game.Items.Interactions.Enums;
using Yupi.Game.Items.Interfaces;
using Yupi.Game.Items.Wired.Handlers.Conditions;
using Yupi.Game.Items.Wired.Handlers.Effects;
using Yupi.Game.Items.Wired.Handlers.Triggers;
using Yupi.Game.Items.Wired.Interfaces;
using Yupi.Game.Rooms;

namespace Yupi.Game.Items.Wired
{
    public class WiredHandler
    {
        private readonly Room _room;
        private readonly List<IWiredItem> _wiredItems;
        private Queue _cycleItems;

        public WiredHandler(Room room)
        {
            _wiredItems = new List<IWiredItem>();
            _room = room;
            _cycleItems = new Queue();
        }

        public static void OnEvent(IWiredItem item)
        {
            if (item.Item.ExtraData == "1")
                return;

            item.Item.ExtraData = "1";
            item.Item.UpdateState(false, true);
            item.Item.ReqUpdate(1, true);
        }

        public IWiredItem LoadWired(IWiredItem fItem)
        {
            if (fItem?.Item == null)
            {
                if (_wiredItems.Contains(fItem))
                    _wiredItems.Remove(fItem);

                return null;
            }

            using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                commitableQueryReactor.SetQuery("SELECT * FROM items_wireds WHERE id=@id LIMIT 1");
                commitableQueryReactor.AddParameter("id", fItem.Item.Id);

                DataRow row = commitableQueryReactor.GetRow();

                if (row == null)
                {
                    IWiredItem wiredItem = GenerateNewItem(fItem.Item);
                    AddWired(wiredItem);
                    SaveWired(wiredItem);

                    return wiredItem;
                }

                fItem.OtherString = row["string"].ToString();
                fItem.OtherBool = row["bool"].ToString() == "1";
                fItem.Delay = (int) row["delay"];
                fItem.OtherExtraString = row["extra_string"].ToString();
                fItem.OtherExtraString2 = row["extra_string_2"].ToString();

                string[] array = row["items"].ToString().Split(';');

                foreach (string s in array)
                {
                    int value;

                    if (!int.TryParse(s, out value))
                        continue;

                    RoomItem item = _room.GetRoomItemHandler().GetItem(Convert.ToUInt32(value));

                    fItem.Items.Add(item);
                }

                AddWired(fItem);
            }

            return fItem;
        }

        public static void SaveWired(IWiredItem fItem)
        {
            if (fItem == null)
                return;

            using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                string text = string.Empty;
                int num = 0;

                foreach (RoomItem current in fItem.Items)
                {
                    if (num != 0) text += ";";
                    text += current.Id;
                    num++;
                }

                if (fItem.OtherString == null)
                    fItem.OtherString = string.Empty;
                if (fItem.OtherExtraString == null)
                    fItem.OtherExtraString = string.Empty;
                if (fItem.OtherExtraString2 == null)
                    fItem.OtherExtraString2 = string.Empty;

                commitableQueryReactor.SetQuery(
                    "REPLACE INTO items_wireds VALUES (@id, @items, @delay, @string, @bool, @extrastring, @extrastring2)");
                commitableQueryReactor.AddParameter("id", fItem.Item.Id);
                commitableQueryReactor.AddParameter("items", text);
                commitableQueryReactor.AddParameter("delay", fItem.Delay);
                commitableQueryReactor.AddParameter("string", fItem.OtherString);
                commitableQueryReactor.AddParameter("bool", Yupi.BoolToEnum(fItem.OtherBool));
                commitableQueryReactor.AddParameter("extrastring", fItem.OtherExtraString);
                commitableQueryReactor.AddParameter("extrastring2", fItem.OtherExtraString2);

                commitableQueryReactor.RunQuery();
            }
        }

        public void ReloadWired(IWiredItem item)
        {
            SaveWired(item);
            RemoveWired(item);
            AddWired(item);
        }

        public void ResetExtraString(Interaction type)
        {
            lock (_wiredItems)
            {
                foreach (IWiredItem current in _wiredItems.Where(current => current != null && current.Type == type))
                    current.OtherExtraString = "0";
            }
        }

        public bool ExecuteWired(Interaction type, params object[] stuff)
        {
            try
            {
                if (!IsTrigger(type) || stuff == null)
                    return false;

                if (type == Interaction.TriggerCollision)
                    foreach (IWiredItem wiredItem in _wiredItems.Where(wiredItem => wiredItem != null && wiredItem.Type == type))
                        wiredItem.Execute(stuff);
                else if (_wiredItems.Any(current => current != null && current.Type == type && current.Execute(stuff)))
                    return true;
            }
            catch
            {
                ServerLogManager.LogException($"Error trying Execute Wired Furniture.");
            }

            return false;
        }

        public void OnCycle()
        {
            try
            {
                Queue queue = new Queue();
                lock (_cycleItems.SyncRoot)
                {
                    while (_cycleItems.Count > 0)
                    {
                        IWiredItem wiredItem = (IWiredItem) _cycleItems.Dequeue();
                        IWiredCycler item = wiredItem as IWiredCycler;

                        if (item == null)
                            continue;

                        IWiredCycler wiredCycler = item;

                        if (!wiredCycler.OnCycle())
                            if (!queue.Contains(item))
                                queue.Enqueue(item);
                    }
                }

                _cycleItems = queue;
            }
            catch (Exception e)
            {
                ServerLogManager.LogException(e, MethodBase.GetCurrentMethod());
            }
        }

        public void EnqueueCycle(IWiredItem item)
        {
            if (!_cycleItems.Contains(item))
                _cycleItems.Enqueue(item);
        }

        public bool IsCycleQueued(IWiredItem item) => _cycleItems.Contains(item);

        public void AddWired(IWiredItem item)
        {
            if (_wiredItems.Contains(item)) _wiredItems.Remove(item);
            _wiredItems.Add(item);
        }

        public void RemoveWired(IWiredItem item)
        {
            if (!_wiredItems.Contains(item))
                _wiredItems.Remove(item);

            _wiredItems.Remove(item);
        }

        public void RemoveWired(RoomItem item)
        {
            foreach (IWiredItem current in _wiredItems)
            {
                if (current?.Item == null || current.Item.Id != item.Id)
                    continue;

                Queue queue = new Queue();

                lock (_cycleItems.SyncRoot)
                {
                    while (_cycleItems.Count > 0)
                    {
                        IWiredItem wiredItem = (IWiredItem) _cycleItems.Dequeue();

                        if (wiredItem.Item.Id != item.Id)
                            queue.Enqueue(wiredItem);
                    }
                }

                _cycleItems = queue;
                _wiredItems.Remove(current);

                break;
            }
        }

        public IWiredItem GenerateNewItem(RoomItem item)
        {
            switch (item.GetBaseItem().InteractionType)
            {
                // Efeitos Antigos
                case Interaction.TriggerTimer:
                    return new TimerTrigger(item, _room);

                case Interaction.TriggerRoomEnter:
                    return new UserEntersRoom(item, _room);

                case Interaction.TriggerGameEnd:
                    return new GameEnds(item, _room);

                case Interaction.TriggerGameStart:
                    return new GameStarts(item, _room);

                case Interaction.TriggerRepeater:
                    return new Repeater(item, _room);

                case Interaction.TriggerLongRepeater:
                    return new LongRepeater(item, _room);

                case Interaction.TriggerOnUserSay:
                    return new SaysKeyword(item, _room);

                case Interaction.TriggerScoreAchieved:
                    return new ScoreAchieved(item, _room);

                case Interaction.TriggerStateChanged:
                    return new FurniStateToggled(item, _room);

                case Interaction.TriggerWalkOnFurni:
                    return new WalksOnFurni(item, _room);

                case Interaction.TriggerWalkOffFurni:
                    return new WalksOffFurni(item, _room);

                case Interaction.TriggerCollision:
                    return new Collision(item, this, _room);

                case Interaction.ActionMoveRotate:
                    return new MoveRotateFurni(item, _room);

                case Interaction.ActionMoveToDir:
                    return new MoveToDir(item, _room);

                case Interaction.ActionShowMessage:
                    return new ShowMessage(item, _room);

                case Interaction.ActionEffectUser:
                    return new EffectUser(item, _room);

                case Interaction.ActionTeleportTo:
                    return new TeleportToFurni(item, _room);

                case Interaction.ActionToggleState:
                    return new ToggleFurniState(item, _room);

                case Interaction.ActionResetTimer:
                    return new ResetTimers(item, _room);

                case Interaction.ActionKickUser:
                    return new KickUser(item, _room);

                case Interaction.ConditionFurnisHaveUsers:
                    return new FurniHasUsers(item, _room);

                // Condições Novas

                case Interaction.ConditionItemsMatches:
                    return new ItemsCoincide(item, _room);

                case Interaction.ConditionFurniTypeMatches:
                    return new ItemsTypeMatches(item, _room);

                case Interaction.ConditionHowManyUsersInRoom:
                    return new HowManyUsers(item, _room);

                case Interaction.ConditionGroupMember:
                    return new IsGroupMember(item, _room);

                case Interaction.ConditionTriggerOnFurni:
                    return new TriggererOnFurni(item, _room);

                case Interaction.ConditionFurniHasFurni:
                    return new FurniHasFurni(item, _room);

                case Interaction.ConditionUserWearingEffect:
                    return new UserIsWearingEffect(item, _room);

                case Interaction.ConditionUserWearingBadge:
                    return new UserIsWearingBadge(item, _room);

                case Interaction.ConditionUserHasFurni:
                    return new UserHasFurni(item, _room);

                case Interaction.ConditionDateRangeActive:
                    return new DateRangeActive(item, _room);

                case Interaction.ConditionTimeMoreThan:
                    return new TimeMoreThan(item, _room);

                case Interaction.ConditionTimeLessThan:
                    return new TimeLessThan(item, _room);

                // Condições Negativas
                case Interaction.ConditionTriggererNotOnFurni:
                    return new TriggererNotOnFurni(item, _room);

                case Interaction.ConditionFurniHasNotFurni:
                    return new FurniHasNotFurni(item, _room);

                case Interaction.ConditionFurnisHaveNotUsers:
                    return new FurniHasNotUsers(item, _room);

                case Interaction.ConditionItemsDontMatch:
                    return new ItemsNotCoincide(item, _room);

                case Interaction.ConditionFurniTypeDontMatch:
                    return new ItemsTypeDontMatch(item, _room);

                case Interaction.ConditionNotGroupMember:
                    return new IsNotGroupMember(item, _room);

                case Interaction.ConditionNegativeHowManyUsers:
                    return new NotHowManyUsersInRoom(item, _room);

                case Interaction.ConditionUserNotWearingEffect:
                    return new UserIsNotWearingEffect(item, _room);

                case Interaction.ConditionUserNotWearingBadge:
                    return new UserIsNotWearingBadge(item, _room);

                // Efeitos Novos
                case Interaction.ActionGiveReward:
                    return new GiveReward(item, _room);

                case Interaction.ActionPosReset:
                    return new ResetPosition(item, _room);

                case Interaction.ActionGiveScore:
                    return new GiveScore(item, _room);

                case Interaction.ActionMuteUser:
                    return new MuteUser(item, _room);

                case Interaction.ActionJoinTeam:
                    return new JoinTeam(item, _room);

                case Interaction.ActionLeaveTeam:
                    return new LeaveTeam(item, _room);

                case Interaction.ActionChase:
                    return new Chase(item, _room);

                case Interaction.ActionCallStacks:
                    return new CallStacks(item, _room);

                // Bots
                case Interaction.TriggerBotReachedStuff:
                    return new BotReachedStuff(item, _room);

                case Interaction.TriggerBotReachedAvatar:
                    return new BotReachedAvatar(item, _room);

                case Interaction.ActionBotClothes:
                    return new BotClothes(item, _room);

                case Interaction.ActionBotFollowAvatar:
                    return new BotFollowAvatar(item, _room);

                case Interaction.ActionBotGiveHanditem:
                    return new BotGiveHanditem(item, _room);

                case Interaction.ActionBotMove:
                    return new BotMove(item, _room);

                case Interaction.ActionBotTalk:
                    return new BotTalk(item, _room);

                case Interaction.ActionBotTalkToAvatar:
                    return new BotTalkToAvatar(item, _room);

                case Interaction.ActionBotTeleport:
                    return new BotTeleport(item, _room);

                case Interaction.ConditionUserHasHanditem:
                    return new UserHasHanditem(item, _room);
            }

            return null;
        }

        public List<IWiredItem> GetConditions(IWiredItem item)
            =>
                _wiredItems.Where(
                    current =>
                        current != null && IsCondition(current.Type) && current.Item.X == item.Item.X &&
                        current.Item.Y == item.Item.Y).ToList();

        public List<IWiredItem> GetEffects(IWiredItem item)
            =>
                _wiredItems.Where(
                    current =>
                        current != null && IsEffect(current.Type) && current.Item.X == item.Item.X &&
                        current.Item.Y == item.Item.Y).ToList();

        public IWiredItem GetWired(RoomItem item)
            => _wiredItems.FirstOrDefault(current => current != null && item.Id == current.Item.Id);

        public List<IWiredItem> GetWiredsByType(Interaction type)
            => _wiredItems.Where(item => item != null && item.Type == type).ToList();

        public List<IWiredItem> GetWiredsByTypes(GlobalInteractions type)
            =>
                _wiredItems.Where(
                    item => item != null && InteractionTypes.AreFamiliar(type, item.Item.GetBaseItem().InteractionType))
                    .ToList();

        public void MoveWired(RoomItem item)
        {
            IWiredItem wired = GetWired(item);

            if (wired == null)
                return;

            wired.Item = item;
            RemoveWired(item);
            AddWired(wired);
        }

        public void Destroy()
        {
            _wiredItems.Clear();
            _cycleItems.Clear();
        }

        private static bool IsTrigger(Interaction type)
            => InteractionTypes.AreFamiliar(GlobalInteractions.WiredTrigger, type);

        private static bool IsEffect(Interaction type)
            => InteractionTypes.AreFamiliar(GlobalInteractions.WiredEffect, type);

        private static bool IsCondition(Interaction type)
            => InteractionTypes.AreFamiliar(GlobalInteractions.WiredCondition, type);
    }
}