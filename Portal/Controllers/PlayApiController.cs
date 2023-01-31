using Microsoft.AspNetCore.Mvc;
using Portal.Data.Models;
using Portal.Services.ModelServices;
using Portal.Services.ModelServices.Plays;

namespace Portal.Controllers
{
    [Route("api/v1/plays")]
    public class PlayApiController : BaseCrudApiController<Play>
    {
        public PlayApiController(PlayModelService modelService) : base(modelService)
        {
        }
    }
}
