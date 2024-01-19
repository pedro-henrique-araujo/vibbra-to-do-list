using VibbraToDoList.Data.Dto;
using VibbraToDoList.Data.Models;
using VibbraToDoList.Repositories;

namespace VibbraToDoList.Services.Imp
{
    public class ToDoListServiceImp : ToDoListService
    {
        private readonly ToDoListRepository _toDoListRepository;
        private readonly ToDoListUserService _toDoListUserService;
        private readonly ToDoService _toDoService;

        public ToDoListServiceImp(ToDoListRepository toDoListRepository, ToDoListUserService toDoListUserService, ToDoService toDoService)
        {
            _toDoListRepository = toDoListRepository;
            _toDoListUserService = toDoListUserService;
            _toDoService = toDoService;
        }

        public async Task<PageDto<ToDoList>> GetPageAsync(ToDoListPageParamsDto pageParamsDto)
        {
            var page = await _toDoListRepository.GetPageAsync(pageParamsDto);

            return page;
        }

        public async Task<ToDoList> GetByIdAsync(ToDoListParamsDto paramsDto)
        {
            var list = await _toDoListRepository.GetByIdAsync(paramsDto.ToDoListId);
            if (list is null) return new();
            if (list.OwnerUserId != paramsDto.UserId)
            {
                await _toDoListUserService.ShareAsync(new() { UserId = paramsDto.UserId, ToDoListId = list.Id });
            }
            var listWithChildren = await _toDoService.PopulateToDosChildrenAsync(list);
            return listWithChildren;
        }

        public async Task CreateAsync(ToDoList toDoList)
        {
            toDoList.Initialize();
            await _toDoListRepository.CreateAsync(toDoList);
            await _toDoService.CreateListToDosAsync(toDoList);
        }

        public async Task UpdateAsync(UpdateToDoListDto listDto)
        {
            var toDoListInDb = await _toDoListRepository.GetByIdAsync(listDto.Id);
            toDoListInDb.Name = listDto.Name;
            await _toDoListRepository.UpdateAsync(toDoListInDb);
            await _toDoService.UpdateListToDosAsync(listDto);
            foreach (var id in listDto.DeletedToDosIds)
            {
                await _toDoService.DeleteAsync(id);
            }
        }

        public async Task DeleteAsync(ToDoListParamsDto toDoListParamsDto)
        {
            var list = await _toDoListRepository.GetByIdAsync(toDoListParamsDto.ToDoListId);
            if (list?.OwnerUserId != toDoListParamsDto.UserId) return;
            await _toDoService.DeleteListChildrenAsync(list);          
            await _toDoListRepository.DeleteAsync(list);
        }

        
    }
}
