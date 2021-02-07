using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Serialization.Libraries.Json
{
    public static partial class JsonSerialize
    {
        public static class SystemTextJson
        {


            public static string Serialize<T>(T objectToSerialize, JsonSerializerOptions jsonSerializerOptions = null)
            {
                return JsonSerializer.Serialize(objectToSerialize, jsonSerializerOptions);
            }

            public static T Deserialize<T>(string json, JsonSerializerOptions jsonSerializerOptions = null)
            {
                return JsonSerializer.Deserialize<T>(json,jsonSerializerOptions);
            }
            public static async Task<T> DeserializeAsync<T>(Stream stream, JsonSerializerOptions jsonSerializerOptions = null)
            {

                return await JsonSerializer.DeserializeAsync<T>(stream, jsonSerializerOptions);
            }

        }
    }
}
