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
            vm.Id = values[0];
            vm.LegalEntity = values[1];           
            vm.Registry = values[2];           
            vm.Date = values[3];           
            vm.Status = values[4];           
            vm.StatusId = values[5];           
            vm.Product = values[6];           
            vm.ProductId = values[7];           
            vm.Value = values[8];           
            vm.Comissioned = values[9];           
            vm.ComissionedId = values[10];           
            return vm;
        }
    }

}
