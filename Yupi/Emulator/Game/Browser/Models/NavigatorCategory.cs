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

using System.Collections.Generic;

namespace Yupi.Emulator.Game.Browser.Models
{
    /// <summary>
    ///     Class Navigator Category.
    /// </summary>
    public class NavigatorCategory
    {
        /// <summary>
        ///     The caption
        /// </summary>
     public string Caption;

        /// <summary>
        ///     The identifier
        /// </summary>
     public int Id;

        /// <summary>
        ///     Default Opened State
        /// </summary>
     public bool IsOpened;

        /// <summary>
        ///     Default Item Size
        /// </summary>
     public bool IsImage;

        /// <summary>
        ///     Sub Categories
        /// </summary>
     public List<NavigatorSubCategory> SubCategories;

        /// <summary>
        ///     Initializes a new instance of the <see cref="NavigatorCategory" /> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="isOpened"></param>
        /// <param name="isImage"></param>
        /// <param name="subCategories"></param>
     public NavigatorCategory(int id, string caption, bool isOpened, bool isImage, List<NavigatorSubCategory> subCategories)
        {
            Id = id;
            Caption = caption;
            IsOpened = isOpened;
            IsImage = isImage;

            SubCategories = new List<NavigatorSubCategory>();

            SubCategories.AddRange(subCategories);
        }
    }
}