﻿namespace Serialization.Benchmark.Models
{ 
    public class GeoLocationRootObject
    {
        public string type { get; set; }
        public Feature[] features { get; set; }
    } 
    public class Feature
    {
        public string type { get; set; }
        public Properties properties { get; set; }
        public Geometry geometry { get; set; }
    }

    public class Properties
    {
        public string cca2 { get; set; }
    }

    public class Geometry
    {
        public string type { get; set; }
        public float[][][] coordinates { get; set; }
    }

}