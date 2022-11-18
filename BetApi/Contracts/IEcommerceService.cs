namespace BetApi.Services
{
    public interface IEcommerceService : ITransaction, IOrderService, IInventoryService, IEmailService
    {
    }
}
