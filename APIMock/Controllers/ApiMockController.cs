using APIMock.DTO;
using APIMock.Extensions;
using APIMock.Models;
using APIMock.Models.AuxiliaryModels;
using APIMock.Services.ExcelService;
using APIMock.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace APIMock.Controllers
{
    [ApiController]
    //[Route("[controller]")]
    public class ApiMockController : ControllerBase
    {
        private readonly ILogger<ApiMockController> _logger;
        private readonly IMapper _autoMapper;

        public ApiMockController(ILogger<ApiMockController> logger, IMapper autoMapper)
        {
            _logger = logger;
            _autoMapper = autoMapper;
        }

        [HttpGet("/GetRegionais")]
        public List<RegionalViewModel> GetRegionais()
        {
            var model = new RegionalModel();
            return model.GetValues();
        }

        [HttpGet("/GetMarcas")]
        public List<MarcaViewModel> GetMarcas()
        {
            var model = new MarcaModel();
            return model.GetValues();
        }

        [HttpGet("/GetCampi")]
        public List<CampusViewModel> GetCampi()
        {
            var model = new CampusModel();
            return model.GetValues();
        }

        [HttpPost("/GetAlunos")]
        public DtResult<AlunoViewModel> GetAlunos([FromBody] DtParameters dtParameters)
        {
            var model = new AlunoModel();
            var alunos = model.GetValues();

            var result = alunos.Where(i => Procurado(i, dtParameters.Columns)).Select(i => _autoMapper.Map<AlunoViewModel>(i));

            var filteredResultsCount = result.Count();
            var totalResultsCount = alunos.Count;

            return new DtResult<AlunoViewModel>
            {
                Draw = dtParameters.Draw,
                RecordsTotal = totalResultsCount,
                RecordsFiltered = filteredResultsCount,
                Data = result
                    .Skip(dtParameters.Start)
                    .Take(dtParameters.Length)
            };
        }

        private Boolean Procurado(TupleDTO registro, DtColumn[] columns)
        {
            foreach (var col in columns)
            {
                var colName = col.Name;
                var searchCriteria = col.Search.Value.Split(',');
                if (searchCriteria.Length > 0 && !searchCriteria.Contains(registro.ToPropertyDictionary()[colName]))
                {
                    return false;
                }
            }
            return true;
        }

        [HttpPost("/ExportTable")]
        public async Task<IActionResult> ExportTable([FromBody] DtParameters dtParameters)
        {
            var model = new AlunoModel();
            var alunos = model.GetValues();

            var result = alunos.Where(i => Procurado(i, dtParameters.Columns));

            return File(
                await new ExcelService().Write(result.ToList()),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "data.xlsx");
        }

    }
}