
using System;

namespace DbModels.GlobalAccelerex
{
    public partial class CommentsData
    {
        public int CommentId { get; set; }
        public string Comment { get; set; }
        public string IpAddressLocation { get; set; }
        public int EpisodeId { get; set; }
        public DateTime Created { get; set; }
        public virtual EpisodeData Episode { get; set; }
    }
}
