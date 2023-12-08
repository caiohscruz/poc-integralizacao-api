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

        [HttpGet("/GetResponsaveis")]
        public IEnumerable<ResponsavelViewModel> GetResponsaveis()
        {
            var model = new ResponsavelModel();
            return model.GetValues();
        }

        [HttpGet("/GetProdutos")]
        public IEnumerable<ProdutoViewModel> GetProdutos()
        {
            var model = new ProdutoModel();
            return model.GetValues();
        }

        [HttpGet("/GetStatus")]
        public IEnumerable<StatusViewModel> GetStatus()
        {
            var model = new StatusModel();
            return model.GetValues();
        }

        [HttpPost("/GetOportunidades")]
        public DataTableResult<OportunidadeViewModel> GetOportunidades([FromBody] DataTableParameters request)
        {
            var model = new OportunidadeModel();
            var alunos = model.GetValues();

            //TODO ignorar filtros para terminar refatoração do front
            //if (request.Filtros != null)
            //{
            //    alunos = alunos.Where(i => Procurado(i, request.Filtros));
            //}

            //TODO incluir ordenação

            var result = alunos.Select(i => _autoMapper.Map<OportunidadeViewModel>(i));

            var filteredResultsCount = result.Count();

            return new DataTableResult<OportunidadeViewModel>
            {                
                Total = filteredResultsCount,
                Total_Pages = (int)Math.Ceiling((double)filteredResultsCount / request.ItemsPerPage),
                Page = request.Page,
                Per_Page= request.ItemsPerPage,
                Data = result
                    .Skip((request.Page - 1)*request.ItemsPerPage)
                    .Take(request.ItemsPerPage).ToList()
            };
        }

        private Boolean Procurado(TupleDTO registro, FiltroPorId[] filtros)
        {
            foreach (var filtro in filtros)
            {
                var colName = filtro.Column;
                if(colName == "Data")
                {
                    return true;
                }
                var searchCriteria = filtro.Ids.ToList();
                var target = registro.ToPropertyDictionary()[colName];
                if (searchCriteria.Count > 0 && !searchCriteria.Contains(target))
                {
                    return false;
                }
            }
            return true;
        }

        [HttpPost("/ExportTable")]
        public async Task<IActionResult> ExportTable([FromBody] DataTableParameters request)
        {
            var model = new OportunidadeModel();
            var result = model.GetValues();

            if(request.Filters != null && result != null)
            {
                result = result.Where(i => Procurado(i, request.Filters));
            }

            return File(
                await new ExcelService().Write(result.ToList()),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "data.xlsx");
        }

    }
}