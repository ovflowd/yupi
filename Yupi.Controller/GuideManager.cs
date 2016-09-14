using System;
using Yupi.Model.Domain;
using System.Collections.Generic;
using Yupi.Util;

namespace Yupi.Controller
{
    public class GuideManager
    {
        public IList<Habbo> Guides;
        public IList<Habbo> Helpers;
        public IList<Habbo> Guardians;

        public GuideManager()
        {
            Guides = new List<Habbo>();
            Helpers = new List<Habbo>();
            Guardians = new List<Habbo>();
        }

        public Habbo GetRandomGuide()
        {
            // TODO Should this also look into Helpers/Guardians? Also we should make sure the selected Guide isn't busy already!
            return Guides.Random();
        }

        public void Add(Habbo user)
        {
            // TODO Implement Helpers & Guardians
            Guides.Add(user);
        }

        public void Remove(Habbo user)
        {
            // TODO Implement Helpers & Guardians
            Guides.Remove(user);
        }
    }
}