namespace pos.core.Models
{

    public class Paging
    {
        public class Request<T>
        {
            public int CurrentPage { get; set; } = 0;

            private string _keyword;
            public string Keyword
            {
                get { return _keyword?.Trim()?.ToLower(); }
                set { _keyword = value; }
            }

            private string _sortBy;
            public string SortBy
            {
                get { return _sortBy?.Trim()?.ToLower(); }
                set { _sortBy = value; }
            }

            public string SortDir { get; set; }

            public T Query { get; set; }

            public bool Searchable => !string.IsNullOrWhiteSpace(Keyword);
            public bool Sortable => !string.IsNullOrWhiteSpace(SortBy);
            public bool SortAsc => SortDir?.ToLower() == "asc";
        }

        public class Response<T>
        {
            public List<T> Records { get; private set; }
            public Metadata Metadata { get; private set; }

            public Response(List<T> records, int total, int current)
            {
                Records = records;
                Metadata = new Metadata(current, total);
            }
        }

        public class Metadata
        {
            public int CurrentPage { get; set; }
            public int Count { get; set; }
            public int ItemPerPage { get; set; }

            public Metadata(int current, int total)
            {
                CurrentPage = current;
                Count = total;
                ItemPerPage = 20;
            }
        }
    }

}
