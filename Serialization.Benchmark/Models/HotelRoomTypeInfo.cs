using System.Collections.Generic;

namespace Serialization.Benchmark.Models
{
    public class HotelRoomTypeInfo
    {
        public HotelRoomTypeInfo()
        {
            BedTypes = new HotelBedTypes();
        }
       
        public int HotelRoomId { get; set; }

       
        public int RoomTypeValue { get; set; }

       
        public string RoomTypeText { get; set; }

       
        public int? ViewTypeValue { get; set; }

       
        public string ViewTypeText { get; set; }

       
        public int? SmokingTypeValue { get; set; }

       
        public string SmokingTypeText { get; set; }

       
        public int? QualityPoint { get; set; }

       
        public int? Size { get; set; }

       
        public string Description { get; set; }

       
        public HotelBedTypes BedTypes { get; set; }

       
        public IList<HotelFacility> Facilities { get; set; }

        
    }

}
