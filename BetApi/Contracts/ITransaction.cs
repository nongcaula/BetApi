using BetApi.Model;
using Microsoft.AspNetCore.Mvc;

namespace BetApi.Services
{
    public interface ITransaction
    {
        public IEnumerable<Transaction> GetTransactions();
        public string SaveTransactions(List<Transaction> transactions);
        public string Delete(int id);
        public Transaction Get(int id);
        public string Update(int id,Transaction transaction);
    }
}
