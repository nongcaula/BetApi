using BetApi.EmailService;
using BetApi.Model;
using BetApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BetApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {

        private readonly IEcommerceService _ecommerceService;
        readonly ApplicationContext _context;
        public TransactionsController(ApplicationContext applicationContext)
        {
            _context= applicationContext;
            _ecommerceService = new TransactionManager(_context);
        }
        [HttpGet]
        public IEnumerable<Transaction> Get()
        {
            return _ecommerceService.GetTransactions();
        }
        [HttpGet("{id}")]
        public Transaction Get(int id)
        {
            return _ecommerceService.Get(id);
        }
        [HttpPost]
        public string Post(List<Transaction> transactions)
        {
            return _ecommerceService.SaveTransactions(transactions);
        }

        private int GetOrderNumber(Transaction transaction)
        {
            return _ecommerceService.GetOrderNumber(transaction);
        }
        private void UpdateStock(List<Transaction> transactions)
        {
            _ecommerceService.UpdateStock(transactions);
        }
        [HttpPut("{id}")]
        public string Put(int id, [FromBody] Transaction transaction)
        {
            return _ecommerceService.Update(id, transaction);
        }
        private void EmailOrder(int orderNo)
        {
            _ecommerceService.EmailOrder(orderNo);
        }
        private void UpdateStatus(int orderNo)
        {
            _ecommerceService.UpdateStatus(orderNo);
        }
        [HttpDelete("{id}")]
        public string Delete(int id)
        {
            return _ecommerceService.Delete(id);
        }
    }
}
