using Microsoft.AspNetCore.Mvc;
using pos.orders.Models;
using pos.orders.Services;

namespace pos.web.Controllers
{
    [ApiController]
    public class OrdersController : ApplicationBaseController
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("pay")]
        public async Task<IActionResult> Pay(CreateOrder.Request request)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest();
            }

            var orderResult = await _orderService.CreateOrderAsync(request);
            return Ok(orderResult);
        }
    }
}
