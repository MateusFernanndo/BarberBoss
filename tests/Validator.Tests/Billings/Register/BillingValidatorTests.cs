using BarberBoss.Application.Usecase.Billing;
using BarberBoss.Comunication.Enums;
using BarberBoss.Exception;
using CommomTestUtilities.Requests;
using DocumentFormat.OpenXml.Presentation;
using FluentAssertions;

namespace Validator.Tests.Billings.Register;

public class BillingValidatorTests
{
    [Fact]
    public void Sucess()
    {
        var validator = new BillingValidator();
        var request = RequestBillingJsonBuilder.Build();

        var result = validator.Validate(request);

        result.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData("")]
    [InlineData("               ")]
    [InlineData(null)]
    public void Error_ClientName_Empty(string client)
    {
        var validator = new BillingValidator();
        var request = RequestBillingJsonBuilder.Build();
        request.ClientName = client;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(b => b.ErrorMessage.Equals(ResourceErrorMessages.CLIENT_NAME_REQUIRED));
    }

    [Theory]
    [InlineData("")]
    [InlineData("               ")]
    [InlineData(null)]
    public void Error_BarberName_Empty(string barber)
    {
        var validator = new BillingValidator();
        var request = RequestBillingJsonBuilder.Build();
        request.BarberName = barber;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(b => b.ErrorMessage.Equals(ResourceErrorMessages.BARBER_NAME_REQUIRED));
    }

    [Theory]
    [InlineData("")]
    [InlineData("               ")]
    [InlineData(null)]
    public void Error_ServiceName_Empty(string service)
    {
        var validator = new BillingValidator();
        var request = RequestBillingJsonBuilder.Build();
        request.ServiceName = service;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(b => b.ErrorMessage.Equals(ResourceErrorMessages.SERVICE_NAME_REQUIRED));
    }

    [Fact]
    public void Error_Date_Future()
    {
        var validator = new BillingValidator();
        var request = RequestBillingJsonBuilder.Build();
        request.Date = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(1));

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.DATE_CANNOT_BE_IN_THE_FUTURE));
    }

    [Fact]
    public void Erro_Payment_Method()
    {
        var validator = new BillingValidator();
        var request = RequestBillingJsonBuilder.Build();
        request.PaymentType = (PaymentType)700;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.PAYMENT_TYPE_INVALID));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-10)]
    [InlineData(-20)]
    public void Error_Amount_Method(decimal amount)
    {
        var validator = new BillingValidator();
        var request = RequestBillingJsonBuilder.Build();
        request.Amount = amount;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.AMOUNT_MUST_BE_GREATER_THAN_ZERO));
    }
}
