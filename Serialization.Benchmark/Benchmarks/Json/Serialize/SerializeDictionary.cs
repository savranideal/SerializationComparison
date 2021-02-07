using AutoFixture;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;
using Serialization.Benchmark.Models;
using Serialization.Benchmark.Models.Geography;
using Serialization.Libraries.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Serialization.Benchmark.Benchmarks
{

    public class SerializeDictionary : BenchmarkCollectionBase
    {
        public IDictionary<string,HotelInformation> Data { get; set; }

        [GlobalSetup]
        public void Setup()
        {
            var f = new Fixture();
            f.Register(() => new City() { Id = f.Create<int>(), Label = f.Create<string>(), });
            f.Register(() => new Coordinate() { Latitude = f.Create<float>(), Longitude = f.Create<float>(), });
            f.Register(() => new Airport() { Id = f.Create<int>(), Label = f.Create<string>(), });
            f.Register(() => new Country() { Id = f.Create<int>(), Label = f.Create<string>(), });
            Data= f.CreateMany<HotelInformation>(DataCount).GroupBy(c=>c.Key).ToDictionary(c=>c.Key,c=>c.First());
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
        public byte[] SimdJsonSharp()
        {
            return JsonSerialize.SimdJsonSharp.Serialize(Data);
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
