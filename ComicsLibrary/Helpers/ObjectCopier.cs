using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace ComicsLibrary.Helpers
{
    public static class ObjectCopier
    {
        public static string NullifyValue(string jsonString, string jsonField)
        {
            dynamic editModel = JObject.Parse(jsonString);

            return editModel.ToString();
        }

        /// <summary>
        /// Perform a deep Copy of the object, using Json as a serialization method. NOTE: Private members are not cloned using this method.
        /// </summary>
        /// <typeparam name="T">The type of object being copied.</typeparam>
        /// <param name="source">The object instance to copy.</param>
        /// <returns>The copied object.</returns>
        public static T CloneJson<T>(this T source, string oldValue, string newValue)
        {
            // Don't serialize a null object, simply return the default for that object
            if (source is null)
            {
                return default;
            }

            // initialize inner objects individually
            // for example in default constructor some list property initialized with some values,
            // but in 'source' these items are cleaned -
            // without ObjectCreationHandling.Replace default constructor values will be added to result
            var deserializeSettings = new JsonSerializerSettings { ObjectCreationHandling = ObjectCreationHandling.Replace };

            var serialized = NullifyValue(JsonConvert.SerializeObject(source), oldValue); //.Replace(oldValue, newValue);

            return JsonConvert.DeserializeObject<T>(serialized, deserializeSettings);
        }
    }
}
