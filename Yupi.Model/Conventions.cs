// ---------------------------------------------------------------------------------
// <copyright file="Conventions.cs" company="https://github.com/sant0ro/Yupi">
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
namespace Yupi.Model
{
    using System;

    using FluentNHibernate.Conventions;
    using FluentNHibernate.Conventions.AcceptanceCriteria;
    using FluentNHibernate.Conventions.Inspections;
    using FluentNHibernate.Conventions.Instances;

    public class Conventions : IHasManyConvention, IHasManyToManyConvention
    {
        #region Methods

        // TODO Use AttributePropertyConvention (https://github.com/jagregory/fluent-nhibernate/wiki/Available-conventions)
        // Taken from http://stackoverflow.com/questions/6091654/fluentnhibernate-automapping-onetomany-relation-using-attribute-and-convention
        public void Apply(IOneToManyCollectionInstance instance)
        {
            if (instance == null)
            {
                return;
            }

            var keyColumnAttribute =
                (OneToManyAttribute) Attribute.GetCustomAttribute(instance.Member, typeof(OneToManyAttribute));
            if (keyColumnAttribute != null)
            {
                instance.Key.Column(instance.EntityType.Name + "Ref");
            }
        }

        public void Apply(IManyToManyCollectionInstance instance)
        {
            if (instance == null)
            {
                return;
            }

            var keyColumnAttribute =
                (ManyToManyAttribute) Attribute.GetCustomAttribute(instance.Member, typeof(ManyToManyAttribute));
            if (keyColumnAttribute != null)
            {
                instance.Key.Column(instance.EntityType.Name + "ManyRef");
            }
        }

        #endregion Methods
    }
}