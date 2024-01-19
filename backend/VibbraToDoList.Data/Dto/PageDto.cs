namespace VibbraToDoList.Data.Dto
{
    public class PageDto<T>
    {
        public int Total { get; set; }
        public List<T> Items { get; set; }
    }
}
