namespace OnlineStore.Application.Abstractions.PageSize
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

        public static PagedResult<T> Create(List<T> items, int totalCount)
            => new(items, totalCount);
    }
}
