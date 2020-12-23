using System;

namespace Application.DataTransferObjects
{
    public class CommentDto
    {
        public string IpAddressLocation { get; set; }
        public DateTime Created { get; set; }

    }
    public class CommentDtoFull : CommentDto
    {
        public int CommentId { get; set; }
        public string Comment { get; set; }
        public int EpisodeId { get; set; }
    }

    
}