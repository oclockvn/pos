using MediatR;
using pos.core.Models;
using pos.products.Models;
using pos.products.Services;

namespace pos.products.Handlers
{
    public class InventoryCreateHandler : IRequestHandler<InventoryCreate.Request, Result<InventoryCreate.Response>>
    {
        private readonly IInventoryService _inventoryService;
        public InventoryCreateHandler(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        public async Task<Result<InventoryCreate.Response>> Handle(InventoryCreate.Request request, CancellationToken cancellationToken)
        {
            var result = await _inventoryService.CreateInventoryAsync(request);
            return result;
        }
    }
}
