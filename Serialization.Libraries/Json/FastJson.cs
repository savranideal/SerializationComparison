using System;
using System.Collections.Generic;
using System.Text;

namespace Serialization.Libraries.Json
{
    public static partial class JsonSerialize
    {
       public class FastJson
        {

            public static string Serialize<T>(T data, fastJSON.JSONParameters options = null)
            { 
                return fastJSON.JSON.ToJSON(data, options?? fastJSON.JSON.Parameters);
            }

             
             
            public static T Deserialize<T>(string data, fastJSON.JSONParameters options = null)
            {
                return fastJSON.JSON.ToObject<T>(data, options ?? fastJSON.JSON.Parameters);

            }
        }
    }
}
