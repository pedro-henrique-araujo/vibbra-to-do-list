using Microsoft.AspNetCore.Mvc;
using VibbraToDoList.Api.Attributes;
using VibbraToDoList.Services;

namespace VibbraToDoList.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly ToDoService _toDoService;

        public ToDoController(ToDoService toDoService)
        {
            _toDoService = toDoService;
        }

        [DevOnly]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            await _toDoService.DeleteAsync(id);
            return Ok();
        }
    }
}
