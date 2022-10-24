using DogApi.Managers;
using DogApi.Models;
using Microsoft.AspNetCore.Mvc;
using Sakur.WebApiUtilities.Models;

namespace DogApi.Controllers
{

    [ApiController]
    [Route("api")]
    public class DogController : ControllerBase
    {

        private readonly ILogger<DogController> _logger;

        public DogController(ILogger<DogController> logger)
        {
            _logger = logger;
        }

        [HttpGet("Dog")]
        public async Task<IActionResult> Get()
        {
            DatabaseManager manager = new DatabaseManager();
            return new ApiResponse(await manager.GetBreedsAsync());
        }
    }
}