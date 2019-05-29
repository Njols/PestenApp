using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PestApp
{
    public static class SessionExtentions
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            JsonSerializerSettings _settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            };
            session.SetString(key, JsonConvert.SerializeObject(value, _settings));
        }

        public static T Get<T>(this ISession session, string key)
        {
            JsonSerializerSettings _settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            };
            var value = session.GetString(key);

            return value== null ? default(T):
                JsonConvert.DeserializeObject<T>(value, _settings);
        }
    }
}
