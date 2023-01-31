using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Portal.Data.Models.BaseModels
{
    public abstract class BaseModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;
    }
}
