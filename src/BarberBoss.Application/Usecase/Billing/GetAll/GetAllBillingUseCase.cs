using AutoMapper;
using BarberBoss.Comunication.Response;
using BarberBoss.Domain.Repositories.Billing;

namespace BarberBoss.Application.Usecase.Billing.GetAll;

public class GetAllBillingUseCase : IGetAllBillingUseCase
{
    private readonly IBillingReadOnlyRepository _repository;
    private readonly IMapper _mapper;

    public GetAllBillingUseCase(IBillingReadOnlyRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<ResponseAllBillingsJson> Execute()
    {
        var result = await _repository.GetAll();
        return new ResponseAllBillingsJson
        {
            Billings = _mapper.Map<List<ResponseShortBillingJson>>(result)
        };
    }
}
