using System;

namespace Serialization.Benchmark.Models
{
    public class HotelSeason
    {
       
        public int Id { get; set; }

       
        public int SeasonTypeValue { get; set; }

       
        public string SeasonTypeText { get; set; }

       
        public DateTime StartDate { get; set; }

       
        public DateTime EndDate { get; set; }
    }

}
