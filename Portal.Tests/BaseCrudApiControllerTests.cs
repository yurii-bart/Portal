using Newtonsoft.Json.Serialization;
using Portal.Data.Models.BaseModels;
using Portal.Tests.Priority;
using System.Net.Http.Json;

namespace Portal.Tests
{
    [Collection("CrudTests")]
    [TestCaseOrderer("Portal.Tests.Priority.PriorityOrderer", "Portal.Tests")]
    public abstract class BaseCrudApiControllerTests<TModel> : IClassFixture<ApiWebApplicationFactory>
        where TModel : BaseModel
    {
        protected HttpClient Client { get; }
        protected abstract string Id { get; }
        protected abstract string Uri { get; }

        public BaseCrudApiControllerTests(ApiWebApplicationFactory application)
        {
            Client = application.CreateClient();
        }

        protected abstract TModel CreateModel();

        protected string SerializeObject(TModel model)
        {
            DefaultContractResolver contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };
            var settings = new JsonSerializerSettings
            {
                ContractResolver = contractResolver,
                Formatting = Formatting.None,
                DateFormatString = "yyyy-MM-ddTHH:mm:ss.fffZ"
            };
            return JsonConvert.SerializeObject(model, settings);
        }

        [Fact, TestPriority(1)]
        public async Task Should_Create()
        {
            var expected = CreateModel();
            
            var result = await Client.PostAsJsonAsync(Uri, expected);
            result.StatusCode.Should().Be(HttpStatusCode.OK);

            var actual = await result.Content.ReadFromJsonAsync<TModel>();

            var actualJson = SerializeObject(actual);
            var expectedJson = SerializeObject(expected);

            Assert.Equal(expectedJson, actualJson);
        }

        [Fact, TestPriority(2)]
        public async Task Should_Get()
        {
            var result = await Client.GetAsync($"{Uri}/{Id}");
            result.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact, TestPriority(3)]
        public async Task Should_Update()
        {
            var updated = CreateModel();

            var put = await Client.PutAsJsonAsync($"{Uri}/{Id}", updated);
            put.StatusCode.Should().Be(HttpStatusCode.OK);

            var actual = await Client.GetFromJsonAsync<TModel>($"{Uri}/{Id}");

            var actualJson = SerializeObject(actual);
            var expectedJson = SerializeObject(updated);

            Assert.Equal(expectedJson, actualJson);
        }

        [Fact, TestPriority(4)]
        public async Task Should_Delete()
        {
            var result = await Client.DeleteAsync($"{Uri}/{Id}");
            result.StatusCode.Should().Be(HttpStatusCode.OK);

            var get = await Client.GetAsync($"{Uri}/{Id}");
            get.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}