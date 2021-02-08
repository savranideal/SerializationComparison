using MessagePack;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Serialization.Benchmark.Models.Hotel
{

    [Serializable, DataContract, ProtoContract, MessagePackObject]
    public class Hotel
    { 
        [DataMember, ProtoMember(1), Key(0)]
        public int Key { get; set; }
         
        [DataMember, ProtoMember(2), Key(1)]  
        public string Name { get; set; } 

        [DataMember, ProtoMember(3), Key(2)]
        public double Rating { get; set; } 

        [DataMember, ProtoMember(4), Key(3)]
        public List<string> Images { get; set; }
    }
}
