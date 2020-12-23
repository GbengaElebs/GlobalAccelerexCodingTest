using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Reflection;
using Application.DataTransferObjects;
using Application.Errors;
using Application.RequestDto;
using DataAccessor;

namespace Application
{
    public class AppUtilities :IAppUtilites
    {
        public bool ValidateQueryData(UrlQuery urlQuery)
        {
            if (!string.IsNullOrEmpty(urlQuery.FilterParams.Status) && !urlQuery.ValidStatus)
            {
                throw new RestException(HttpStatusCode.BadRequest,
                new { ErrorDescription = "Status Not Valid" });
            }
            if (!string.IsNullOrEmpty(urlQuery.FilterParams.Gender) && !urlQuery.ValidGender)
            {
                throw new RestException(HttpStatusCode.BadRequest,
                new { ErrorDescription = "Gender Not Valid" });
            }

            if (CheckForDuplicateObjectEntries(urlQuery.SortParams))
            {
                throw new RestException(HttpStatusCode.BadRequest,
                new { ErrorDescription = "You Can Only Sort By One Parameter" });
            }
            if (CheckForDuplicateObjectEntries(urlQuery.FilterParams))
            {
                throw new RestException(HttpStatusCode.BadRequest,
                new { ErrorDescription = "You Can Only Filter By One Parameter" });
            }
            return true;
        }

        public bool CheckForDuplicateObjectEntries(object myObject)
        {
            int counter = 0;
            foreach (PropertyInfo pi in myObject.GetType().GetProperties())
            {
                if (pi.PropertyType == typeof(string))
                {
                    string value = (string)pi.GetValue(myObject);
                    if (!string.IsNullOrEmpty(value))
                    {
                        counter++;
                    }
                    if (counter > 1)
                    {
                        return true;
                    }
                }
                else if (pi.PropertyType == typeof(bool))
                {
                    bool value = (bool)pi.GetValue(myObject);
                    if (value)
                    {
                        counter++;
                    }
                    if (counter > 1)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public  List<CharacterDto> FilterQueryResult(UrlQuery urlQuery, List<CharacterDto> result)
        {
            if (urlQuery.HaveFilter)
            {
                if (!string.IsNullOrEmpty(urlQuery.FilterParams.Gender))
                {
                    result = result.Where(x => x.Gender == urlQuery.FilterParams.Gender).ToList();
                }
                else if (!string.IsNullOrEmpty(urlQuery.FilterParams.Status))
                {
                    result = result.Where(x => x.Status == urlQuery.FilterParams.Status).ToList();
                }
                else if (!string.IsNullOrEmpty(urlQuery.FilterParams.Location))
                {
                    result = result.Where(x => x.LocationName == urlQuery.FilterParams.Location).ToList();
                }
            }

            return result;
        }

        public  List<CharacterDto> SortQueryResult(UrlQuery urlQuery, SqlModelRes<CharacterDto> resultQueryable)
        {
            List<CharacterDto> result;
            if (urlQuery.SortParams.sortByFirstName && urlQuery.descending)
            {
                result = resultQueryable.ResultList.OrderByDescending(x => x.FirstName).ToList();
            }

            else if (urlQuery.SortParams.sortByLastName && urlQuery.descending)
            {
                result = resultQueryable.ResultList.OrderByDescending(x => x.LastName).ToList();
            }
            else if (urlQuery.SortParams.sortByGender && urlQuery.descending)
            {
                result = resultQueryable.ResultList.OrderByDescending(x => x.Gender).ToList();
            }
            else
            {
                result = resultQueryable.ResultList.ToList();
            }

            return result;
        }



    }
}