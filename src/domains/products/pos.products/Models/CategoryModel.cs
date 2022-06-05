using System.ComponentModel.DataAnnotations;

namespace pos.products.Models
{
    public class CategoryCreate
    {
        public class Request
        {
            [Required]
            [MaxLength(120)]
            public string Name { get; set; }
        }

        public class Response
        {
            public long Id { get; set; }
        }
    }

    public class CategoryList
    {
        public class Request
        {
        }

        public class Response
        {
            public long Id { get; set; }
            public string Name { get; set; }
        }
    }
}
