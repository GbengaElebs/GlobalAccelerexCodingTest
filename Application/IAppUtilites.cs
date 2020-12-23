using System.Collections.Generic;
using Application.DataTransferObjects;
using Application.RequestDto;
using DataAccessor;

namespace Application
{
    public interface IAppUtilites
    {
        bool ValidateQueryData(UrlQuery urlQuery);
         List<CharacterDto> FilterQueryResult(UrlQuery urlQuery, List<CharacterDto> result);
         List<CharacterDto> SortQueryResult(UrlQuery urlQuery, SqlModelRes<CharacterDto> resultQueryable);
    }
}