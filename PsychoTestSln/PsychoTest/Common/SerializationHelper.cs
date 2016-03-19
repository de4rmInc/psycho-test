using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace PsychoTest.Common
{
    public static class SerializationHelper
    {
        public static void Serialize<T>(Stream stream, T serializationObject) where T : class
        {
            var serializer = new BinaryFormatter();
            serializer.Serialize(stream, serializationObject);
        }

        public static void Serialize<T>(string path, T serializationObject) where T : class
        {
            using (var stream = File.OpenWrite(path))
            {
                Serialize(stream, serializationObject);
            }
        }

        public static T Deserialize<T>(Stream stream) where T : class
        {
            var deserializer = new BinaryFormatter();
            var obj = (T)deserializer.Deserialize(stream);
            return obj;
        }

        public static T Deserialize<T>(string path) where T : class
        {
            T obj;
            using (Stream stream = File.OpenRead(path))
            {
                obj = Deserialize<T>(stream);
            }
            return obj;
        }
    }
}
