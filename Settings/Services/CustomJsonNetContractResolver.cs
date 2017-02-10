using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Tyrrrz.Settings.Services
{
    internal sealed class CustomJsonNetContractResolver : DefaultContractResolver
    {
        public static CustomJsonNetContractResolver Instance { get; } = new CustomJsonNetContractResolver();

        private static bool IsIgnored(Type declaringType, string propertyName)
        {
            var prop = declaringType.GetProperty(propertyName);
            if (prop == null) return false;
            return prop.GetCustomAttributes(typeof(IgnorePropertyAttribute), false).Any();
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