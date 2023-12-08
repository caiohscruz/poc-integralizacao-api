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
                    Id = c.Id,
                    LegalEntity= c.LegalEntity,
                    Registry= c.Registry,
                    Date = c.Date,
                    Status = c.Status,
                    Product= c.Product,
                    Value= c.Value,
                    Comissioned= c.Comissioned,                    
                });
        }
    }

}
