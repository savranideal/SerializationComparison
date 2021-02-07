using System.Collections.Generic;

namespace Serialization.Benchmark.Models
{
    public class HotelMealTypeInfo
    {
       
        public int HotelMealTypeId { get; set; }

       
        public int MealTypeValue { get; set; }

       
        public string MealTypeText { get; set; }

       
        public bool IsSeosonal { get; set; }

       
        public int? SeasonId { get; set; }

       
        public string Description { get; set; }

       
        public List<int> MealTypeFoodDrink { get; set; } = new List<int>();
    }

}
