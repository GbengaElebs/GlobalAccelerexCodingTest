using System;

namespace Application.DataTransferObjects
{
    public class EpisodeDto
    {
        public string Name { get; set; }
        public int CommentCount { get; set; }
    }
    public class EpisodeDb
    {
        public int EpisodeId { get; set; }
    }
    public class EpisodeDtoFull : EpisodeDto
    {
        public DateTime ReleaseDate { get; set; }
        public string EpisodeCode { get; set; }
        public string Created { get; set; }
    }
}