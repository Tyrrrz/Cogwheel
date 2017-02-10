namespace Tyrrrz.Settings.Services
{
    /// <summary>
    /// Handles serialization and deserialization of classes
    /// </summary>
    public interface ISerializationService
    {
        /// <summary>
        /// Serializes an object
        /// </summary>
        byte[] Serialize(object obj);

        /// <summary>
        /// Deserializes an object
        /// </summary>
        T Deserialize<T>(byte[] data);

        /// <summary>
        /// Populates an existing instance
        /// </summary>
        void Populate(byte[] data, object obj);
    }
}