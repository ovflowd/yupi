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

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Xml;
using Yupi.Core.Io;
using Yupi.Core.Settings;
using Yupi.Data.Structs;

namespace Yupi.Data
{
    /// <summary>
    ///     Class FurnitureDataManager.
    /// </summary>
    internal static class FurnitureDataManager
    {
        /// <summary>
        ///     The floor items
        /// </summary>
        public static Dictionary<string, FurnitureData> FloorItems;

        /// <summary>
        ///     The wall items
        /// </summary>
        public static Dictionary<string, FurnitureData> WallItems;

        /// <summary>
        ///     Sets the cache.
        /// </summary>
        public static void SetCache(bool forceReload = false)
        {
            XmlDocument xmlParser = new XmlDocument();
            WebClient wC = new WebClient();

            try
            {
                string xmlFileContent;
                string cacheDirectory = $"{Yupi.YupiVariablesDirectory}\\Cache";

                Directory.CreateDirectory(cacheDirectory);

                if (File.Exists($"{cacheDirectory}\\FurniDataCache.xml") && !forceReload)
                    xmlFileContent = File.ReadAllText($"{cacheDirectory}\\FurniDataCache.xml");
                else
                    File.WriteAllText($"{cacheDirectory}\\FurniDataCache.xml",
                        xmlFileContent = wC.DownloadString(ServerExtraSettings.FurnitureDataUrl));

                xmlParser.LoadXml(xmlFileContent);

                FloorItems = new Dictionary<string, FurnitureData>();

                XmlNodeList xmlNodeList = xmlParser.DocumentElement?.SelectNodes("/furnidata/roomitemtypes/furnitype");

                if (xmlNodeList != null)
                {
                    foreach (XmlNode node in xmlNodeList)
                    {
                        try
                        {
                            FloorItems.Add(node?.Attributes?["classname"]?.Value,
                                new FurnitureData(int.Parse(node.Attributes["id"].Value),
                                    node.SelectSingleNode("name").InnerText,
                                    ushort.Parse(node.SelectSingleNode("xdim").InnerText),
                                    ushort.Parse(node.SelectSingleNode("ydim").InnerText),
                                    node.SelectSingleNode("cansiton").InnerText == "1",
                                    node.SelectSingleNode("canstandon").InnerText == "1"));
                        }
                        catch (Exception e)
                        {
                            if (!string.IsNullOrEmpty(node?.Attributes?["classname"]?.Value))
                                Console.WriteLine("Errror parsing furnidata by {0} with exception: {1}",
                                    node.Attributes["classname"].Value, e.StackTrace);
                        }
                    }
                }

                WallItems = new Dictionary<string, FurnitureData>();

                foreach (XmlNode node in xmlParser.DocumentElement.SelectNodes("/furnidata/wallitemtypes/furnitype"))
                    WallItems.Add(node.Attributes["classname"].Value,
                        new FurnitureData(int.Parse(node.Attributes["id"].Value),
                            node.SelectSingleNode("name").InnerText));
            }
            catch (WebException e)
            {
                Writer.WriteLine(
                    $"Impossible to reach remote host to download FurniData content. Details: \n {Environment.NewLine + e}",
                    "Yupi.Data", ConsoleColor.Red);
                Writer.WriteLine("Type a key to close");
                Console.ReadKey();
                Environment.Exit(e.HResult);
            }
            catch (XmlException e)
            {
                Writer.WriteLine(
                    $"The XML content of the FurniData is in an invalid XML format, Details: \n {Environment.NewLine + e}",
                    "Yupi.Data",
                    ConsoleColor.Red);
                Writer.WriteLine("Type a key to close");
                Console.ReadKey();
                Environment.Exit(e.HResult);
            }
            catch (NullReferenceException e)
            {
                Writer.WriteLine(
                    $"The content of the FurniData file is empty, impossible to parse, Detials: \n {Environment.NewLine + e}",
                    "Yupi.XML", ConsoleColor.Red);
                Writer.WriteLine("Type a key to close");
                Console.ReadKey();
                Environment.Exit(e.HResult);
            }

            wC.Dispose();
        }

        /// <summary>
        ///     Clears this instance.
        /// </summary>
        public static void Clear()
        {
            FloorItems.Clear();
            WallItems.Clear();
            FloorItems = null;
            WallItems = null;

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}