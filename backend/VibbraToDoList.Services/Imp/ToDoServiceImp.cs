using VibbraToDoList.Data.Dto;
using VibbraToDoList.Data.Models;
using VibbraToDoList.Repositories;

namespace VibbraToDoList.Services.Imp
{
    public class ToDoServiceImp : ToDoService
    {
        private readonly ToDoRepository _toDoRepository;

        public ToDoServiceImp(ToDoRepository toDoRepository)
        {
            _toDoRepository = toDoRepository;
        }

        public async Task CreateListToDosAsync(ToDoList toDoList)
        {
            if (toDoList?.ToDos is null) return;
            foreach (var toDo in toDoList?.ToDos)
            {
                toDo.ToDoId = null;
                await _toDoRepository.CreateAsync(toDo);
                await CreateOrUpdateAsync(toDo.Children, toDo.Id);
            }
        }

        public async Task DeleteListChildrenAsync(ToDoList toDoList)
        {
            if (toDoList?.ToDos is null) return;
            foreach (var toDo in toDoList?.ToDos)
            {
                await MarkAsDeletedAsync(toDo.Id);
            }
            await _toDoRepository.SaveChangesAsync();
        }

        public async Task<ToDoList> PopulateToDosChildrenAsync(ToDoList list)
        {
            list.ToDos = await PopulateToDosChildrenAsync(list.ToDos);
            return list;
        }

        public async Task UpdateListToDosAsync(UpdateToDoListDto toDoListDto)
        {
            if (toDoListDto?.ToDos is null) return;
            foreach (var toDo in toDoListDto?.ToDos)
            {
                toDo.ToDoId = null;
                toDo.ToDoListId = toDoListDto.Id;
                await CreateOrUpdateAsync(toDo);
                await CreateOrUpdateAsync(toDo.Children, toDo.Id);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var toDo = await _toDoRepository.GetByIdAsync(id);
            if (toDo?.Id is null) return;
            var children = await GetChildrenAsync(toDo.Id);
            if (children is not null)
            {
                await MarkAsDeletedAsync(children);
            }
            await _toDoRepository.DeleteAsync(toDo);
        }

        private async Task MarkAsDeletedAsync(Guid id)
        {
            var toDo = await _toDoRepository.GetByIdAsync(id);
            if (toDo?.Id is null) return;
            var children = await GetChildrenAsync(toDo.Id);
            if (children is not null)
            {
                await MarkAsDeletedAsync(children);
            }
            _toDoRepository.MarkAsDeleted(toDo);
        }

        private async Task CreateOrUpdateAsync(List<ToDo> toDos, Guid parentId)
        {
            if (toDos is null) return;
            foreach (var toDo in toDos)
            {
                toDo.ToDoId = parentId;
                toDo.ToDoListId = null;
                await CreateOrUpdateAsync(toDo);
                await CreateOrUpdateAsync(toDo.Children, toDo.Id);
            }
        }

        private async Task CreateOrUpdateAsync(ToDo toDo)
        {
            var toDoInDb = await _toDoRepository.GetByIdAsync(toDo.Id);
            if (toDoInDb is null)
            {
                toDo.Initialize();
                await _toDoRepository.CreateAsync(toDo);
                return;
            }

            toDoInDb.Name = toDo.Name;
            toDoInDb.IsDone = toDo.IsDone;
            toDoInDb.Index = toDo.Index;
            toDoInDb.ToDoId = toDo.ToDoId;
            toDoInDb.ToDoListId = toDo.ToDoListId;
            await _toDoRepository.UpdateAsync(toDoInDb);
        }

        private async Task<List<ToDo>> PopulateToDosChildrenAsync(List<ToDo> toDos)
        {
            foreach (var toDo in toDos)
            {
                toDo.Children = await GetChildrenAsync(toDo.Id);
                if (toDo.Children is null) continue;
                toDo.Children = await PopulateToDosChildrenAsync(toDo.Children);
            }

            return toDos;
        }

        private async Task MarkAsDeletedAsync(List<ToDo> toDos)
        {
            foreach (var toDo in toDos)
            {
                toDo.Children = await GetChildrenAsync(toDo.Id);
                if (toDo.Children is not null)
                {
                    await MarkAsDeletedAsync(toDo.Children);
                }
                _toDoRepository.MarkAsDeleted(toDo);
            }
        }

        private async Task<List<ToDo>?> GetChildrenAsync(Guid parentId)
        {
            var children = await _toDoRepository.GetByParentIdAsync(parentId);

            return children;
        }
    }
}
