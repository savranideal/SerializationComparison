namespace Serialization.Benchmark.Models
{
    public class FreeChildAge
    {
        /// <summary>
        /// Ücretsiz çocuk yaş aralığı başlangıç
        /// </summary>
       
        public decimal AgeFrom { get; set; }

        /// <summary>
        /// Ücretsiz çocuk yaş aralığı bitiş
        /// </summary>
       
        public decimal AgeTo { get; set; }

        /// <summary>
        /// Kaçıncı çocuk ?
        /// </summary>
       
        public int WhichChild { get; set; }
    }

}
