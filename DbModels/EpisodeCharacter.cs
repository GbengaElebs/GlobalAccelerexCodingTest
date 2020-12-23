
using System;

namespace DbModels.GlobalAccelerex
{
    public partial class EpisodeCharacter
    {
        public int Id { get; set; }
        public int EpisodeId { get; set; }
        public int CharacterId { get; set; }
        public virtual CharacterData Character { get; set; }
        public virtual EpisodeData Episode { get; set; }
    }
}
