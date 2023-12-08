using APIMock.ViewModels;

namespace APIMock.Models
{
    public class StatusModel : CSVReader<StatusViewModel>
    {
        public StatusModel()
        {
            _csvName = "Status";
        }
        protected override StatusViewModel FromCsv(string csvLine)
        {
            string[] values = csvLine.Split(',');
            StatusViewModel vm = new StatusViewModel();
            vm.Id = values[0];
            vm.Name = values[1];           
            return vm;
        }
    }
}
