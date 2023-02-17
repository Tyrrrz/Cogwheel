using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Cogwheel.Serialization
{
    internal sealed class ContractResolver : DefaultContractResolver
    {
        public static ContractResolver Instance { get; } = new ContractResolver();

        private static bool IsIgnored(Type declaringType, string propertyName)
        {
            var prop = declaringType.GetProperty(propertyName);
            if (prop == null) return false;
            return prop.GetCustomAttributes(typeof(IgnoreAttribute), false).Any();
        }

        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            return base.CreateProperties(type, memberSerialization)
                // Not ignored
                .Where(p => !IsIgnored(type, p.UnderlyingName))
                .ToList();
        }
    }
}