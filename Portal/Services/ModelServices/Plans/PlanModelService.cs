using Microsoft.Extensions.Options;
using Portal.Data;
using Portal.Data.Models;

namespace Portal.Services.ModelServices.Invoices
{
    public class PlanModelService : BaseModelService<Plan>
    {
        public PlanModelService(IOptions<PortalDatabaseSettings> portalDatabaseSettings) : base(portalDatabaseSettings)
        {
        }
    }
}
