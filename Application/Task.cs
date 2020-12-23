using System;
using System.Threading.Tasks;
using Application.DataTransferObjects;
using DataAccessor;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using Application.Errors;
using System.Net;
using Microsoft.Extensions.Logging;
using Application.RequestDto;
using DataAccessor.GlobalAccelerex;
using Microsoft.Extensions.Logging.Abstractions;

namespace Application
{
    public class Task : ITask
    {
        private GlobalAccelerexDataContext _context;
        private ITaskRepository _repo;
        private IAppUtilites _util;
        private ILogger _logger;
        public Task(GlobalAccelerexDataContext context, IDbInterfacing db, 
        ITaskRepository repo, IAppUtilites util,ILogger logger = null)
        {
            _context = context;
            _logger = logger ?? NullLogger.Instance;;
            _util = util;
            _repo = repo;
        }
        public async Task<List<EpisodeDto>> GetAllEpisodes()
        {
            var result = await _repo.GetAllEpisodesSorted();
            if (!result.Success || result.ResultList.Count <= 0)
            {
                _logger.LogInformation("Error Getting Episodes");
                throw new RestException(HttpStatusCode.NotFound,
                new { ErrorDescription = "Episodes Could Not Be Retrieved" });
            }
            _logger.LogInformation("Episodes SuccessFully Gotten");
            return result.ResultList;

            throw new Exception("Problem Getting Episodes");
        }

        public async Task<List<CommentDto>> GetAllComments()
        {
            var result = await _repo.GetAllCommentss();
            if (!result.Success || result.ResultList.Count <= 0)
            {
                _logger.LogInformation("GetAllComments Failed");
                throw new RestException(HttpStatusCode.NotFound,
                new { ErrorDescription = "Comments Could Not Be Retrieved" });
            }
            _logger.LogInformation("GetAllComments SuccessFul");
            return result.ResultList;

            throw new Exception("Problem Getting Comments");

        }

        public async Task<List<CharacterDto>> GetCharacters(UrlQuery urlQuery)
        {
            var result = new List<CharacterDto>();
            bool queryIsValid = _util.ValidateQueryData(urlQuery);
            if (queryIsValid)
            {
                var resultQueryable = await _repo.GetCharacterUnSortedAndUnFiltered();
                if (!resultQueryable.Success || resultQueryable.ResultList.Count <= 0)
                {
                    _logger.LogInformation("No Character Found");
                    throw new RestException(HttpStatusCode.NotFound,
                    new { ErrorDescription = "No Character Found" });
                }

                result = _util.SortQueryResult(urlQuery, resultQueryable);

                result = _util.FilterQueryResult(urlQuery, result);
                if (result.Count <= 0)
                {
                    _logger.LogInformation("No Character Matches the Search Parameters");
                    throw new RestException(HttpStatusCode.NotFound,
                    new { ErrorDescription = "No Character Matches the Search Parameters" });
                }
                _logger.LogInformation("Get Search Characters SuccessFul");
            }
            _logger.LogInformation("Get Search Characters UnSuccessFul");
            return result;
            throw new Exception("Problem Getting Characters");
        }
        public async Task<List<EpisodeDtoFull>> GetListOfEpisodesByCharacter(int charId)
        {
            var result = await _repo.GetListOfEpisodesByCharacterId(charId);
            if (!result.Success || result.ResultList.Count <= 0)
            {
                _logger.LogInformation("GetListOfEpisodesByCharacter Failed");
                throw new RestException(HttpStatusCode.NotFound,
                new { ErrorDescription = "Episodes Could Not Be Retrieved" });
            }
            _logger.LogInformation("GetListOfEpisodesByCharacter SuccessFul");
            return result.ResultList;

            throw new Exception("Problem GetListOfEpisodesByCharacter");

        }

        public async Task<List<EpisodeDtoFull>> GetListOfEpisodesByCharacterName(string charName)
        {
            var resultQueryable = await _repo.GetCharacterIdByCharacterName(charName);
            if (!resultQueryable.Success || resultQueryable.ResultList.Count <= 0)
            {
                _logger.LogInformation("Character Name Not Found");
                throw new RestException(HttpStatusCode.NotFound,
                new { ErrorDescription = "Character Name Not Found" });
            }

            var result = await _repo.GetListOfEpisodeByACharactersName(resultQueryable);
            if (!result.Success || result.ResultList.Count <= 0)
            {
                _logger.LogInformation("Episodes Not Found For Character Name");
                throw new RestException(HttpStatusCode.NotFound,
                new { ErrorDescription = "Episodes Not Found For Character Name" });
            }
            _logger.LogInformation("GetListOfEpisodesByCharacter SuccessFul");
            return result.ResultList;
            throw new Exception("Problem GetListOfEpisodesByCharacterName");
        }

        public async Task<CommentDtoFull> AddComments(AddCommentRequest request)
        {
            // validate Episode Id
            var queryResult = await _repo.ValidateEpisodeId(request);
            if (!queryResult.Success || queryResult.Result.EpisodeId <= 0)
            {
                _logger.LogInformation("Episode Not Found");
                throw new RestException(HttpStatusCode.NotFound,
                new { ErrorDescription = "Please Input a Valid Episode Id" });
            }
            var result = await _repo.InsertCommentData(request);
            if (!result.Success || result.ResultInt <= 0)
            {
                _logger.LogInformation("AddComments Failed", result.ErrorMessage);
                throw new RestException(HttpStatusCode.NotFound,
                new { ErrorDescription = "Comment Could Not Be Added" });
            }
            var comment = await _repo.GetAddedComment(request.EpisodeId);
            _logger.LogInformation("AddComments SuccessFul");
            return comment.Result;
            throw new Exception("Problem Adding Comments");
        }

    }
}