using AutoMapper;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ComicsStore.MiddleWare.Common
{
    public static class JsonHelper
    {
        public static bool IsNullOrEmpty(this JToken token)
        {
            return (token == null) ||
                   (token.Type == JTokenType.Array && !token.HasValues) ||
                   (token.Type == JTokenType.Object && !token.HasValues) ||
                   (token.Type == JTokenType.String && token.ToString() == String.Empty) ||
                   (token.Type == JTokenType.Null);
        }

        public static IDictionary<string, object> ModifiedData<TInput, TData>(TInput input)
        {
            var jsonInput = JObject.FromObject(input);
            var modifiedFields = jsonInput.Properties().Where(p => !p.Value.IsNullOrEmpty()).Select(x => x.Name).ToList();

            var data = Mapper.Map<TData>(input);

            var jsonData = JObject.FromObject(data);
            return jsonData.Properties()
                .Where(p => modifiedFields.Contains(p.Name))
                .Select(x => new KeyValuePair<string, object>(x.Name, x.Value is JValue ? ((JValue)x.Value).Value : null))
                .ToDictionary(x => x.Key, x => x.Value);
        }
    }
}
