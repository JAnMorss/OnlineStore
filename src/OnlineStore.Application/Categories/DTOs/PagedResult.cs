namespace OnlineStore.Application.Categories.DTOs
{
    public sealed class PagedResult<T>
    {
        public List<T> Items { get; }
        public int TotalCount { get; }

        public PagedResult(List<T> items, int totalCount)
        {
            Items = items;
            TotalCount = totalCount;
        }
    }
}
