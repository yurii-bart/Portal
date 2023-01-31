namespace Portal.Tests
{
    [TestCaseOrderer("Portal.Tests.Priority.PriorityOrderer", "Portal.Tests")]
    public class PlanControllerTests : BaseCrudApiControllerTests<Plan>, IClassFixture<ApiWebApplicationFactory>
    {
        public PlanControllerTests(ApiWebApplicationFactory application) : base(application)
        {
        }

        protected override string Id => "000000000141e35df58e9a40";

        protected override string Uri => "api/v1/plans";

        protected override Plan CreateModel()
        {
            var faker = new Faker();
            return new Plan()
            {
                Id = Id,
                RegularRate = faker.Random.Double(),
                RegularServiceCharge = faker.Random.Double(),
                SummerEnd = DateTime.UtcNow.AddDays(10),
                SummerStart = DateTime.UtcNow.AddDays(5),
                SummerRate = faker.Random.Double()
            };
        }
    }
}