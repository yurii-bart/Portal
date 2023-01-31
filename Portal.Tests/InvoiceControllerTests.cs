using Bogus;
using Bogus.DataSets;
using Portal.Services.Reports;
using Portal.Tests.Priority;
using System.Net.Http.Json;

namespace Portal.Tests
{
    [TestCaseOrderer("Portal.Tests.Priority.PriorityOrderer", "Portal.Tests")]
    public class InvoiceControllerTests : BaseCrudApiControllerTests<Invoice>, IClassFixture<ApiWebApplicationFactory>
    {
        public InvoiceControllerTests(ApiWebApplicationFactory application) : base(application)
        {

        }

        protected override string Id => "000000000141e35df58e9a38";

        protected override string Uri => "api/v1/invoices";

        protected override Invoice CreateModel()
        {
            var faker = new Faker();
            return new Invoice()
            {
                Id = Id,
                CustomerName = faker.Name.FirstName(),
                DueDate = DateTime.UtcNow.AddDays(faker.Random.Int(1, 100)),
                Performances = new List<Performance>()
                {
                    new Performance
                    {
                        Audience = faker.Random.Int(1, 100),
                        PlayId = ObjectId.GenerateNewId().ToString()
                    }
                },
                Orders = new List<Order>
                {
                    new Order
                    {
                        Amount = faker.Random.Decimal(1, 100)
                    }
                }
            };
        }

        [Fact, TestPriority(5)]
        public async Task Should_Generate_Plain_Text_Report()
        {
            var id = await PrepareReportData();

            var result = await Client.GetAsync($"{Uri}/{id}/report?type={ReportType.PlainText}");
            result.StatusCode.Should().Be(HttpStatusCode.OK);

            var report = await result.Content.ReadAsStringAsync();
        }

        [Fact, TestPriority(6)]
        public async Task Should_Generate_Plain_Html_Report()
        {
            var id = await PrepareReportData();

            var result = await Client.GetAsync($"{Uri}/{id}/report?type={ReportType.Html}");
            result.StatusCode.Should().Be(HttpStatusCode.OK);

            var report = await result.Content.ReadAsStringAsync();
        }

        private async Task<string> PrepareReportData()
        {
            var invoice = CreateModel();
            invoice.Id = ObjectId.GenerateNewId().ToString();
            await Client.PostAsJsonAsync("api/v1/invoices", invoice);

            var play = new Play
            {
                Id = invoice.Performances.First().PlayId,
                Name = invoice.CustomerName,
                Type = PlayType.Tragedy
            };
            await Client.PostAsJsonAsync("api/v1/plays", play);

            return invoice.Id;
        }
    }
}