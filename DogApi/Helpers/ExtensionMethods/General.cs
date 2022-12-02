using Sakur.WebApiUtilities.Models;
using System.Net;

namespace DogApi.Helpers.ExtensionMethods
{
    public static class General
    {
        public static int GetUserId(this Dictionary<string, string> claims)
        {
            if (!claims.ContainsKey("sub"))
                return 0;

            if (int.TryParse(claims["sub"], out int userId))
                return userId;

            throw new ApiException("invalid id in token object", HttpStatusCode.BadRequest);
        }
    }
}
