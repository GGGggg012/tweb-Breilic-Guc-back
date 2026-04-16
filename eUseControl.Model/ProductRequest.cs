using System.ComponentModel.DataAnnotations;

namespace eUseControl.Model
{
    public class ProductRequest
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        [Required]
        [Range(0.01, 999999.99)]
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}
