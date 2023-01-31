using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Portal.Data;
using Portal.Data.Models.BaseModels;
using Portal.Exceptions;

namespace Portal.Services.ModelServices
{
    public abstract class BaseModelService<TModel> where TModel : BaseModel
    {
        private readonly IMongoCollection<TModel> _modelsCollection;

        public BaseModelService(IOptions<PortalDatabaseSettings> portalDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                portalDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                portalDatabaseSettings.Value.DatabaseName);

            _modelsCollection = mongoDatabase.GetCollection<TModel>(typeof(TModel).Name);
        }

        public virtual Task<List<TModel>> GetAllAsync() =>
            _modelsCollection.Find(_ => true).ToListAsync();

        public virtual async Task<TModel> GetAsync(string id) 
        {
            var model = await _modelsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
            if (model == null) 
            {
                throw new ModelNotFoundException<TModel>(id);
            }

            return model;
        }
           

        public virtual async Task<TModel> CreateAsync(TModel model)
        {
            if (string.IsNullOrEmpty(model.Id))
            {
                model.Id = ObjectId.GenerateNewId().ToString();
            }

            await _modelsCollection.InsertOneAsync(model);

            return model;
        }

        public virtual Task UpdateAsync(string id, TModel updatedModel) =>
            _modelsCollection.ReplaceOneAsync(x => x.Id == id, updatedModel);

        public virtual Task DeleteAsync(string id) =>
            _modelsCollection.DeleteOneAsync(x => x.Id == id);
    }
}
