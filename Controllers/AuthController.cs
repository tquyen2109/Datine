using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApplication.Data;
using DatingApplication.Model;
using Microsoft.AspNetCore.Mvc;

namespace DatingApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        public AuthController(IAuthRepository repo)
        {
            _repo = repo;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register (string username, string password)
        {
            username = username.ToLower();
            if(await _repo.UserExists(username))
                return BadRequest("User is already existed");
            var userToCreate = new User
            {
                Username = username
            };

            var createUser = await _repo.Register(userToCreate, password);

            return StatusCode(201);

        }
    }
}