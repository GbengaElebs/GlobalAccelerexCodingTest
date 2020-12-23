using Application;
using Application.DataTransferObjects;
using Application.RequestDto;
using DataAccessor;
using Moq;

namespace Tests.Mock
{
    public class MockTaskRepo: Mock<ITaskRepository>
    {
        public MockTaskRepo MockGetCharacterUnSortedAndUnFiltered(SqlModelRes<CharacterDto> result)
        {
            Setup(x => x.GetCharacterUnSortedAndUnFiltered()).ReturnsAsync(result);
            return this;
        }
        public MockTaskRepo MockGetListOfEpisodeByACharactersName(SqlModelRes<EpisodeDtoFull> result)
        {
            Setup(x => x.GetListOfEpisodeByACharactersName(It.IsAny<SqlModelRes<Character>>())).ReturnsAsync(result);
            return this;
        }
        public MockTaskRepo MockInsertCommentData(SqlModelRes<int> result)
        {
            Setup(x => x.InsertCommentData(It.IsAny<AddCommentRequest>())).ReturnsAsync(result);
            return this;
        }
        public MockTaskRepo VerifyInsertCommentData(Times times)
        {
            Verify(x => x.InsertCommentData(It.IsAny<AddCommentRequest>()),times);
            return this;
        }
        public MockTaskRepo MockValidateEpisodeId(SqlModelRes<EpisodeDb> result)
        {
            Setup(x => x.ValidateEpisodeId(It.IsAny<AddCommentRequest>())).ReturnsAsync(result);
            return this;
        }

        public MockTaskRepo MockGetListOfEpisodesByCharacterId(SqlModelRes<EpisodeDtoFull> result)
        {
            Setup(x => x.GetListOfEpisodesByCharacterId(It.IsAny<int>())).ReturnsAsync(result);
            return this;
        }
        public MockTaskRepo MockGetAllEpisodesSorted(SqlModelRes<EpisodeDto> result)
        {
            Setup(x => x.GetAllEpisodesSorted()).ReturnsAsync(result);
            return this;
        }
         public MockTaskRepo MockGetCharacterIdByCharacterName(SqlModelRes<Character> result)
        {
            Setup(x => x.GetCharacterIdByCharacterName(It.IsAny<string>())).ReturnsAsync(result);
            return this;
        }
         public MockTaskRepo MockGetAllCommentss(SqlModelRes<CommentDto> result)
        {
            Setup(x => x.GetAllCommentss()).ReturnsAsync(result);
            return this;
        }
    }
}