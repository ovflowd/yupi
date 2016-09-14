namespace Yupi.Model.Domain
{
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;

    public class CatalogPage
    {
        #region Constructors

        public CatalogPage()
        {
            Items = new List<CatalogItem>();
            Children = new List<CatalogPage>();
        }

        #endregion Constructors

        #region Properties

        public virtual string Caption
        {
            get; set;
        }

        public virtual List<CatalogPage> Children
        {
            get; protected set;
        }

        public virtual string CodeName
        {
            get; set;
        }

        public virtual bool ComingSoon
        {
            get; set;
        }

        // TODO Isn't enabled the same as visible?
        public virtual bool Enabled
        {
            get; set;
        }

        public virtual Dictionary<uint, uint> FlatOffers
        {
            get; set;
        }

        public virtual int IconImage
        {
            get; set;
        }

        public virtual int Id
        {
            get; protected set;
        }

        public virtual IList<CatalogItem> Items
        {
            get; protected set;
        }

        public virtual string Layout
        {
            get; set;
        }

        public virtual string LayoutHeadline
        {
            get; set;
        }

        public virtual string LayoutSpecial
        {
            get; set;
        }

        public virtual string LayoutTeaser
        {
            get; set;
        }

        public virtual uint MinRank
        {
            get; set;
        }

        public virtual int OrderNum
        {
            get; set;
        }

        public virtual string PageLink
        {
            get; set;
        }

        public virtual string PageLinkTitle
        {
            get; set;
        }

        public virtual CatalogPage Parent
        {
            get; set;
        }

        public virtual string Text1
        {
            get; set;
        }

        public virtual string Text2
        {
            get; set;
        }

        public virtual string TextDetails
        {
            get; set;
        }

        public virtual string TextTeaser
        {
            get; set;
        }

        public virtual bool Visible
        {
            get; set;
        }

        #endregion Properties
    }
}