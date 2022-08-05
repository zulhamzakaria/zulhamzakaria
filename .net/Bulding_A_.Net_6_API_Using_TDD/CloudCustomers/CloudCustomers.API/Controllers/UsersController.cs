using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CloudCustomers.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService usersService;

        //private readonly ILogger<UsersController> logger;

        public UsersController(IUsersService usersService)
        { 
            this.usersService = usersService;
        }
        //public UsersController(ILogger<UsersController> logger)
        //{
        //    this.logger = logger;
        //}

        [HttpGet(Name = "GetUsers")]
        public async Task<IActionResult> Get()
        {
            var users = await usersService.GetAllUsers();
            if (users.Any())
            {
                return Ok(users);
            }
            return NotFound();
        }
    }
}
