using Microsoft.AspNetCore.Mvc;
using Portal.Data.Models.BaseModels;
using Portal.Services.ModelServices;

namespace Portal.Controllers;

[ApiController]
public abstract class BaseCrudApiController<TModel> : ControllerBase where TModel : BaseModel
{
    protected BaseModelService<TModel> ModelService;

    public BaseCrudApiController(BaseModelService<TModel> modelService)
    {
        ModelService = modelService;
    }

    [HttpGet]
    public virtual Task<List<TModel>> GetAll() => ModelService.GetAllAsync();

    [HttpGet("{id:length(24)}")]
    public virtual Task<TModel> Get(string id) => ModelService.GetAsync(id);

    [HttpPost]
    public virtual Task<TModel> Post([FromBody] TModel model) => ModelService.CreateAsync(model);

    [HttpPut("{id:length(24)}")]
    public virtual Task Put(string id, [FromBody] TModel model) => ModelService.UpdateAsync(id, model);

    [HttpDelete("{id:length(24)}")]
    public virtual Task Delete(string id) => ModelService.DeleteAsync(id);
}