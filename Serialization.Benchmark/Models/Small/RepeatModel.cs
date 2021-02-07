
namespace Serialization.Benchmark.Models
{
    public class RepeatModel
    {
        public int id { get; set; }
        public string jsonrpc { get; set; }
        public int total { get; set; }
        public Result[] result { get; set; }
    }

    public class Result
    {
        public int id { get; set; }
        public string name { get; set; }
    }
}
