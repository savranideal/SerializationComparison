using MessagePack;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Serialization.Benchmark.Models.Hotel
{
    [Serializable, DataContract, ProtoContract, MessagePackObject]
    public class LargeHotel
    {
         
        [DataMember, ProtoMember(1), Key(0)]
        public int Id { get; set; }

        [DataMember, ProtoMember(2), Key(1)]
        public string Name { get; set; }

        [DataMember, ProtoMember(3), Key(2)]
        public string Address { get; set; }

        [DataMember, ProtoMember(4), Key(3)]
        public string Phone { get; set; }

        [DataMember, ProtoMember(5), Key(4)]
        public double Rating { get; set; }
        [DataMember, ProtoMember(5), Key(4)]
        public string Key10 { get; set; }


        [DataMember, ProtoMember(6), Key(5)]
        public string Key11 { get; set; }
        [DataMember, ProtoMember(7), Key(6)]
        public string Key12 { get; set; }
        [DataMember, ProtoMember(8), Key(7)]
        public string Key13 { get; set; }
        [DataMember, ProtoMember(9), Key(8)]
        public string Key14 { get; set; }
        [DataMember, ProtoMember(10), Key(9)]
        public string Key15 { get; set; }
        [DataMember, ProtoMember(11), Key(10)]
        public string Key16 { get; set; }
        [DataMember, ProtoMember(12), Key(11)]
        public string Key17 { get; set; }
        [DataMember, ProtoMember(13), Key(12)]
        public string Key18 { get; set; }
        [DataMember, ProtoMember(14), Key(13)]
        public string Key19 { get; set; }

        [DataMember, ProtoMember(15), Key(14)] 
        public List<string> Images { get; set; }
    }
}
