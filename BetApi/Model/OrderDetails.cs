namespace BetApi.Model
{
    public class OrderDetails
    {
        public IQueryable<OrderModel> Order { get; set; }
        public int OrderId { get; set; }
        public decimal SubTotal { get; set; }
    }
}
