using BetApi.Model;
using Microsoft.AspNetCore.Mvc;

namespace BetApi.Contracts
{
    public interface IProductService
    {
        public IEnumerable<Product> Get();
        public Product Get(int id);
        public string Post(Product product);
        public string Put(int id, Product product);
        public string Delete(int id);
    }
}
