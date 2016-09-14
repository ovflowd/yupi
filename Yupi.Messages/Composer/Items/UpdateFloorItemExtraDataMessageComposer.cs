namespace Yupi.Messages.Items
{
    using System;
    using System.Collections.Generic;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public class UpdateFloorItemExtraDataMessageComposer : Yupi.Messages.Contracts.UpdateFloorItemExtraDataMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender room, FloorItem item)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendString(item.Id.ToString());
                /*
                switch (item.GetBaseItem().InteractionType)
                {
                case Interaction.MysteryBox:
                    {
                        if (item.ExtraData.Contains('\u0005'.ToString()))
                        {
                            string[] mysteryBoxData = item.ExtraData.Split('\u0005');

                            int num = int.Parse(mysteryBoxData[0]);
                            int num2 = int.Parse(mysteryBoxData[1]);

                            item.ExtraData = (3 * num - num2).ToString();
                        }

                        break;
                    }
                case Interaction.Mannequin:
                    {
                        message.AppendInteger(1);
                        message.AppendInteger(3);

                        if (item.ExtraData.Contains('\u0005'.ToString()))
                        {
                            string[] mannequinData = item.ExtraData.Split('\u0005');

                            message.AppendString("GENDER");
                            message.AppendString(mannequinData[0]);
                            message.AppendString("FIGURE");
                            message.AppendString(mannequinData[1]);
                            message.AppendString("OUTFIT_NAME");
                            message.AppendString(mannequinData[2]);

                            break;
                        }

                        message.AppendString("GENDER");
                        message.AppendString(string.Empty);
                        message.AppendString("FIGURE");
                        message.AppendString(string.Empty);
                        message.AppendString("OUTFIT_NAME");
                        message.AppendString(string.Empty);

                        break;
                    }
                case Interaction.Pinata:
                    {
                        message.AppendInteger(7);

                        if (item.ExtraData.Length <= 0)
                        {
                            message.AppendString("6");
                            message.AppendInteger(0);
                            message.AppendInteger(100);

                            break;
                        }

                        message.AppendString(int.Parse(item.ExtraData) == 100 ? "8" : "6");
                        message.AppendInteger(int.Parse(item.ExtraData));
                        message.AppendInteger(100);

                        break;
                    }
                case Interaction.WiredHighscore:
                    {
                        if (item.HighscoreData == null)
                            item.HighscoreData = new HighscoreData(item);

                        message.AppendInteger(6);
                        message.AppendString(item.ExtraData); //Ouvert/fermé

                        if (item.GetBaseItem().Name.StartsWith("highscore_classic"))
                            message.AppendInteger(2);
                        else if (item.GetBaseItem().Name.StartsWith("highscore_mostwin"))
                            message.AppendInteger(1);
                        else if (item.GetBaseItem().Name.StartsWith("highscore_perteam"))
                            message.AppendInteger(0);

                        message.AppendInteger(0); //Time : ["alltime", "daily", "weekly", "monthly"]
                        message.AppendInteger(item.HighscoreData.Lines.Count); //Count

                        foreach (KeyValuePair<int, HighScoreLine> line in item.HighscoreData.Lines)
                        {
                            message.AppendInteger(line.Value.Score);
                            message.AppendInteger(1);
                            message.AppendString(line.Value.Name);
                        }

                        break;
                    }
                case Interaction.CrackableEgg:
                    {
                        CrackableEggHandler handler = Yupi.GetGame().GetCrackableEggHandler();

                        int cracks = 0;
                        int cracksMax = handler.MaxCracks(item.GetBaseItem().Name);

                        if (Yupi.IsNum(item.ExtraData))
                            cracks = Convert.ToInt16(item.ExtraData);

                        string state = "0";

                        if (cracks >= cracksMax)
                            state = "14";
                        else if (cracks >= cracksMax*6/7)
                            state = "12";
                        else if (cracks >= cracksMax*5/7)
                            state = "10";
                        else if (cracks >= cracksMax*4/7)
                            state = "8";
                        else if (cracks >= cracksMax*3/7)
                            state = "6";
                        else if (cracks >= cracksMax*2/7)
                            state = "4";
                        else if (cracks >= cracksMax*1/7)
                            state = "2";

                        message.AppendInteger(7);
                        message.AppendString(state); //state (0-7)
                        message.AppendInteger(cracks); //actual
                        message.AppendInteger(cracksMax); //max

                        break;
                    }
                case Interaction.YoutubeTv:
                    {
                        message.AppendInteger(1);
                        message.AppendInteger(1);
                        message.AppendString("THUMBNAIL_URL");
                        message.AppendString(item.ExtraData);

                        break;
                    }
                default:
                    {
                        message.AppendInteger(0);
                        message.AppendString(item.ExtraData);

                        break;
                    }
                }
            */
                throw new NotImplementedException();
                room.Send(message);
            }
        }

        #endregion Methods
    }
}