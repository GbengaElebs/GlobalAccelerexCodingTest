using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Application.DataTransferObjects;
using Application.RequestDto;
using Dapper;
using DataAccessor;
using Microsoft.Extensions.Configuration;

namespace Application
{
    public class TaskRepository : ITaskRepository
    {
        private IDbInterfacing _db;
        private readonly IConfiguration _configuration;
        private readonly string connString;
        public TaskRepository(IDbInterfacing db, IConfiguration configuration)
        {
            _db = db;
             _configuration = configuration;
            connString = _configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<SqlModelRes<CharacterDto>> GetCharacterUnSortedAndUnFiltered()
        {
            string query = @"SELECT a.FirstName, a.LastName,a.Status,a.StateOfOrigin, a.Gender,a.Created, b.Name AS LocationName
                                    FROM CharacterData a, LocationData b
                                    WHERE a.LocationId = b.LocationId";

            var resultQueryable = await _db.GetList<CharacterDto>(connString, query, CommandType.Text, null);
            return resultQueryable;
        }

        public async Task<SqlModelRes<EpisodeDtoFull>> GetListOfEpisodeByACharactersName(SqlModelRes<Character> resultQueryable)
        {
            int charId = resultQueryable.ResultList.Select(x => x.CharacterId).FirstOrDefault();

            string query = @"SELECT * FROM EpisodeData
                                WHERE EpisodeId in (SELECT EpisodeId FROM EpisodeCharacter WHERE  CharacterId= @charId)";
            var parameters = new DynamicParameters();
            parameters.Add("@charId", charId);
            var result = await _db.GetList<EpisodeDtoFull>(connString, query, CommandType.Text, parameters);
            return result;
        }
        public async Task<SqlModelRes<int>> InsertCommentData(AddCommentRequest request)
        {
            string insertQuery = @"INSERT INTO CommentsData (Comment,IpAddressLocation,Created,EpisodeId) 
                                VALUES (@Comment,@IpAddressLocation,@Created,@EpisodeId)";

            var parameters = new DynamicParameters();
            parameters.Add("@Comment", request.Comment);
            parameters.Add("@IpAddressLocation", request.IpAddressLocation);
            parameters.Add("@EpisodeId", request.EpisodeId);
            parameters.Add("@Created", DateTime.Now);
            var result = await _db.ModifyDB(connString, insertQuery, CommandType.Text, parameters);
            return result;
        }

        public async Task<SqlModelRes<EpisodeDb>> ValidateEpisodeId(AddCommentRequest request)
        {
            string query = @"SELECT EpisodeId FROM EpisodeData WHERE EpisodeId=@EpisodeId";
            var param = new DynamicParameters();
            param.Add("@EpisodeId", request.EpisodeId);
            var queryResult = await _db.GetOneItem<EpisodeDb>(connString, query, CommandType.Text, param);
            return queryResult;
        }

        public async Task<SqlModelRes<EpisodeDtoFull>> GetListOfEpisodesByCharacterId(int charId)
        {
            string query = @"SELECT * FROM EpisodeData
                                WHERE EpisodeId in (SELECT EpisodeId FROM EpisodeCharacter WHERE  CharacterId= @charId)";

            var parameters = new DynamicParameters();
            parameters.Add("@charId", charId);
            var result = await _db.GetList<EpisodeDtoFull>(connString, query, CommandType.Text, parameters);
            return result;
        }
        public async Task<SqlModelRes<EpisodeDto>> GetAllEpisodesSorted()
        {
            string query = @"SELECT a.Name, COUNT(b.EpisodeId) as CommentCount
                                FROM EpisodeData a, CommentsData b
                                WHERE a.EpisodeId = b.EpisodeId
                                GROUP BY b.EpisodeId
                                ORDER BY a.ReleaseDate ASC";
            var result = await _db.GetList<EpisodeDto>(connString, query, CommandType.Text, null);
            return result;
        }
        public async Task<SqlModelRes<Character>> GetCharacterIdByCharacterName(string charName)
        {
            string getCharIdQuery = @"SELECT CharacterId FROM CharacterData WHERE FirstName=@firstName";
            var param = new DynamicParameters();
            param.Add("@firstName", charName);
            var resultQueryable = await _db.GetList<Character>(connString, getCharIdQuery, CommandType.Text, param);
            return resultQueryable;
        }
        public async Task<SqlModelRes<CommentDto>> GetAllCommentss()
        {
            string query = @"SELECT IpAddressLocation,Created FROM CommentsData ORDER BY CommentId DESC";
            var result = await _db.GetList<CommentDto>(connString, query, CommandType.Text, null);
            return result;
        }
        public async Task<SqlModelRes<CommentDtoFull>> GetAddedComment(int episodeId)
        {
            string query = @"SELECT * FROM CommentsData WHERE EpisodeId=@episodeId ORDER BY CommentId DESC LIMIT 1";
            var param = new DynamicParameters();
            param.Add("@episodeId", episodeId);
            var result = await _db.GetOneItem<CommentDtoFull>(connString, query, CommandType.Text, param);
            return result;
        }



    }
}