using Portal.Data.Models.BaseModels;

namespace Portal.Data.Models
{
    public class Invoice : BaseModel
    {
        public DateTime DueDate { get; set; }
        public string CustomerName { get; set; } = null!;
        public List<Order> Orders { get; set; } = null!;
        public List<Performance> Performances { get; set; } = null!;
    }

    public class Performance
    {
        public int Audience { get; set; }
        public string PlayId { get; set; } = null!;
    }

    public class Order
    {
        public decimal Amount { get; set; }
    }
}