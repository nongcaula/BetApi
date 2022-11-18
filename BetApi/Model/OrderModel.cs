namespace BetApi.Model
{
    public class OrderModel
    {
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal ProductPrice { get; set; }
        public int OderdedQuantity { get; set; }
        public decimal ItemPrice { get; set; }

    }
}
