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
using System.Text;

namespace Serialization.Benchmark.Benchmarks
{

    public class SerializeList : BenchmarkCollectionBase
    {
        public IEnumerable<HotelInformation> Data { get; set; }

        [GlobalSetup]
        public void Setup()
        {
            var f = new Fixture();
            f.Register(() => new City() { Id = f.Create<int>(), Label = f.Create<string>(), });
            f.Register(() => new Coordinate() { Latitude = f.Create<float>(), Longitude = f.Create<float>(), });
            f.Register(() => new Airport() { Id = f.Create<int>(), Label = f.Create<string>(), });
            f.Register(() => new Country() { Id = f.Create<int>(), Label = f.Create<string>(), });
            Data= f.CreateMany<HotelInformation>(DataCount);
        }
        [Benchmark]
        [BenchmarkCategory("Json_List")]
        public string Newtonsoft()
        {
            return JsonSerialize.Newtonsoft.Serialize(Data);

        }
        [Benchmark]
        [BenchmarkCategory("Json_List")]
        public byte[] UTF8Json()
        {
            return JsonSerialize.Utf8Json.Serialize(Data);

        }
        [Benchmark]
        [BenchmarkCategory("Json_List")]
        public string FastJson()
        {
            return JsonSerialize.FastJson.Serialize(Data);

        }
        [Benchmark]
        [BenchmarkCategory("Json_List")]
        public string ServiceStackText()
        {
            return JsonSerialize.ServiceStackText.Serialize(Data);
        }
        [Benchmark]
        [BenchmarkCategory("Json_List")]
        public byte[] SimdJsonSharp()
        {
            return JsonSerialize.SimdJsonSharp.Serialize(Data);
        }

        [Benchmark]
        [BenchmarkCategory("Json_List")]
        public string Swifter()
        {
            return JsonSerialize.Swifter.Serialize(Data);
        }

        [Benchmark]
        [BenchmarkCategory("Json_List")]
        public string SystemTextJson()
        {
            return JsonSerialize.SystemTextJson.Serialize(Data);
        }

        [Benchmark]
        [BenchmarkCategory("Json_List")]
        public string Jil()
        {
            return JsonSerialize.JIL.Serialize(Data);
        }

    }


}
