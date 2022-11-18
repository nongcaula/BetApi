using BetApi.EmailService;
using BetApi.Model;
using Microsoft.AspNetCore.Mvc;

namespace BetApi.Services
{
    public class TransactionManager : IEcommerceService
    {
        readonly ApplicationContext _context;
    
        public TransactionManager(ApplicationContext applicationContext)
        {
            _context = applicationContext;
        }
        public string Delete(int id)
        {
            var result = "Record Added Successfully";
            var transaction = _context.Transactions.Find(id);
            if (transaction != null)
            {
                try
                {
                    _context.Remove(transaction);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    result = ex.Message;
                }
            }
            return result;
        }

        public void EmailOrder(int orderNo)
        {
            var orders = _context.Orders.Where(x => x.OrderNo == orderNo);
            var model = from o in orders
                        join u in _context.Customers on o.UserId equals u.Id
                        join t in _context.Transactions on o.OrderNo equals t.OrderNo
                        join p in _context.Products on t.ProductId equals p.Id
                        join od in _context.Transactions on p.Id equals od.ProductId
                        where o.OrderNo == orderNo && od.OrderNo == orderNo && t.OrderNo == orderNo
                        select new OrderModel()
                        {
                            ProductName = p.Name.Trim(),
                            ProductDescription = p.Description.Trim(),
                            ProductPrice = p.Price,
                            OderdedQuantity = od.Quantity,
                            ItemPrice = p.Price * od.Quantity
                        };
            var total = model.Sum(x => x.ItemPrice);
            var email = _context.Customers.Where(s => s.Id == orders.FirstOrDefault().UserId).FirstOrDefault();
            OrderDetails order = new()
            {
                Order = model,
                OrderId = orderNo,
                SubTotal = total
            };
            var getEmailBody = Email.GenerateEmailBody(order);
            var subject = "Confirmation of Order : " + orderNo;
            Email.SendEmail(email.Email, subject, getEmailBody);
            UpdateStatus(orderNo);
        }

        public Transaction Get(int id)
        {
            return _context.Transactions.Find(id);
        }

        public int GetOrderNumber(Transaction transaction)
        {
            Order order = new()
            {
                UserId = transaction.UserId,
                DateCreated = DateTime.Now,
                IsProcessed = false
            };
            _context.Orders.Add(order);
            _context.SaveChanges();

            return order.OrderNo;
        }

        public IEnumerable<Transaction> GetTransactions()
        {
            return _context.Transactions;
        }

        public string SaveTransactions(List<Transaction> transactions)
        {
            try
            {
                var orderNumber = GetOrderNumber(transactions[0]);
                transactions.ForEach(transaction =>
                {
                    transaction.OrderNo = orderNumber;
                    _context.Transactions.Add(transaction);
                });
                _context.SaveChanges();
                UpdateStock(transactions);
                EmailOrder(orderNumber);
                return "Record Added Successfully";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        public string Update(int id, Transaction transaction)
        {
            var result = "Record Updated Successfully";
            var trans = _context.Transactions.Find(id) ?? new Transaction();
            if (trans != null)
            {
                try
                {
                    _context.Transactions.Update(transaction);
                    _context.SaveChanges(true);
                }
                catch (Exception ex)
                {
                    result = ex.Message;
                }
            }
            return result;
        }

        public void UpdateStatus(int orderNo)
        {
            var order = _context.Orders.Find(orderNo);
            order.IsProcessed = true; // Emailed to client
            _context.SaveChanges();
        }

        public void UpdateStock(List<Transaction> transactions)
        {
            transactions.ForEach(prd =>
            {
                var product = _context.Products.Find(prd.ProductId);
                product.Quantity = (product.Quantity - prd.Quantity);
                if (product.Quantity > 0)
                    _context.SaveChanges();

            });
        }
    }
}
