using Portal.Data.Models.BaseModels;

namespace Portal.Data.Models
{
    public class Plan : BaseModel
    {
        public DateTime SummerStart { get; set; }
        public DateTime SummerEnd { get; set; }
        public double SummerRate { get; set; }
        public double RegularRate { get; set; }
        public double RegularServiceCharge { get; set; }
    }
}
