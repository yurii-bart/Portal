using Portal.Data.Enums;
using Portal.Data.Models.BaseModels;

namespace Portal.Data.Models
{
    public class Play : BaseModel
    {
        public PlayType Type { get; set; }
        public string Name { get; set; } = null!;
    }
}