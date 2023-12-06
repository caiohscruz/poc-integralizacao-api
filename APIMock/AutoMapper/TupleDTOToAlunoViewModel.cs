using APIMock.DTO;
using APIMock.ViewModels;
using AutoMapper;

namespace APIMock.AutoMapper
{
    public class TupleToOportunidadeViewModelProfile : Profile
    {
        public TupleToOportunidadeViewModelProfile()
        {
            CreateMap<TupleDTO, OportunidadeViewModel>()
                .ConstructUsing(c => new OportunidadeViewModel()
                {
                    NumOportunidade = c.NumOportunidade,
                    Nome= c.Nome,
                    Documento= c.Documento,
                    Data = c.Data,
                    Status = c.Status,
                    Produto= c.Produto,
                    Valor= c.Valor,
                    Responsavel= c.Responsavel,                    
                });
        }
    }

}
