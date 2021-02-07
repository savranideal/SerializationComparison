using System;
using System.Collections.Generic;
using System.Text;

namespace Serialization.Libraries.Json
{
    public static partial class JsonSerialize
    {
       public class SimdJsonSharp
        {

            public static byte[] Serialize<T>(T data)
            {
                return JsonSerialize.Utf8Json.Serialize(data);
            }

             
            public static T Deserialize<T>(string data, fastJSON.JSONParameters options = null)
            {
                return fastJSON.JSON.ToObject<T>(data, options);

            }
        }
    }
}
