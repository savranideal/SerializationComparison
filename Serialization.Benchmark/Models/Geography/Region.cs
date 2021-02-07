using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Serialization.Benchmark.Models.Geography
{
    /// <summary>
    /// Bir koda sahip olan bir lokasyon
    /// </summary>
    
    [DebuggerDisplay("{GetType().Name} ({Id}) - {Label}")] 
    public abstract class Region : Location 
    {
        /// <summary>
        /// Lokasyonun tipten bağımsız idsi.  
        /// </summary>
        
        public int Id { get; set; }
               

        /// <summary>
        /// Bu lokasyonu kapsayan üst region
        /// </summary>
        
        public Region ParentRegion { get; set; }

        /// <summary>
        /// Uygulama culture'na göre Etiketi
        /// </summary>
        
        public string Label { get; set; }
          
         
         
    }
}