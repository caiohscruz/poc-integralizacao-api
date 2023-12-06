using APIMock.ViewModels;

namespace APIMock.Models
{
    public class ResponsavelModel : CSVReader<ResponsavelViewModel>
    {
        public ResponsavelModel()
        {
            _csvName = "Responsaveis";
        }
        protected override ResponsavelViewModel FromCsv(string csvLine)
        {
            string[] values = csvLine.Split(',');
            ResponsavelViewModel regional = new ResponsavelViewModel();
            regional.Codigo = values[0];
            regional.Nome = values[1];           
            return regional;
        }
    }
}
