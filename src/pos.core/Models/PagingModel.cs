namespace pos.core.Models
{
    public class PagingMetadata
    {
        public int CurrentPage { get; set; }
        public int Count { get; set; }
        public int ItemPerPage { get; set; }
    }

    public class Paging<T>
    {
        public List<T> Records { get; set; }
        public PagingMetadata Metadata { get; set; }
    }
}
