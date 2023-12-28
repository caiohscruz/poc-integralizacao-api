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

        [HttpGet("/GetCommisioneds")]
        public IEnumerable<ResponsavelViewModel> GetResponsaveis()
        {
            var model = new ResponsavelModel();
            return model.GetValues();
        }

        [HttpGet("/GetProducts")]
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

        [HttpPost("/GetOpportunities")]
        public DataTableResult<OportunidadeViewModel> GetOportunidades([FromBody] DataTableParameters request)
        {
            var model = new OportunidadeModel();
            var alunos = model.GetValues();


            if (request.Filters != null)
            {
                alunos = alunos.Where(i => Procurado(i, request.Filters));
            }

            var result = alunos.Select(i => _autoMapper.Map<OportunidadeViewModel>(i));

            var filteredResultsCount = result.Count();

            return new DataTableResult<OportunidadeViewModel>
            {                
                DataCount = filteredResultsCount,
                PageCount = (int)Math.Ceiling((double)filteredResultsCount / request.PageSize),
                PageIndex = request.PageIndex,
                PageSize= request.PageSize,
                Page = result
                    .Skip((request.PageIndex - 1)*request.PageSize)
                    .Take(request.PageSize).ToList()
            };
        }

        private Boolean Procurado(TupleDTO registro, Filtro filtros)
        {
            if (filtros.Status.Length > 0 && !filtros.Status.Contains(registro.StatusId))
            {
                return false;
            }

            if (filtros.Products.Length > 0 && !filtros.Products.Contains(registro.ProductId))
            {
                return false;
            }

            if (filtros.Commissioneds.Length > 0 && !filtros.Commissioneds.Contains(registro.ComissionedId))
            {
                return false;
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