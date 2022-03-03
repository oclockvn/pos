using Light.GuardClauses;
using Microsoft.EntityFrameworkCore;
using pos.common.extensions;
using pos.core.Data;
using pos.orders.Exceptions;
using pos.orders.Models;

namespace pos.orders.Services
{
    public interface IOrderService
    {
        /// <summary>
        /// Create the order
        /// </summary>
        /// <param name="request"><see cref="CreateOrder.Request"/></param>
        /// <returns><see cref="CreateOrder.Response"/></returns>
        Task<CreateOrder.Response> CreateOrderAsync(CreateOrder.Request request);
    }

    public class OrderService : IOrderService
    {
        private readonly ITenantDbContextFactory _tenantDbContextFactory;

        public OrderService(ITenantDbContextFactory dbContextFactory)
        {
            _tenantDbContextFactory = dbContextFactory;
        }

        public async Task<CreateOrder.Response> CreateOrderAsync(CreateOrder.Request request)
        {
            request.MustNotBeNull();
            request.Items.MustHaveMinimumCount(1);

            using var context = _tenantDbContextFactory.CreateDbContext();

            var ids = request.Items.Select(x => x.Id);
            var products = await context.Products
                .Where(p => ids.Contains(p.Id))
                .Select(x => new ValidatableProduct
                {
                    Id = x.Id,
                    Sku = x.Sku,
                    ImportPrice = x.ImportPrice,
                    SalesPrice = x.SalesPrice,
                    WholesalesPrice = x.WholesalesPrice,
                    Tax = 0, // todo: config product tax
                })
                .ToListAsync();

            var order = new core.Entities.Order
            {
                OrderNumber = Guid.NewGuid().ToString()[..8], // todo: gen order #
                OrderStatus = core.Enums.OrderStatus.Authorized,
                Total = 0
            };

            context.Orders.Add(order);
            order.OrderItems.AddRange(request.Items.Select(i => ValidateAndMapOrderItem(products, i)));

            order.Total = order.OrderItems.Sum(i => i.Total);
            order.TotalWithTax = order.OrderItems.Sum(i => i.TotalWithTax);

            await context.SaveChangesAsync();

            // todo: add inventory history

            return new CreateOrder.Response();
        }

        private core.Entities.OrderItem ValidateAndMapOrderItem(List<ValidatableProduct> products, CreateOrder.ProductItem cartItem)
        {
            var product = products.Single(p => p.Id == cartItem.Id);
            product.MustNotBeNullReference();

            if (string.Equals(product.Sku, cartItem.Sku, StringComparison.InvariantCultureIgnoreCase) == false)
            {
                throw new OrderException(core.StatusCode.Shopping_cart_product_sku_mis_match);
            }

            var total = cartItem.Qty * product.SalesPrice;
            if (total != cartItem.Total)
            {
                throw new OrderException(core.StatusCode.Shopping_cart_product_price_mis_match);
            }

            // valid already
            return new core.Entities.OrderItem
            {
                DiscountPercentage = 0, // todo: set discount
                DiscountPrice = 0,
                ImportPrice = product.ImportPrice,
                ProductId = product.Id,
                Qty = cartItem.Qty,
                SalesPrice = product.SalesPrice,
                Sku = cartItem.Sku,
                Tax = product.Tax,
                Total = total,
                TotalWithTax = total.WithTax(product.Tax),
                UnitPrice = product.SalesPrice,
                WholesalesPrice = product.WholesalesPrice
            };
        }

        struct ValidatableProduct
        {
            public long Id { get; set; }
            public string Sku { get; set; }
            public decimal ImportPrice { get; set; }
            public decimal SalesPrice { get; set; }
            public decimal WholesalesPrice { get; set; }
            public float Tax { get; set; }
        }
    }
}
