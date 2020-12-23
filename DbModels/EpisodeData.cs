using System;
using System.Collections.Generic;

namespace DbModels.GlobalAccelerex
{
    public partial class EpisodeData
    {
        public EpisodeData()
        {
            CommentsData = new HashSet<CommentsData>();
            EpisodeCharacter = new HashSet<EpisodeCharacter>();
        }
        public int EpisodeId { get; set; }
        public string Name { get; set; }
        public string EpisodeCode { get; set; }
        public DateTime Created { get; set; }
        public DateTime ReleaseDate { get; set; }
        public virtual ICollection<CommentsData> CommentsData { get; set; }
        public virtual ICollection<EpisodeCharacter> EpisodeCharacter { get; set; }
    }
}
