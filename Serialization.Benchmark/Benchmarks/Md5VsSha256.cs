using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Exporters.Csv;
using BenchmarkDotNet.Order;
using System;
using System.Security.Cryptography;

namespace Serialization.Benchmark.Benchmarks
{

    [ShortRunJob]
    [MediumRunJob]
    [KeepBenchmarkFiles]

    [AsciiDocExporter]
    [CsvExporter]
    [CsvMeasurementsExporter]
    [HtmlExporter]
    [PlainExporter]
    [RPlotExporter]
    [JsonExporterAttribute.Brief]
    [JsonExporterAttribute.BriefCompressed]
    [JsonExporterAttribute.Full]
    [JsonExporterAttribute.FullCompressed]
    [MarkdownExporterAttribute.Default]
    [MarkdownExporterAttribute.GitHub]
    [MarkdownExporterAttribute.StackOverflow]
    [MarkdownExporterAttribute.Atlassian]
    [XmlExporterAttribute.Brief]
    [XmlExporterAttribute.BriefCompressed]
    [XmlExporterAttribute.Full]
    [XmlExporterAttribute.FullCompressed]
    public class Md5VsSha256
    {
        private const int N = 10000;
        private readonly byte[] data;

        private readonly SHA256 sha256 = SHA256.Create();
        private readonly MD5 md5 = MD5.Create();

        public Md5VsSha256()
        {
            data = new byte[N];
            new Random(42).NextBytes(data);
        }

        [Benchmark]
        public byte[] Sha256() => sha256.ComputeHash(data);

        [Benchmark]
        public byte[] Md5() => md5.ComputeHash(data);
    }
     
}
