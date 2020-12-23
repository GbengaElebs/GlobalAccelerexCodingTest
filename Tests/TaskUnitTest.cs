using System.Collections.Generic;
using Application;
using Application.DataTransferObjects;
using Bogus;
using DataAccessor;
using Tests.Mock;
using Xunit;

namespace CodingApp.Tests
{
    public class TaskRepoUnitTest
    {
        [Fact]
        public void TestGetAllEpisodes_ReturnsSuccess()
        {
            //Arrange - Setup the mock method
            var testepisodes = new Faker<EpisodeDto>()
            .RuleFor(u => u.Name, f => f.Name.JobDescriptor())
            .RuleFor(u => u.CommentCount, (3));

            var testSqlEpisodes = new Faker<SqlModelRes<EpisodeDto>>()
            .RuleFor(u => u.Success, true)
             .RuleFor(u => u.ResultList, f => new List<EpisodeDto> { f.PickRandom(testepisodes), f.PickRandom(testepisodes)});
            var mockTaskRepo = new MockTaskRepo().MockGetAllEpisodesSorted(testSqlEpisodes);
            //Create the Service instance
            var taskService = new Task(null, null, mockTaskRepo.Object, null, null);

            //Act - Call the method being tested
            var episodes = taskService.GetAllEpisodes();

            //Assert
            //First, assert that the episode list returned is not empty.
            Assert.NotEmpty(episodes.Result);
        }

        [Fact]
        public void TestGetAllComments_ReturnsSuccess()
        {
            //Arrange - Setup the mock method
            var testData = new Faker<CommentDto>()
            .RuleFor(u => u.Created, f => f.Date.Recent())
            .RuleFor(u => u.IpAddressLocation, f => "1092029");

            var testSqlEpisodes = new Faker<SqlModelRes<CommentDto>>()
            .RuleFor(u => u.Success, true)
             .RuleFor(u => u.ResultList, f => new List<CommentDto> { f.PickRandom(testData), f.PickRandom(testData)});
            var mockTaskRepo = new MockTaskRepo().MockGetAllCommentss(testSqlEpisodes);
            //Create the Service instance
            var taskService = new Task(null, null, mockTaskRepo.Object, null, null);

            //Act - Call the method being tested
            var comments = taskService.GetAllComments();

            //Assert
            //First, assert that the comment list returned is not empty.
            Assert.NotEmpty(comments.Result);
        }
        
    }
}
