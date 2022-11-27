using DogApi.Managers;
using DogApi.Models;
using DogApi.RequestBodies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sakur.WebApiUtilities.Models;
using System.Net;

namespace DogApi.Controllers
{

    [ApiController]
    [Route("api")]
    public class PostController : ControllerBase
    {
        [Authorize]
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreatePostRequestBody requestBody)
        {
            try
            {
                DatabaseManager database = new DatabaseManager();

                if (!requestBody.Valid)
                    return new ApiResponse(requestBody.GetInvalidBodyMessage(), HttpStatusCode.BadRequest);

                //user = await database.AddUser(user);
                //return new ApiResponse(user);
            }
            catch (ApiException apiException)
            {
                return new ApiResponse(apiException);
            }
        }
    }
}