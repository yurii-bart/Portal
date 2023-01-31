using Microsoft.Extensions.Options;
using Portal.Cache;
using Portal.Data;
using Portal.Data.Models;

namespace Portal.Services.ModelServices.Plays
{
    public class PlayModelService : BaseModelService<Play>
    {
        private readonly ZoneTreeCache<Play> _cache;
        public PlayModelService(ZoneTreeCache<Play> cache, IOptions<PortalDatabaseSettings> portalDatabaseSettings) : base(portalDatabaseSettings)
        {
            _cache = cache;
        }

        public override async Task<Play> CreateAsync(Play model)
        {
            await base.CreateAsync(model);
            _cache.Set(model);
            return model;
        }

        public override Task UpdateAsync(string id, Play updatedModel)
        {
            _cache.Set(updatedModel);
            return base.UpdateAsync(id, updatedModel);
        }

        public override Task DeleteAsync(string id)
        {
            _cache.Delete(id);
            return base.DeleteAsync(id);
        }

        public override async Task<Play> GetAsync(string id)
        {
            var model = _cache.Get(id);
            if (model == null)
            {
                model = await base.GetAsync(id);
                _cache.Set(model);
            }

            return model;
        }
    }
}
