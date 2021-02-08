using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using Serialization.Benchmark.Models;
using Serialization.Benchmark.Models.Hotel;
using Serialization.Libraries.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serialization.Benchmark.Benchmarks
{
    [Config(typeof(Config))]
    public class DeserializeLarge : BenchmarkCollectionBase
    {
        private class Config : ManualConfig
        {
            public Config()
            {
                
                AddColumn(new TagColumn("Format", name => "Json Deserialize"));
                AddColumn(new TagColumn("Object Size", name => "Collection"));
            }
        }
        public IDictionary<string, DeserializeFile> Data { get; set; }

        [GlobalSetup]
        public void Setup()
        {
            Data = new Dictionary<string, DeserializeFile>();
            string jsonExamples = Environment.GetEnvironmentVariable("Data");
            string[] files = Directory.GetFiles(jsonExamples, $"*_{DataCount}.json", SearchOption.AllDirectories).ToArray();

            foreach (var file in files)
            {
                var bytes = File.ReadAllText(file);
                var filename = Path.GetFileName(file);
                Data[filename.Split("_")[1]] = new DeserializeFile
                {
                    Data = bytes,
                    FileName = filename,
                    Size = Math.Round(bytes.Length / 1000.0, 2).ToString("N") + " Kb"
                };
            }
        }
        [Benchmark]
        [BenchmarkCategory("Json_Large")]
        public List<SmallHotelChain> Newtonsoft()
        {
            return JsonSerialize.Newtonsoft.Deserialize<List<SmallHotelChain>>(Data["Newtonsoft"].Data);

        }
        [Benchmark]
        [BenchmarkCategory("Json_Large")]
        public List<SmallHotelChain> UTF8Json()
        {
            return JsonSerialize.Utf8Json.Deserialize<List<SmallHotelChain>>(Data["Utf8Json"].Data);

        }
        [Benchmark]
        [BenchmarkCategory("Json_Large")]
        public IList<SmallHotelChain> FastJson()
        {
            return JsonSerialize.FastJson.Deserialize<List<SmallHotelChain>>(Data["FastJson"].Data);

        }
        [Benchmark]
        [BenchmarkCategory("Json_Large")]
        public List<SmallHotelChain> ServiceStackText()
        {
            return JsonSerialize.ServiceStackText.Deserialize<List<SmallHotelChain>>(Data["ServiceStackText"].Data);
        }


        [Benchmark]
        [BenchmarkCategory("Json_Large")]
        public List<SmallHotelChain> Swifter()
        {
            return JsonSerialize.Swifter.Deserialize<List<SmallHotelChain>>(Data["Swifter"].Data);
        }

        [Benchmark]
        [BenchmarkCategory("Json_Large")]
        public List<SmallHotelChain> SystemTextJson()
        {
            return JsonSerialize.SystemTextJson.Deserialize<List<SmallHotelChain>>(Data["SystemTextJson"].Data);
        }

        [Benchmark]
        [BenchmarkCategory("Json_Large")]
        public List<SmallHotelChain> JIL()
        {
            return JsonSerialize.JIL.Deserialize<List<SmallHotelChain>>(Data["JIL"].Data);
        }
    }

}
