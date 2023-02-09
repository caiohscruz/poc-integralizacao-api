using APIMock.ViewModels;

namespace APIMock.Models
{
    public class RegionalModel : CSVReader<RegionalViewModel>
    {
        public RegionalModel()
        {
            _csvName = "Regionais";
        }
        protected override RegionalViewModel FromCsv(string csvLine)
        {
            string[] values = csvLine.Split(',');
            RegionalViewModel regional = new RegionalViewModel();
            regional.Codigo = values[0];
            regional.Nome = values[1];           
            return regional;
        }
    }
}
