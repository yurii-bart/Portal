namespace Portal.Tests
{
    [TestCaseOrderer("Portal.Tests.Priority.PriorityOrderer", "Portal.Tests")]
    public class PlayControllerTests : BaseCrudApiControllerTests<Play>, IClassFixture<ApiWebApplicationFactory>
    {
        public PlayControllerTests(ApiWebApplicationFactory application) : base(application)
        {
        }

        protected override string Id => "000000000141e35df58e9a39";

        protected override string Uri => "api/v1/plays";

        protected override Play CreateModel()
        {
            var faker = new Faker();
            return new Play()
            {
                Id = Id,
                Name = faker.Name.FirstName(),
                Type = faker.PickRandom<PlayType>()
            };
        }
    }
}