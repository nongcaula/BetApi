using System.ComponentModel.DataAnnotations;

namespace BetApi.Model
{
    public class Customer
    {
        [Key]
        public string Id { get; set; }
        public string Email { get; set; }
 
    }
}