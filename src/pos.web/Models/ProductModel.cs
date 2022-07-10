using pos.products.Models;

namespace pos.web.Models
{
    public class ProductCreateUpdate : ProductCreate.Request
    {
        public IFormFileCollection Files { get; set; }
    }
}
