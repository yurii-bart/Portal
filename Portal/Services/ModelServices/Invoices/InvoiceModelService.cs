using Microsoft.Extensions.Options;
using Portal.Data;
using Portal.Data.Models;

namespace Portal.Services.ModelServices.Invoices
{
    public class InvoiceModelService : BaseModelService<Invoice>
    {
        public InvoiceModelService(IOptions<PortalDatabaseSettings> portalDatabaseSettings) : base(portalDatabaseSettings)
        {
        }
    }
}
