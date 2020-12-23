using System.Collections.Generic;
using System.Threading.Tasks;
using Application.DataTransferObjects;
using Application.RequestDto;

namespace Application
{
    public interface ITask
    {
         Task<List<EpisodeDto>> GetAllEpisodes();
         Task<List<CommentDto>> GetAllComments();
         Task<List<CharacterDto>> GetCharacters(UrlQuery urlQuery);
         Task<List<EpisodeDtoFull>> GetListOfEpisodesByCharacter(int charId);
         Task<List<EpisodeDtoFull>> GetListOfEpisodesByCharacterName(string charName);
         Task<CommentDtoFull> AddComments(AddCommentRequest request, string IpAddressLocation);     
         Task<CommentDtoFull> GetCommentById(int commentId);
    }
}