using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Utilities;
using Application;
using Application.DataTransferObjects;
using Application.RequestDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CodingTest.Api.Controllers
{
    [ApiController]
    [Route("api")]
    public class TaskController : ControllerBase
    {
        private readonly ILogger<TaskController> _logger;
        private ITask _task;
        private IUtilities _util;
        public TaskController(ILogger<TaskController> logger, ITask task, IUtilities util)
        {
            _logger = logger;
            _task = task;
            _util = util;
        }

        ///<Summary>
        /// /This Endpoint is to GetAllComments 
        ///</Summary>
        [HttpGet("GetAllEpisodes")]
        public async Task<ActionResult<List<EpisodeDto>>> GetAllEpisodes()
        {
            var result = await _task.GetAllEpisodes();
            return Ok(result);
        }
        ///<Summary>
        ///This Endpoint is to GetAllComments 
        ///</Summary>
        [HttpGet("GetAllComments")]
        public async Task<ActionResult<List<CommentDto>>> GetAllComments()
        {
            var result = await _task.GetAllComments();
            return Ok(result);
        }
        ///<Summary>
        /// This Endpoint is to GetCharacters Can Be Filtered or Sorted Based on the Query String Param
        ///</Summary>
        [HttpGet("GetCharacters")]
        public async Task<ActionResult<List<CharacterDto>>> GetCharacters([FromQuery] UrlQuery urlQuery)
        {
            var result = await _task.GetCharacters(urlQuery);
            return Ok(result);
        }
        ///<Summary>
        /// This Endpoint is to GetListOfEpisodesByCharacter
        ///</Summary>
        [HttpGet("GetListOfEpisodesByCharacterId/{charId}")]
        public async Task<ActionResult<List<EpisodeDtoFull>>> GetListOfEpisodesByCharacter(int charId)
        {
            var result = await _task.GetListOfEpisodesByCharacter(charId);
            return Ok(result);
        }
         ///<Summary>
        /// This Endpoint is to GetListOfEpisodesByCharacterFirstName
        ///</Summary>
        [HttpGet("GetListOfEpisodesByCharacterFirstName/{charName}")]
        public async Task<ActionResult<List<EpisodeDtoFull>>> GetListOfEpisodesByCharacterFirstName(string charName)
        {
            var result = await _task.GetListOfEpisodesByCharacterName(charName);
            return Ok(result);
        }
        ///<Summary>
        /// This Endpoint is to AddComments
        ///</Summary>
        [HttpPost("AddComments")]
        public async Task<ActionResult<CommentDtoFull>> AddComments(AddCommentRequest request)
        {
            request.IpAddressLocation = _util.GetUserIP();
            var result = await _task.AddComments(request);
            return Ok(result);
        }
    }
}
