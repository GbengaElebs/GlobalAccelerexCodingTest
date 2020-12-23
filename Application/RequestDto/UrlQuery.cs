using Newtonsoft.Json;

namespace Application.RequestDto
{
    public class UrlQuery
    {
        public UrlQuery()
        {
            FilterParams= new FilterParams();
            SortParams = new SortParams();
        }
        public FilterParams FilterParams { get; set; }
        public SortParams SortParams { get; set; }
        public bool descending { get; set; }

         [JsonIgnore]
        internal bool HaveFilter => !string.IsNullOrEmpty(FilterParams.Gender) || !string.IsNullOrEmpty(FilterParams.Status) || !string.IsNullOrEmpty(FilterParams.Location);
         [JsonIgnore]
        internal bool ValidStatus => FilterParams.Status == "ACTIVE" || FilterParams.Status == "DEAD" || FilterParams.Status == "UNKNOWN";
         [JsonIgnore]
        internal bool ValidGender => FilterParams.Gender == "MALE" || FilterParams.Gender == "FEMALE";

    }
    public class FilterParams
    {
        public string Gender { get; set; }
        public string Status { get; set; }
        public string Location { get; set; }
    }
    public class SortParams
    {
        public bool sortByFirstName { get; set; }
        public bool sortByLastName { get; set; }
        public bool sortByGender { get; set; }
    }
}