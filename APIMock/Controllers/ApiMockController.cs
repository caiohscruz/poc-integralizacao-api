using APIMock.DTO;
using APIMock.Extensions;
using APIMock.Models;
using APIMock.Models.AuxiliaryModels;
using APIMock.Services.ExcelService;
using APIMock.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;

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
        public IEnumerable<RegionalViewModel> GetRegionais()
        {
            var model = new RegionalModel();
            return model.GetValues();
        }

        [HttpGet("/GetMarcas")]
        public IEnumerable<MarcaViewModel> GetMarcas()
        {
            var model = new MarcaModel();
            return model.GetValues();
        }

        [HttpGet("/GetCampi")]
        public IEnumerable<CampusViewModel> GetCampi()
        {
            var model = new CampusModel();
            return model.GetValues();
        }

        [HttpPost("/GetAlunos")]
        public DataTableResult<AlunoViewModel> GetAlunos([FromBody] DataTableParameters request)
        {
            var model = new AlunoModel();
            var alunos = model.GetValues();

            if (request.Filtros != null)
            {
                alunos = alunos.Where(i => Procurado(i, request.Filtros));
            }

            var result = alunos.Select(i => _autoMapper.Map<AlunoViewModel>(i));

            var filteredResultsCount = result.Count();

            return new DataTableResult<AlunoViewModel>
            {                
                TotalRegistros = filteredResultsCount,
                PaginaRegistros = result
                    .Skip((request.Pagina - 1)*request.ItemsPorPagina)
                    .Take(request.ItemsPorPagina).ToList()
            };
        }

        private Boolean Procurado(TupleDTO registro, FiltroPorId[] filtros)
        {
            foreach (var filtro in filtros)
            {
                var colName = filtro.Coluna;
                var searchCriteria = filtro.Ids.ToList();
                var target = registro.ToPropertyDictionary()[colName];
                if (searchCriteria.Count > 0 && !searchCriteria.Contains(Convert.ToInt32(target)))
                {
                    return false;
                }
            }
            return true;
        }

        [HttpPost("/ExportTable")]
        public async Task<IActionResult> ExportTable([FromBody] DataTableParameters request)
        {
            var model = new AlunoModel();
            var result = model.GetValues();

            if(request.Filtros != null && result != null)
            {
                result = result.Where(i => Procurado(i, request.Filtros));
            }

            return File(
                await new ExcelService().Write(result.ToList()),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "data.xlsx");
        }

    }
}