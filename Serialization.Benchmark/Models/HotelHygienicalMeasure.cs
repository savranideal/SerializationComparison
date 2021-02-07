using System;
using System.Collections.Generic;

namespace Serialization.Benchmark.Models
{
    public class HotelHygienicalMeasure
    {
       
        public int Id { get; set; }
       
        public string TagName { get; set; }
       
        public string Description { get; set; }
       
        public DateTime StartDate { get; set; }
       
        public DateTime EndDate { get; set; }
       
        public List<HotelHygienicalMeasureMedia> HotelDetailHygienicalMeasureMedias { get; set; } = new List<HotelHygienicalMeasureMedia>();
       
        public List<HotelHygienicalMeasureDetail> HotelDetailHygienicalMeasureDetails { get; set; } = new List<HotelHygienicalMeasureDetail>();
       
        public int Priority { get; set; }
    }

}
