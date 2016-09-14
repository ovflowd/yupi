namespace Yupi.Model
{
    public class VectorConvention : IUserTypeConvention
    {
        public void Apply(IPropertyInstance instance)
        {
            instance.CustomType<Vector3UserType>();
        }

        public void Accept(IAcceptanceCriteria<IPropertyInspector> criteria)
        {
            criteria.Expect(x => x.Property.PropertyType == typeof(Vector3));
        }
    }
}