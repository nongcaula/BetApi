using BetApi.Model;
using Microsoft.EntityFrameworkCore;

namespace BetApi.Contracts
{
    public class ProductManager : IProductService
    {
        readonly ApplicationContext _context;
        public ProductManager(ApplicationContext applicationContext) {
            _context = applicationContext;
        }
        public string Delete(int id)
        {
            var result = "Record Deleted Successfully";
            var trans = _context.Products.Where(s => s.Id == id);
            if (trans.Any())
            {
                try
                {
                    _context.Remove(trans.FirstOrDefault());
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    result = ex.Message;
                }
            }
            return result;
        }

        public IEnumerable<Product> Get()
        {
            return _context.Products.ToList();
        }

        public Product Get(int id)
        {
            return _context.Products.Find(id) ?? new Product();
        }

        public string Post(Product product)
        {
            try
            {
                _context.Products.Add(product);
                _context.SaveChanges();
                return "Record Added Successfully";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string Put(int id, Product product)
        {
            var result = "Record Updated Successfully";
            var trans = _context.Products.Where(s => s.Id == id);
            if (trans.Any())
            {
                try
                {
                    _context.Entry(product).State = EntityState.Modified;
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    result = ex.Message;
                }
            }
            return result;
        }
    }
}
