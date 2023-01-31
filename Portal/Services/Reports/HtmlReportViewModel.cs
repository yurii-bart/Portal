using Portal.Data.Models;

namespace Portal.Services.Reports
{
    public class HtmlReportViewModel
    {
        public HtmlReportViewModel(Invoice invoice, List<Play> plays)
        {
            Invoice = invoice;
            Plays = plays;
        }

        public Invoice Invoice { get; set; }
        public List<Play> Plays { get; set; }
    }
}
