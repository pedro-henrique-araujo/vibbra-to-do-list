using Microsoft.AspNetCore.Mvc;
using VibbraToDoList.Data.Dto;
using VibbraToDoList.Data.Models;
using VibbraToDoList.Services;

namespace VibbraToDoList.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ToDoListController : ControllerBase
    {
        private ToDoListService _toDoListService;

        public ToDoListController(ToDoListService toDoListService)
        {
            _toDoListService = toDoListService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] int skip, [FromQuery] int take, [FromHeader] Guid userId)
        {
            var page = await _toDoListService.GetPageAsync(new()
            {
                Skip = skip,
                Take = take,
                UserId = userId
            });
            return Ok(page);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id, [FromHeader] Guid userId)
        {
            var list = await _toDoListService.GetByIdAsync(new() { ToDoListId = id, UserId = userId });
            return Ok(list);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] ToDoList toDoList, [FromHeader] Guid userId)
        {
            toDoList.OwnerUserId = userId;
            await _toDoListService.CreateAsync(toDoList);
            return Created("", null);
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] UpdateToDoListDto toDoListDto)
        {
            await _toDoListService.UpdateAsync(toDoListDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id, [FromHeader] Guid userId)
        {
            await _toDoListService.DeleteAsync(new() { ToDoListId = id, UserId = userId });

            return Ok();
        }
    }
}
