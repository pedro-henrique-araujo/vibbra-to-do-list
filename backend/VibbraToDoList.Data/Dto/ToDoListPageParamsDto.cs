namespace VibbraToDoList.Data.Dto
{
    public class ToDoListPageParamsDto
    {
        public int Skip { get; set; }
        public int Take { get; set; }
        public Guid UserId { get; set; }
    }
}
