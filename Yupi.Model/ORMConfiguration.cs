using System;

namespace Yupi.Model
{
    public class ORMConfiguration : DefaultAutomappingConfiguration
    {
        public override bool ShouldMap(Type type)
        {
            return base.ShouldMap(type) && type.Namespace.StartsWith("Yupi.Model.Domain")
                   && !type.IsEnum // Don't map enums. These will be mapped automatically where required.
                   && !EnumTypeConvention.IsEnumerationType(type)
                   && !Attribute.IsDefined(type, typeof(IgnoreAttribute));
        }

        public override bool IsComponent(Type type)
        {
            return ((type != null) && type.Namespace.EndsWith("Components")) || (type == typeof(Vector3UserType));
        }

        public override bool IsDiscriminated(Type type)
        {
            var attribute = Attribute.GetCustomAttribute(type, typeof(IsDiscriminatedAttribute));
            return attribute != null;
        }

        public override bool ShouldMap(FluentNHibernate.Member member)
        {
            return base.ShouldMap(member)
                   && !Attribute.IsDefined(member.MemberInfo, typeof(IgnoreAttribute));
        }
    }
}