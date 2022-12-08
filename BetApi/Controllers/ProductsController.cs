using BetApi.Contracts;
using BetApi.Model;
using BetApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace BetApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _product;
        
        public ProductController(IProductService productService)
        {
            _product = productService ?? throw new NullReferenceException(typeof(IProductService).Name);
        }
        [HttpGet]
        public IEnumerable<Product> Get()
        {
           return _product.Get();
        }
        [HttpGet("{id}")]
        public Product Get(int id)
        {
            return _product.Get(id);
        }
        [HttpPost]
        public string Post([FromBody] Product product)
        {
           return _product.Post(product);
        }
        [HttpPut("{id}")]
        public string Put(int id, [FromBody] Product product)
        {
           return _product.Put(id, product);   
        }
        [HttpDelete("{id}")]
        public string Delete(int id)
        {
           return _product.Delete(id);
        }
    }
}
