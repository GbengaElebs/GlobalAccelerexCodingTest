using Microsoft.AspNetCore.Http;

namespace Api.Utilities
{
    public class HelperMethods : IUtilities
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public HelperMethods(IHttpContextAccessor httpContextAccessor)
        {
          _httpContextAccessor = httpContextAccessor;

        }
        public string GetUserIP()
        {
            string ipAddress =  _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            return ipAddress;
        }
    }
}