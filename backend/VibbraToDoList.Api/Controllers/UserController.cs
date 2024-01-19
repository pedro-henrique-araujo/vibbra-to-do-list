using Microsoft.AspNetCore.Mvc;
using VibbraToDoList.Api.Attributes;
using VibbraToDoList.Data.Models;
using VibbraToDoList.Services;

namespace VibbraToDoList.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{userName}")]
        public async Task<IActionResult> Get(string userName)
        {
            var user = await _userService.GetByUserNameAsync(userName);
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]User user)
        {
            var userInDb = await _userService.GetByUserNameAsync(user.UserName);
            if (userInDb.Id != Guid.Empty) return Ok();
            await _userService.SignUpAsync(user.UserName);
            return Created("", null);
        }

        [DevOnly]
        [HttpDelete("{userName}")]
        public async Task<IActionResult> Delete(string userName)
        {
            await _userService.DeleteAsync(userName);
            return Ok();
        }
    }
}
