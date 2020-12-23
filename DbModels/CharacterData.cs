using System;
using System.Collections.Generic;

namespace DbModels.GlobalAccelerex
{
    public partial class CharacterData
    {
        public CharacterData()
        {
            EpisodeCharacter = new HashSet<EpisodeCharacter>();
        }
        public int CharacterId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Status { get; set; }
        public string StateOfOrigin { get; set; }
        public string Gender { get; set; }
        public int? LocationId { get; set; }
        public DateTime Created { get; set; }
        public virtual LocationData Location { get; set; }
        public virtual ICollection<EpisodeCharacter> EpisodeCharacter { get; set; }
    }
}
