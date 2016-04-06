using System.Data;
using System.Globalization;
using Yupi.Emulator.Data.Base.Adapters.Interfaces;
using Yupi.Emulator.Game.Groups.Structs;
using Yupi.Emulator.Game.Items.Interactions.Enums;
using Yupi.Emulator.Messages.Buffers;

namespace Yupi.Emulator.Game.Items.Interfaces
{
    /// <summary>
    ///     Class UserItem.
    /// </summary>
     public class UserItem
    {
        /// <summary>
        ///     The base item
        /// </summary>
     public readonly Item BaseItem;

        /// <summary>
        ///     The extra data
        /// </summary>
     public string ExtraData;

        /// <summary>
        ///     The group identifier
        /// </summary>
     public uint GroupId;

        /// <summary>
        ///     The identifier
        /// </summary>
     public uint Id;

        /// <summary>
        ///     The is wall item
        /// </summary>
     public bool IsWallItem;

        /// <summary>
        ///     The limited sell identifier
        /// </summary>
     public uint LimitedSellId, LimitedStack;

        /// <summary>
        ///     The song code
        /// </summary>
     public string SongCode;

        /// <summary>
        ///     Initializes a new instance of the <see cref="UserItem" /> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="baseName">The base item identifier.</param>
        /// <param name="extraData">The extra data.</param>
        /// <param name="group">The group.</param>
        /// <param name="songCode">The song code.</param>
     public UserItem(uint id, string baseName, string extraData, uint group, string songCode)
        {
            Id = id;
            ExtraData = extraData;
            GroupId = group;

            BaseItem = Yupi.GetGame().GetItemManager().GetItemByName(baseName);

            if (BaseItem == null)
                return;

            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                queryReactor.SetQuery($"SELECT * FROM items_limited WHERE item_id={id} LIMIT 1");
                DataRow row = queryReactor.GetRow();

                if (row != null)
                {
                    uint.TryParse(row[1].ToString(), out LimitedSellId);
                    uint.TryParse(row[2].ToString(), out LimitedStack);
                }
            }

            IsWallItem = BaseItem.Type == 'i';
            SongCode = songCode;
        }

        /// <summary>
        ///     Serializes the wall.
        /// </summary>
        /// <param name="message">The messageBuffer.</param>
        /// <param name="inventory">if set to <c>true</c> [inventory].</param>
     public void SerializeWall(SimpleServerMessageBuffer messageBuffer, bool inventory)
        {
            messageBuffer.AppendInteger(Id);
            messageBuffer.AppendString(BaseItem.Type.ToString().ToUpper());
            messageBuffer.AppendInteger(Id);
            messageBuffer.AppendInteger(BaseItem.SpriteId);

            if (BaseItem.Name.Contains("a2") || BaseItem.Name == "floor")
                messageBuffer.AppendInteger(3);
            else if (BaseItem.Name.Contains("wallpaper") && BaseItem.Name != "wildwest_wallpaper")
                messageBuffer.AppendInteger(2);
            else if (BaseItem.Name.Contains("landscape"))
                messageBuffer.AppendInteger(4);
            else
                messageBuffer.AppendInteger(1);

            messageBuffer.AppendInteger(0);
            messageBuffer.AppendString(ExtraData);
            messageBuffer.AppendBool(BaseItem.AllowRecycle);
            messageBuffer.AppendBool(BaseItem.AllowTrade);
            messageBuffer.AppendBool(BaseItem.AllowInventoryStack);
            messageBuffer.AppendBool(false); //SELLABLE_ICON
            messageBuffer.AppendInteger(-1); //secondsToExpiration
            messageBuffer.AppendBool(true); //hasRentPeriodStarted
            messageBuffer.AppendInteger(-1); //flatId
        }

        /// <summary>
        ///     Serializes the floor.
        /// </summary>
        /// <param name="message">The messageBuffer.</param>
        /// <param name="inventory">if set to <c>true</c> [inventory].</param>
     public void SerializeFloor(SimpleServerMessageBuffer messageBuffer, bool inventory)
        {
            messageBuffer.AppendInteger(Id);
            messageBuffer.AppendString(BaseItem.Type.ToString(CultureInfo.InvariantCulture).ToUpper());
            messageBuffer.AppendInteger(Id);
            messageBuffer.AppendInteger(BaseItem.SpriteId);
            int extraParam = 0;

            try
            {
                if (BaseItem.InteractionType == Interaction.Gift)
                {
                    string[] split = ExtraData.Split((char) 9);
                    int ribbon, color;
                    int.TryParse(split[2], out ribbon);
                    int.TryParse(split[3], out color);
                    extraParam = ribbon*1000 + color;
                }
            }
            catch
            {
                extraParam = 1001;
            }

            messageBuffer.AppendInteger(extraParam);

            if (BaseItem.IsGroupItem)
            {
                Group group = Yupi.GetGame().GetGroupManager().GetGroup(GroupId);

                if (group != null)
                {
                    messageBuffer.AppendInteger(2);
                    messageBuffer.AppendInteger(5);
                    messageBuffer.AppendString(ExtraData);
                    messageBuffer.AppendString(group.Id.ToString(CultureInfo.InvariantCulture));
                    messageBuffer.AppendString(group.Badge);
                    messageBuffer.AppendString(Yupi.GetGame().GetGroupManager().GetGroupColour(group.Colour1, true));
                    messageBuffer.AppendString(Yupi.GetGame().GetGroupManager().GetGroupColour(group.Colour2, false));
                }
                else
                {
                    messageBuffer.AppendInteger(0);
                    messageBuffer.AppendString(string.Empty);
                }
            }
            else if (LimitedStack > 0)
            {
                messageBuffer.AppendString(string.Empty);
                messageBuffer.AppendBool(true);
                messageBuffer.AppendBool(false);
                messageBuffer.AppendString(ExtraData);
            }
            else if ((BaseItem.InteractionType == Interaction.Moplaseed) &&
                     (BaseItem.InteractionType == Interaction.RareMoplaSeed))
            {
                messageBuffer.AppendInteger(1);
                messageBuffer.AppendInteger(1);
                messageBuffer.AppendString("rarity");
                messageBuffer.AppendString(ExtraData);
            }
            else
            {
                switch (BaseItem.InteractionType)
                {
                    case Interaction.BadgeDisplay:
                        string[] extra = ExtraData.Split('|');
                        messageBuffer.AppendInteger(2);
                        messageBuffer.AppendInteger(4);
                        messageBuffer.AppendString("0");
                        messageBuffer.AppendString(extra[0]);
                        messageBuffer.AppendString(extra.Length > 1 ? extra[1] : "");
                        messageBuffer.AppendString(extra.Length > 1 ? extra[2] : "");
                        break;

                    case Interaction.YoutubeTv:
                        messageBuffer.AppendInteger(1);
                        messageBuffer.AppendInteger(1);
                        messageBuffer.AppendString("THUMBNAIL_URL");
                        messageBuffer.AppendString(ExtraData);
                        break;

                    case Interaction.Mannequin:
                        messageBuffer.AppendInteger(1);

                        if (!ExtraData.Contains('\u0005'.ToString()))
                        {
                            messageBuffer.AppendInteger(3); // Count Of Values
                            messageBuffer.AppendString("GENDER");
                            messageBuffer.AppendString("M");
                            messageBuffer.AppendString("FIGURE");
                            messageBuffer.AppendString(string.Empty);
                            messageBuffer.AppendString("OUTFIT_NAME");
                            messageBuffer.AppendString(string.Empty);
                        }
                        else
                        {
                            string[] extradatas = ExtraData.Split('\u0005');

                            messageBuffer.AppendInteger(3); // Count Of Values
                            messageBuffer.AppendString("GENDER");
                            messageBuffer.AppendString(extradatas[0]);
                            messageBuffer.AppendString("FIGURE");
                            messageBuffer.AppendString(extradatas[1]);
                            messageBuffer.AppendString("OUTFIT_NAME");
                            messageBuffer.AppendString(extradatas[2]);
                        }
                        break;

                    default:
                        messageBuffer.AppendInteger(0);
                        if (!BaseItem.IsGroupItem)
                            messageBuffer.AppendString(ExtraData);
                        break;
                }
            }

            if (LimitedSellId > 0)
            {
                messageBuffer.AppendInteger(LimitedSellId);
                messageBuffer.AppendInteger(LimitedStack);
            }

            /* messageBuffer.AppendInteger((BaseItem.InteractionType == InteractionType.gift) ? 9 : 0);
                messageBuffer.AppendInteger(0);
                messageBuffer.AppendString((BaseItem.InteractionType == InteractionType.gift)
                    ? string.Empty
                    : ExtraData);*/

            messageBuffer.AppendBool(BaseItem.AllowRecycle);
            messageBuffer.AppendBool(BaseItem.AllowTrade);
            messageBuffer.AppendBool(LimitedSellId <= 0 && BaseItem.AllowInventoryStack);
            messageBuffer.AppendBool(false); // sellable
            messageBuffer.AppendInteger(-1); // expireTime
            messageBuffer.AppendBool(true); // hasRentPeriodStarted
            messageBuffer.AppendInteger(-1); // flatId

            if (BaseItem.Type != 's') return;
            messageBuffer.AppendString(string.Empty); //slotId
            messageBuffer.AppendInteger(0);
        }
    }
}