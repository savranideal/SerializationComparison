using Swifter.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Serialization.Libraries.Json
{
    public static partial class JsonSerialize
    {
       public static class Swifter
        { 
            public static string Serialize<T>(T obj)
            {
               return JsonFormatter.SerializeObject(obj);
            }

            public static T Deserialize<T>(string json)
            {
                return JsonFormatter.DeserializeObject<T>(json);
            }
        }
    }
}
