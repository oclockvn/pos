namespace pos.core.Models
{
    public class PagingMetadata
    {
        public int CurrentPage { get; set; }
        public int Count { get; set; }
        public int ItemPerPage { get; set; }

        public PagingMetadata(int current, int total)
        {
            CurrentPage = current;
            Count = total;
            ItemPerPage = 20;
        }
    }

    public class Paging<T>
    {
        public List<T> Records { get; set; }
        public PagingMetadata Metadata { get; set; }
    }

    public class SearchInfo
    {
        public int CurrentPage { get; set; } = 0;
        public string Keyword { get; set; }
        public string SortBy { get; set; }
        public string SortDir { get; set; }
    }
}
