using BetApi.Contracts;
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

        private readonly ITransaction _transactionService;

        public TransactionsController(ITransaction transaction)
        {
            _transactionService = transaction?? throw new NullReferenceException(typeof(ITransaction).Name);
        }
        [HttpGet]
        public IEnumerable<Transaction> Get()
        {
            return _transactionService.GetTransactions();
        }
        [HttpGet("{id}")]
        public Transaction Get(int id)
        {
            return _transactionService.Get(id);
        }
        [HttpPost]
        public string Post(List<Transaction> transactions)
        {
            return _transactionService.SaveTransactions(transactions);
        }

        [HttpPut("{id}")]
        public string Put(int id, [FromBody] Transaction transaction)
        {
            return _transactionService.Update(id, transaction);
        }

        [HttpDelete("{id}")]
        public string Delete(int id)
        {
            return _transactionService.Delete(id);
        }
    }
}
