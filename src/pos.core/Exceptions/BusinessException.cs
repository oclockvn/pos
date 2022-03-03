namespace pos.core.Exceptions
{
    [Serializable]
    public class BusinessException : Exception
    {
        public StatusCode StatusCode { get; set; }

        public BusinessException(StatusCode statusCode) : base($"An exception occurred: {statusCode}")
        {
            StatusCode = statusCode;
        }

        protected BusinessException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
