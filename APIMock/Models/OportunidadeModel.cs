using APIMock.DTO;
using APIMock.ViewModels;
using System.Text.RegularExpressions;

namespace APIMock.Models
{
    public class OportunidadeModel : CSVReader<TupleDTO>
    {
        public OportunidadeModel()
        {
            _csvName = "Oportunidades";
        }
        protected override TupleDTO FromCsv(string csvLine)
        {
            string[] values = csvLine.Split(',');
            TupleDTO vm = new TupleDTO();
            vm.NumOportunidade = values[0];
            vm.Nome = values[1];           
            vm.Documento = values[2];           
            vm.Data = values[3];           
            vm.Status = values[4];           
            vm.CodStatus = values[5];           
            vm.Produto = values[6];           
            vm.CodProduto = values[7];           
            vm.Valor = values[8];           
            vm.Responsavel = values[9];           
            vm.CodResponsavel = values[10];           
            return vm;
        }
    }

}
