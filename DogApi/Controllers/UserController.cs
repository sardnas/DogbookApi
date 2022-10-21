﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sakur.WebApiUtilities.Models;
using System.Net;
using System.Threading.Tasks;
using DogApi.Helpers;
using DogApi.Managers;
using DogApi.Models;
using WebApiUtilities.Helpers;
using DogApi.RequestBodies;

namespace DogApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateUserRequestBody requestBody)
        {
            try
            {
                DatabaseManager database = new DatabaseManager();

                if (!requestBody.Valid)
                    return new ApiResponse(requestBody.GetInvalidBodyMessage(), HttpStatusCode.BadRequest);

                User user = requestBody.GetUser();
                user.Password = PasswordHelper.CreatePasswordHash(user.Password);

                user = await database.AddUser(user);
                return new ApiResponse(user);
            }
            catch (ApiException apiException)
            {
                return new ApiResponse(apiException);
            }
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> GetInformation([FromBody] UserLoginRequestBody userLoginInformation)
        {
            try
            {
                DatabaseManager database = new DatabaseManager();
                User user = await database.GetUserByUsername(userLoginInformation.Username);

                if (user == null)
                    return new ApiResponse("Invalid username and/or password", HttpStatusCode.Unauthorized);

                UserLoginRequestBody authenticatedUser = UserHelper.AuthenticateUser(userLoginInformation, user);

                if (authenticatedUser != null)
                {
                    var tokenString = UserHelper.GenerateJsonWebToken(user);
                    return new ApiResponse(new TokenResponse(tokenString, user));
                }

                return new ApiResponse("Invalid username and/or password", HttpStatusCode.Unauthorized);
            }
            catch (ApiException apiException)
            {
                return new ApiResponse(apiException);
            }
        }
    }
}
