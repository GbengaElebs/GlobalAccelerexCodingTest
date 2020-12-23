using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Application.RequestDto
{
    public class AddCommentRequest
    {
        public string Comment { get; set; }
        [Required(ErrorMessage = "EpisodeId is Required")]
        public int EpisodeId { get; set; }
        [JsonIgnore]
        public string IpAddressLocation { get; set; }

    }
}