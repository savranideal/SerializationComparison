using System;
using System.Collections.Generic;

namespace Serialization.Benchmark.Models
{
    public class HotelAddonDetail
    {
       
        public int Id { get; set; }

       
        public string Key { get; set; }

       
        public string Name { get; set; }

       
        public string Description { get; set; }

       
        public DateTime? StartDate { get; set; }

       
        public DateTime? EndDate { get; set; }

       
        public bool IsRequired { get; set; }

       
        public string StartTime { get; set; }

       
        public string EndTime { get; set; }
       
        public string ImagePath { get; set; }

       
        public List<string> ImagePaths { get; set; }

       
        public List<HotelRoomInformation> AvailableRooms { get; set; }
    }

}
