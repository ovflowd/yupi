// ---------------------------------------------------------------------------------
// <copyright file="LandingLoadWidgetMessageEvent.cs" company="https://github.com/sant0ro/Yupi">
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
namespace Yupi.Messages.Landing
{
    using System;

    public class LandingLoadWidgetMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            string text = request.GetString();

            /*
             * TODO Implement properly
             * Called, when landing.view.dynamic.slot.[1-6].widget=widgetcontainer
             * 
             * text has value of landing.view.dynamic.slot.[1-6].conf
             */
           

            router.GetComposer<LandingWidgetMessageComposer>().Compose(session, text);

            /*
             * TODO
             * Available containers are:
             * 
             * (Resulting in different messages from client)
             * 
             * public static const §_-32O§:String = "avatarimage";
      
      public static const §_-5cj§:String = "expiringcatalogpage";
      
      public static const §_-6Vs§:String = "expiringcatalogpagesmall";
      
      public static const §_-4yT§:String = "communitygoal";
      
      public static const §_-3tJ§:String = "communitygoalvsmode";
      
      public static const §_-4Rs§:String = "communitygoalvsmodevote";
      
      public static const §_-3Xy§:String = "catalogpromo";
      
      public static const §_-4VK§:String = "catalogpromosmall";
      
      public static const §_-44O§:String = "achievementcompetition_hall_of_fame";
      
      public static const §_-6SM§:String = "achievementcompetition_prizes";
      
      public static const §_-5C2§:String = "dailyquest";
      
      public static const §_-3bo§:String = "nextlimitedrarecountdown";
      
      public static const §_-4c1§:String = "habbomoderationpromo";
      
      public static const §_-5Fe§:String = "habbotalentspromo";
      
      public static const §_-5Yp§:String = "habbowaypromo";
      
      public static const §_-0Eo§:String = "fastfoodgamepromo";
      
      public static const §_-6Bh§:String = "roomhoppernetwork";
      
      public static const §_-2B8§:String = "safetyquizpromo";
      
      public static const §_-M6§:String = "generic";
      
      public static const §_-1Ci§:String = "widgetcontainer";
      
      public static const §_-46j§:String = "promoarticle";
      
      public static const §_-5mm§:String = "bonusrare";
             */
        }

        #endregion Methods
    }
}