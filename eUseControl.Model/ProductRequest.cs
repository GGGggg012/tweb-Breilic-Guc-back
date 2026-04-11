using System.ComponentModel.DataAnnotations;

namespace eUseControl.Model
{
    public class ProductRequest
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        [Required]
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}
