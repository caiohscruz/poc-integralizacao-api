using APIMock.ViewModels;

namespace APIMock.Models
{
    public class CampusModel : CSVReader<CampusViewModel>
    {
        public CampusModel()
        {
            _csvName = "Campi";
        }
        protected override CampusViewModel FromCsv(string csvLine)
        {
            string[] values = csvLine.Split(',');
            CampusViewModel vm = new CampusViewModel();
            vm.Codigo = values[0];
            vm.Nome = values[1];           
            vm.Referencia = values[2];           
            return vm;
        }
    }
}
