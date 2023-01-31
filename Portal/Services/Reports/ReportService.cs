using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Portal.Data;
using Portal.Data.Enums;
using Portal.Data.Models;
using Portal.Exceptions;
using RazorEngine.Templating;
using RazorEngine;
using System.Text;
using Microsoft.AspNetCore.Routing.Template;

namespace Portal.Services.Reports
{
    public class ReportService
    {
        private readonly IMongoCollection<Play> _playCollection;
        private readonly IMongoCollection<Invoice> _invoiceCollection;

        private const string _htmlTemplateKey = "invoice-report-template";
        private readonly string _htmlTemplatePath;

        public ReportService(IOptions<PortalDatabaseSettings> portalDatabaseSettings, IWebHostEnvironment hostEnvironment)
        {
            _htmlTemplatePath = Path.Combine(hostEnvironment.WebRootPath, "Report.cshtml");

            var mongoClient = new MongoClient(
                portalDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(
            portalDatabaseSettings.Value.DatabaseName);

            _playCollection = mongoDatabase.GetCollection<Play>(typeof(Play).Name);
            _invoiceCollection = mongoDatabase.GetCollection<Invoice>(typeof(Invoice).Name);
        }

        public async Task<string> GenerateReport(string id, ReportType reportType)
        {
            var invoice = await _invoiceCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
            if (invoice == null)
            {
                throw new ModelNotFoundException<Invoice>(id);
            }

            var playFilter = Builders<Play>.Filter
                .In(it => it.Id, invoice.Performances.Select(it => it.PlayId));
            var plays = await _playCollection.Find(playFilter).ToListAsync();

            string report = reportType switch
            {
                ReportType.PlainText => PlainTextReport(invoice, plays),
                ReportType.Html => HtmlReport(invoice, plays),
                _ => throw new Exception($"Report type: {reportType} is not supported")
            };

            return report;
        }

        private string HtmlReport(Invoice invoice, List<Play> plays)
        {
            try
            {
                var template = File.ReadAllText(_htmlTemplatePath);
                var html = Engine.Razor.RunCompile(
                    template, 
                    _htmlTemplateKey, 
                    typeof(HtmlReportViewModel), 
                    new HtmlReportViewModel(invoice, plays));
                return html;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

        private string PlainTextReport(Invoice invoice, List<Play> plays)
        {
            var totalAmount = 0.0;
            var volumeCredits = 0.0;

            StringBuilder report = new StringBuilder();
            report.AppendLine($"Statement for {invoice.CustomerName}");

            foreach (var performance in invoice.Performances)
            {
                var play = plays.First(it => it.Id == performance.PlayId);
                var amount = 0;
                switch (play.Type)
                {
                    case PlayType.Tragedy: 
                        amount = 40000;
                        if (performance.Audience > 30)
                        {
                            amount += 1000 * (performance.Audience - 30);
                        }

                        break;

                    case PlayType.Comedy:
                        amount = 30000;
                        if (performance.Audience > 20)
                        {
                            amount += 10000 + 500 * (performance.Audience - 20);
                        }

                        amount += 300 * performance.Audience;
                        break;

                    default:
                        throw new ApplicationException($"unknown type: {play.Type}");
                }

                // add volume credits
                volumeCredits += Math.Max(performance.Audience - 30, 0);

                // add extra credit for every ten comedy attendees
                if (play.Type == PlayType.Comedy) volumeCredits += Math.Floor(performance.Audience / 5.0);
                
                // print line for this order
                report.AppendLine($"{play.Name}: {string.Format("{0, 0:C2}", amount / 100)} ({performance.Audience} seats)");
                totalAmount += amount;
            }

            report.AppendLine($"Amount owed is {string.Format("{0, 0:C2}", totalAmount / 100)}");
            report.AppendLine($"You earned ${volumeCredits} credits");

            return report.ToString();
        }
    }
}
