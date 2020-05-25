using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApplication.Data;
using DatingApplication.Dtos;
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
        public async Task<IActionResult> Register (UserForRegister userForRegister)
        {
            userForRegister.Username = userForRegister.Username.ToLower();
            if(await _repo.UserExists(userForRegister.Username))
                return BadRequest("User is already existed");
            var userToCreate = new User
            {
                Username = userForRegister.Username
            };

            var createUser = await _repo.Register(userToCreate, userForRegister.Password);

            return StatusCode(201);

        }
    }
}