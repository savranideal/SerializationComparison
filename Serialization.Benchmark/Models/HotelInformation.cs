using Serialization.Benchmark.Models.Geography;
using System.Collections.Generic;
using System.Text;

namespace Serialization.Benchmark.Models
{

    public class HotelInformation
    { 
        public string Key { get; set; }
         
        public string Name { get; set; }
         
        public string Address { get; set; }
         
        public string Phone { get; set; }

       
        public string Fax { get; set; }
         
        public double Rating { get; set; }

       
        public string ShortDescription { get; set; }

       
        public string Description { get; set; }

       
        public string NearBy { get; set; }

       
        public string LocationInformations { get; set; }

       
        public string RoomInformations { get; set; }
         
        public IList<string> Videos { get; set; }

       
        public string HotelPolicy { get; set; }
          

       
        public string Email { get; set; }

       
        public int? MarketId { get; set; }

       
        public int DestinationId { get; set; }

       
        public int? HotelCategoryId { get; set; }

       
        public string HotelCategory { get; set; }

       
        public string Chain { get; set; }
       
        public int? ChainDefinitionId { get; set; }

       
        public string Code { get; set; }

       
        public string PostCode { get; set; }

       
        public string WebSite { get; set; }

       
        public int? ZoomLevel { get; set; }

       
        public string CheckIn { get; set; }

       
        public string CheckOut { get; set; }

       
        public bool MaleRestriction { get; set; }

       
        public bool PetRestriction { get; set; }

       
        public bool MarriageCertificateRestriction { get; set; }

       
        public bool? ChildRestriction { get; set; }

       
        public int? ChildRestrictionAge { get; set; }

       
        public string BuildYear { get; set; }

       
        public int? RoomCount { get; set; }

       
        public int? BarCount { get; set; }

       
        public int? AuditoriumCount { get; set; }

       
        public int? RestaurantCount { get; set; }

       
        public string SizeInfo { get; set; }

       
        public int? YearPoint { get; set; }

       
        public int? HotelPoint { get; set; }

       
        public int? ServicePoint { get; set; }


        public Coordinate Coordinate { get; set; }


        public City City { get; set; }


        public Country Country { get; set; }


        public Airport NearestAirport { get; set; }

        public IList<HotelRoomTypeInfo> RoomTypeInfos { get; set; } = new List<HotelRoomTypeInfo>();

       
        public IList<HotelMealTypeInfo> MealTypeInfos { get; set; } = new List<HotelMealTypeInfo>();

       
        public IList<HotelFacility> FacilityDetails { get; set; } = new List<HotelFacility>();

       
        public IList<HotelDescription> Descriptions { get; set; } = new List<HotelDescription>();

       
        public IList<HotelFoodDrink> FoodDrinks { get; set; } = new List<HotelFoodDrink>();

       
        public IList<HotelNearDistance> NearDistances { get; set; } = new List<HotelNearDistance>();

       
        public IList<HotelSeason> Seasons { get; set; } = new List<HotelSeason>();


       
        public IList<HotelAddonDetail> Addons { get; set; } = new List<HotelAddonDetail>();

       
        public HotelThemes Themes { get; set; } = new HotelThemes();

       
        public HotelTypes Types { get; set; } = new HotelTypes();
         
          
       
        public List<FreeChildAge> FreeChildAges { get; set; }
         
       
        public List<HotelRepresentative> HotelRepresentatives { get; set; }
     
       
        public List<HotelHygienicalMeasure> HotelHygienicalMeasures { get; set; } = new List<HotelHygienicalMeasure>();
      
       
        public bool HasHygienicalMeasure { get; set; }

    }

}
