using System.Text;
using Newtonsoft.Json;

namespace Tyrrrz.Settings.Services
{
    /// <summary>
    /// Performs serialization using Json.Net
    /// </summary>
    public class JsonNetSerializationService : ISerializationService
    {
        private static readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
            DefaultValueHandling = DefaultValueHandling.Include,
            ObjectCreationHandling = ObjectCreationHandling.Replace,
            ContractResolver = CustomJsonNetContractResolver.Instance
        };

        /// <summary>
        /// Default instance
        /// </summary>
        public static JsonNetSerializationService Instance { get; } = new JsonNetSerializationService();

        /// <inheritdoc />
        public virtual byte[] Serialize(object obj)
        {
            return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(obj, SerializerSettings));
        }

        /// <inheritdoc />
        public virtual T Deserialize<T>(byte[] data)
        {
            return JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(data), SerializerSettings);
        }

        /// <inheritdoc />
        public virtual void Populate(byte[] data, object obj)
        {
            JsonConvert.PopulateObject(Encoding.UTF8.GetString(data), obj, SerializerSettings);
        }
    }
}