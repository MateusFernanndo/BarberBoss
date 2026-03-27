using AutoMapper;
using BarberBoss.Comunication.Request;
using BarberBoss.Domain.Repositories;
using BarberBoss.Domain.Repositories.Billing;
using BarberBoss.Exception;
using BarberBoss.Exception.ExceptionsBase;

namespace BarberBoss.Application.Usecase.Billing.Update;

public class UpdateBillingUseCase : IUpdateBillingUseCase
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBillingsUpdateOnlyRepository _repository;
    
    public UpdateBillingUseCase(IBillingsUpdateOnlyRepository repository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _repository = repository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task Execute(long id, RequestUpdateBillingJson request)
    {
        Validate(request);
        var billing = await _repository.GetById(id);

        if(billing is null)
        {
            throw new NotFoundException(ResourceErrorMessages.BILLING_NOT_FOUND);
        }
        _mapper.Map(request, billing);
        _repository.Update(billing);
        await _unitOfWork.Commit();
        
    }

    public void Validate(RequestUpdateBillingJson request)
    {
        var validator = new BillingUpdateValidator();
        var result = validator.Validate(request);

        if(result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }

}
