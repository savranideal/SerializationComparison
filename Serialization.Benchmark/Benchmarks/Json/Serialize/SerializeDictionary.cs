using AutoFixture;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;
using Serialization.Benchmark.Models;
using Serialization.Benchmark.Models.Geography;
using Serialization.Benchmark.Models.Hotel;
using Serialization.Libraries.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Serialization.Benchmark.Benchmarks
{

    public class SerializeDictionary : BenchmarkCollectionBase
    { 
        public IDictionary<string, HotelChain> Data { get; set; }

       
        [GlobalSetup]
        public void Setup()
        {

            var f = new Fixture() { RepeatCount = 1 };
            f.Customizations.Add(new StringGenerator(() => Guid.NewGuid().ToString().Substring(0, 2)));

            Data = f.CreateMany<HotelChain>(DataCount).GroupBy(c => c.Title).ToDictionary(c => c.Key, c => c.First());
        }

        [Benchmark]
        [BenchmarkCategory("Json_Dictionary")]
        public string Newtonsoft()
        {
            return JsonSerialize.Newtonsoft.Serialize(Data);

        }
        [Benchmark]
        [BenchmarkCategory("Json_Dictionary")]
        public byte[] UTF8Json()
        {
            return JsonSerialize.Utf8Json.Serialize(Data);

        }
        [Benchmark]
        [BenchmarkCategory("Json_Dictionary")]
        public string FastJson()
        {
            return JsonSerialize.FastJson.Serialize(Data);

        }
        [Benchmark]
        [BenchmarkCategory("Json_Dictionary")]
        public string ServiceStackText()
        {
            return JsonSerialize.ServiceStackText.Serialize(Data);
        }
       

        [Benchmark]
        [BenchmarkCategory("Json_Dictionary")]
        public string Swifter()
        {
            return JsonSerialize.Swifter.Serialize(Data);
        }

        [Benchmark]
        [BenchmarkCategory("Json_Dictionary")]
        public string SystemTextJson()
        {
            return JsonSerialize.SystemTextJson.Serialize(Data);
        }

        [Benchmark]
        [BenchmarkCategory("Json_Dictionary")]
        public string Jil()
        {
            return JsonSerialize.JIL.Serialize(Data);
        }

    }


}
