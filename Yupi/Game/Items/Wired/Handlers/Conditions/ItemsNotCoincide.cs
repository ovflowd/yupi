using System;
using System.Collections.Generic;
using System.Linq;
using Yupi.Data;
using Yupi.Game.Items.Interactions.Enums;
using Yupi.Game.Items.Interfaces;
using Yupi.Game.Items.Wired.Interfaces;
using Yupi.Game.Rooms;

namespace Yupi.Game.Items.Wired.Handlers.Conditions
{
    internal class ItemsNotCoincide : IWiredItem
    {
        public ItemsNotCoincide(RoomItem item, Room room)
        {
            Item = item;
            Room = room;
            Items = new List<RoomItem>();
            OtherString = string.Empty;
            OtherExtraString = string.Empty;
            OtherExtraString2 = string.Empty;
        }

        public Interaction Type => Interaction.ConditionItemsDontMatch;

        public RoomItem Item { get; set; }

        public Room Room { get; set; }

        public List<RoomItem> Items { get; set; }

        public string OtherString { get; set; }

        public string OtherExtraString { get; set; }

        public string OtherExtraString2 { get; set; }

        public bool OtherBool
        {
            get { return true; }
            set { }
        }

        public int Delay
        {
            get { return 0; }
            set { }
        }

        public bool Execute(params object[] stuff)
        {
            if (!Items.Any())
                return true;

            bool useExtradata, useRot, usePos;

            Dictionary<uint, string[]> itemsOriginalData;

            try
            {
                if (string.IsNullOrWhiteSpace(OtherString) || !OtherString.Contains(",") ||
                    !OtherExtraString.Contains("|"))
                    return false;

                string[] booleans = OtherString.ToLower().Split(',');

                useExtradata = booleans[0] == "true";
                useRot = booleans[1] == "true";
                usePos = booleans[2] == "true";

                itemsOriginalData =
                    OtherExtraString.Split('/')
                        .Select(data => data.Split('|'))
                        .ToDictionary(array => uint.Parse(array[0]), array => array.Skip(1).ToArray());
            }
            catch (Exception e)
            {
                ServerLogManager.LogException(e.ToString());

                return false;
            }

            foreach (RoomItem current in Items)
            {
                if (current == null || !itemsOriginalData.ContainsKey(current.Id))
                    return false;

                string[] originalData = itemsOriginalData[current.Id];

                if (useRot)
                    if (current.Rot != int.Parse(originalData[1]))
                        return true;

                if (useExtradata)
                {
                    if (current.ExtraData == string.Empty)
                        current.ExtraData = "0";

                    if (current.ExtraData != (originalData[0] == string.Empty ? "0" : originalData[0]))
                        return true;
                }

                if (!usePos)
                    continue;

                string[] originalPos = originalData[2].Split(',');

                if ((current.X != int.Parse(originalPos[0])) && (current.Y != int.Parse(originalPos[1])))
                    return true;
            }

            return false;
        }
    }
}