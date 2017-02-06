using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Tyrrrz.Settings
{
    internal sealed class CustomContractResolver : DefaultContractResolver
    {
        public static CustomContractResolver Instance { get; } = new CustomContractResolver();

        private static bool IsCustomStruct(Type objectType)
        {
            return
                objectType.IsValueType && !objectType.IsPrimitive && !objectType.IsEnum &&
                !string.IsNullOrWhiteSpace(objectType.Namespace) && !objectType.Namespace.StartsWith("System.");
        }

        private static ObjectConstructor<object> CreateParameterizedConstructor(MethodBase method)
        {
            var c = method as ConstructorInfo;
            if (c != null)
                return a => c.Invoke(a);
            return a => method.Invoke(null, a);
        }

        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            return
                base.CreateProperties(type, memberSerialization)
                    // Not ignored
                    .Where(p => !p.PropertyType.GetCustomAttributes(typeof(IgnorePropertyAttribute), false).Any())
                    // Writable
                    .Where(p => p.Writable)
                    .ToList();
        }

        protected override JsonObjectContract CreateObjectContract(Type objectType)
        {
            var contract = base.CreateObjectContract(objectType);
            if (!IsCustomStruct(objectType)) return contract;

            // For custom-defined constructs, find the most specific constructor instead
            var constructors = objectType.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            var mostSpecific = constructors.OrderByDescending(c => c.GetParameters().Length).FirstOrDefault();
            if (mostSpecific != null)
            {
                contract.OverrideCreator = CreateParameterizedConstructor(mostSpecific);
                contract.CreatorParameters.Clear();
                foreach (var param in CreateConstructorParameters(mostSpecific, contract.Properties))
                    contract.CreatorParameters.Add(param);
            }
            return contract;
        }
    }
}