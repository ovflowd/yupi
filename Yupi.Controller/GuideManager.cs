namespace Yupi.Controller
{
    using System;
    using System.Collections.Generic;

    using Yupi.Model.Domain;
    using Yupi.Util;

    public class GuideManager
    {
        #region Fields

        public IList<Habbo> Guardians;
        public IList<Habbo> Guides;
        public IList<Habbo> Helpers;

        #endregion Fields

        #region Constructors

        public GuideManager()
        {
            Guides = new List<Habbo>();
            Helpers = new List<Habbo>();
            Guardians = new List<Habbo>();
        }

        #endregion Constructors

        #region Methods

        public void Add(Habbo user)
        {
            // TODO Implement Helpers & Guardians
            Guides.Add(user);
        }

        public Habbo GetRandomGuide()
        {
            // TODO Should this also look into Helpers/Guardians? Also we should make sure the selected Guide isn't busy already!
            return Guides.Random();
        }

        public void Remove(Habbo user)
        {
            // TODO Implement Helpers & Guardians
            Guides.Remove(user);
        }

        #endregion Methods
    }
}