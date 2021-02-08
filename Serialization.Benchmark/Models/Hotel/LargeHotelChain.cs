using MessagePack;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Serialization.Benchmark.Models.Hotel
{
    [Serializable, DataContract, ProtoContract, MessagePackObject]
    public class LargeHotelChain
    {
        [DataMember, ProtoMember(1), Key(0)]
        public List<LargeHotel> Hotels  {  get;  set;  }

        [DataMember, ProtoMember(2), Key(1)]
        public string Title { get; set; }

        [DataMember, ProtoMember(3), Key(2)]
        public int Key { get; set; }

        [DataMember, ProtoMember(4), Key(3)]
        public int Key4 { get; set; }

        [DataMember, ProtoMember(5), Key(4)]
        public double Key5 { get; set; }

        [DataMember, ProtoMember(6), Key(5)]
        public List<float> Key6 { get; set; }

        [DataMember, ProtoMember(7), Key(6)]
        public List<float> Key7 { get; set; }

        [DataMember, ProtoMember(8), Key(7)]
        public List<float> Key8 { get; set; }

        [DataMember, ProtoMember(9), Key(8)]
        public List<float> Key9 { get; set; }

        [DataMember, ProtoMember(10), Key(9)]
        public Dictionary<int, string> Key10 { get; set; }

        [DataMember, ProtoMember(11), Key(10)]
        public Dictionary<int, string> Key11 { get; set; }

        [DataMember, ProtoMember(12), Key(11)]
        public Dictionary<int, string> Key12 { get; set; }

        [DataMember, ProtoMember(13), Key(12)]
        public Dictionary<int, string> Key13 { get; set; }

        [DataMember, ProtoMember(14), Key(13)]
        public Dictionary<int, string> Key14 { get; set; }

        [DataMember, ProtoMember(15), Key(14)]
        public Dictionary<int, string> Key15 { get; set; }

        [DataMember, ProtoMember(16), Key(15)]
        public Dictionary<int, string> Key16 { get; set; }

        [DataMember, ProtoMember(17), Key(16)]
        public Dictionary<int, string> Key17 { get; set; }

        [DataMember, ProtoMember(18), Key(17)]
        public Dictionary<int, string> Key18 { get; set; }

        [DataMember, ProtoMember(19), Key(18)]
        public Dictionary<int, string> Key19 { get; set; }

        [DataMember, ProtoMember(20), Key(19)]
        public Dictionary<int, string> Key20 { get; set; }

        public LargeHotelChain(string title)
        {
            Title = title;
        }

        public LargeHotelChain() // Parameterless ctor is needed for every protocol buffer class during deserialization
        { }


    }
}
