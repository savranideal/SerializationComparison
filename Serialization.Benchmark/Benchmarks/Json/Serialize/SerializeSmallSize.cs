using AutoFixture;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;
using Serialization.Benchmark.Models;
using Serialization.Benchmark.Models.Geography;
using Serialization.Libraries.Json;
using System.Collections.Generic;
using System.Text;

namespace Serialization.Benchmark.Benchmarks
{
    
    public class SerializeSmallSize: BenchmarkBase
    {
        private class Config : ManualConfig
        {
            public Config()
            { 
                AddColumn(new TagColumn("Format", name => "Json"));
                AddColumn(new TagColumn("Object Size", name => "Small"));
            }
        }
        public HotelDescription Data { get; set; } 
        [GlobalSetup]
        public void Setup()
        { 
            Data = new Fixture().Create<HotelDescription>();
        }
        [Benchmark]
        [BenchmarkCategory("Json_Small")]
        public string Newtonsoft()
        {
            return JsonSerialize.Newtonsoft.Serialize(Data);

        }
        [Benchmark]
        [BenchmarkCategory("Json_Small")]
        public byte[] UTF8Json()
        {
            return JsonSerialize.Utf8Json.Serialize(Data);

        }
        [Benchmark]
        [BenchmarkCategory("Json_Small")]
        public string FastJson()
        {
            return JsonSerialize.FastJson.Serialize(Data);

        }
        [Benchmark]
        [BenchmarkCategory("Json_Small")]
        public string ServiceStackText()
        {
            return JsonSerialize.ServiceStackText.Serialize(Data); 
        }
        [Benchmark]
        [BenchmarkCategory("Json_Small")]
        public byte[] SimdJsonSharp()
        {
            return JsonSerialize.SimdJsonSharp.Serialize(Data); 
        }

        [Benchmark]
        [BenchmarkCategory("Json_Small")]
        public string Swifter()
        {
            return JsonSerialize.Swifter.Serialize(Data); 
        }

        [Benchmark]
        [BenchmarkCategory("Json_Small")]
        public string SystemTextJson()
        {
            return JsonSerialize.SystemTextJson.Serialize(Data); 
        }
        [Benchmark]
        [BenchmarkCategory("Json_Small")]
        public string Jil()
        {
            return JsonSerialize.JIL.Serialize(Data); 
        }


    }

     
}
