using Microsoft.AspNetCore.Mvc;
using Portal.Data.Models;
using Portal.Services.ModelServices;
using Portal.Services.ModelServices.Invoices;

namespace Portal.Controllers
{
    [Route("api/v1/plans")]
    public class PlanApiController : BaseCrudApiController<Plan>
    {
        public PlanApiController(PlanModelService modelService) : base(modelService)
        {
        }
    }
}
