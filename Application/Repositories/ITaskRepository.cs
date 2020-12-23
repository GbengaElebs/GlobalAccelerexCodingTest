using System.Threading.Tasks;
using Application.DataTransferObjects;
using Application.RequestDto;
using DataAccessor;
namespace Application
{
    public interface ITaskRepository
    {
        Task<SqlModelRes<CharacterDto>> GetCharacterUnSortedAndUnFiltered();
        Task<SqlModelRes<EpisodeDtoFull>> GetListOfEpisodeByACharactersName(SqlModelRes<Character> resultQueryable);
        Task<SqlModelRes<int>> InsertCommentData(AddCommentRequest request, string IpAddressLocation);
        Task<SqlModelRes<EpisodeDb>> ValidateEpisodeId(AddCommentRequest request);
        Task<SqlModelRes<EpisodeDtoFull>> GetListOfEpisodesByCharacterId(int charId);
        Task<SqlModelRes<EpisodeDto>> GetAllEpisodesSorted();
        Task<SqlModelRes<Character>> GetCharacterIdByCharacterName(string charName);
        Task<SqlModelRes<CommentDto>> GetAllCommentss();  
        Task<SqlModelRes<CommentDtoFull>> GetAddedComment(int episodeId); 
        Task<SqlModelRes<CommentDtoFull>> GetCommentById(int commentId);
    }
}
