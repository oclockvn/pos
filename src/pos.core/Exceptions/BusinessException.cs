namespace pos.core.Exceptions
{
    [Serializable]
    public class BusinessException : Exception
    {
        public StatusCode StatusCode { get; set; }

        public BusinessException(StatusCode statusCode, string message = null) : base($"An exception occurred: {statusCode}{(!string.IsNullOrWhiteSpace(message) ? " due to reason: " + message : string.Empty)}")
        {
            StatusCode = statusCode;
        }

        protected BusinessException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
