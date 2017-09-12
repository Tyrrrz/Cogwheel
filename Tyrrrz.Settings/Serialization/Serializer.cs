using System.Text;
using Newtonsoft.Json;
namespace Tyrrrz.Settings.Serialization
{
    /// <summary>
    /// Performs serialization and deserialization
    /// </summary>
    public static class Serializer
    {
        private static readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
            DefaultValueHandling = DefaultValueHandling.Include,
            ObjectCreationHandling = ObjectCreationHandling.Replace,
            ContractResolver = ContractResolver.Instance
        };

        /// <summary>
        /// Serialize object
        /// </summary>
        public static byte[] Serialize(object obj)
        {
            return Encoding.Unicode.GetBytes(JsonConvert.SerializeObject(obj, SerializerSettings));
        }

        /// <summary>
        /// Deserialize object
        /// </summary>
        public static T Deserialize<T>(byte[] data)
        {
            return JsonConvert.DeserializeObject<T>(Encoding.Unicode.GetString(data), SerializerSettings);
        }

        /// <summary>
        /// Populate an existing object with serialized data
        /// </summary>
        public static void Populate(byte[] data, object obj)
        {
            JsonConvert.PopulateObject(Encoding.Unicode.GetString(data), obj, SerializerSettings);
        }
    }
}