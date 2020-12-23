using System;
using System.Collections.Generic;
using Application;
using Application.DataTransferObjects;
using Application.RequestDto;
using DataAccessor;
using Moq;

namespace Tests.Mock
{
    public class MockAppUtilities :  Mock<IAppUtilites>
    {
        public MockAppUtilities MockValidateQueryData(bool result)
        {
            Setup(x => x.ValidateQueryData(It.IsAny<UrlQuery>())).Returns(result);
            return this;
        }
        public MockAppUtilities MockValidateQueryDataInValid(bool result)
        {
            Setup(x => x.ValidateQueryData(It.IsAny<UrlQuery>())).Throws(new Exception());
            return this;
        }
        public MockAppUtilities MockFilterQueryResult(List<CharacterDto> result)
        {
            Setup(x => x.FilterQueryResult(It.IsAny<UrlQuery>(),It.IsAny<List<CharacterDto>>())).Returns(result);
            return this;
        }
        public MockAppUtilities MockFilterQueryResultInValid(List<CharacterDto> result)
        {
            Setup(x => x.FilterQueryResult(It.IsAny<UrlQuery>(),It.IsAny<List<CharacterDto>>())).Throws(new Exception());
            return this;
        }
        public MockAppUtilities VerifyFilterQueryResult(Times times)
        {
            Verify(x => x.FilterQueryResult(It.IsAny<UrlQuery>(),It.IsAny<List<CharacterDto>>()),times);
            return this;
        }
        

        public MockAppUtilities MockSortQueryResult(List<CharacterDto> result)
        {
            Setup(x => x.SortQueryResult(It.IsAny<UrlQuery>(),It.IsAny<SqlModelRes<CharacterDto>>())).Returns(result);
            return this;
        }
        public MockAppUtilities VerifySortQueryResult(Times times)
        {
            Verify(x => x.SortQueryResult(It.IsAny<UrlQuery>(),It.IsAny<SqlModelRes<CharacterDto>>()),times);
            return this;
        }
        public MockAppUtilities MockSortQueryResultInValid(List<CharacterDto> result)
        {
            Setup(x => x.SortQueryResult(It.IsAny<UrlQuery>(),It.IsAny<SqlModelRes<CharacterDto>>())).Throws(new Exception());
            return this;
        }
    }
}