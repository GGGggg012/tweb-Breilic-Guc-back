namespace eUseControl.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Stock { get; set; }

        public bool IsAvailable()
        {
            return Stock > 0;
        }

        public void DecreaseStock(int amount)
        {
            if (amount > Stock)
                throw new Exception("Not enough stock");
            Stock -= amount;
        }
    }
}
