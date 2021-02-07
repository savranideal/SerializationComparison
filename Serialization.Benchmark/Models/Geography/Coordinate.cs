using System.Diagnostics;
using System.Runtime.Serialization;

namespace Serialization.Benchmark.Models.Geography
{
    /// <summary>
    /// Koordinat olarak lokasyon
    /// </summary>
    
    [DebuggerDisplay("Lat={Latitude};Long={Longitude}")]
    public class Coordinate: Marker
    {
                
        public float Latitude { get; set; }

        public float Longitude { get; set; }
    }
}
