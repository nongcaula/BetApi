using System.ComponentModel.DataAnnotations;

namespace BetApi.Model
{
    public class Order
    {
        [Key]
        public int OrderNo { get; set; }
        public string UserId { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsProcessed { get; set; }
    }
}