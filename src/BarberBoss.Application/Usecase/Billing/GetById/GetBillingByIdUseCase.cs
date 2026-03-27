using AutoMapper;
using BarberBoss.Comunication.Response;
using BarberBoss.Domain.Repositories.Billing;
using BarberBoss.Exception;
using BarberBoss.Exception.ExceptionsBase;

namespace BarberBoss.Application.Usecase.Billing.GetById;

public class GetBillingByIdUseCase : IGetBillingByIdUseCase
{
    private readonly IBillingReadOnlyRepository _repository;
    private readonly IMapper _mapper;

    public GetBillingByIdUseCase(IBillingReadOnlyRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<ResponseBillingJson> Execute(long id)
    {
        var result = await _repository.GetById(id);
        if(result is null)
        {
            throw new NotFoundException(ResourceErrorMessages.EXPENSE_NOT_FOUND);
        }
        return _mapper.Map<ResponseBillingJson>(result);
        
    }
}
