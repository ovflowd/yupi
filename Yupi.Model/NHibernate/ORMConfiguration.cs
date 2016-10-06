// ---------------------------------------------------------------------------------
// <copyright file="ORMConfiguration.cs" company="https://github.com/sant0ro/Yupi">
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

    using FluentNHibernate.Automapping;
    using FluentNHibernate.Data;
    using FluentNHibernate.Utils;

    public class ORMConfiguration : DefaultAutomappingConfiguration
    {
        #region Methods

        public override bool IsComponent(Type type)
        {
            return type != null && type.Namespace.EndsWith("Components") || type == typeof(Vector3UserType);
        }

        public override bool IsDiscriminated(Type type)
        {
            return !Attribute.IsDefined (type, typeof (TablePerClassAttribute));
        }

        public override bool ShouldMap(Type type)
        {
            return base.ShouldMap(type) && type.Namespace.StartsWith("Yupi.Model.Domain")
                   && !type.IsEnum // Don't map enums. These will be mapped automatically where required.
                   && !EnumTypeConvention.IsEnumerationType(type)
                   && !Attribute.IsDefined(type, typeof(IgnoreAttribute));
        }

        public override bool ShouldMap(FluentNHibernate.Member member)
        {
            return base.ShouldMap(member)
                   && !Attribute.IsDefined(member.MemberInfo, typeof(IgnoreAttribute));
        }

        #endregion Methods
    }
}