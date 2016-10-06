#region Header

// ---------------------------------------------------------------------------------
// <copyright file="CatalogPage.cs" company="https://github.com/sant0ro/Yupi">
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
using Yupi.Util.Settings;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using NHibernate;

#endregion Header

namespace Yupi.Model.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;

    [Serializable]
    public class CatalogPage : IPopulate
    {
        #region Properties

        public virtual TString Caption
        {
            get;
            set;
        }

        public virtual IList<CatalogPage> Children
        {
            get;
            protected set;
        }

        public virtual int Icon
        {
            get;
            set;
        }

        public virtual int Id
        {
            get;
            protected set;
        }

        public virtual bool IsRoot
        {
            get;
            set;
        }

        public virtual CatalogPageLayout Layout
        {
            get;
            set;
        }

        public virtual int MinRank
        {
            get;
            set;
        }

        public virtual IList<CatalogOffer> Offers
        {
            get;
            protected set;
        }

        
        public virtual CatalogOffer SelectedOffer
        {
            get;
            set;
        }

        public virtual CatalogType Type
        {
            get;
            set;
        }

        public virtual bool Visible
        {
            get;
            set;
        }

        #endregion Properties

        #region Constructors

        public CatalogPage(TString caption,
                           int icon,
                           int minRank,
                           CatalogPageLayout layout) : this()
        {
            this.Caption = caption;
            this.Icon = icon;
            this.Layout = layout;
            this.MinRank = minRank;
        }

        public CatalogPage()
        {
            this.Offers = new List<CatalogOffer>();
            this.Children = new List<CatalogPage>();
            this.Visible = true;
            this.Type = CatalogType.Normal;
            this.Layout = new Default3x3Layout();
        }

        #endregion Constructors

        #region Methods

        public virtual void Add(CatalogPage child)
        {
            this.Children.Add(child);
        }


        public virtual void Populate()
        {
            ISessionFactory sessionFactory = DependencyFactory.Resolve<ISessionFactory>();

            string catalogFile = Path.Combine(Settings.AssemblyDir, "Import", "catalog.bin");
           
            CatalogPage root;

            using (Stream stream = File.Open(catalogFile, FileMode.Open))
            {
                BinaryFormatter bin = new BinaryFormatter();
                root = (CatalogPage)bin.Deserialize(stream);
            }

            using (ISession session = sessionFactory.OpenSession())
            {
                ITransaction tx = session.BeginTransaction();

                if (session.QueryOver<CatalogPage>().RowCount() == 0)
                {
                    session.Save(root);
                }

                tx.Commit();
            }
        }

        #endregion Methods
    }
}