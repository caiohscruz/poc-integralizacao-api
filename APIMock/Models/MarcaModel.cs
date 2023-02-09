using APIMock.ViewModels;

namespace APIMock.Models
{
    public class MarcaModel : CSVReader<MarcaViewModel>
    {
        public MarcaModel()
        {
            _csvName = "Marcas";
        }
        protected override MarcaViewModel FromCsv(string csvLine)
        {
            string[] values = csvLine.Split(',');
            MarcaViewModel vm = new MarcaViewModel();
            vm.Codigo = values[0];
            vm.Nome = values[1];           
            vm.Referencia = values[2];           
            return vm;
        }
    }
}
