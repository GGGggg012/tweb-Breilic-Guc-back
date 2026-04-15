using System.ComponentModel.DataAnnotations;

namespace eUseControl.Model
{
    public class OrderRequest
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        [Range(1, 100)]
        public int Quantity { get; set; }
    }
}
