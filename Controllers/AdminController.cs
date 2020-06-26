using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatingApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet("usersWithRoles")]
       public IActionResult GetUsersWithRoles()
       {
            return Ok("Only ADmin");
       }
        [Authorize(Policy = "ModeratorPhotoRole")]
        [HttpGet("photosForModeration")]
       public IActionResult GetPhotosForModeration()
       {
            return Ok("Admin or moderators");
       }
    }
}