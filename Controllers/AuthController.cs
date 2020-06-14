using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DatingApplication.Data;
using DatingApplication.Dtos;
using DatingApplication.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DatingApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public AuthController(IAuthRepository repo, IConfiguration config, IMapper mapper)
        {
            _repo = repo;
            _config = config;
            _mapper = mapper;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegister userForRegister)
        {
            userForRegister.Username = userForRegister.Username.ToLower();
            if (await _repo.UserExists(userForRegister.Username))
                return BadRequest("User is already existed");
            var userToCreate = new User
            {
                Username = userForRegister.Username
            };

            var createUser = await _repo.Register(userToCreate, userForRegister.Password);

            return StatusCode(201);

        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLogin userForLogin)
        {
        
                var userFromRepo = await _repo.Login(userForLogin.Username.ToLower(), userForLogin.Password);
                if (userFromRepo == null)
                    return Unauthorized("Unauthorized");
                var claims = new[]
                {
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name, userFromRepo.Username)
            };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
                var tokenDescriptior = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddDays(1),
                    SigningCredentials = creds
                };

                var tokenHandler = new JwtSecurityTokenHandler();

                var token = tokenHandler.CreateToken(tokenDescriptior);
                var user = _mapper.Map<UserForList>(userFromRepo);

                return Ok(new
                {
                    token = tokenHandler.WriteToken(token),
                    user
                });
          
        }
    }
}