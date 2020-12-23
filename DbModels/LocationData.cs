using System;
using System.Collections.Generic;

namespace DbModels.GlobalAccelerex
{
    public partial class LocationData
    {
        public LocationData()
        {
            CharacterData = new HashSet<CharacterData>();
        }
        public int LocationId { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime Created { get; set; }
        public virtual ICollection<CharacterData> CharacterData { get; set; }
    }
}
