using BetApi.Model;

namespace BetApi.Services
{
    public interface IOrderService
    {
        public int GetOrderNumber(Transaction transaction);
        public void UpdateStatus(int orderNo);
    }
}
