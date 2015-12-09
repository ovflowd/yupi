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
    /// Class FurnitureDataManager.
    /// </summary>
    internal static class FurnitureDataManager
    {
        /// <summary>
        /// The floor items
        /// </summary>
        public static Dictionary<string, FurnitureData> FloorItems;

        /// <summary>
        /// The wall items
        /// </summary>
        public static Dictionary<string, FurnitureData> WallItems;

        /// <summary>
        /// Sets the cache.
        /// </summary>
        public static void SetCache()
        {
            var xmlParser = new XmlDocument();       
            var wC = new WebClient();

            try
            {
                string xmlFileContent;

                if (File.Exists($"{Environment.CurrentDirectory}\\Cache\\FurniDataCache.xml"))
                    xmlFileContent = File.ReadAllText($"{Environment.CurrentDirectory}\\Cache\\FurniDataCache.xml");
                else
                    File.WriteAllText($"{Environment.CurrentDirectory}\\Cache\\FurniDataCache.xml", xmlFileContent = wC.DownloadString(ServerExtraSettings.FurnitureDataUrl));

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
                                new FurnitureData(int.Parse(node.Attributes["id"].Value), node.SelectSingleNode("name").InnerText,
                                    ushort.Parse(node.SelectSingleNode("xdim").InnerText),
                                    ushort.Parse(node.SelectSingleNode("ydim").InnerText),
                                    node.SelectSingleNode("cansiton").InnerText == "1",
                                    node.SelectSingleNode("canstandon").InnerText == "1"));
                        }
                        catch (Exception e)
                        {
                            if (!string.IsNullOrEmpty(node?.Attributes?["classname"]?.Value))
                                Console.WriteLine("Errror parsing furnidata by {0} with exception: {1}", node.Attributes["classname"].Value, e.StackTrace);
                        }
                    }
                }

                WallItems = new Dictionary<string, FurnitureData>();

                foreach (XmlNode node in xmlParser.DocumentElement.SelectNodes("/furnidata/wallitemtypes/furnitype"))
                    WallItems.Add(node.Attributes["classname"].Value, new FurnitureData(int.Parse(node.Attributes["id"].Value), node.SelectSingleNode("name").InnerText));
            }
            catch (WebException e)
            {
                Writer.WriteLine($"Error downloading furnidata.xml: {Environment.NewLine + e}", "Yupi.XML", ConsoleColor.Red);
                Writer.WriteLine("Type a key to close");
                Console.ReadKey();
                Environment.Exit(e.HResult);
            }
            catch (XmlException e)
            {
                Writer.WriteLine($"Error parsing furnidata.xml: {Environment.NewLine + e}", "Yupi.XML",
                    ConsoleColor.Red);
                Writer.WriteLine("Type a key to close");
                Console.ReadKey();
                Environment.Exit(e.HResult);
            }
            catch (NullReferenceException e)
            {
                Writer.WriteLine($"Error parsing value null of furnidata.xml: {Environment.NewLine + e}", "Yupi.XML", ConsoleColor.Red);
                Writer.WriteLine("Type a key to close");
                Console.ReadKey();
                Environment.Exit(e.HResult);
            }

            wC.Dispose();
        }

        /// <summary>
        /// Clears this instance.
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