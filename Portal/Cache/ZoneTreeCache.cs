using Portal.Data.Models.BaseModels;
using Tenray.ZoneTree;
using Tenray.ZoneTree.Serializers;

namespace Portal.Cache
{
    public class ZoneTreeCache<TModel> : IDisposable where TModel : BaseModel
    {
        private readonly IZoneTree<string, TModel> _zoneTree;

        public ZoneTreeCache()
        {
            _zoneTree = new ZoneTreeFactory<string, TModel>()
                .SetValueSerializer(new Serializer<TModel>())
                .OpenOrCreate();
        }

        public TModel Get(string key)
        {
            _zoneTree.TryGet(key, out var model);
            return model;
        }

        public bool Delete(string key)
        {
            return _zoneTree.TryDelete(key);
        }

        public void Set(TModel model)
        {
            _zoneTree.Upsert(model.Id, model);
        }

        public void Dispose()
        {
            _zoneTree.Dispose();
        }
    }

    public class Serializer<TModel> : ISerializer<TModel> where TModel : BaseModel
    {
        public TModel Deserialize(byte[] bytes)
        {
            using (var ms = new MemoryStream(bytes))
            {
                return System.Text.Json.JsonSerializer.Deserialize<TModel>(ms)!;
            }
        }

        public byte[] Serialize(in TModel entry)
        {
            return System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(entry);
        }
    }
}
