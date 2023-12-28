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
                    CustomerName= c.LegalEntity,
                    CustomerDocument= c.Registry,
                    CommisionDate = c.Date,
                    Status = c.Status,
                    ProductName= c.Product,
                    Value= c.Value,
                    Commissioned= c.Comissioned,                    
                });
        }
    }

}
