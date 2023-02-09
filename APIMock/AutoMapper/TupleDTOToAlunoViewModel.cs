using APIMock.DTO;
using APIMock.ViewModels;
using AutoMapper;

namespace APIMock.AutoMapper
{
    public class TupleToAlunoViewModelProfile : Profile
    {
        public TupleToAlunoViewModelProfile()
        {
            CreateMap<TupleDTO, AlunoViewModel>()
                .ConstructUsing(c => new AlunoViewModel()
                {
                    RA = c.RA,
                    Nome= c.Nome,
                    Regional= c.Regional,
                    Polo = $"{c.Marca} - {c.Campus}"
                });
        }
    }

}
