using System;

namespace eUseControl.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; } = "Pending";
        public DateTime CreatedAt { get; set; }

        public void CalculateTotal(decimal unitPrice)
        {
            TotalPrice = unitPrice * Quantity;
        }
    }
}
