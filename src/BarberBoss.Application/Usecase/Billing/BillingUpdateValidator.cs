using BarberBoss.Comunication.Request;
using BarberBoss.Exception;
using FluentValidation;

namespace BarberBoss.Application.Usecase.Billing;

public class BillingUpdateValidator : AbstractValidator<RequestUpdateBillingJson>
{
    public BillingUpdateValidator()
    {
        RuleFor(billing => billing.ClientName).NotEmpty().WithMessage(ResourceErrorMessages.CLIENT_NAME_REQUIRED);
        RuleFor(billing => billing.BarberName).NotEmpty().WithMessage(ResourceErrorMessages.BARBER_NAME_REQUIRED);
        RuleFor(billing => billing.ServiceName).NotEmpty().WithMessage(ResourceErrorMessages.SERVICE_NAME_REQUIRED);
        RuleFor(billing => billing.Amount).GreaterThan(0).WithMessage(ResourceErrorMessages.AMOUNT_MUST_BE_GREATER_THAN_ZERO);
        RuleFor(billing => billing.PaymentType).IsInEnum().WithMessage(ResourceErrorMessages.PAYMENT_TYPE_INVALID);
    }
}
