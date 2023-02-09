using APIMock.DTO;
using APIMock.ViewModels;
using System.Text.RegularExpressions;

namespace APIMock.Models
{
    public class AlunoModel : CSVReader<TupleDTO>
    {
        public AlunoModel()
        {
            _csvName = "Alunos";
        }
        protected override TupleDTO FromCsv(string csvLine)
        {
            string[] values = csvLine.Split(',');
            TupleDTO vm = new TupleDTO();
            vm.RA = values[0];
            vm.Nome = values[1];           
            vm.Regional = values[2];           
            vm.CodRegional = values[3];           
            vm.Marca = values[4];           
            vm.CodMarca = values[5];           
            vm.Campus = values[6];           
            vm.CodCampus = values[7];           
            return vm;
        }
    }

}
