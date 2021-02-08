using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using Serialization.Benchmark.Models;
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
            string[] files = Directory.GetFiles(jsonExamples, $"*_{1}.json", SearchOption.AllDirectories).ToArray();

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
        public IEnumerable<HotelInformation> Newtonsoft()
        {
            return JsonSerialize.Newtonsoft.Deserialize<IEnumerable<HotelInformation>>(Data["Newtonsoft"].Data);

        }
        [Benchmark]
        [BenchmarkCategory("Json_Large")]
        public IEnumerable<HotelInformation> UTF8Json()
        {
            return JsonSerialize.Utf8Json.Deserialize<IEnumerable<HotelInformation>>(Data["UTF8Json"].Data);

        }
        [Benchmark]
        [BenchmarkCategory("Json_Large")]
        public IList<HotelInformation> FastJson()
        {
            return JsonSerialize.FastJson.Deserialize<List<HotelInformation>>(Data["FastJson"].Data);

        }
        [Benchmark]
        [BenchmarkCategory("Json_Large")]
        public IEnumerable<HotelInformation> ServiceStackText()
        {
            return JsonSerialize.ServiceStackText.Deserialize<IEnumerable<HotelInformation>>(Data["ServiceStackText"].Data);
        }


        [Benchmark]
        [BenchmarkCategory("Json_Large")]
        public IEnumerable<HotelInformation> Swifter()
        {
            return JsonSerialize.Swifter.Deserialize<IEnumerable<HotelInformation>>(Data["Swifter"].Data);
        }

        [Benchmark]
        [BenchmarkCategory("Json_Large")]
        public IEnumerable<HotelInformation> SystemTextJson()
        {
            return JsonSerialize.SystemTextJson.Deserialize<IEnumerable<HotelInformation>>(Data["SystemTextJson"].Data);
        }

        [Benchmark]
        [BenchmarkCategory("Json_Large")]
        public IEnumerable<HotelInformation> Jil()
        {
            return JsonSerialize.JIL.Deserialize<IEnumerable<HotelInformation>>(Data["JIL"].Data);
        }
    }

}
