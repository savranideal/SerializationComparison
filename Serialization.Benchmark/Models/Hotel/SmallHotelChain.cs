using MessagePack;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Serialization.Benchmark.Models.Hotel
{
    [Serializable, DataContract, ProtoContract, MessagePackObject]
    public class SmallHotelChain
    {
        [DataMember, ProtoMember(1), Key(0)]
        public List<Hotel> Hotels  {  get;  set;  }

        [DataMember, ProtoMember(2), Key(1)]
        public string Title { get; set; } 


        public SmallHotelChain(string title)
        {
            Title = title;
        }

        public SmallHotelChain() 
        {
        }
    }
}
