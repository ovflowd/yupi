/**
     Because i love chocolat...                                      
                                    88 88  
                                    "" 88  
                                       88  
8b       d8 88       88 8b,dPPYba,  88 88  
`8b     d8' 88       88 88P'    "8a 88 88  
 `8b   d8'  88       88 88       d8 88 ""  
  `8b,d8'   "8a,   ,a88 88b,   ,a8" 88 aa  
    Y88'     `"YbbdP'Y8 88`YbbdP"'  88 88  
    d8'                 88                 
   d8'                  88     
   
   Private Habbo Hotel Emulating System
   @author Claudio A. Santoro W.
   @author Kessiler R.
   @version dev-beta
   @license MIT
   @copyright Sulake Corporation Oy
   @observation All Rights of Habbo, Habbo Hotel, and all Habbo contents and it's names, is copyright from Sulake
   Corporation Oy. Yupi! has nothing linked with Sulake. 
   This Emulator is Only for DEVELOPMENT uses. If you're selling this you're violating Sulakes Copyright.
*/

using Yupi.Emulator.Game.Browser.Enums;
using Yupi.Emulator.Game.Rooms.Data;

namespace Yupi.Emulator.Game.Browser.Models
{
    /// <summary>
    ///     Class PublicItem.
    /// </summary>
    internal class PublicItem
    {
        /// <summary>
        ///     The caption
        /// </summary>
        internal string Caption;

        /// <summary>
        ///     The category identifier
        /// </summary>
        internal int CategoryId;

        /// <summary>
        ///     The description
        /// </summary>
        internal string Description;

        /// <summary>
        ///     The image
        /// </summary>
        internal string Image;

        /// <summary>
        ///     The image type
        /// </summary>
        internal PublicImageType ImageType;

        /// <summary>
        ///     The item type
        /// </summary>
        internal PublicItemType ItemType;

        /// <summary>
        ///     The parent identifier
        /// </summary>
        internal int ParentId;

        /// <summary>
        ///     The recommended
        /// </summary>
        internal bool Recommended;

        /// <summary>
        ///     The room identifier
        /// </summary>
        internal uint RoomId;

        /// <summary>
        ///     The tags to search
        /// </summary>
        internal string TagsToSearch = string.Empty;

        /// <summary>
        ///     The type
        /// </summary>
        internal int Type;

        /// <summary>
        ///     Initializes a new instance of the <see cref="PublicItem" /> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="type">The type.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="desc">The desc.</param>
        /// <param name="image">The image.</param>
        /// <param name="imageType">Type of the image.</param>
        /// <param name="roomId">The room identifier.</param>
        /// <param name="categoryId">The category identifier.</param>
        /// <param name="parentId">The parent identifier.</param>
        /// <param name="recommand">if set to <c>true</c> [recommand].</param>
        /// <param name="typeOfData">The type of data.</param>
        internal PublicItem(uint id, int type, string caption, string desc, string image, PublicImageType imageType, uint roomId, int categoryId, int parentId, bool recommand, int typeOfData)
        {
            Id = id;
            Type = type;
            Caption = caption;
            Description = desc;
            Image = image;
            ImageType = imageType;
            RoomId = roomId;
            CategoryId = categoryId;
            ParentId = parentId;
            Recommended = recommand;

            switch (typeOfData)
            {
                case 1:
                    ItemType = PublicItemType.Tag;
                    break;
                case 2:
                    ItemType = PublicItemType.Flat;
                    break;
                case 3:
                    ItemType = PublicItemType.PublicFlat;
                    break;
                case 4:
                    ItemType = PublicItemType.Category;
                    break;
                default:
                    ItemType = PublicItemType.None;
                    break;
            }
        }

        /// <summary>
        ///     Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        internal uint Id { get; set; }

        /// <summary>
        ///     Gets the room data.
        /// </summary>
        /// <value>The room data.</value>
        /// <exception cref="System.NullReferenceException"></exception>
        internal RoomData GetPublicRoomData => RoomId == 0u ? new RoomData() : Yupi.GetGame().GetRoomManager().GenerateRoomData(RoomId);
    }
}