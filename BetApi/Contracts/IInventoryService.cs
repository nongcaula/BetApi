

using BetApi.Model;

namespace BetApi.Services
{
    public interface IInventoryService
    {
        public void UpdateStock(List<Transaction> transactions);

    }
}
