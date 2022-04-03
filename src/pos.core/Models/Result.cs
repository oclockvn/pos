namespace pos.core.Models
{
    public class Result<T>
    {
        public StatusCode? StatusCode { get; private set; }
        public T Data { get; private set; }

        public bool IsOk => StatusCode == null;

        public Result(StatusCode statusCode)
        {
            StatusCode = statusCode;
        }

        public Result(T data)
        {
            Data = data;
        }
    }
}
