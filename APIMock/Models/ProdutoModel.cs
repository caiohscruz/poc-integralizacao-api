using APIMock.ViewModels;

namespace APIMock.Models
{
    public class ProdutoModel : CSVReader<ProdutoViewModel>
    {
        public ProdutoModel()
        {
            _csvName = "Produtos";
        }
        protected override ProdutoViewModel FromCsv(string csvLine)
        {
            string[] values = csvLine.Split(',');
            ProdutoViewModel vm = new ProdutoViewModel();
            vm.Id = values[0];
            vm.Name = values[1];           
            return vm;
        }
    }
}
