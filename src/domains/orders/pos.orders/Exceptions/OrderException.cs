using pos.core;
using pos.core.Exceptions;

namespace pos.orders.Exceptions
{
    public class OrderException : BusinessException
    {
        public OrderException(StatusCode statusCode) : base(statusCode)
        {
        }
    }
}
