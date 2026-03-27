using AutoMapper;
using BarberBoss.Comunication.Request;
using BarberBoss.Comunication.Response;
using BarberBoss.Domain.Repositories;
using BarberBoss.Domain.Repositories.Billing;
using BarberBoss.Exception.ExceptionsBase;
using FluentValidation;

namespace BarberBoss.Application.Usecase.Billing.Register;

public class RegisterBillingUseCase : IRegisterBillingUseCase
{
    private readonly IBillingWriteOnlyRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public RegisterBillingUseCase(
        IBillingWriteOnlyRepository repository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<ResponseRegisterBillingJson> Execute(RequestBillingJson request)
    {
        Validade(request);
        var entity = _mapper.Map<Domain.Entities.Billing>(request);
        
        await _repository.Add(entity);
        await _unitOfWork.Commit();
        return _mapper.Map<ResponseRegisterBillingJson>(entity);
    }

    public void Validade(RequestBillingJson request)
    {
        var validator = new BillingValidator();
        var result = validator.Validate(request);
        if(result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
