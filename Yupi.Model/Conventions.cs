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